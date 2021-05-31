using System.Data.Entity;
using BlogApplication.Models.Attributes.DependencyInjection;
using BlogApplication.Models.Entities.Article;
using BlogApplication.Models.Entities.User;
using BlogApplication.Models.Enums;

namespace BlogApplication.Infrastructure.Database
{
    [InstanceScope(InstanceLifetime.PerRequest)]
    public sealed class BlogDbContext : DbContext
    {
        public BlogDbContext() : base("BlogContext")
        {
        }

        public DbSet<ArticleCommentEntity> ArticleComments { get; set; }

        public DbSet<ArticleCommentReportEntity> ArticleCommentReports { get; set; }

        public DbSet<ArticleEntity> Articles { get; set; }

        public DbSet<ArticleLikeEntity> ArticleRank { get; set; }

        public DbSet<RoleEntity> AspNetRoles { get; set; }

        public DbSet<UserClaimEntity> AspNetUserClaims { get; set; }

        public DbSet<UserEntity> AspNetUsers { get; set; }

        public DbSet<UserLoginEntity> AspNetUserLogins { get; set; }

        protected override void Dispose(bool disposing)
        {
            Database.CurrentTransaction?.Dispose();
            base.Dispose(disposing);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleEntity>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .Map(t => t.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.UserClaims)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.UserLogins)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId);
        }
    }
}