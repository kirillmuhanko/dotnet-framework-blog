using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BlogApplication.Domain.Interfaces.Expressions.User;
using BlogApplication.Models.Constants.Security;
using BlogApplication.Models.Entities.User;

namespace BlogApplication.Domain.Expressions.User
{
    public class UserExpressions : IUserExpressions
    {
        public Expression<Func<UserEntity, bool>> ById()
        {
            return t => !t.Id.Equals(UserIds.Admin);
        }

        public Expression<Func<UserEntity, bool>> ById(string id)
        {
            return t => t.Id.Equals(id);
        }

        public Expression<Func<UserEntity, ICollection<RoleEntity>>> ByRoles()
        {
            return t => t.Roles;
        }

        public Expression<Func<UserEntity, string>> ByUserName()
        {
            return t => t.UserName;
        }
    }
}