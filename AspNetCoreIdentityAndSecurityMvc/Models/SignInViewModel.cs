using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityAndSecurityMvc.Models
{
    public class SignInViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }


        public bool RememberMe { get; set; }
    }
}
