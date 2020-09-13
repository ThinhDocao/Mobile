using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using S3Train.Domain;
using S3Train.Models;
using S3Train.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace S3Train.Web.Areas.Admin.Controllers
{
    public class EmployeeManagerController : Controller
    {
        private ApplicationRoleManager _roleManager;
        private ApplicationUserManager _userManager;

        public EmployeeManagerController()
        {
        }

        public EmployeeManagerController(ApplicationRoleManager roleManager, ApplicationUserManager userManager)
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
        // GET: Admin/EmployeeManager
        public ActionResult Index(int page=1,int pageSize=3)
        {

            var role = (from r in RoleManager.Roles where r.Name.Contains("Employee")select r).FirstOrDefault();
            var users = UserManager.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            var userVM = users.Select(user => new UserViewModel
            {
                Id=user.Id,
                Username=user.UserName,
                Email=user.Email,
                PhoneNumber=user.PhoneNumber,
                RoleName="Employee"
            }).ToList();

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


        public async Task<ActionResult> Edit(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            return View(new UserViewModel(user));
        }

        
        [HttpPost]
        public async Task<ActionResult> Edit(string id,UserViewModel model)
        {
            var user = await UserManager.FindByIdAsync(id);
            user.Id = model.Id;
            user.PasswordHash = model.PassWord;
            user.PhoneNumber = model.PhoneNumber;
            user.UserName = model.Username;
            user.Address = model.Address;
            user.ModifyDate = DateTime.Now;
            user.ModifyBy = model.ModifyBy;
            user.CreateDate = DateTime.Now;
            user.status = true;
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


    }
}