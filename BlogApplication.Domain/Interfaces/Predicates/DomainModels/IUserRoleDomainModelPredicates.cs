using System;
using BlogApplication.Models.DomainModels.User;

namespace BlogApplication.Domain.Interfaces.Predicates.DomainModels
{
    public interface IUserRoleDomainModelPredicates
    {
        Func<UserRoleDomainModel, bool> ByIsChecked(bool isChecked);

        Func<UserRoleDomainModel, string> ByName();
    }
}