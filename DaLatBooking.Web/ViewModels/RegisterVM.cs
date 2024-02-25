using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DaLatBooking.Web.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Display(Name = "Xác nhận mật khẩu")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Têm đăng nhập")]
        public string Name { get; set; }
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get;set; }
        public string? RedirectUrl { get; set; }
        public string? Role { get; set; }

        //[ValidateNever]
        //public IEnumerable<SelectListItem>? RoleList { get; set; }
    }
}
