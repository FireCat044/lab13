using lab13.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace lab13.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogDebug("This is a Debug log");
            _logger.LogInformation("This is a Information log");
            _logger.LogWarning("This is a Warning log");
            _logger.LogError("This is a Error log");
            _logger.LogCritical("This is a Critical log");

            var user = new { Name = "Andrew", Role = "Admin" };
            _logger.LogInformation("User {Name} with role {Role} logged in at {Time}", user.Name, user.Role, DateTime.Now);

            _logger.LogInformation("User details: {@User}", user);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
