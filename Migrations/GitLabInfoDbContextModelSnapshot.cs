﻿// <auto-generated />
using System;
using GitlabInfo.Code.EntiyFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GitlabInfo.Migrations
{
    [DbContext(typeof(GitLabInfoDbContext))]
    partial class GitLabInfoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GitlabInfo.Models.Group", b =>
                {
                    b.Property<int>("GitLabId");

                    b.HasKey("GitLabId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("GitlabInfo.Models.Project", b =>
                {
                    b.Property<int>("GitLabId");

                    b.Property<int?>("GroupGitLabId");

                    b.HasKey("GitLabId");

                    b.HasIndex("GroupGitLabId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("GitlabInfo.Models.User", b =>
                {
                    b.Property<int>("GitLabId");

                    b.Property<DateTime>("FirstJoined");

                    b.Property<DateTime>("LastJoined");

                    b.HasKey("GitLabId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GitlabInfo.Models.UserGroup", b =>
                {
                    b.Property<int>("UserGitLabId");

                    b.Property<int>("GroupGitLabId");

                    b.Property<int>("Role");

                    b.HasKey("UserGitLabId", "GroupGitLabId");

                    b.HasIndex("GroupGitLabId");

                    b.ToTable("UserGroup");
                });

            modelBuilder.Entity("GitlabInfo.Models.Project", b =>
                {
                    b.HasOne("GitlabInfo.Models.Group")
                        .WithMany("Projects")
                        .HasForeignKey("GroupGitLabId");
                });

            modelBuilder.Entity("GitlabInfo.Models.UserGroup", b =>
                {
                    b.HasOne("GitlabInfo.Models.Group", "Group")
                        .WithMany("AssignedUsers")
                        .HasForeignKey("GroupGitLabId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GitlabInfo.Models.User", "User")
                        .WithMany("OwnedGroups")
                        .HasForeignKey("UserGitLabId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
