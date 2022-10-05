using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;

namespace ThoiTrang.Controllers
{
    public class AccoutController : BaseSiteController
    {
        private OrderDAO orderDAO = new OrderDAO();
        //private ProductsBought productsBought = new ProductsBought();
        // GET: Accout
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductsBought()
        {
            int userID = int.Parse(Session["customerId"].ToString());
            var list = orderDAO.getListProductsBought(userID);
            return View(list);
        }
        
    }
}