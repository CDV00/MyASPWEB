using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThoiTrang.Controllers
{
    public class AuthSiteController : Controller
    {
        // GET: AuthSite

        private MyDBContext db = new MyDBContext();


        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult DoLogin(FormCollection field)
        {
            //lấy dữ liệu từ form login 
            ViewBag.Error = "";
            string username = field["username"];
            string password = field["password"];
            // kiển tra xem trong cơ sở dữ liêu có Roles = Admin, status = 1 và UserName = <username> hoặc Email = <username>
            // nếu có lấy giá trị dầu tiên gán vào biến user_row
            User user = db.Users.Where(m => m.Roles == "Customer" && m.Status == 1 && (m.UserName == username || m.Email == username)).FirstOrDefault();
            // nếu user_row có giá trị
            if (user != null)
            {
                //có tài khoản

                //Nhập sai mật khẩu quá 5 lần
                if (user.CountError >= 5 && user.Roles != "Admin")
                {
                    ViewBag.Error = "<strong class=\"text-danger \">Bạn đã nhập sai mật khẩu quá 5 lần!<br>Vui lòng liên hệ người quản lý.</strong>";
                }
                if (user.Password.Equals(password))
                {
                    //đúng mật khẩu
                    Session["customerName"] = username;
                    Session["customerId"] = user.Id.ToString();
                    Session["FullName"] = user.FullName.ToString();
                    //Session["Img"] = user.Img.ToString();
                    return RedirectToAction("Index", "Accout");

                }
                else
                {
                    if (user.CountError == null)
                    {
                        user.CountError = 1;
                    }
                    else
                    {
                        user.CountError += 1;
                    }
                    db.Entry(User).State = EntityState.Modified;
                    db.SaveChanges();
                    //sai mật khẩu
                    ViewBag.Error = "<strong class=\"text-danger \">Mật khẩu không chính xác!</strong>";
                }
            }
            else
            {
                //thông báo tài khoản không tồn tại
                ViewBag.Error = "<strong class=\"text-danger \">Tai khoản " + username + " không tồn tại!</strong>";
            }
            return View("Login");
        }
        public JsonResult Logout()
        {
            Session["customerName"] = null;
            Session["customerId"] = null;
            Session["FullName"] = null;
            return Json(new
            {
                status = true,
            });
        }
    }
}