using Access_Management_Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccessManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        // Show Login page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Process Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Temporary login for testing
            if (model.Username == "admin" && model.Password == "admin123")
            {
                HttpContext.Session.SetString("IsAdminLoggedIn", "true");

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(
                "",
                "Invalid username or password."
            );

            return View(model);
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Account");
        }
    }
}