using Access_Management_Application.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccessManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Check if administrator is logged in
            var isLoggedIn = HttpContext.Session.GetString("IsAdminLoggedIn");

            if (isLoggedIn != "true")
            {
                return RedirectToAction("Login", "Account");
            }

            // Get dashboard statistics
            var totalUsers = await _context.Users.CountAsync();

            var activeUsers = await _context.Users
                .CountAsync(u => u.Status == "Active");

            var inactiveUsers = await _context.Users
                .CountAsync(u => u.Status == "Inactive");

            var grantedAccess = await _context.AccessLogs
                .CountAsync(a => a.Decision == "Granted");

            var deniedAccess = await _context.AccessLogs
                .CountAsync(a => a.Decision == "Denied");

            // Get latest 5 access attempts
            var recentAccess = await _context.AccessLogs
                .OrderByDescending(a => a.Date)
                .ThenByDescending(a => a.Time)
                .Take(5)
                .ToListAsync();

            ViewBag.TotalUsers = totalUsers;
            ViewBag.ActiveUsers = activeUsers;
            ViewBag.InactiveUsers = inactiveUsers;
            ViewBag.GrantedAccess = grantedAccess;
            ViewBag.DeniedAccess = deniedAccess;
            ViewBag.RecentAccess = recentAccess;

            return View();
        }
    }
}