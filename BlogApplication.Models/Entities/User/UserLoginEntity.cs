using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace BlogApplication.Models.Entities.User
{
    [NoReorder]
    [Table("AspNetUserLogins")]
    public class UserLoginEntity
    {
        [Key] [Column(Order = 0)] public string LoginProvider { get; set; }

        [Key] [Column(Order = 1)] public string ProviderKey { get; set; }

        [Key] [Column(Order = 2)] public string UserId { get; set; }

        public virtual UserEntity User { get; set; }
    }
}