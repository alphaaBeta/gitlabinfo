using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitlabInfo.Models;
using GitlabInfo.Models.EFModels;
using Microsoft.EntityFrameworkCore;

namespace GitlabInfo.Code.EntityFramework
{
    public class GitLabInfoDbContext : DbContext
    {
        private readonly string _connectionString;
        public GitLabInfoDbContext(DbContextOptions<GitLabInfoDbContext> options) : base(options)
        { }
        public virtual DbSet<UserModel> Users { get; set; }
        public virtual DbSet<GroupModel> Groups { get; set; }
        public virtual DbSet<ProjectModel> Projects { get; set; }
        public virtual DbSet<JoinRequestModel> JoinRequests { get; set; }
        public virtual DbSet<ProjectRequestModel> ProjectRequests { get; set; }
        public virtual DbSet<ReportedTimeModel> ReportedTimes { get; set; }
        public virtual DbSet<EngagementPointsModel> EngagementPointsModels { get; set; }
        public virtual DbSet<UserProjectRequestModel> UserProjectRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroupModel>()
                .HasKey(t => new {UserId = t.UserId, GroupId = t.GroupId});

            modelBuilder.Entity<UserProjectRequestModel>()
                .HasKey(t => new { UserId = t.UserId, ProjectRequestId = t.ProjectRequestId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(_connectionString);
            }
        }
    }
}
