using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using S3Train.Models;
using S3Train.Contract;

namespace S3Train.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationRoleManager _roleManager;
        private ApplicationUserManager _userManager;

        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IBrandService _brandService;

        public HomeController()
        {
        }
        public HomeController(IProductService productService, IProductCategoryService productAdvertisementService, IBrandService brandService)
        {
            _productService = productService;
            _productCategoryService = productAdvertisementService;
            _brandService = brandService;
        }

        public HomeController(ApplicationRoleManager roleManager, ApplicationUserManager userManager)
        {
            RoleManager = roleManager;
            UserManager = userManager;

        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
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

        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Navigation()
        {
            var UserID = User.Identity.GetUserId();
            var users = UserManager.Users.Where(x => x.Id == UserID).FirstOrDefault();

            var role = RoleManager.Roles.Where(x => x.Users.Select(y => y.UserId).Contains(users.Id)).ToList();
            
            ViewBag.Name = role;
            var productCategory = _productCategoryService.ListAll();


            ViewBag.productCategory = _productCategoryService.ListAll();
            return PartialView();
        }


        [ChildActionOnly]
        public ActionResult Header()
        {
            var UserID = User.Identity.GetUserId();
            var users = UserManager.Users.Where(x => x.Id == UserID).FirstOrDefault();


            
            ViewBag.userName = users.Name;
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView();
        }

        public async Task<ActionResult> Edit(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            return View(new UserViewModel(user));
        }


        [HttpPost]
        public async Task<ActionResult> Edit(string id, UserViewModel model)
        {
            var user = await UserManager.FindByIdAsync(id);
            user.Id = id;
            
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;
            user.ModifyDate = DateTime.Now;
            user.ModifyBy = User.Identity.GetUserName();
            user.Name = model.Name;
            //var user = new ApplicationUser()
            //{
            //    Id = model.Id,
            //    //Email = model.Email,
            //    //PhoneNumber = model.PhoneNumber,
            //    UserName = model.Username,
            //    Address = model.Address,
            //    ModifyDate = DateTime.Now,
            //    ModifyBy = model.ModifyBy,
            //    CreateDate = DateTime.Now,
            //    status = true
            //}; 
            await UserManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }


        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }


        

    }
}