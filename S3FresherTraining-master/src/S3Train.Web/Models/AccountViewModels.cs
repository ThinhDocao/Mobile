using S3Train.Domain;
using S3Train.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace S3Train.Models
{

    public class HomeModelIndex
    {
        public LoginViewModel loginModel { get; set; }
        public RegisterViewModel registerModel{ get; set; }
    }

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }


    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class GroupedUserViewModel
    {
        public List<UserViewModel> Users { get; set; }
        public List<UserViewModel> Admins { get; set; }

        public List<UserViewModel> Customer { get; set; }
    }
    public class UserViewModel
    {
        public UserViewModel() { }

        public UserViewModel(ApplicationUser user)
        {
            Id = user.Id;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            Address = user.Address;
            UserName = user.UserName;
            Name = user.Name;
            CreateDate = user.CreateDate;
            CreateBy = user.CreateBy;
            ModifyDate = user.ModifyDate;
            ModifyBy = user.ModifyBy;
            status = user.status;
        }
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string PassWord { get; set; }
        public string Address { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string ModifyBy { get; set; }

        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public bool status { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("PassWord", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

}
