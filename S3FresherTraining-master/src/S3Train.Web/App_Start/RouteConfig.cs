using System.Web.Mvc;
using System.Web.Routing;

namespace S3Train
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                 name: "Product Chitiet",
                 url: "chi-tiet/{metatitle}/{id}",
                 defaults: new { controller = "Home", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "S3Train.Controllers" }
             );
            routes.MapRoute(
                name: "Product Category",
                url: "san-pham/{metatitle}/{id}",
                defaults: new { controller = "Product", action = "ProductCategory", id = UrlParameter.Optional },
                namespaces: new[] { "S3Train.Web.Controllers" }
            );
            //  routes.MapRoute(
            //     name: "Product Brand",
            //     url: "dien-thoai/{metatitle}",
            //     defaults: new { controller = "Product", action = "ProductSP", id = UrlParameter.Optional },
            //      namespaces: new[] { "S3Train.Controllers" }
            // );

            //  routes.MapRoute(
            //      name: "Home",
            //      url: "{controller}/{action}/{ten}/{gia}"
            //  ); 
            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "S3Train.Controllers" }
            );



            routes.MapRoute(
                name: "Add Cart",
                url: "them-gio-hang/{id}-{quantity}",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
                namespaces: new[] { "S3Train.Controllers" }
            );

            routes.MapRoute(
                name: "Cart",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "S3Train.Controllers" }
            );

            routes.MapRoute(
                name: "Payment",
                url: "thanh-toan",
                defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
                namespaces: new[] { "S3Train.Controllers" }
            );

            routes.MapRoute(
                name: "Payment Success",
                url: "hoan-thanh",
                defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
                namespaces: new[] { "S3Train.Controllers" }
            );

            //  routes.MapRoute(
            //    name: "Menu Product",
            //    url: "dien-thoai/{text}-{category_id}",
            //    defaults: new { controller = "Product", action = "SearchDK", id = UrlParameter.Optional },
            //      namespaces: new[] { "S3Train.Controllers" }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "S3Train.Controllers" }
            );
        }
    }
}
