using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using S3Train.Contract;
using S3Train.Domain;
using S3Train.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace S3Train.Web.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private readonly IProductService _productService;
        private readonly IProductAdvertisementService _productAdvertisementService;
        private readonly IBrandService _brandService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IFooterClientService _footerClientService;
        private readonly IMenuService _menuService;
        private readonly IContentCategoryService _contentCategoryService;
        private readonly IMenuTypeService _menuTypeService;
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderDetailTempService _orderDetailTempService;

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;


        public CartController(IProductService productService, IProductAdvertisementService productAdvertisementService, IBrandService brandService, IProductCategoryService productCategoryService, IFooterClientService footerClientService, IMenuService menuService, IContentCategoryService contentCategoryService, IMenuTypeService menuTypeService, IOrderService orderService, IOrderDetailService orderDetailService,IOrderDetailTempService orderDetailTempService)
        {
            
            _productService = productService;
            _productAdvertisementService = productAdvertisementService;
            _productCategoryService = productCategoryService;
            _brandService = brandService;
            _footerClientService = footerClientService;
            _menuService = menuService;
            _contentCategoryService = contentCategoryService;
            _menuTypeService = menuTypeService;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
            _orderDetailTempService = orderDetailTempService;
        }
        public CartController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            var list = new List<CartItem>();

            if (User.Identity.IsAuthenticated == false)
            {
                HttpCookie Cookie = HttpContext.Request.Cookies[ListCart.CartCookie];// lấy cookie

                if (Cookie != null)// check cookies có value nếu cookie not null
                {
                    string ValueCookie = Server.UrlDecode(Cookie.Value);//Decode dịch ngược mã  các ký tự đặc biệt tham khảo
                    var cart = JsonConvert.DeserializeObject<List<CartItem>>(ValueCookie);// convert json to  list object
                    if (cart != null)
                    {
                        list = (List<CartItem>)cart;
                    }

                    int i = 0;
                    while (1 != 2)
                    {
                        i++;
                        HttpCookie cooki = HttpContext.Request.Cookies[i.ToString()];// lấy cookie
                        if (cooki != null)
                        {
                            string valueCookie = Server.UrlDecode(cooki.Value);//Decode dịch ngược mã  các ký tự đặc biệt tham khảo
                            var cart2 = JsonConvert.DeserializeObject<List<CartItem>>(valueCookie);// convert json to  list object
                            if (cart2 != null)
                            {
                                var list2 = (List<CartItem>)cart2;

                                list.AddRange(list2);

                            }

                        }
                        else
                            break;
                    }
                }
            }
            else
            {
                var orderDetailTemp = _orderDetailTempService.ListAllByUserID(User.Identity.GetUserId());
                if(orderDetailTemp!=null)
                {
                    string Value = Server.UrlDecode(orderDetailTemp.CartContent);//Decode dịch ngược mã  các ký tự đặc biệt tham khảo
                    var cart = JsonConvert.DeserializeObject<List<CartItem>>(Value);// convert json to  list object
                    list = (List<CartItem>)cart;
                }
                


            }


            

            return View(list);
        }

        public JsonResult Delete(Guid id)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                int i = -1;
                int thu = 0;
                while (1 != 2)
                {
                    i++;
                    HttpCookie cooki = HttpContext.Request.Cookies[i.ToString()];// lấy cookie
                    if (cooki != null)
                    {
                        string valueCookie = Server.UrlDecode(cooki.Value);//Decode dịch ngược mã  các ký tự đặc biệt tham khảo
                        var cart2 = JsonConvert.DeserializeObject<List<CartItem>>(valueCookie);// convert json to  list object
                        var list2 = (List<CartItem>)cart2;
                        if (cart2 != null)
                        {
                            HttpCookie cookiAfter = HttpContext.Request.Cookies[(i + 1).ToString()];// lấy cookie
                            if (list2.Exists(x => x.Product.Id == id))
                            {
                                if (cookiAfter == null)
                                {

                                    HttpContext.Response.Cookies[i.ToString()].Expires = DateTime.Now.AddDays(-1);
                                    break;
                                }
                                else
                                {
                                    thu = 3;
                                }

                            }
                            if (thu == 3)
                            {
                                if (cookiAfter == null)
                                {
                                    HttpContext.Response.Cookies[i.ToString()].Expires = DateTime.Now.AddDays(-1);
                                    break;
                                }
                                //cooki.Value = cookiAfter.Value;
                                //HttpContext.Response.Cookies.Add(cooki);
                                var a = cooki.Value;
                                string b = cookiAfter.Value;
                                HttpContext.Response.Cookies[i.ToString()].Value = b;
                                HttpContext.Response.Cookies[i.ToString()].Expires = DateTime.Now.AddHours(15);

                            }
                        }

                    }
                    else
                        break;
                }
            }
            else
            {
                try
                {
                    var orderDetailTemp = _orderDetailTempService.ListAllByUserID(User.Identity.GetUserId());
                    string Value = Server.UrlDecode(orderDetailTemp.CartContent);//Decode dịch ngược mã  các ký tự đặc biệt tham khảo
                    var cart = JsonConvert.DeserializeObject<List<CartItem>>(Value);// convert json to  list object
                    var list = (List<CartItem>)cart;
                    if (list.Exists(x => x.Product.Id == id))
                    {
                        foreach (var item in list)
                        {
                            if (item.Product.Id == id)
                            {
                                list.Remove(item);
                                break;
                            }
                        }
                    }

                    string jsonItem = JsonConvert.SerializeObject(list, Formatting.Indented);
                    OrderDetailTemp temp = new OrderDetailTemp();
                    temp.Id = _orderDetailTempService.ListAllByUserID(User.Identity.GetUserId()).Id;
                    temp.UserID = User.Identity.GetUserId();
                    temp.CartContent = jsonItem;
                    _orderDetailTempService.Update(temp);
                }
                catch
                {

                }
                



            }
            




            return Json(new
            {
                status = true
            });
        }

        public JsonResult DeleteAll()
        {
            if(User.Identity.IsAuthenticated==false)
            {
                int i = -1;
                while (1 != 2)
                {
                    i++;
                    HttpCookie cooki = HttpContext.Request.Cookies[i.ToString()];// lấy cookie
                    if (cooki == null)
                        break;
                    //cooki.Expires = DateTime.Now.AddDays(-100);     
                    //HttpContext.Response.Cookies.Add(cooki);
                    HttpContext.Response.Cookies[i.ToString()].Expires = DateTime.Now.AddDays(-1);


                }
            }
            else
            {
                try
                {
                    _orderDetailTempService.DeleteAll(_orderDetailTempService.ListAllByUserID(User.Identity.GetUserId()).Id);
                }
                catch { }
                
            }
            

            return Json(new
            {
                status = true
            });

        }

        public JsonResult Update(string cartModel)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);

                int i = -1;
                while (1 != 2)
                {
                    i++;
                    HttpCookie cooki = HttpContext.Request.Cookies[i.ToString()];// lấy cookie
                    if (cooki == null)
                        break;

                    string valueCookie = Server.UrlDecode(cooki.Value);//Decode dịch ngược mã  các ký tự đặc biệt tham khảo
                    var cart2 = JsonConvert.DeserializeObject<List<CartItem>>(valueCookie);// convert json to  list object
                    var list2 = (List<CartItem>)cart2;
                    foreach (var item in list2)
                    {
                        var jsonItem = jsonCart.SingleOrDefault(x => x.Product.Id == item.Product.Id);
                        if (jsonItem != null)
                        {
                            item.Quatity = jsonItem.Quatity;
                        }
                    }

                    string jsonitem = JsonConvert.SerializeObject(list2, Formatting.Indented);
                    cooki.Value = Server.UrlEncode(jsonitem);

                    string b = cooki.Value;
                    HttpContext.Response.Cookies[i.ToString()].Value = b;
                    HttpContext.Response.Cookies[i.ToString()].Expires = DateTime.Now.AddHours(15);
                }





                //var cart = (List<CartItem>)Session[ListCart.CartSession];

                //foreach (var item in cart)
                //{
                //    var jsonItem = jsonCart.SingleOrDefault(x => x.Product.Id == item.Product.Id);
                //    if(jsonItem!=null)
                //    {
                //        item.Quatity = jsonItem.Quatity;
                //    }
                //}
                //Session[ListCart.CartSession] = cart;
            }
            else
            {
                var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
                try
                {
                    var orderDetailTemp = _orderDetailTempService.ListAllByUserID(User.Identity.GetUserId());
                    string Value = Server.UrlDecode(orderDetailTemp.CartContent);//Decode dịch ngược mã  các ký tự đặc biệt tham khảo
                    var cart = JsonConvert.DeserializeObject<List<CartItem>>(Value);// convert json to  list object
                    var list = (List<CartItem>)cart;
                    foreach (var item in list)
                    {
                        var jsonItem = jsonCart.SingleOrDefault(x => x.Product.Id == item.Product.Id);
                        if (jsonItem != null)
                        {
                            item.Quatity = jsonItem.Quatity;
                        }
                    }
                    string jsonitem = JsonConvert.SerializeObject(list, Formatting.Indented);
                    OrderDetailTemp temp = new OrderDetailTemp();
                    temp.Id = _orderDetailTempService.ListAllByUserID(User.Identity.GetUserId()).Id;
                    temp.UserID = User.Identity.GetUserId();
                    temp.CartContent = jsonitem;
                    _orderDetailTempService.Update(temp);
                }
                catch
                {

                }
                


            }


            return Json(new
            {
                status = true
            });

        }

        public ActionResult AddItem(Guid id, int quantity)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                var product = _productService.GetById(id);
                int k = -1;
                var list3 = new List<CartItem>();
                while (1 != 2)
                {
                    k++;
                    HttpCookie cooki = HttpContext.Request.Cookies[k.ToString()];// lấy cookie// convert json to  list object
                    if (cooki != null)
                    {
                        string ValueCookie = Server.UrlDecode(cooki.Value);//Decode dịch ngược mã  các ký tự đặc biệt
                        var cart = JsonConvert.DeserializeObject<List<CartItem>>(ValueCookie);
                        var list = (List<CartItem>)cart.ToList();

                        if (list.Exists(x => x.Product.Id == id))
                        {
                            foreach (var item in list)
                            {
                                if (item.Product.Id == product.Id)
                                {
                                    item.Quatity += quantity;
                                }
                                list3.Add(item);
                            }
                            string jsonitem = JsonConvert.SerializeObject(list3, Formatting.Indented);
                            //HttpCookie cookie = new HttpCookie(ListCart.CartCookie);// create 
                            cooki.Value = Server.UrlEncode(jsonitem);
                            string b = cooki.Value;
                            HttpContext.Response.Cookies[k.ToString()].Value = b;
                            HttpContext.Response.Cookies[k.ToString()].Expires = DateTime.Now.AddHours(15);
                            return RedirectToAction("Index");

                            //int f = 0;
                            //while (1 != 2)
                            //{
                            //    f++;
                            //    HttpCookie cookie = HttpContext.Request.Cookies[f.ToString()];
                            //    if (cookie == null)
                            //    {
                            //        HttpCookie Cooki2 = new HttpCookie(f.ToString());// create 
                            //        Cooki2.Expires = DateTime.Now.AddHours(15);

                            //        Cooki2.Value = Server.UrlEncode(jsonitem);//Encode dịch  mã  các ký tự đặc biệt, từ string 
                            //        HttpContext.Response.Cookies.Add(Cooki2);// cookie mới đè lên cookie cũ
                            //        break;
                            //    }


                            //}
                            //return RedirectToAction("Index");
                        }
                    }
                    else
                        break;
                }

                HttpCookie Cookie = HttpContext.Request.Cookies[ListCart.CartCookie];// lấy cookie



                if (Cookie != null)
                {
                    string ValueCookie = Server.UrlDecode(Cookie.Value);//Decode dịch ngược mã  các ký tự đặc biệt
                    var cart = JsonConvert.DeserializeObject<List<CartItem>>(ValueCookie);// convert json to  list object
                    if (cart != null)
                    {
                        var list = (List<CartItem>)cart;
                        var list2 = new List<CartItem>();
                        if (list.Exists(x => x.Product == product))
                        {
                            foreach (var item in list)
                            {
                                if (item.Product == product)
                                {
                                    item.Quatity += quantity;
                                }
                            }
                        }
                        else
                        {
                            var item = new CartItem();
                            item.Product = product;
                            item.Quatity = quantity;

                            list2.Add(item);

                        }

                        string jsonItem = JsonConvert.SerializeObject(list2, Formatting.Indented);
                        //HttpCookie cookie = new HttpCookie(ListCart.CartCookie);// create 

                        int i = 0;
                        while (1 != 2)
                        {
                            i++;
                            HttpCookie cooki = HttpContext.Request.Cookies[i.ToString()];
                            if (cooki == null)
                            {
                                HttpCookie Cooki2 = new HttpCookie(i.ToString());// create 
                                Cooki2.Expires = DateTime.Now.AddHours(15);

                                Cooki2.Value = Server.UrlEncode(jsonItem);//Encode dịch  mã  các ký tự đặc biệt, từ string 
                                HttpContext.Response.Cookies.Add(Cooki2);// cookie mới đè lên cookie cũ
                                break;
                            }


                        }


                        //cookie.Expires.AddDays(1);
                        //cookie.Value = Server.UrlEncode(jsonItem);//Encode dịch  mã  các ký tự đặc biệt, từ string 
                        //HttpContext.Response.Cookies.Add(cookie);// cookie mới đè lên cookie cũ


                    }
                }
                else
                {
                    //Tạo mới CartItem
                    var item = new CartItem();
                    item.Product = product;
                    item.Quatity = quantity;
                    var list = new List<CartItem>();
                    list.Add(item);

                    string jsonItem = JsonConvert.SerializeObject(list, Formatting.Indented);
                    HttpCookie cookie = new HttpCookie(ListCart.CartCookie);// create 
                    cookie.Expires = DateTime.Now.AddHours(15);

                    cookie.Value = Server.UrlEncode(jsonItem);//Encode dịch  mã  các ký tự đặc biệt, từ string 
                    HttpContext.Response.Cookies.Add(cookie);// cookie mới đè lên cookie cũ
                }



            }
            else
            {
                var orderDetailTemp = _orderDetailTempService.ListAllByUserID(User.Identity.GetUserId());
                var product = _productService.GetById(id);
                if (orderDetailTemp == null)
                {
                    OrderDetailTemp temp = new OrderDetailTemp();
                    temp.Id = Guid.NewGuid();
                    temp.UserID = User.Identity.GetUserId();
                    _orderDetailTempService.Create(temp);
                    orderDetailTemp = _orderDetailTempService.ListAllByUserID(User.Identity.GetUserId());
                }
                if(orderDetailTemp.CartContent==null)
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.Quatity = quantity;
                    var list = new List<CartItem>();
                    list.Add(item);

                    string jsonItem = JsonConvert.SerializeObject(list, Formatting.Indented);

                    var value = Server.UrlEncode(jsonItem);//Encode dịch  mã  các ký tự đặc biệt, từ string 
                    orderDetailTemp.CartContent = value;// cookie mới đè lên cookie cũ
                }
                else
                {
                    string value = Server.UrlDecode(orderDetailTemp.CartContent);//Decode dịch ngược mã  các ký tự đặc biệt tham khảo
                    var cart = JsonConvert.DeserializeObject<List<CartItem>>(value);// convert json to  list object
                    var list = (List<CartItem>)cart;
                    if (list.Exists(x => x.Product.Id == product.Id))
                    {
                        foreach (var item in list)
                        {
                            if (item.Product.Id == product.Id)
                            {
                                item.Quatity += quantity;
                            }
                            
                        }
                    }
                    else
                    {
                        var item = new CartItem();
                        item.Product = product;
                        item.Quatity = quantity;

                        list.Add(item);

                    }

                    string jsonItem = JsonConvert.SerializeObject(list, Formatting.Indented);
                    OrderDetailTemp temp = new OrderDetailTemp();
                    temp.Id = _orderDetailTempService.ListAllByUserID(User.Identity.GetUserId()).Id;
                    temp.UserID = User.Identity.GetUserId();
                    temp.CartContent = jsonItem;
                    _orderDetailTempService.Update(temp);

                }

                //string Value = Server.UrlDecode(orderDetailTemp.CartContent);//Decode dịch ngược mã  các ký tự đặc biệt tham khảo
                //var cart = JsonConvert.DeserializeObject<List<CartItem>>(Value);// convert json to  list object
                //list = (List<CartItem>)cart;
            }


            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Payment()
        {
            if(User.Identity.IsAuthenticated)
            {
                string id = User.Identity.GetUserId();
                var user = await UserManager.FindByIdAsync(id);
                ViewBag.user = user;
            }


            HttpCookie Cookie = HttpContext.Request.Cookies[ListCart.CartCookie];// lấy cookie


            var list = new List<CartItem>();
            if (Cookie != null)// check cookies có value nếu cookie not null
            {
                string ValueCookie = Server.UrlDecode(Cookie.Value);//Decode dịch ngược mã  các ký tự đặc biệt tham khảo
                var cart = JsonConvert.DeserializeObject<List<CartItem>>(ValueCookie);// convert json to  list object
                if (cart != null)
                {
                    list = (List<CartItem>)cart;
                }

                int i = 0;
                while (1 != 2)
                {
                    i++;
                    HttpCookie cooki = HttpContext.Request.Cookies[i.ToString()];// lấy cookie
                    if (cooki != null)
                    {
                        string valueCookie = Server.UrlDecode(cooki.Value);//Decode dịch ngược mã  các ký tự đặc biệt tham khảo
                        var cart2 = JsonConvert.DeserializeObject<List<CartItem>>(valueCookie);// convert json to  list object
                        if (cart2 != null)
                        {
                            var list2 = (List<CartItem>)cart2;

                            list.AddRange(list2);

                        }

                    }
                    else
                        break;
                }
            }

            return View(list);
        }
        [HttpPost]
        public ActionResult Payment(string ShipName,string Mobile,string Address,string Email)
        {
            var order = new Order();
            order.Id = Guid.NewGuid();
            order.CreateDate = DateTime.Now;
            order.ShipName = ShipName;
            order.ShipMobile = Mobile;
            order.ShipAddress = Address;
            order.ShipEmail = Email;
            order.Status = true;
            if(User.Identity.IsAuthenticated)
            {
                order.CreateBy = User.Identity.Name;
            }

            var id = _orderService.Insert(order);

            var list = new List<CartItem>();
            int i = -1;
            while (1 != 2)
            {
                i++;
                HttpCookie cooki = HttpContext.Request.Cookies[i.ToString()];// lấy cookie
                if (cooki == null)
                    break;
                string valueCookie = Server.UrlDecode(cooki.Value);//Decode dịch ngược mã  các ký tự đặc biệt tham khảo
                var cart2 = JsonConvert.DeserializeObject<List<CartItem>>(valueCookie);// convert json to  list object
                var list2 = (List<CartItem>)cart2;
                list.AddRange(list2);
            }

            foreach (var item in list)
            {
                var orderDetail =new OrderDetail();
                orderDetail.Id = Guid.NewGuid();
                orderDetail.ProductID = item.Product.Id;
                orderDetail.OrderID = id;
                orderDetail.Price = (item.Product.Price * item.Quatity);
                orderDetail.Quantity = item.Quatity;
                var detail = _orderDetailService.Insert(orderDetail);

            }


            //Xóa
            i = -1;
            while (1 != 2)
            {
                i++;
                HttpCookie cooki = HttpContext.Request.Cookies[i.ToString()];// lấy cookie
                if (cooki == null)
                    break;
                cooki.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies.Add(cooki);
            }

            return Redirect("/hoan-thanh");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}