using MyClass.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThoiTrang.Controllers
{
    public class TopicsController : Controller
    {
        private PostDAO postDAO = new PostDAO();
        // GET: Toppic
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult postDetail(string slug)
        {
            var list = postDAO.getRow(slug);
            return View("postDetail", list);
        }

        public ActionResult _ThongTinShop()
        {

            var list = postDAO.getList(1);
            return View("_ThongTinShop", list);
        }
        public ActionResult _TinCongNghe()
        {
            var list = postDAO.getList(2);
            return View("_TinCongNghe", list);
        }
    }
}