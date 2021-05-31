using System;
using System.ComponentModel.DataAnnotations.Schema;
using BlogApplication.Models.Entities.User;
using JetBrains.Annotations;

namespace BlogApplication.Models.Entities.Base
{
    [NoReorder]
    public abstract class EntityBase
    {
        [Column(Order = 0)] public long Id { get; set; }

        public DateTime AddedDateTime { get; set; }

        public DateTime ModifiedDateTime { get; set; }

        [ForeignKey("AddedByUser")] public string AddedByUserId { get; set; }

        [ForeignKey("ModifiedByUser")] public string ModifiedByUserId { get; set; }

        public virtual UserEntity AddedByUser { get; set; }

        public virtual UserEntity ModifiedByUser { get; set; }
    }
}