using Microsoft.AspNet.Identity.Owin;
using PagedList;
using S3Train.Domain;
using S3Train.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace S3Train.Web.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationRoleManager _roleManager;
        private ApplicationUserManager _userManager;

        public CustomerController()
        {
        }

        public CustomerController(ApplicationRoleManager roleManager, ApplicationUserManager userManager)
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
        // GET: Admin/Customer
        public ActionResult Index(string search, int? i)
        {
            if (search == null)
            {
                search = "";
            }
            else
            {
                i = 1;
            }
            var role = (from r in RoleManager.Roles where r.Name.Contains("Customer") select r).FirstOrDefault();
            var users = UserManager.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            var userVM = users.Select(user => new UserViewModel
            {
                Id = user.Id,
                Name=user.Name,
                UserName = user.UserName,
                Email = user.Email,
                ModifyDate = user.ModifyDate,
                PhoneNumber = user.PhoneNumber,
                status = user.status,
                RoleName = "Customer"
            }).Where(x => x.UserName.Contains(search) || x.Email.Contains(search) || search == null).ToList().ToPagedList(i ?? 1, 3);

            //var role2 = (from r in RoleManager.Roles where r.Name.Contains("Admin") select r).FirstOrDefault();
            //var admins = UserManager.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role2.Id)).ToList();

            //var adminVM = admins.Select(user => new UserViewModel
            //{
            //    Username = user.UserName,
            //    Email = user.Email,
            //    RoleName = "Admin"
            //}).ToList();
            //var model = new GroupedUserViewModel { Users = userVM, Admins = adminVM };
            var model = userVM;



            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        //Danh sách Role
        //public string IdRole(string Name)
        //{
        //    foreach (var item in RoleManager.Roles)
        //    {
        //        if (item.Name == Name)
        //            return item.Id;
        //    }
        //    return "";
        //}

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, CreateDate = DateTime.Now, ModifyDate = DateTime.Now, CreateBy = User.Identity.GetUserName(), Name = model.Name, status = true };
                var result = await UserManager.CreateAsync(user, model.PassWord);
                if (result.Succeeded)
                {
                    result = await UserManager.AddToRoleAsync(user.Id, "Customer");
                    //    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    //    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    //    // Send an email with this link
                    //    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    //    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    //    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index");
                }
                //AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
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
            user.Name = model.Name;
            user.Address = model.Address;
            user.ModifyDate = DateTime.Now;
            user.ModifyBy = User.Identity.GetUserName();
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

        public async Task<ActionResult> Details(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            return View(new UserViewModel(user));
        }


        public async Task<ActionResult> Delete(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            return View(new UserViewModel(user));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            await UserManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<JsonResult> ChangeStatus(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            
            if (user.status == true)
                user.status = false;
            else
                user.status = true;
            
            var result = await UserManager.UpdateAsync(user);
            return Json(new
            {
                user.status
            });

        }


   
        


        //
        // POST: /Account/ResetPassword
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(string id, ResetPasswordViewModel model)
        {
            var user = await UserManager.FindByIdAsync(id);
            model.Email = user.Email;
            return View(model);
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

            var result = await UserManager.ResetPasswordAsync(user.Id,code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Customer");
            }
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}