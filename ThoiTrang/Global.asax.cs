using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ThoiTrang
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Session_Start()
        {
            Session["UserAdmin"]="1";
            Session["UserId"] = "1";
            Session["FullName"] = "";
            Session["Img"] = "";
            Session["customerName"] = "";
            Session["customerId"] = "";
            Session["cart"] = "";
            Session["cat"] = 1;
            Session["Test"] = "";
            Session["imgOld"] = "";

        }
    }
}
