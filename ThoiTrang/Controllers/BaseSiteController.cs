using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThoiTrang.Controllers
{
    public class BaseSiteController : Controller
    {
        public BaseSiteController()
        {
            if (System.Web.HttpContext.Current.Session["customerId"].Equals(""))
            {
                System.Web.HttpContext.Current.Response.Redirect("~/dang-nhap");
            }
        }
    }
}