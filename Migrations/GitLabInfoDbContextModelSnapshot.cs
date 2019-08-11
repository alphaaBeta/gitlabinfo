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

            modelBuilder.Entity("GitlabInfo.Models.EFModels.GroupModel", b =>
                {
                    b.Property<int>("Id");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.JoinRequestModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("RequestedGroupId");

                    b.Property<int?>("RequesteeId");

                    b.HasKey("Id");

                    b.HasIndex("RequestedGroupId");

                    b.HasIndex("RequesteeId");

                    b.ToTable("JoinRequests");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.ProjectModel", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int?>("GroupModelId");

                    b.HasKey("Id");

                    b.HasIndex("GroupModelId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.ProjectRequestModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ParentGroupId");

                    b.Property<string>("ProjectDescription");

                    b.Property<string>("ProjectName");

                    b.Property<int?>("RequesteeId");

                    b.HasKey("Id");

                    b.HasIndex("ParentGroupId");

                    b.HasIndex("RequesteeId");

                    b.ToTable("ProjectRequests");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.UserGroupModel", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("GroupId");

                    b.Property<int>("Role");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("UserGroupModel");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.UserModel", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Email");

                    b.Property<DateTime>("FirstJoined");

                    b.Property<DateTime>("LastJoined");

                    b.Property<int?>("ProjectRequestModelId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectRequestModelId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.JoinRequestModel", b =>
                {
                    b.HasOne("GitlabInfo.Models.EFModels.GroupModel", "RequestedGroup")
                        .WithMany()
                        .HasForeignKey("RequestedGroupId");

                    b.HasOne("GitlabInfo.Models.EFModels.UserModel", "Requestee")
                        .WithMany()
                        .HasForeignKey("RequesteeId");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.ProjectModel", b =>
                {
                    b.HasOne("GitlabInfo.Models.EFModels.GroupModel")
                        .WithMany("Projects")
                        .HasForeignKey("GroupModelId");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.ProjectRequestModel", b =>
                {
                    b.HasOne("GitlabInfo.Models.EFModels.GroupModel", "ParentGroup")
                        .WithMany()
                        .HasForeignKey("ParentGroupId");

                    b.HasOne("GitlabInfo.Models.EFModels.UserModel", "Requestee")
                        .WithMany()
                        .HasForeignKey("RequesteeId");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.UserGroupModel", b =>
                {
                    b.HasOne("GitlabInfo.Models.EFModels.GroupModel", "Group")
                        .WithMany("AssignedUsers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GitlabInfo.Models.EFModels.UserModel", "User")
                        .WithMany("OwnedGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.UserModel", b =>
                {
                    b.HasOne("GitlabInfo.Models.EFModels.ProjectRequestModel")
                        .WithMany("Members")
                        .HasForeignKey("ProjectRequestModelId");
                });
#pragma warning restore 612, 618
        }
    }
}
