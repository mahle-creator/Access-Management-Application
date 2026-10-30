using Access_Management_Application.Data;
using Access_Management_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccessManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display all users
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();

            return View(users);
        }

        // Show registration page
        public IActionResult Register()
        {
            return View();
        }

        // Register User
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Check if the User ID already exists
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserId == user.UserId);

                if (existingUser != null)
                {
                    ModelState.AddModelError("UserId", "This user is already registered.");
                    return View(user);
                }

                // Check if the same name already exists
                var existingName = await _context.Users
                    .FirstOrDefaultAsync(u => u.Name == user.Name);

                if (existingName != null)
                {
                    ModelState.AddModelError("Name", "This user is already registered.");
                    return View(user);
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(user);
        }

        // Edit User
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // Save edited User
        [HttpPost]
        public async Task<IActionResult> Edit(User updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedUser);
            }

            var user = await _context.Users.FindAsync(updatedUser.UserId);

            if (user == null)
            {
                return NotFound();
            }

            user.Name = updatedUser.Name;
            user.FaceId = updatedUser.FaceId;
            user.FacialData = updatedUser.FacialData;
            user.Status = updatedUser.Status;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Deactivate User
        public async Task<IActionResult> Deactivate(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Status = "Inactive";

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Remove User
        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}