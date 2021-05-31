using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BlogApplication.Models.Entities.User;

namespace BlogApplication.Domain.Interfaces.Expressions.User
{
    public interface IUserExpressions
    {
        Expression<Func<UserEntity, bool>> ById(string id);

        Expression<Func<UserEntity, bool>> ById();

        Expression<Func<UserEntity, ICollection<RoleEntity>>> ByRoles();

        Expression<Func<UserEntity, string>> ByUserName();
    }
}