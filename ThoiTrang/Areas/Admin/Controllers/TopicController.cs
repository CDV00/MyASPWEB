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

namespace ThoiTrang.Areas.Admin.Controllers
{

    public class TopicController : Controller//BaseController
    {
        TopicDAO topicDAO = new TopicDAO();
        LinkDAO linkDAO = new LinkDAO();


        // GET: Admin/topic
        public ActionResult Index()
        {
            return View(topicDAO.getList("Index"));
        }

        // GET: Admin/topic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = topicDAO.getRow((int)id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Admin/topic/Create
        public ActionResult Create()
        {
            ViewBag.listCat = new SelectList(topicDAO.getList(), "Id", "Name", 0);
            ViewBag.listOrder = new SelectList(topicDAO.getList(), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/topic/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Topic topic)
        {
            if (ModelState.IsValid)
            {
                topic.Slug = XString.Str_Slug(topic.Name);
                topic.ParentId = (topic.ParentId == null) ? 0 : topic.ParentId;
                topic.Orders = (topic.Orders == null) ? 1 : (topic.Orders + 1);

                topic.Created_by = Session["UserId"].ToString();
                topic.Created_at = DateTime.Now;
                if (topicDAO.Insert(topic) == 1)
                {
                    Link link = new Link();
                    link.Slug = topic.Slug;
                    link.TableId = topic.Id;
                    link.TypeLink = "topic";
                    linkDAO.Insert(link);

                    TempData["Message"] = new XMessage("success", "Thêm thành công.");
                }
                return RedirectToAction("Index");
            }
            ViewBag.listCat = new SelectList(topicDAO.getList(), "Id", "Name", 0);
            ViewBag.listOrder = new SelectList(topicDAO.getList(), "Orders", "Name", 0);
            return View(topic);
        }

        // GET: Admin/topic/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.listCat = new SelectList(topicDAO.getList(), "Id", "Name", 0);
            ViewBag.listOrder = new SelectList(topicDAO.getList(), "Orders", "Name", 0);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = topicDAO.getRow((int)id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Admin/topic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Topic topic)
        {
            if (ModelState.IsValid)
            {
                topic.Slug = XString.Str_Slug(topic.Name);
                topic.ParentId = (topic.ParentId == null) ? 0 : topic.ParentId;
                topic.Orders = (topic.Orders == null) ? 1 : (topic.Orders + 1);

                topic.Updated_by = Session["UserId"].ToString();
                topic.Updated_at = DateTime.Now;
                if (topicDAO.Update(topic) == 1)
                {
                    Link link = linkDAO.getRow(topic.Id, "topic");
                    link.Slug = topic.Slug;
                    linkDAO.Insert(link);
                }
                TempData["Message"] = new XMessage("success", "Thêm thành công.");
                return RedirectToAction("Index");

            }
            ViewBag.ListCat = new SelectList(topicDAO.getList("Index"), "Id", "Name");
            ViewBag.ListOrder = new SelectList(topicDAO.getList("Index"), "Orders", "Name");
            return View(topic);
        }

        // GET: Admin/topic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Trash");
            }
            Topic topic = topicDAO.getRow((int)id);
            if (topic == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Trash");
            }
            if (topic.Status != 0)
            {
                Status(id);
                TempData["message"] = new XMessage("danger", "Chỉ xóa mẫu tin trong thùng rác.");
                return RedirectToAction(id.ToString());
            }
            return View(topic);
        }

        // POST: Admin/topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Topic topic = topicDAO.getRow((int)id);
            Link link = linkDAO.getRow(topic.Id, "topic");

            if (topicDAO.Delete(topic) == 1)
            {
                linkDAO.Delete(link);
            }
            return RedirectToAction("Trash");
        }
        public ActionResult Trash()
        {
            return View(topicDAO.getList("Trash"));
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Index", "topic");
            }
            Topic topic = topicDAO.getRow((int)id);
            if (topic == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Index", "topic");
            }
            topic.Status = (topic.Status == 1) ? 2 : 1;
            topic.Updated_by = Session["UserId"].ToString();
            topic.Updated_at = DateTime.Now;
            topicDAO.Update(topic);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công.");
            return RedirectToAction("Index", "topic");
        }
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Index", "topic");
            }
            Topic topic = topicDAO.getRow((int)id);
            if (topic == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Index", "topic");
            }
            topic.Status = 0 /*(topic.Status == 1) ? 0 : 1*/;
            topic.Updated_by = Session["UserId"].ToString();
            topic.Updated_at = DateTime.Now;
            topicDAO.Update(topic);
            TempData["message"] = new XMessage("success", "Xóa thành công.");
            return RedirectToAction("Index", "topic");
        }

        public ActionResult ReTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Trash", "topic");
            }
            Topic topic = topicDAO.getRow((int)id);
            if (topic == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại.");
                return RedirectToAction("Trash", "topic");
            }
            topic.Status = 2 /*(topic.Status == 1) ? 0 : 1*/;
            topic.Updated_by = Session["UserId"].ToString();
            topic.Updated_at = DateTime.Now;
            topicDAO.Update(topic);
            TempData["message"] = new XMessage("success", "Khối phục thành công.");
            return RedirectToAction("Trash", "topic");
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
