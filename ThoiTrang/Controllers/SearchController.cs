using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;

namespace ThoiTrang.Controllers
{
    public class SearchController : Controller
    {
        ProductDAO productDAO = new ProductDAO();
        // GET: Search
        
        public ActionResult Index(FormCollection field)
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult layGiaTri(FormCollection field)
        {
            
            var list = productDAO.getListSearch(field["key"]);
            ViewBag.so = list.Count();
            return View(list);
        }
    }
}