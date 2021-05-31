using System.Web.Mvc;
using BlogApplication.Models.Constants.StringSymbols;

namespace BlogApplication.Models.Attributes.Authorization
{
    public class RoleBasedAuthorizationAttribute : AuthorizeAttribute
    {
        public RoleBasedAuthorizationAttribute(params string[] roles)
        {
            Roles = string.Join(StringSymbols.Comma, roles);
        }
    }
}