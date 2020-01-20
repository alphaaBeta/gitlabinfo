﻿// <auto-generated />
using System;
using GitlabInfo.Code.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GitlabInfo.Migrations
{
    [DbContext(typeof(GitLabInfoDbContext))]
    [Migration("20191230100630_Project ID to survey")]
    partial class ProjectIDtosurvey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GitlabInfo.Models.EFModels.EngagementPointsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AwardingUserId");

                    b.Property<int>("Points");

                    b.Property<int?>("ProjectId");

                    b.Property<DateTime>("ReceivingDate");

                    b.Property<int?>("ReceivingUserId");

                    b.HasKey("Id");

                    b.HasIndex("AwardingUserId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ReceivingUserId");

                    b.ToTable("EngagementPoints");
                });

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

                    b.Property<int?>("AssignedGroupId");

                    b.HasKey("Id");

                    b.HasIndex("AssignedGroupId");

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

                    b.HasKey("Id");

                    b.HasIndex("ParentGroupId");

                    b.ToTable("ProjectRequests");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.ReportedTimeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<int>("IssueId");

                    b.Property<int?>("ProjectId");

                    b.Property<double>("TimeInHours");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("ReportedTimes");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.SurveyAnswerModel", b =>
                {
                    b.Property<int>("SurveyAnswerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnswerString")
                        .HasColumnName("Answer");

                    b.Property<int>("ProjectId");

                    b.Property<int>("SurveyId");

                    b.Property<int>("UserId");

                    b.HasKey("SurveyAnswerId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SurveyId");

                    b.HasIndex("UserId", "SurveyId")
                        .IsUnique();

                    b.ToTable("SurveyAnswers");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.SurveyModel", b =>
                {
                    b.Property<int>("SurveyId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AssignedGroupId");

                    b.Property<string>("SurveyString")
                        .HasColumnName("Survey");

                    b.HasKey("SurveyId");

                    b.HasIndex("AssignedGroupId");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.UserGroupModel", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("GroupId");

                    b.Property<int>("Role");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.UserModel", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Email");

                    b.Property<DateTime>("FirstJoined");

                    b.Property<DateTime>("LastJoined");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.UserProjectRequestModel", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("ProjectRequestId");

                    b.Property<bool>("IsRequestee");

                    b.HasKey("UserId", "ProjectRequestId");

                    b.HasIndex("ProjectRequestId");

                    b.ToTable("UserProjectRequests");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.WorkDescriptionCommentModel", b =>
                {
                    b.Property<int>("WorkDescriptionCommentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment");

                    b.Property<int>("CommenterId");

                    b.Property<int>("WorkDescriptionId");

                    b.HasKey("WorkDescriptionCommentId");

                    b.HasIndex("CommenterId");

                    b.HasIndex("WorkDescriptionId");

                    b.ToTable("WorkDescriptionComments");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.WorkDescriptionModel", b =>
                {
                    b.Property<int>("WorkDescriptionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int>("ProjectId");

                    b.Property<int>("UserId");

                    b.HasKey("WorkDescriptionId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("WorkDescriptions");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.EngagementPointsModel", b =>
                {
                    b.HasOne("GitlabInfo.Models.EFModels.UserModel", "AwardingUser")
                        .WithMany()
                        .HasForeignKey("AwardingUserId");

                    b.HasOne("GitlabInfo.Models.EFModels.ProjectModel", "Project")
                        .WithMany("EngagementPoints")
                        .HasForeignKey("ProjectId");

                    b.HasOne("GitlabInfo.Models.EFModels.UserModel", "ReceivingUser")
                        .WithMany()
                        .HasForeignKey("ReceivingUserId");
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
                    b.HasOne("GitlabInfo.Models.EFModels.GroupModel", "AssignedGroup")
                        .WithMany("Projects")
                        .HasForeignKey("AssignedGroupId");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.ProjectRequestModel", b =>
                {
                    b.HasOne("GitlabInfo.Models.EFModels.GroupModel", "ParentGroup")
                        .WithMany()
                        .HasForeignKey("ParentGroupId");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.ReportedTimeModel", b =>
                {
                    b.HasOne("GitlabInfo.Models.EFModels.ProjectModel", "Project")
                        .WithMany("ReportedTimes")
                        .HasForeignKey("ProjectId");

                    b.HasOne("GitlabInfo.Models.EFModels.UserModel", "User")
                        .WithMany("ReportedTimes")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.SurveyAnswerModel", b =>
                {
                    b.HasOne("GitlabInfo.Models.EFModels.ProjectModel", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GitlabInfo.Models.EFModels.SurveyModel", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GitlabInfo.Models.EFModels.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.SurveyModel", b =>
                {
                    b.HasOne("GitlabInfo.Models.EFModels.GroupModel", "AssignedGroup")
                        .WithMany()
                        .HasForeignKey("AssignedGroupId");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.UserGroupModel", b =>
                {
                    b.HasOne("GitlabInfo.Models.EFModels.GroupModel", "Group")
                        .WithMany("AssignedUsers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GitlabInfo.Models.EFModels.UserModel", "User")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.UserProjectRequestModel", b =>
                {
                    b.HasOne("GitlabInfo.Models.EFModels.ProjectRequestModel", "ProjectRequest")
                        .WithMany("Members")
                        .HasForeignKey("ProjectRequestId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GitlabInfo.Models.EFModels.UserModel", "User")
                        .WithMany("UserProjectRequestModels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.WorkDescriptionCommentModel", b =>
                {
                    b.HasOne("GitlabInfo.Models.EFModels.UserModel", "Commenter")
                        .WithMany()
                        .HasForeignKey("CommenterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GitlabInfo.Models.EFModels.WorkDescriptionModel", "WorkDescription")
                        .WithMany("Comments")
                        .HasForeignKey("WorkDescriptionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.WorkDescriptionModel", b =>
                {
                    b.HasOne("GitlabInfo.Models.EFModels.ProjectModel", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GitlabInfo.Models.EFModels.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
