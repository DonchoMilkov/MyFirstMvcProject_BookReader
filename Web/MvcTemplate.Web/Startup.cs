using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using MvcTemplate.Common;
using MvcTemplate.Data;
using MvcTemplate.Data.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcTemplate.Web.Startup))]

namespace MvcTemplate.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
            this.CreateRolesandUsers();
        }

        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists(GlobalConstants.AdministratorRoleName))
            {
                var role = new IdentityRole();
                role.Name = GlobalConstants.AdministratorRoleName;
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = GlobalConstants.AdministratorEmail;
                user.Email = GlobalConstants.AdministratorEmail;

                string userPWD = GlobalConstants.AdministratorPassword;

                var chkUser = userManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, GlobalConstants.AdministratorRoleName);
                }

                context.SaveChanges();
            }
        }
    }
}
