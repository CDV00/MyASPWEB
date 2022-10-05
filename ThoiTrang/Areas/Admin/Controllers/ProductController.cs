using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;
using ThoiTrang.Library;

namespace ThoiTrang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private ProductDAO productDAO = new ProductDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        private SupplierDAO supplierDAO = new SupplierDAO();

        // GET: Admin/Product
        public ActionResult Index()
        {
            List<ProductInfo> list = productDAO.getListJoin("Index");
            return View("Index",list);
        }

        // GET: Admin/product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/product/Create
        public ActionResult Create()
        {
            ViewBag.listCat = new SelectList(categoryDAO.getList(), "Id", "Name", 0);
            ViewBag.listSup = new SelectList(supplierDAO.getList(), "Id", "Name", 0);
            return View();
        }

        // POST: Admin/product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Slug = XString.Str_Slug(product.Name);

                //upload file
                var img = Request.Files["img"];
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" };
                    if (!FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                    {
                        ModelState.AddModelError("Err", "Kiểu tập tin không đúng, Kiểu tập tin hợp lệ là: " + string.Join(",", FileExtentions));
                    }
                    else
                    {
                        //upload hình
                        string imgName = product.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        product.Image = imgName;
                        string PathDir = "~/Public/Images/Product";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }
                //end upload file

                //product.CategoryId = (product.CategoryId == null) ? 0 : product.CategoryId;
                //product.SupplierId = (product.SupplierId == null) ? 1 : (product.SupplierId + 1);

                product.Created_by = Session["UserId"].ToString();
                product.Updated_at = DateTime.Now;
                productDAO.Insert(product);
                

                    TempData["Message"] = new XMessage("success", "Thêm thành công.");
                
                return RedirectToAction("Index");
            }
            ViewBag.listCat = new SelectList(productDAO.getList(), "Id", "Name", 0);
            ViewBag.listSup = new SelectList(productDAO.getList(), "Id", "Name", 0);
            return View(product);
        }

        // GET: Admin/product/Edit/5
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.listCat = new SelectList(categoryDAO.getList(), "Id", "Name", 0);
            ViewBag.listSup = new SelectList(supplierDAO.getList(), "Id", "Name", 0);
            return View(product);
        }

        // POST: Admin/product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Slug = XString.Str_Slug(product.Name);

                
                //upload file
                var img = Request.Files["img"];
               
                if (img.FileName.Length != 0)
                {
                    //Session["Test"] = "ddd"+img.FileName;
                    string[] FileExtentions = new string[] { ".jpg", ".jepg", ".png", ".gif" };
                     if (!FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))
                     {
                         ModelState.AddModelError("Err", "Kiểu tập tin không đúng, Kiểu tập tin hợp lệ là: " + string.Join(",", FileExtentions));
                     }
                     else
                     {
                         //upload hình
                         string imgName = product.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                         product.Image = imgName;
                         //Session["Test"] = img.FileName;
                         string PathDir = "~/Public/Images/Product";
                         string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                         //Xóa file
                         if (product.Image != null)
                         {
                             string DelPath = Path.Combine(Server.MapPath(PathDir), product.Image);
                             System.IO.File.Delete(DelPath);
                         }
                         img.SaveAs(PathFile);
                     }
                }
                else
                {
                    product.Image = Session["imgOld"].ToString();
                }
                
                //end upload file

                //product.CategoryId = (product.CategoryId == null) ? 0 : product.CategoryId;
                //product.SupplierId = (product.SupplierId == null) ? 1 : (product.SupplierId + 1);

                product.Updated_by = Session["UserId"].ToString();
                product.Updated_at = DateTime.Now;
                productDAO.Update(product);
                TempData["Message"] = new XMessage("success", "Thêm thành công.");
                return RedirectToAction("Index");

            }
            ViewBag.ListCat = new SelectList(productDAO.getList("Index"), "Id", "Name");
            ViewBag.ListSup = new SelectList(productDAO.getList("Index"), "Id", "Name");
            return View(product);
        }

        // GET: Admin/product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Trash");
            }
            Product product = productDAO.getRow((int)id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Trash");
            }
            if (product.Status != 0)
            {
                Status(id);
                TempData["message"] = new XMessage("danger", "Chỉ xóa mẫu tin trong thùng rác.");
                return RedirectToAction(id.ToString());
            }
            return View(product);
        }

        // POST: Admin/product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = productDAO.getRow(id);

            return RedirectToAction("Trash");
        }
        public ActionResult Trash()
        {
            List<ProductInfo> list = productDAO.getListJoin("Trash");
            return View("Trash", list);
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Index", "product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Index", "product");
            }
            product.Status = (product.Status == 1) ? 2 : 1;
            product.Updated_by = Session["UserId"].ToString();
            product.Updated_at = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công.");
            return RedirectToAction("Index", "product");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Index", "product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Index", "product");
            }
            product.Status = 0 /*(product.Status == 1) ? 0 : 1*/;
            product.Updated_by = Session["UserId"].ToString();
            product.Updated_at = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Xóa thành công.");
            return RedirectToAction("Index", "product");
        }

        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Trash", "product");
            }
            Product product = productDAO.getRow(id);
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Trash", "product");
            }
            product.Status = 2 /*(product.Status == 1) ? 0 : 1*/;
            product.Updated_by = Session["UserId"].ToString();
            product.Updated_at = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Khối phục thành công.");
            return RedirectToAction("Trash", "product");
        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}