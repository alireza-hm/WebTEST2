using System.ComponentModel.DataAnnotations;

namespace WebTEST2.Models
{
    public class LoginModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Id { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MinLength(6, ErrorMessage = "{0} باید بیشتر از 5 کاراکتر باشد")]
        public string Password { get; set; }

    }
}
