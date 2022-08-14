using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityAndSecurityMvc.Models
{
    public class SignUpViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email address is invalid")]
        public string Email{ get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "Incorrect or missing password")]
        public string Password{ get; set; }


    }
}
