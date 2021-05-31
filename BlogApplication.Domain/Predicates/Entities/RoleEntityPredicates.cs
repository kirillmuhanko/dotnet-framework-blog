using System;
using System.Collections.Generic;
using System.Linq;
using BlogApplication.Domain.Interfaces.Predicates.Entities;
using BlogApplication.Models.DomainModels.User;
using BlogApplication.Models.Entities.User;

namespace BlogApplication.Domain.Predicates.Entities
{
    public class RoleEntityPredicates : IRoleEntityPredicates
    {
        public Func<RoleEntity, bool> ById(string id)
        {
            return t => t.Id == id;
        }

        public Func<RoleEntity, UserRoleDomainModel> MapToUserRoleDomainModel(IEnumerable<RoleEntity> roles)
        {
            return t => new UserRoleDomainModel
            {
                Id = t.Id,
                Name = t.Name,
                IsChecked = roles.Count(ById(t.Id)) != 0
            };
        }
    }
}