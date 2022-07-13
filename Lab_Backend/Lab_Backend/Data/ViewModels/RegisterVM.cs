using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lab_Backend.Data.ViewModels
{
    public class RegisterVM
    {
        [DisplayName("Full name")]
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        [DisplayName("Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
