using Microsoft.AspNetCore.Mvc;

namespace LearningResourceSystem
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            //PLACEHOLDER ONLY
            // MUST UPDATE WITH SAMREETS AUTHENTICATION INFO

            ModelState.AddModelError(
                string.Empty,
                "Login is not connected yet, check again soon");

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register (
            string username,
            string email,
            string password,
            string confirmPassword)
        {
            if (password != confirmPassword)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "The passwords do not match.");

                return View();
            }
            //PLACEHOLDER ONNLLYYYY 
            //Reminder not to save passwords here or create accounts?

            ModelState.AddModelError(
                string.Empty,
                "Registration is not connected yet. WILL BE UPDATED SOON");

            return View();
        }
    }
}
