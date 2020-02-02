﻿// <auto-generated />
using System;
using GitlabInfo.Code.EntityFramework;
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
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GitlabInfo.Models.EFModels.EngagementPointsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AwardingUserId")
                        .HasColumnType("int");

                    b.Property<bool>("Bonus")
                        .HasColumnType("bit");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReceivingDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ReceivingUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AwardingUserId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ReceivingUserId");

                    b.ToTable("EngagementPoints");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.GroupModel", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.GroupOptionsModel", b =>
                {
                    b.Property<int>("GroupOptionsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AllowsProjectCreation")
                        .HasColumnType("bit");

                    b.Property<bool>("EngagementPointsEnabled")
                        .HasColumnType("bit");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("HasNewData")
                        .HasColumnType("bit");

                    b.Property<bool>("ReportTimeEnabled")
                        .HasColumnType("bit");

                    b.Property<bool>("SurveyEnabled")
                        .HasColumnType("bit");

                    b.Property<int?>("SurveyId")
                        .HasColumnType("int");

                    b.Property<bool>("WorkDescriptionCommentsEnabled")
                        .HasColumnType("bit");

                    b.Property<bool>("WorkDescriptionEnabled")
                        .HasColumnType("bit");

                    b.HasKey("GroupOptionsId");

                    b.HasIndex("GroupId");

                    b.HasIndex("SurveyId");

                    b.ToTable("GroupOptions");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.JoinRequestModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("RequestedGroupId")
                        .HasColumnType("int");

                    b.Property<int?>("RequesteeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RequestedGroupId");

                    b.HasIndex("RequesteeId");

                    b.ToTable("JoinRequests");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.ProjectModel", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("AssignedGroupId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssignedGroupId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.ProjectRequestModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ParentGroupId")
                        .HasColumnType("int");

                    b.Property<string>("ProjectDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentGroupId");

                    b.ToTable("ProjectRequests");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.ReportedTimeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IssueId")
                        .HasColumnType("int");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReportedDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("TimeInHours")
                        .HasColumnType("float");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("ReportedTimes");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.SurveyAnswerModel", b =>
                {
                    b.Property<int>("SurveyAnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AnswerDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AnswerString")
                        .HasColumnName("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("SurveyAnswerId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SurveyId");

                    b.HasIndex("UserId");

                    b.ToTable("SurveyAnswers");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.SurveyModel", b =>
                {
                    b.Property<int>("SurveyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SurveyString")
                        .HasColumnName("Survey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SurveyId");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.UserGroupModel", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FirstJoined")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastJoined")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.UserProjectRequestModel", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectRequestId")
                        .HasColumnType("int");

                    b.Property<bool>("IsRequestee")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "ProjectRequestId");

                    b.HasIndex("ProjectRequestId");

                    b.ToTable("UserProjectRequests");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.WorkDescriptionCommentModel", b =>
                {
                    b.Property<int>("WorkDescriptionCommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CommenterId")
                        .HasColumnType("int");

                    b.Property<int>("WorkDescriptionId")
                        .HasColumnType("int");

                    b.HasKey("WorkDescriptionCommentId");

                    b.HasIndex("CommenterId");

                    b.HasIndex("WorkDescriptionId");

                    b.ToTable("WorkDescriptionComments");
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.WorkDescriptionModel", b =>
                {
                    b.Property<int>("WorkDescriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

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

            modelBuilder.Entity("GitlabInfo.Models.EFModels.GroupOptionsModel", b =>
                {
                    b.HasOne("GitlabInfo.Models.EFModels.GroupModel", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GitlabInfo.Models.EFModels.SurveyModel", "Survey")
                        .WithMany("GroupOptionsList")
                        .HasForeignKey("SurveyId");
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
                        .HasForeignKey("ParentGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GitlabInfo.Models.EFModels.SurveyModel", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GitlabInfo.Models.EFModels.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.UserGroupModel", b =>
                {
                    b.HasOne("GitlabInfo.Models.EFModels.GroupModel", "Group")
                        .WithMany("AssignedUsers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GitlabInfo.Models.EFModels.UserModel", "User")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.UserProjectRequestModel", b =>
                {
                    b.HasOne("GitlabInfo.Models.EFModels.ProjectRequestModel", "ProjectRequest")
                        .WithMany("Members")
                        .HasForeignKey("ProjectRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GitlabInfo.Models.EFModels.UserModel", "User")
                        .WithMany("UserProjectRequestModels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.WorkDescriptionCommentModel", b =>
                {
                    b.HasOne("GitlabInfo.Models.EFModels.UserModel", "Commenter")
                        .WithMany()
                        .HasForeignKey("CommenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GitlabInfo.Models.EFModels.WorkDescriptionModel", "WorkDescription")
                        .WithMany("Comments")
                        .HasForeignKey("WorkDescriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GitlabInfo.Models.EFModels.WorkDescriptionModel", b =>
                {
                    b.HasOne("GitlabInfo.Models.EFModels.ProjectModel", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GitlabInfo.Models.EFModels.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
