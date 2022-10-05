using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;
using ThoiTrang.Library;
using System.IO;

namespace ThoiTrang.Areas.Admin.Controllers
{
    public class CategoryController : Controller//BaseController
    {
        CategoryDAO categoryDAO = new CategoryDAO();
        LinkDAO linkDAO = new LinkDAO();

        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(categoryDAO.getList("Index"));
        }
        
        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryDAO.getRow((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.listCat= new SelectList(categoryDAO.getList(), "Id", "Name",0);
            ViewBag.listOrder = new SelectList(categoryDAO.getList(), "Orders", "Name",0);
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "Id,Name,Slug,ParentId,Orders,Image,Title,MetaKey,MetaDesc,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Status")]*/ Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = XString.Str_Slug(category.Name);
                category.ParentId = (category.ParentId == null)?0:category.ParentId;
                category.Orders = (category.Orders == null)? 1:(category.Orders +1);

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
                        string imgName = category.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        category.Image = imgName;
                        string PathDir = "~/Public/Images/Categorys";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }
                //end upload file


                category.CreatedBy = Session["UserId"].ToString();
                category.CreatedAt = DateTime.Now;
                if (categoryDAO.Insert(category) == 1)
                {
                    Link link = new Link();
                    link.Slug = category.Slug;
                    link.TableId = category.Id;
                    link.TypeLink = "category";
                    linkDAO.Insert(link);

                    TempData["Message"] = new XMessage("success", "Thêm thành công.");
                }
                return RedirectToAction("Index");
            }
            ViewBag.listCat = new SelectList(categoryDAO.getList(), "Id", "Name", 0);
            ViewBag.listOrder = new SelectList(categoryDAO.getList(), "Orders", "Name", 0);
            return View(category);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.listCat = new SelectList(categoryDAO.getList(), "Id", "Name", 0);
            ViewBag.listOrder = new SelectList(categoryDAO.getList(), "Orders", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryDAO.getRow((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = XString.Str_Slug(category.Name);
                category.ParentId = (category.ParentId == null) ? 0 : category.ParentId;
                category.Orders = (category.Orders == null) ? 1 : (category.Orders + 1);

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
                        string imgName = category.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        category.Image = imgName;
                        string PathDir = "~/Public/Images/Categorys";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        //Xóa file
                        if (category.Image != null)
                        {
                            string DelPath = Path.Combine(Server.MapPath(PathDir), category.Image);
                            System.IO.File.Delete(DelPath);
                        }
                        img.SaveAs(PathFile);
                    }
                }
                //end upload file


                category.UpdatedBy = Session["UserId"].ToString();
                category.UpdatedAt = DateTime.Now;
                if (categoryDAO.Update(category) == 1)
                {
                    Link link = linkDAO.getRow(category.Id, "category");
                    link.Slug = category.Slug;
                    linkDAO.Update(link);
                }
                TempData["Message"] = new XMessage("success", "Thêm thành công.");
                return RedirectToAction("Index");

            }
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"),"Id","Name");
            ViewBag.ListOrder = new SelectList(categoryDAO.getList("Index"),"Orders","Name");
            return View(category);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Trash");
            }
            Category category = categoryDAO.getRow((int)id);
            if (category == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Trash");
            }
            if (category.Status!=0)
            {
                Status(id);
                TempData["message"] = new XMessage("danger", "Chỉ xóa mẫu tin trong thùng rác.");
                return RedirectToAction(id.ToString());
            }
            return View(category);
        }
        
        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = categoryDAO.getRow((int)id);
            Link link = linkDAO.getRow(category.Id, "category");

            if (categoryDAO.Delete(category) == 1)
            {
                linkDAO.Delete(link);
            }
            return RedirectToAction("Trash");
        }
        public ActionResult Trash()
        {
            return View(categoryDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Index", "Category");
            }
            Category category = categoryDAO.getRow((int)id);
            if (category == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Index", "Category");
            }
            category.Status = (category.Status == 1) ? 2 : 1;
            category.UpdatedBy = Session["UserId"].ToString();
            category.UpdatedAt = DateTime.Now;
            categoryDAO.Update(category);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công.");
            return RedirectToAction("Index", "Category");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Index", "Category");
            }
            Category category = categoryDAO.getRow((int)id);
            if (category == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Index", "Category");
            }
            category.Status =0 /*(category.Status == 1) ? 0 : 1*/;
            category.UpdatedBy = Session["UserId"].ToString();
            category.UpdatedAt = DateTime.Now;
            categoryDAO.Update(category);
            TempData["message"] = new XMessage("success", "Xóa thành công.");
            return RedirectToAction("Index", "Category");
        }

        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Trash", "Category");
            }
            Category category = categoryDAO.getRow((int)id);
            if (category == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Trash", "Category");
            }
            category.Status = 2 /*(category.Status == 1) ? 0 : 1*/;
            category.UpdatedBy = Session["UserId"].ToString();
            category.UpdatedAt = DateTime.Now;
            categoryDAO.Update(category);
            TempData["message"] = new XMessage("success", "Khối phục thành công.");
            return RedirectToAction("Trash", "Category");
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
