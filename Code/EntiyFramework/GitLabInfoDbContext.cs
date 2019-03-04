using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitlabInfo.Models;
using Microsoft.EntityFrameworkCore;

namespace GitlabInfo.Code.EntiyFramework
{
    public class GitLabInfoDbContext : DbContext
    {
        public GitLabInfoDbContext(DbContextOptions<GitLabInfoDbContext> options) : base(options)
        { }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroup>()
                .HasKey(t => new {t.UserGitLabId, t.GroupGitLabId});
        }
    }
}
