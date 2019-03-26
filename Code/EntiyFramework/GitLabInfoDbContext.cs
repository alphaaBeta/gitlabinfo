using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitlabInfo.Models;
using GitlabInfo.Models.EFModels;
using Microsoft.EntityFrameworkCore;

namespace GitlabInfo.Code.EntiyFramework
{
    public class GitLabInfoDbContext : DbContext
    {
        public GitLabInfoDbContext(DbContextOptions<GitLabInfoDbContext> options) : base(options)
        { }
        public virtual DbSet<UserModel> Users { get; set; }
        public virtual DbSet<GroupModel> Groups { get; set; }
        public virtual DbSet<ProjectModel> Projects { get; set; }
        public virtual DbSet<JoinRequestModel> JoinRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroupModel>()
                .HasKey(t => new {UserId = t.UserId, GroupId = t.GroupId});
        }
    }
}
