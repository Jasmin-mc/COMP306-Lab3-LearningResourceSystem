using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearningResourceSystem
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(
            [Required] string username,
            [Required, EmailAddress] string email,
            [Required, MinLength(8)] string password,
            [Required] string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "The passwords do not match.");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new IdentityUser
            {
                UserName = username.Trim(),
                Email = email.Trim()
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        error.Description);
                }

                return View();
            }

            TempData["SuccessMessage"] =
                "Your account was created. Please log in.";

            return RedirectToAction(nameof(Login));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            [Required, EmailAddress] string email,
            [Required] string password)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(email.Trim());

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    user,
                    password,
                    isPersistent: false,
                    lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(
                string.Empty,
                "Invalid login attempt.");

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return Content("You do not have permission to access this page.");
        }
    }
}