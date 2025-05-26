using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LocalFarmerMarket.ViewModels.Home
{
    public class LoginVM
    {
        [DisplayName("Username: ")]
        [Required(ErrorMessage = "Username is Required!")]
        public string Username { get; set; }


        [DisplayName("Password: ")]
        [Required(ErrorMessage = "Password is Required!")]
        public string Password { get; set; }
    }
}
