using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using BlogApplication.Models.Constants.Controllers;
using BlogApplication.Models.Entities.User;
using Microsoft.AspNet.Identity;

namespace BlogApplication.Infrastructure.Database
{
    internal sealed class Configuration : DbMigrationsConfiguration<BlogDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BlogDbContext dbContext)
        {
            var roles = new List<RoleEntity>
            {
                new RoleEntity {Id = GetGuid(0), Name = UserRoles.Administrator},
                new RoleEntity {Id = GetGuid(1), Name = UserRoles.Author},
                new RoleEntity {Id = GetGuid(2), Name = UserRoles.Contributor},
                new RoleEntity {Id = GetGuid(3), Name = UserRoles.Editor},
                new RoleEntity {Id = GetGuid(4), Name = UserRoles.Rater},
                new RoleEntity {Id = GetGuid(5), Name = UserRoles.Subscriber}
            };

            roles.ForEach(t => dbContext.AspNetRoles.AddOrUpdate(entity => entity.Id, t));
            dbContext.SaveChanges();

            var passwordHasher = new PasswordHasher();

            var users = new List<UserEntity>
            {
                new UserEntity
                {
                    Id = Guid.Empty.ToString(),
                    Email = "admin@admin.com",
                    EmailConfirmed = true,
                    PasswordHash = passwordHasher.HashPassword("Qwerty123"),
                    SecurityStamp = GetGuid(1001),
                    PhoneNumber = null,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEndDateUtc = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    UserName = "admin",
                    Roles = roles
                }
            };

            users.ForEach(t => dbContext.AspNetUsers.AddOrUpdate(user => user.Id, t));
            dbContext.SaveChanges();

            base.Seed(dbContext);
        }

        private static string GetGuid(int seed)
        {
            var random = new Random(seed);
            var bytes = new byte[16];
            random.NextBytes(bytes);
            var guid = new Guid(bytes).ToString();
            return guid;
        }
    }
}