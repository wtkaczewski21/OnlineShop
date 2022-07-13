using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lab_Backend.Data.ViewModels
{
    public class LoginVM
    {
        [DisplayName("Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
