using System.Web.Mvc;

namespace DoanWeb.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            // context.MapRoute(
            //     "AdminSignIn",
            //     "{controller}/{action}",
            //     new { controller = "User", action = "SignIn" },
            //      namespaces: new[] { "DoanWeb.Controllers" }
            // );
           // context.MapRoute(
           //    "",
           //    "{controller}/{action}",
           //    new { controller = "User", action = "SignOut" },
           //     namespaces: new[] { "DoanWeb.Controllers" }
           //);

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                  new[] { "DoanWeb.Areas.Admin.Controllers" }
            );
        }
    }
}