using Access_Management_Application.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccessManagementSystem.Controllers
{
    public class AccessLogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccessLogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display all access logs
        public async Task<IActionResult> Index()
        {
            var accessLogs = await _context.AccessLogs
                .OrderByDescending(a => a.Date)
                .ThenByDescending(a => a.Time)
                .ToListAsync();

            return View(accessLogs);
        }
        public async Task<IActionResult> Attempt()
        {
            var users = await _context.Users
        .ToListAsync();

            ViewBag.Users = users;

            return View();
        }

        // Record access attempt
        [HttpPost]
        public async Task<IActionResult> Attempt(int userId)
        {
            string userName;
            string decision;

            if (userId == 0)
            {
                // Person is not registered
                userName = "Unknown Person";
                decision = "Denied";
            }
            else
            {
                var user = await _context.Users.FindAsync(userId);

                if (user == null)
                {
                    return NotFound();
                }

                userName = user.Name;

                if (user.Status == "Active")
                {
                    decision = "Granted";
                }
                else
                {
                    decision = "Denied";
                }
            }

            var accessLog = new Access_Management_Application.Models.AccessLog
            {
                UserName = userName,
                Date = DateTime.Now.Date,
                Time = DateTime.Now,
                Decision = decision
            };

            _context.AccessLogs.Add(accessLog);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}