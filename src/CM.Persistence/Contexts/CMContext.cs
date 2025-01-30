using CM.Domain.Entities.PostEntities;
using CM.Domain.Entities.UserEntities;
using CM.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CM.Persistence.Contexts
{
    public class CMContext : DbContext
    {
        public CMContext(DbContextOptions<CMContext> options)
            : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<SelectedPost>().Property(r => r.CreatedDate).HasDefaultValueSql("getdate()");
            builder.Entity<Session>().Property(r => r.CreatedDate).HasDefaultValueSql("getdate()");
            builder.Entity<Post>().Property(r => r.CreatedDate).HasDefaultValueSql("getdate()");
            builder.Entity<PostComment>().Property(r => r.CreatedDate).HasDefaultValueSql("getdate()");
            builder.Entity<UserInfo>().Property(r => r.CreatedDate).HasDefaultValueSql("getdate()");
            builder.Entity<Role>().Property(r => r.CreatedDate).HasDefaultValueSql("getdate()");
            builder.Entity<UserRole>().Property(r => r.CreatedDate).HasDefaultValueSql("getdate()");

            builder.Entity<UserInfo>().HasData(
                new UserInfo()
                {
                    FullName = "Ali Rasouli",
                    UserName = "rasouli",
                    IsDeleted = false,
                    PasswordHash = "nucYY3XECcoPL4ucgRt+ND/iVyP0xsmiB6z31UeuBdU=",
                    Salt = "157,165,66,15,197,109,172,34,78,58,246,140,174,1,216,161",
                    Id = 1,
                    Tel = "09127433108,09011454747"
                }
            );

            builder.Entity<Role>().HasData(
                new Role() { Title = "Admin", IsDeleted = false, Id = 1 },
                new Role() { Title = "Normal", IsDeleted = false, Id = 2 }
            );

            builder.Entity<UserRole>().HasData(
                new UserRole() { UserId = 1, RoleId = 1, IsDeleted = false, Id = 1, }
            );
        }

        public DbSet<SelectedPost> SelectedPosts { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
