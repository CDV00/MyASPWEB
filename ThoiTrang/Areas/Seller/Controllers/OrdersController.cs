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

namespace ThoiTrang.Areas.Seller.Controllers
{
    public class OrdersController : Controller
    {
        OrderDAO orderDAO = new OrderDAO();

        public string PathFile { get; private set; }

        //LinkDAO linkDAO = new LinkDAO();

        // GET: Admin/order
        public ActionResult Index()
        {
            return View(orderDAO.getList("Index"));
        }

        public ActionResult Delivering()
        {
            return View(orderDAO.getList("Delivering"));
        }
        public ActionResult Delivered()
        {
            return View(orderDAO.getList("Delivered"));
        }

        // GET: Admin/order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Admin/order/Create
        public ActionResult Create()
        {
            ViewBag.listCat = new SelectList(orderDAO.getList(), "Id", "Name", 0);
            ViewBag.listOrder = new SelectList(orderDAO.getList(), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                //order.Slug = XString.Str_Slug(order.Name);
                //order.ParentId = (order.ParentId == null) ? 0 : order.ParentId;
                //order.Orders = (order.Orders == null) ? 1 : (order.Orders + 1);




                //order.CreatedBy = Session["UserId"].ToString();
                //order.CreatedAt = DateTime.Now;
                orderDAO.Insert(order);


                TempData["Message"] = new XMessage("success", "Thêm thành công.");

                return RedirectToAction("Index");
            }
            ViewBag.listCat = new SelectList(orderDAO.getList(), "Id", "Name", 0);
            ViewBag.listOrder = new SelectList(orderDAO.getList(), "Orders", "Name", 0);
            return View(order);
        }

        // GET: Admin/order/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.listCat = new SelectList(orderDAO.getList(), "Id", "Name", 0);
            ViewBag.listOrder = new SelectList(orderDAO.getList(), "Orders", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = orderDAO.getRow((int)id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                //order.Slug = XString.Str_Slug(order.Name);
                //order.ParentId = (order.ParentId == null) ? 0 : order.ParentId;
                //order.Orders = (order.Orders == null) ? 1 : (order.Orders + 1);




                //order.UpdatedBy = Session["UserId"].ToString();
                //order.UpdatedAt = DateTime.Now;
                orderDAO.Update(order);

                TempData["Message"] = new XMessage("success", "Thêm thành công.");
                return RedirectToAction("Index");

            }
            ViewBag.ListCat = new SelectList(orderDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(orderDAO.getList("Index"), "Orders", "Name");
            return View(order);
        }

        // GET: Admin/order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Trash");
            }
            Order order = orderDAO.getRow((int)id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Trash");
            }
            if (order.Status != 0)
            {
                Status(id);
                TempData["message"] = new XMessage("danger", "Chỉ xóa mẫu tin trong thùng rác.");
                return RedirectToAction(id.ToString());
            }
            return View(order);
        }

        // POST: Admin/order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = orderDAO.getRow((int)id);


            return RedirectToAction("Trash");
        }
        public ActionResult Trash()
        {
            return View(orderDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Index", "order");
            }
            Order order = orderDAO.getRow(id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Index", "order");
            }
            order.Status = (order.Status == 1) ? 2 : 1;
            //order.UpdatedBy = Session["UserId"].ToString();
            //order.UpdatedAt = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công.");
            return RedirectToAction("Index", "order");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Index", "order");
            }
            Order order = orderDAO.getRow((int)id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Index", "order");
            }
            order.Status = 0 /*(order.Status == 1) ? 0 : 1*/;
            //order.UpdatedBy = Session["UserId"].ToString();
            //order.UpdatedAt = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Xóa thành công.");
            return RedirectToAction("Index", "order");
        }

        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Trash", "order");
            }
            Order order = orderDAO.getRow((int)id);
            if (order == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Trash", "order");
            }
            order.Status = 2 /*(order.Status == 1) ? 0 : 1*/;
            //order.UpdatedBy = Session["UserId"].ToString();
            //order.UpdatedAt = DateTime.Now;
            orderDAO.Update(order);
            TempData["message"] = new XMessage("success", "Khối phục thành công.");
            return RedirectToAction("Trash", "order");
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
