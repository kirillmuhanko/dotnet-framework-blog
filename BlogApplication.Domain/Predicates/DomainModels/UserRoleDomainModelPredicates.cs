using System;
using BlogApplication.Domain.Interfaces.Predicates.DomainModels;
using BlogApplication.Models.DomainModels.User;

namespace BlogApplication.Domain.Predicates.DomainModels
{
    public class UserRoleDomainModelPredicates : IUserRoleDomainModelPredicates
    {
        public Func<UserRoleDomainModel, bool> ByIsChecked(bool isChecked)
        {
            return t => t.IsChecked == isChecked;
        }

        public Func<UserRoleDomainModel, string> ByName()
        {
            return t => t.Name;
        }
    }
}