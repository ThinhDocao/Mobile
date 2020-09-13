using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Facebook;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using S3Train.Contract;
using S3Train.Domain;
using S3Train.Models;
using S3Train.Web.Models;

namespace S3Train.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductAdvertisementService _productAdvertisementService;
        private readonly IBrandService _brandService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IFooterClientService _footerClientService;
        private readonly IMenuService _menuService;
        private readonly IContentCategoryService _contentCategoryService;
        private readonly IMenuTypeService _menuTypeService;
        private readonly IProductSpecSpecValueService _productSpecSpecValueService;
        private readonly IOrderDetailTempService _orderDetailTempService;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public HomeController() { }

        public HomeController(IProductService productService, IProductAdvertisementService productAdvertisementService, IBrandService brandService, IProductCategoryService productCategoryService, IFooterClientService footerClientService, IMenuService menuService, IContentCategoryService contentCategoryService, IMenuTypeService menuTypeService, IProductSpecSpecValueService productSpecSpecValueService,IOrderDetailTempService orderDetailTempService)
        {
            _productService = productService;
            _productAdvertisementService = productAdvertisementService;
            _productCategoryService = productCategoryService;
            _brandService = brandService;
            _footerClientService = footerClientService;
            _menuService = menuService;
            _contentCategoryService = contentCategoryService;
            _menuTypeService = menuTypeService;
            _productSpecSpecValueService = productSpecSpecValueService;
            _orderDetailTempService = orderDetailTempService;
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
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
            ViewBag.TopHotALL = _productService.ListTopHotALL();

            ViewBag.productCategory = _productCategoryService.ListAll();

            var list = new List<CartItem>();
            try
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

                    int kk = 0;
                    while (1 != 2)
                    {
                        kk++;
                        HttpCookie cooki = HttpContext.Request.Cookies[kk.ToString()];// lấy cookie
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
            catch { }



            ////////
            //
            if (User.Identity.IsAuthenticated && list.Count!=0)
            {

                foreach (var lst in list)
                {
                    var orderDetailTemp = _orderDetailTempService.ListAllByUserID(User.Identity.GetUserId());
                    var product = _productService.GetById(lst.Product.Id);
                    if (orderDetailTemp == null)
                    {
                        OrderDetailTemp temp = new OrderDetailTemp();
                        temp.Id = Guid.NewGuid();
                        temp.UserID = User.Identity.GetUserId();
                        _orderDetailTempService.Create(temp);
                        orderDetailTemp = _orderDetailTempService.ListAllByUserID(User.Identity.GetUserId());
                    }
                    if (orderDetailTemp.CartContent == null)
                    {
                        var item = new CartItem();
                        item.Product = product;
                        item.Quatity = lst.Quatity;
                        var list2 = new List<CartItem>();
                        list2.Add(item);

                        string jsonItem = JsonConvert.SerializeObject(list2, Formatting.Indented);

                        var value = Server.UrlEncode(jsonItem);//Encode dịch  mã  các ký tự đặc biệt, từ string 
                        orderDetailTemp.CartContent = value;// cookie mới đè lên cookie cũ
                    }
                    else
                    {
                        string value = Server.UrlDecode(orderDetailTemp.CartContent);//Decode dịch ngược mã  các ký tự đặc biệt tham khảo
                        var cart = JsonConvert.DeserializeObject<List<CartItem>>(value);// convert json to  list object
                        var list2 = (List<CartItem>)cart;
                        if (list2.Exists(x => x.Product.Id == product.Id))
                        {
                            foreach (var item in list2)
                            {
                                if (item.Product.Id == product.Id)
                                {
                                    item.Quatity += lst.Quatity;
                                }

                            }
                        }
                        else
                        {
                            var item = new CartItem();
                            item.Product = product;
                            item.Quatity = lst.Quatity;

                            list2.Add(item);

                        }

                        string jsonItem = JsonConvert.SerializeObject(list2, Formatting.Indented);
                        OrderDetailTemp temp = new OrderDetailTemp();
                        temp.Id = _orderDetailTempService.ListAllByUserID(User.Identity.GetUserId()).Id;
                        temp.UserID = User.Identity.GetUserId();
                        temp.CartContent = jsonItem;
                        _orderDetailTempService.Update(temp);

                    }


                }

                /////Xóa cooki
                ///
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


            return View();
        }

        [ChildActionOnly]
        public ActionResult _Footer(FooterClientViewModels model)
        {
            var footer = _footerClientService.ListAll();
            model = new FooterClientViewModels(footer);
            return PartialView(model);
        }



        [ChildActionOnly]
        public ActionResult ProductCategoryMenu()
        {
            var productCategory = _productCategoryService.ListAllOrderByCreateDate();
            var model = productCategory.Select(item=>new ProductCategoryViewModels(item)).ToList();
            
            ViewBag.ContentCategory = _contentCategoryService.ListAllDisplayOrder();
            ViewBag.MenuType = _menuTypeService.ListAll();
            ViewBag.Menu = _menuService.ListAll();

            if(User.Identity.IsAuthenticated==false)
            {
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
                ViewBag.Cart = list;
            }
            else
            {
                var list = new List<CartItem>();
                var orderDetailTemp = _orderDetailTempService.ListAllByUserID(User.Identity.GetUserId());
                if (orderDetailTemp != null)
                {
                    string Value = Server.UrlDecode(orderDetailTemp.CartContent);//Decode dịch ngược mã  các ký tự đặc biệt tham khảo
                    var cart = JsonConvert.DeserializeObject<List<CartItem>>(Value);// convert json to  list object
                    list = (List<CartItem>)cart;
                }
                ViewBag.Cart = list;
            }
            

            return PartialView(model);
        }

        public ActionResult MenuPrice()
        {
            ViewBag.MenuType = _menuTypeService.ListAll();
            ViewBag.Menu = _menuService.ListAll();
            return PartialView();
        }

        public ActionResult Brand()
        {
            var view = _brandService.ListAll();
            var model = view.Select(item => new BrandVIewModels(item)).ToList();
            return PartialView(model);
        }

        





        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Detail(Guid id)
        {
            var product = _productService.GetById(id);


            try
            {
                //More Image
                var images = product.MoreImage;
                List<string> listImagesReturn = new List<string>();
                if (images == null)
                {
                    return View();
                }
                XElement xImages = XElement.Parse(images);


                foreach (XElement element in xImages.Elements())
                {
                    listImagesReturn.Add(element.Value);
                }
                ////////////
                ViewBag.MoreImage = listImagesReturn.ToList();
            }
            catch { }


            var model = new ProductViewModel(product);
            ViewBag.relatedProduct = _productService.relatedProduct(id);






            ///Hieu
            ViewBag.relatedProduct = _productService.relatedProduct(id);
            var producht = _productService.detailProduct(id);
            //ViewBag.view = _productSpecSpecValueService.loadTS(id);
            var a = _productSpecSpecValueService.loadTS(id);

            return PartialView(model);
        }



        [AllowAnonymous]
        public PartialViewResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return PartialView();
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string name = model.Email.Substring(0, model.Email.IndexOf("@"));
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, CreateDate = DateTime.Now, ModifyDate = DateTime.Now, CreateBy = User.Identity.GetUserName(), Name = name, status = true };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    result = await UserManager.AddToRoleAsync(user.Id, "Customer");
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return PartialView(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }





        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public async Task<ActionResult> FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code,
            });
            var accessToken = result.access_token;
            if(!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,email,id");
                string email = me.first_name + " " + me.middle_name + " " + me.last_name;
                string userName = me.first_name + " " + me.middle_name + " " + me.last_name;
                string firstName = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;

                email = email.Replace(" ", "")+"@gmail.com";
                userName= userName.Replace(" ", "")+ "@gmail.com";

                //InsertFB
                var userFB= await UserManager.FindByEmailAsync(email);
                if(userFB==null)
                {
                    var user = new ApplicationUser { UserName = email, Email = email, CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Name = firstName+" "+middlename+" "+lastname, status = true };
                    var kq = await UserManager.CreateAsync(user);
                    if (kq.Succeeded)
                    {
                        kq = await UserManager.AddToRoleAsync(user.Id, "Employee");

                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    }
                }
                else
                {
                    await SignInManager.SignInAsync(userFB, isPersistent: false, rememberBrowser: false);
                }
            }
            return Redirect("/");
        }

        ////////////////Login ACcount
        ///

        [AllowAnonymous]
        public ActionResult LoginAccount()
        {
            return PartialView();
        }

        public ActionResult ExternalLoginFailure()
        {
            return View();
        }
        public ActionResult ExternalLoginConfirmation()
        {
            return View();
        }
        public ActionResult GitHub()
        {
            return View();
        }
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }




        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public void ExternalLogin(string provider, string returnUrl = null)
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("ExternalLoginCallback", "Home", new { ReturnUrl = returnUrl }) };
            ControllerContext.HttpContext.GetOwinContext().Authentication.Challenge(properties, provider);

        }

        private string m_url;

        [JsonProperty("url")]
        public string ImageUrl
        {
            get { return m_url; }
            set { m_url = value; }
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl = null)
        {
            ExternalLoginConfirmationViewModel model = new ExternalLoginConfirmationViewModel();
            var info = await AuthenticationManager.GetExternalLoginInfoAsync();

            if (info == null) { return RedirectToAction("Index", "Home"); }
            ViewBag.LoginProvider = info.Login.LoginProvider;

            if (info.Login.LoginProvider == "Facebook")
            {
                var identity = AuthenticationManager.GetExternalIdentity(DefaultAuthenticationTypes.ExternalCookie);
                var accessToken = identity.FindFirstValue("FacebookAccessToken");
                var fb = new FacebookClient(accessToken);
                dynamic email = fb.Get("me?fields=email");
                dynamic birthday = fb.Get("me?fields=birthday");
                dynamic name = fb.Get("me?fields=name");
                dynamic first_name = fb.Get("me?fields=first_name");
                dynamic last_name = fb.Get("me?fields=last_name");
                dynamic link = fb.Get("me?fields=link");
                dynamic gender = fb.Get("me?fields=gender");
                dynamic locale = fb.Get("me?fields=locale");
                //You can find other fields at https://developers.facebook.com/docs/graph-api/reference/user

                model = AddToModel(name.name, null, first_name.first_name, last_name.last_name, email.email, birthday.birthday, link.link, gender.gender, locale.locale, null, null, null);
            }
            else if (info.Login.LoginProvider == "Google")
            {
                string emailaddress = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
                //string name = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
                //string givenname = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").Value;
                //string surname = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname").Value;
                //string url = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:google:url").Value;
                //string profile = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:google:profile").Value;
                ////Profile and Url returns same value.
                //string image = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:google:image").Value;
                //var imageUrl = JsonConvert.DeserializeObject<HomeController>(image).ImageUrl;
                var userFB = await UserManager.FindByEmailAsync(emailaddress);
                if (userFB == null)
                {
                    var user = new ApplicationUser { UserName = emailaddress, Email = emailaddress, CreateDate = DateTime.Now, ModifyDate = DateTime.Now, Name = emailaddress.Substring(0, emailaddress.IndexOf("@")), status = true };
                    var kq = await UserManager.CreateAsync(user);
                    if (kq.Succeeded)
                    {
                        kq = await UserManager.AddToRoleAsync(user.Id, "Employee");

                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    }
                }
                else
                {
                    await SignInManager.SignInAsync(userFB, isPersistent: false, rememberBrowser: false);
                }
                return Redirect("/");
            }
            else if (info.Login.LoginProvider == "Microsoft")
            {
                string birth_day = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:microsoft:birth_day").Value;
                string birth_month = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:microsoft:birth_month").Value;
                string birth_year = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:microsoft:birth_year").Value;
                string birthday = birth_month + "/" + birth_day + "/" + birth_year;
                string email = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                string name = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:microsoft:name").Value;
                string first_name = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:microsoft:first_name").Value;
                string last_name = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:microsoft:last_name").Value;
                string link = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:microsoft:link").Value;
                string gender = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:microsoft:gender").Value;
                string locale = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:microsoft:locale").Value;

                model = AddToModel(name, null, first_name, last_name, email, birthday, link, gender, locale, null, null, null);
            }
            else if (info.Login.LoginProvider == "LinkedIn")
            {
                string id = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:linkedin:id").Value;
                string imageUrl = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:linkedin:pictureUrl").Value;
                string name = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:linkedin:name").Value;
                string formattedName = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:linkedin:formattedName").Value;
                string firstName = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:linkedin:firstName").Value;
                string lastName = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:linkedin:lastName").Value;
                string emailAddress = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:linkedin:emailAddress").Value;
                string headline = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:linkedin:headline").Value;
                string url = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:linkedin:url").Value;
                string publicProfileUrl = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:linkedin:publicProfileUrl").Value;

                //You can find other fields at https://developer.linkedin.com/docs/fields/basic-profile

                model = AddToModel(name, formattedName, firstName, lastName, emailAddress, null, url, null, null, headline, publicProfileUrl, imageUrl);
            }
            else if (info.Login.LoginProvider == "Twitter")
            {
                string userid = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:twitter:userid").Value;
                string screenname = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:twitter:screenname").Value;

            }
            else if (info.Login.LoginProvider == "GitHub")
            {
                string name = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;
                string emailaddress = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
                string url = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:url").Value;
                string login = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:login").Value;
                string id = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:id").Value;
                string avatar_url = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:avatar_url").Value;
                string gravatar_id = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:gravatar_id").Value;
                string html_url = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:html_url").Value;
                string followers_url = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:followers_url").Value;
                string following_url = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:following_url").Value;
                string gists_url = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:gists_url").Value;
                string starred_url = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:starred_url").Value;
                string subscriptions_url = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:subscriptions_url").Value;
                string organizations_url = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:organizations_url").Value;
                string repos_url = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:repos_url").Value;
                string events_url = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:events_url").Value;
                string received_events_url = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:received_events_url").Value;
                string type = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:type").Value;
                string site_admin = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:site_admin").Value;
                string name2 = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:name").Value;
                string company = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:company").Value;
                string blog = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:blog").Value;
                string location = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:location").Value;
                string email = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:email").Value;
                string hireable = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:hireable").Value;
                string bio = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:bio").Value;
                string public_repos = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:public_repos").Value;
                string public_gists = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:public_gists").Value;
                string followers = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:followers").Value;
                string following = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:following").Value;
                string created_at = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:created_at").Value;
                string updated_at = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:updated_at").Value;
                string private_gists = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:private_gists").Value;
                string total_private_repos = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:total_private_repos").Value;
                string owned_private_repos = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:owned_private_repos").Value;
                string disk_usage = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:disk_usage").Value;
                string collaborators = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:collaborators").Value;
                string plan = info.ExternalIdentity.Claims.FirstOrDefault(c => c.Type == "urn:github:plan").Value;

                string[] git = new string[] { name, emailaddress, url, login, id, avatar_url, gravatar_id, html_url, followers_url, following_url, gists_url, starred_url, subscriptions_url, organizations_url, repos_url, events_url, received_events_url, type, site_admin, name2, company, blog, location, email, hireable, bio, public_repos, public_gists, followers, following, created_at, updated_at, private_gists, total_private_repos, owned_private_repos, disk_usage, collaborators, plan };
                return View("GitHub", git);
            }
            else
            {
                model.Email = null;
                model.BirthDate = null;
                model.FirstName = null;
                model.LastName = null;
            }
            return View("ExternalLoginConfirmation", model);
        }

        public ExternalLoginConfirmationViewModel AddToModel(string name, string formattedName, string firstName, string lastName, string email, string birthday, string link, string gender, string locale, string headline, string publicProfileUrl, string imageUrl)
        {
            var model = new ExternalLoginConfirmationViewModel();
            model.BirthDate = birthday;
            model.Email = email;
            model.FirstName = firstName;
            model.FormattedName = formattedName;
            model.Gender = gender;
            model.Headline = headline;
            model.LastName = lastName;
            model.Link = link;
            model.Locale = locale;
            model.Name = name;
            model.PublicProfileUrl = publicProfileUrl;
            model.ImageUrl = imageUrl;
            return model;
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl = null)
        {
            //ToDo
            return View("Index");
        }


        public IEnumerable<SelectListItem> GetCountries()
        {
            List<SelectListItem> countryNames = new List<SelectListItem>();
            countryNames.Add(new SelectListItem { Text = "AAA", Value = "AAA" });
            countryNames.Add(new SelectListItem { Text = "BBB", Value = "BBB" });
            countryNames.Add(new SelectListItem { Text = "CCC", Value = "CCC" });
            countryNames.Add(new SelectListItem { Text = "DDD", Value = "DDD" });
            return countryNames;
        }

    }

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Name (All)")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "FormattedName (LinkedIn)")]
        public string FormattedName { get; set; }

        [Required]
        [Display(Name = "First Name (All)")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name (All)")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email (All)")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "BirthDate (Facebook, Microsoft)")]
        public string BirthDate { get; set; }

        [Required]
        [Display(Name = "Link (All)")]
        public string Link { get; set; }


        [Required]
        [Display(Name = "Gender (Facebook, Microsoft)")]
        public string Gender { get; set; }


        [Required]
        [Display(Name = "Locale (Facebook, Microsoft)")]
        public string Locale { get; set; }


        [Required]
        [Display(Name = "Headline (LinkedIn)")]
        public string Headline { get; set; }


        [Required]
        [Display(Name = "PublicProfileUrl (LinkedIn)")]
        public string PublicProfileUrl { get; set; }

        [Required]
        [Display(Name = "ImageUrl (Google, LinkedIn)")]
        public string ImageUrl { get; set; }
    }

}