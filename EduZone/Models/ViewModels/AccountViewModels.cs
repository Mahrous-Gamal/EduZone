using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduZone.Models
{
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
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(compit\.aun\.edu\.eg)$", ErrorMessage = "Not valid academic mail")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(compit\.aun\.edu\.eg)$", ErrorMessage = "Not valid academic mail")]
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
        public string Name  { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage ="Must be Number Not charactor")]
        [MaxLength(14,ErrorMessage ="Must be 14 Number")]
        public string NationalID { get; set; }

        public string Address { get; set; }
            

        [Required]
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(compit\.aun\.edu\.eg)$", ErrorMessage = "Not valid academic mail")]
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
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(compit\.aun\.edu\.eg)$", ErrorMessage = "Not valid academic mail")]
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
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(compit\.aun\.edu\.eg)$", ErrorMessage = "Not valid academic mail")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
