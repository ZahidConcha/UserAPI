using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI_UI.ViewModels
{
    public class RegistratioVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Password")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password", ErrorMessage ="Password and Confirmation Password don´t match")]
        public string PasswordConfirmation { get; set; }
    }
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Password")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class TokenResponse
    {
        public string Token { get; set; }
    }
}
