using System.Web.Mvc;

namespace ThoiTrang.Areas.Admin
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
            
            context.MapRoute(
                "Admin_Login",
                "Admin/login",
                new { controller = "Auth", action = "Login", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller="Dashboard",action = "Index", id = UrlParameter.Optional },
                new[] { "ThoiTrang.Areas.Admin.Controllers" }
            );
        }
    }
}