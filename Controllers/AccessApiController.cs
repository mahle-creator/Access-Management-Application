using Access_Management_Application.Data;
using Access_Management_Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccessManagementSystem.Controllers
{
    [ApiController]
    [Route("api/access")]
    public class AccessApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccessApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyFace([FromBody] FaceRequest request)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.FaceId == request.FaceId);

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

            return Ok(new
            {
                userName = userName,
                faceId = request.FaceId,
                decision = decision,
                reason = reason
            });
        }
    }

    public class FaceRequest
    {
        public string FaceId { get; set; } = "";
    }
}