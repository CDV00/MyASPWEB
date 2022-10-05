using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;

namespace ThoiTrang.Controllers
{
    public class SiteController : Controller
    {
        CategoryDAO categoryDAO = new CategoryDAO();
        SupplierDAO supplierDAO = new SupplierDAO();
        ProductDAO productDAO = new ProductDAO();
        // GET: Site
        public ActionResult Index(string slug=null)
        {
            //Console.WriteLine("aaa");
            if(slug!= null)
            {

                return this.productCategory(slug);
            }
            else
            {
                return this.Home();
            }

        }

        public ActionResult Home()
        {
            return View("Home");
        }



        public ActionResult productCategory(string slug)
        {
            var row = categoryDAO.getRow(slug);
            ViewBag.CatName = row.Name;
            int catId = row.Id;
            var list =productDAO.getListCat(catId);
            return View("productCategory", list);
        }
        public ActionResult productSupplier(string slug)
        {
            var row = supplierDAO.getRow(slug);
            ViewBag.SupName = row.Name;
            int supId = row.Id;
            var list = productDAO.getListSup(supId);
            return View("productSupplier", list);
        }
        public ActionResult productDetail(string slug)
        {
            var row = productDAO.getRow(slug);
            ViewBag.Name = row.Name;
            int productId = row.Id;
            var list = productDAO.getRow(productId);
            //Session["customerName"] = "vũ";
            return View("productDetail", list);
        }


    }
}