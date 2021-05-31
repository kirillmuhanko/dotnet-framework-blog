using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace BlogApplication.Models.Entities.User
{
    [NoReorder]
    [Table("AspNetUsers")]
    public class UserEntity
    {
        public UserEntity()
        {
            UserClaims = new HashSet<UserClaimEntity>();
            UserLogins = new HashSet<UserLoginEntity>();
            Roles = new HashSet<RoleEntity>();
        }

        public string Id { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public string UserName { get; set; }

        public virtual ICollection<UserClaimEntity> UserClaims { get; set; }

        public virtual ICollection<UserLoginEntity> UserLogins { get; set; }

        public virtual ICollection<RoleEntity> Roles { get; set; }
    }
}