using MyClass.Models;
using MyClass.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ThoiTrang.Controllers
{
    public class CartController : Controller
    {
        private const string CartSesstion="CartSesstion";
        // GET: Cart
        public ActionResult Index()
        {
            var Cart = Session[CartSesstion];
            var list = new List<CartItem>();
            if (Cart != null)
            {
                list = (List<CartItem>)Cart;

            }
            return View(list);
        }

        public JsonResult DeleteAll()
        {
            Session["CartSesstion"] = null;
            return Json(new
            {
                status = true,
            });
        }
        public JsonResult Delete(long id)
        {
            var sesstionCart = (List<CartItem>)Session[CartSesstion];
            sesstionCart.RemoveAll(x => x.Product.Id == id);
            Session[CartSesstion] = sesstionCart;
            return Json(new
            {
                status = true,
            });
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sesstionCart = (List<CartItem>)Session[CartSesstion];
            foreach(var item in sesstionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.Id == item.Product.Id);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session["CartSesstion"] = sesstionCart;
            return Json(new
            {
                status = true,
            });
        }

        //[HttpPost]
        public ActionResult AddToCart(long productId, int quantity=1)
        {
            var product = new ProductDAO().getRow((int)productId);
            var Cart = Session[CartSesstion];
            if(Cart != null)
            {
                var list = (List<CartItem>)Cart;

                if (list.Exists(x => x.Product.Id == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.Id == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    CartItem Item = new CartItem();
                    Item.Product = product;
                    Item.Quantity = quantity;
                    list.Add(Item);
                }
                //gán vào sesstion
                Session[CartSesstion] = list;

            }
            else
            {
                //tạo mới đối tượng cart item
                CartItem Item = new CartItem();
                Item.Product = product;
                Item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(Item);
                //gán vào sesstion
                Session[CartSesstion] = list;
            }
            return RedirectToAction("index");
        }

        public ActionResult Payment()
        {
            var Cart = Session[CartSesstion];
            var list = new List<CartItem>();
            if (Cart != null)
            {
                list = (List<CartItem>)Cart;

            }
            return View(list);
        }
        

        [HttpPost]
        public ActionResult Payment(string name,string phone,string email,string calc_shipping_provinces, string calc_shipping_district,string phuong, string soNha,string ghiChu)
        {
           
            var order = new Order();
            order.UserId = 1;
            order.CreatedDate = DateTime.Now;
            order.ExportDate= DateTime.Now.AddDays(7);
            order.DeliveryAddress = soNha +", " + phuong + ", " + calc_shipping_district + ", " + calc_shipping_provinces;
            order.DeliveryName = name;
            order.DeliveryEmail = email;
            order.DeliveryPhone = phone;
            order.Status = 1;
            order.Code = ghiChu;
            try
            {
                var id = new OrderDAO().InsertUser(order);
                var Cart = (List<CartItem>)Session[CartSesstion];
                var detailDao = new OrderDetailDAO();
                foreach (var item in Cart)
                {
                    var orderDetail = new Orderdetail();
                    orderDetail.OrderId = (int)id;
                    orderDetail.ProductId = item.Product.Id;
                    orderDetail.Quantity = item.Quantity;

                    if (item.Product.PriceSale != 0)
                    {
                        orderDetail.Price = item.Product.PriceSale;
                        orderDetail.Amount = item.Product.PriceSale;
                    }
                    else
                    {
                        orderDetail.Price = item.Product.Price;
                        orderDetail.Amount = item.Product.Price;
                    }

                    detailDao.InsertUser(orderDetail);
                }
            }
            catch(Exception ex)
            {
                //ghi log
                return Redirect("/loi-thanh-toan");
            }




            return Redirect("/hoan-thanh");
        }

        public ActionResult Success()
        {
            var Cart = Session[CartSesstion];
            var list = new List<CartItem>();
            if (Cart != null)
            {
                list = (List<CartItem>)Cart;

            }
            return View(list);
        }
        public ActionResult _InfoDHTC()
        {
            OrderDAO orderDAO = new OrderDAO();
            //int customerID = int.Parse(Session["customerId"].ToString());
            var list = orderDAO.getRowNew();
            return View("_InfoDHTC", list);
        }
    }
}