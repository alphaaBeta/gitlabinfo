using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using AutoMapper;
using GitlabInfo.Code;
using GitlabInfo.Code.APIs.GitLab;
using GitlabInfo.Code.EntityFramework;
using GitlabInfo.Code.Extensions;
using GitlabInfo.Code.GitLabApis;
using GitlabInfo.Code.Repositories;
using GitlabInfo.Code.Repositories.Interfaces;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace GitlabInfo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Values Api", Version = "v1" });
                });

            services
                .AddSingleton<Config>(new Config(Configuration))
                .TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services
                .AddTransient<IGroupApiClient, GitLabGroupApiClient>()
                .AddTransient<IStandaloneApiClient, GitLabStandaloneApiClient>()
                .AddTransient<IProjectApiClient, GitLabProjectApiClient>()
                .AddTransient<IGroupRepository, GitLabGroupRepository>()
                .AddTransient<IProjectRepository, GitLabProjectRepository>()
                .AddTransient<IStandaloneRepository, GitLabStandaloneRepository>()
                .AddTransient<IGitLabInfoDbRepository, GitLabInfoDbRepository>()
                .AddTransient<IExcelExportRepository, ExcelExportRepository>();


            services.AddHttpClient("GitLabApi", c =>
            {
                c.BaseAddress = new Uri(Config.GitLab_ApiUrl);
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = "GitLab";
                })
                .AddCookie()
                .AddOAuth("GitLab", options =>
                {
                    options.ClientId = Config.GitLab_ClientId;
                    options.ClientSecret = Config.GitLab_ClientSecret;
                    options.CallbackPath = new PathString(Config.GitLab_CallbackUrl);

                    options.AuthorizationEndpoint = @"https://gitlab.com/oauth/authorize";
                    options.TokenEndpoint = @"https://gitlab.com/oauth/token";
                    options.UserInformationEndpoint = @"https://gitlab.com/api/v4/user";

                    options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                    options.ClaimActions.MapJsonKey(ClaimsTypesExtensions.Login, "username");
                    options.ClaimActions.MapJsonKey(ClaimsTypesExtensions.WebUrl, "web_url");
                    options.ClaimActions.MapJsonKey(ClaimsTypesExtensions.AvatarUrl, "avatar_url");

                    options.SaveTokens = true;

                    options.Events = new OAuthEvents
                    {
                        OnCreatingTicket = async context =>
                        {
                            var request =
                                new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            request.Headers.Authorization =
                                new AuthenticationHeaderValue("Bearer", context.AccessToken);

                            var response = await context.Backchannel.SendAsync(request,
                                HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                            response.EnsureSuccessStatusCode();

                            //var user = JObject.Parse(await response.Content.ReadAsStringAsync());
                            var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                            
                            context.RunClaimActions(user.RootElement);
                        }
                    };

                });

            services.AddDbContext<GitLabInfoDbContext>(options =>
            {
                options.UseSqlServer(Config.Database_ConnectionString);
            });

            services
                .AddAutoMapper(typeof(Startup))
                .AddMvc(options =>
                {
                    options.CacheProfiles.Add("Default30",
                        new CacheProfile()
                        {
                            Duration = 30
                        });
                    options.EnableEndpointRouting = false;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
               configuration.RootPath = "ClientApp/dist";
            });

            var aiOptions = new ApplicationInsightsServiceOptions()
            {
                EnableAdaptiveSampling = false
            };
            aiOptions.RequestCollectionOptions.TrackExceptions = true;
            services.AddApplicationInsightsTelemetry(aiOptions);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.OAuthConfigObject = new OAuthConfigObject()
                {
                    AppName = "GitLabInfoDevLocal",
                    ClientId = Config.GitLab_ClientId,
                    ClientSecret = Config.GitLab_ClientSecret
                };
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Values Api V1");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "swagger",
                    template: "swagger");
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
               // To learn more about options for serving an Angular SPA from ASP.NET Core,
               // see https://go.microsoft.com/fwlink/?linkid=864501

               spa.Options.SourcePath = "ClientApp";

               if (env.IsDevelopment())
               {
                   spa.UseAngularCliServer(npmScript: "start");
               }
            });
            // app.UseSpa(spa => spa.UseProxyToSpaDevelopmentServer("http://localhost:4200"));
        }
    }
}
