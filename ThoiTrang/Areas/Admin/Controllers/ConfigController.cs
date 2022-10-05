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

namespace ThoiTrang.Areas.Admin.Controllers
{
    public class ConfigController : Controller
    {
        ConfigDAO configDAO = new ConfigDAO();
        // GET: Admin/Config
        public ActionResult Index()
        {
            return View(configDAO.getList());
        }

        // GET: Admin/Config/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Config config = configDAO.getRow((int)id);
            if (config == null)
            {
                return HttpNotFound();
            }
            return View(config);
        }

        // GET: Admin/Config/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Config/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Value")] Config config)
        {
            if (ModelState.IsValid)
            {
                configDAO.Insert(config);
                return RedirectToAction("Index");
            }

            return View(config);
        }

        // GET: Admin/Config/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Config config = configDAO.getRow((int)id);
            if (config == null)
            {
                return HttpNotFound();
            }
            return View(config);
        }

        // POST: Admin/Config/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Value")] Config config)
        {
            if (ModelState.IsValid)
            {
                configDAO.Update(config);
                return RedirectToAction("Index");
            }
            return View(config);
        }

        // GET: Admin/Config/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Config config = configDAO.getRow((int)id);
            if (config == null)
            {
                return HttpNotFound();
            }
            return View(config);
        }

        // POST: Admin/Config/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Config config = configDAO.getRow((int)id);
            configDAO.Delete(config);
            return RedirectToAction("Index");
        }

    }
}
