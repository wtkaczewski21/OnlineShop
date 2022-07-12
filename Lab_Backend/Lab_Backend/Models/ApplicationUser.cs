using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace Lab_Backend.Models
{
    public class ApplicationUser : IdentityUser
    {
        [DisplayName("Full name")]
        public string FullName { get; set; }
    }
}
