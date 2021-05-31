using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace BlogApplication.Models.Entities.User
{
    [NoReorder]
    [Table("AspNetUserClaims")]
    public class UserClaimEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public virtual UserEntity User { get; set; }
    }
}