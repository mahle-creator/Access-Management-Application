using Access_Management_Application.Data;
using Access_Management_Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccessManagementSystem.Controllers
{
    public class FaceRecognitionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FaceRecognitionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Show face recognition page
        public IActionResult Index()
        {
            return View();
        }

        // Show API test page
        public IActionResult ApiTest()
        {
            return View();
        }

        // Process Face ID
        [HttpPost]
        public async Task<IActionResult> Scan(string faceId)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.FaceId == faceId);

            string userName;
            string decision;
            string reason;

            if (user == null)
            {
                userName = "Unknown Person";
                decision = "Denied";
                reason = "Face not registered";
            }
            else if (user.Status != "Active")
            {
                userName = user.Name;
                decision = "Denied";
                reason = "User is inactive";
            }
            else
            {
                userName = user.Name;
                decision = "Granted";
                reason = "User is registered and active";
            }

            // Record access attempt
            var accessLog = new AccessLog
            {
                UserName = userName,
                Date = DateTime.Now.Date,
                Time = DateTime.Now,
                Decision = decision
            };

            _context.AccessLogs.Add(accessLog);

            await _context.SaveChangesAsync();

            ViewBag.UserName = userName;
            ViewBag.FaceId = faceId;
            ViewBag.Decision = decision;
            ViewBag.Reason = reason;

            return View("Result");
        }
    }
}