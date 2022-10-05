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
    public class SupplierController : Controller
    {
        SupplierDAO supplierDAO = new SupplierDAO();
        LinkDAO linkDAO = new LinkDAO();


        // GET: Admin/supplier
        public ActionResult Index()
        {
            return View(supplierDAO.getList("Index"));
        }

        // GET: Admin/supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow((int)id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Admin/supplier/Create
        public ActionResult Create()
        {
            ViewBag.listCat = new SelectList(supplierDAO.getList(), "Id", "Name", 0);
            ViewBag.listOrder = new SelectList(supplierDAO.getList(), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                supplier.Slug = XString.Str_Slug(supplier.Name);
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
                        string imgName = supplier.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        supplier.Image = imgName;
                        string PathDir = "~/Public/Images/Suppliers";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }
                //end upload file
                supplier.ParentId = (supplier.ParentId == null) ? 0 : supplier.ParentId;
                supplier.Orders = (supplier.Orders == null) ? 1 : (supplier.Orders + 1);

                supplier.CreatedBy = Session["UserId"].ToString();
                supplier.CreatedAt = DateTime.Now;
                if (supplierDAO.Insert(supplier) == 1)
                {
                    Link link = new Link();
                    link.Slug = supplier.Slug;
                    link.TableId = supplier.Id;
                    link.TypeLink = "supplier";
                    linkDAO.Insert(link);

                    TempData["Message"] = new XMessage("success", "Thêm thành công.");
                }
                return RedirectToAction("Index");
            }
            ViewBag.listCat = new SelectList(supplierDAO.getList(), "Id", "Name", 0);
            ViewBag.listOrder = new SelectList(supplierDAO.getList(), "Orders", "Name", 0);
            return View(supplier);
        }

        // GET: Admin/supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.listCat = new SelectList(supplierDAO.getList(), "Id", "Name", 0);
            ViewBag.listOrder = new SelectList(supplierDAO.getList(), "Orders", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow((int)id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Admin/supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                supplier.Slug = XString.Str_Slug(supplier.Name);
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
                        string imgName = supplier.Slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        supplier.Image = imgName;
                        string PathDir = "~/Public/Images/Suppliers";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        //Xóa file
                        if (supplier.Image != null)
                        {
                            string DelPath = Path.Combine(Server.MapPath(PathDir), supplier.Image);
                            System.IO.File.Delete(DelPath);
                        }
                        img.SaveAs(PathFile);
                    }
                }
                //end upload file


                supplier.ParentId = (supplier.ParentId == null) ? 0 : supplier.ParentId;
                supplier.Orders = (supplier.Orders == null) ? 1 : (supplier.Orders + 1);
                supplier.UpdatedBy = Session["UserId"].ToString();
                supplier.UpdatedAt = DateTime.Now;
                if (supplierDAO.Update(supplier) == 1)
                {
                    Link link = linkDAO.getRow(supplier.Id, "supplier");
                    link.Slug = supplier.Slug;
                    linkDAO.Update(link);
                }
                TempData["Message"] = new XMessage("success", "Thêm thành công.");
                return RedirectToAction("Index");

            }
            ViewBag.ListCat = new SelectList(supplierDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(supplierDAO.getList("Index"), "Orders", "Name");
            return View(supplier);
        }

        // GET: Admin/supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Trash");
            }
            Supplier supplier = supplierDAO.getRow((int)id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Trash");
            }
            if (supplier.Status != 0)
            {
                Status(id);
                TempData["message"] = new XMessage("danger", "Chỉ xóa mẫu tin trong thùng rác.");
                return RedirectToAction(id.ToString());
            }
            return View(supplier);
        }

        // POST: Admin/supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = supplierDAO.getRow((int)id);
            Link link = linkDAO.getRow(supplier.Id, "supplier");

            //Xóa file
            string PathDir = "~/Public/Images/Suppliers";
            if (supplier.Image != null)
            {
                string DelPath = Path.Combine(Server.MapPath(PathDir), supplier.Image);
                System.IO.File.Delete(DelPath);
            }
            //xóa link
            if (supplierDAO.Delete(supplier) == 1)
            {
                linkDAO.Delete(link);
            }
            return RedirectToAction("Trash");
        }
        public ActionResult Trash()
        {
            return View(supplierDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Index", "supplier");
            }
            Supplier supplier = supplierDAO.getRow((int)id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Index", "supplier");
            }
            supplier.Status = (supplier.Status == 1) ? 2 : 1;
            supplier.UpdatedBy = Session["UserId"].ToString();
            supplier.UpdatedAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công.");
            return RedirectToAction("Index", "supplier");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Index", "supplier");
            }
            Supplier supplier = supplierDAO.getRow((int)id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Index", "supplier");
            }
            supplier.Status = 0 /*(supplier.Status == 1) ? 0 : 1*/;
            supplier.UpdatedBy = Session["UserId"].ToString();
            supplier.UpdatedAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Xóa thành công.");
            return RedirectToAction("Index", "supplier");
        }

        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Trash", "supplier");
            }
            Supplier supplier = supplierDAO.getRow((int)id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Trash", "supplier");
            }
            supplier.Status = 2 /*(supplier.Status == 1) ? 0 : 1*/;
            supplier.UpdatedBy = Session["UserId"].ToString();
            supplier.UpdatedAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Khối phục thành công.");
            return RedirectToAction("Trash", "supplier");
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

