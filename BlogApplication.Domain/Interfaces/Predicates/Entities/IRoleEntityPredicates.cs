using System;
using System.Collections.Generic;
using BlogApplication.Models.DomainModels.User;
using BlogApplication.Models.Entities.User;

namespace BlogApplication.Domain.Interfaces.Predicates.Entities
{
    public interface IRoleEntityPredicates
    {
        Func<RoleEntity, bool> ById(string id);

        Func<RoleEntity, UserRoleDomainModel> MapToUserRoleDomainModel(IEnumerable<RoleEntity> roles);
    }
}