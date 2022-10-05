using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ThoiTrang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //Sản phẩm đã mua
            routes.MapRoute(
                name: "Sản phẩm đã mua",
                url: "san-pham-da-mua",
                defaults: new { controller = "Accout", action = "ProductsBought", id = UrlParameter.Optional }
            );
            //Tài khoản
            routes.MapRoute(
                name: "Accout",
                url: "tai-khoan",
                defaults: new { controller = "Accout", action = "Index", id = UrlParameter.Optional }
            );
             //Dăng nhap
            routes.MapRoute(
                name: "Đăng nhập",
                url: "dang-nhap",
                defaults: new { controller = "AuthSite", action = "Login", id = UrlParameter.Optional }
            );

            //bai viet
            routes.MapRoute(
                name: "chi tiết bài viết",
                url: "chi-tiet-bai-viet/{slug}",
                defaults: new { controller = "Topics", action = "postDetail", id = UrlParameter.Optional }
            );
            //bai viet
            routes.MapRoute(
                name: "chủ đề bài viết",
                url: "chu-de-bai-viet",
                defaults: new { controller = "Topics", action = "Index", id = UrlParameter.Optional },
                 new[] { "ThoiTrang.Controllers" }
            );
            //đường dẫn thêm thanh toán
            routes.MapRoute(
                name: "thanh toán",
                url: "thanh-toan",
                defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional }
            );
            //đường dẫn hoàn thành thanh toán
            routes.MapRoute(
                name: "hoàn thành thanh toán",
                url: "hoan-thanh",
                defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional }
            );
            //đường dẫn thêm vào giỏ hàng
            routes.MapRoute(
                name: "them-gio-hang",
                url: "them-gio-hang",
                defaults: new { controller = "Cart", action = "AddToCart", id = UrlParameter.Optional }
            );
            //đường dẫn giỏ hàng
            routes.MapRoute(
                name: "gio-hang",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
            );
            //đường dẫn chi tiết sản phẩm
            routes.MapRoute(
                name: "Chi tiet san pham",
                url: "chi-tiet-san-pham/{slug}",
                defaults: new { controller = "Site", action = "productDetail", id = UrlParameter.Optional }
            );
            //đường dẫn nhà sản xuất
            routes.MapRoute(
                name: "Nha san xuat",
                url: "nha-san-xuat/{slug}",
                defaults: new { controller = "Site", action = "productSupplier", id = UrlParameter.Optional }
            );
            //đường dẫn loại sản phẩm
            routes.MapRoute(
                name: "loai san pham",
                url: "loai-san-pham/{slug}",
                defaults: new { controller = "Site", action = "productCategory", id = UrlParameter.Optional }
            );
            //đương dẫn home
            routes.MapRoute(
                name: "trang chu",
                url: "trang-chu",
                defaults: new { controller = "Site", action = "index", id = UrlParameter.Optional }
            );
            //đường dẫn mặc định
            routes.MapRoute(
                name: "link 1cap",
                url: "{slug}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );
            //đường dẫn mặc định
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
