using System.ComponentModel.DataAnnotations;

namespace WebTEST2.Models
{
    public class RegisterModel
    {

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Name { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Family { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MinLength(6, ErrorMessage = "{0} باید بیشتر از 5 کاراکتر باشد")]
        public string Password { get; set; }
        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MinLength(6, ErrorMessage = "{0} باید بیشتر از 5 کاراکتر باشد")]
        public string Repassword { get; set; }
        [Display(Name = "نام پدر")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Father_name { get; set; }
        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public int Code_meli { get; set; }

        [Display(Name = "دانشکده")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Daneshkade { get; set; }
        [Display(Name = "رشته تحصیلی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Reshte { get; set; }
    }
}
