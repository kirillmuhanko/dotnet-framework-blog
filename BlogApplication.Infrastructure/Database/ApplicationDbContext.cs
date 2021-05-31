using BlogApplication.Infrastructure.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BlogApplication.Infrastructure.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("BlogContext", false)
        {
        }

        public static ApplicationDbContext Create()
        {
            var context = new ApplicationDbContext();
            return context;
        }
    }
}