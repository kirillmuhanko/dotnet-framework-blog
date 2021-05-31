using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace BlogApplication.Models.Entities.User
{
    [NoReorder]
    [Table("AspNetRoles")]
    public class RoleEntity
    {
        public RoleEntity()
        {
            Users = new HashSet<UserEntity>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<UserEntity> Users { get; set; }
    }
}