using AspNetCoreIdentityAndSecurityMvc.Models;
using AspNetCoreIdentityAndSecurityMvc.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreIdentityAndSecurityMvc.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        public IdentityController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> SignUp()
        {
            var model = new SignUpViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                if ((await _userManager.FindByEmailAsync(model.Email)) == null)
                {
                    var user = new IdentityUser()
                    {
                        Email = model.Email,
                        UserName = model.Email
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    user = await _userManager.FindByEmailAsync(user.Email);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    if (result.Succeeded)
                    {
                        var confirmationLink = Url.ActionLink("ConfirmEmail", "Identity", new
                        {
                            userId = user.Id,
                            @token = token,
                        });

                        if (confirmationLink != null)
                            await _emailSender.SendEmailAsync("mydatng@gmail.com", user.Email, "Confirm your email",
                                confirmationLink);
                        return RedirectToAction("SignIn");
                    }
                    ModelState.AddModelError("Sign Up", string.Join("", result.Errors.Select(x => x.Description)));
                    return View(model);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn");
            }

            return new NotFoundResult();
        }

        public async Task<IActionResult> SignIn()
        {
            var model = new SignInViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (!result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    result.
                }
            }
            else
            {

            }
            return View();
        }


        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
    }
}
