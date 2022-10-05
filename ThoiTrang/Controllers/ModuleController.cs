using MyClass.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThoiTrang.Controllers
{
    public class ModuleController : Controller
    {
        ProductDAO productDAO = new ProductDAO();
        CategoryDAO categoryDAO = new CategoryDAO();
        SupplierDAO supplierDAO = new SupplierDAO();
        // GET: Module
        public ActionResult _MainMenu()
        {
            return View("_MainMenu");
        }
        public ActionResult _Slider()
        {
            SliderDAO sliderDAO = new SliderDAO();
            var list= sliderDAO.getList("Index");
            return View("_Slider", list);
        }
        public ActionResult _Sale()
        {
            var list = productDAO.getListSale();
            return View("_Sale", list);
        }
        public ActionResult _productHome()
        {
            int id = Convert.ToInt32(Session["cat"]) ;
            var Cat = categoryDAO.getRow(id);
            ViewBag.CatName = Cat.Name;
            ViewBag.CatSlug = Cat.Slug;
            var list = productDAO.getListCat(id);
            return View("_productHome", list);
        }
        public ActionResult _MenuCat()
        {
            var list = categoryDAO.getList("HienHanh");
            return View("_MenuCat", list);
        }
        public ActionResult _MenuSup()
        {
            var list = supplierDAO.getList("HienHanh");
            return View("_MenuSup", list);
        }
        public ActionResult _CatHome()
        {
            var list = categoryDAO.getList("index");
            return View("_CatHome", list);
        }
        //private const string cat = "cat";
        public JsonResult Update(long id)
        {
            Session["cat"] = id;
            //var sesstionCart = (List<CartItem>)Session[CartSesstion];
            //var list = categoryDAO.getRow((int)id);
            return Json(new
            {
                status = true,
            });
        }
    }
}