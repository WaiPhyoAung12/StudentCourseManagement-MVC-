using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentCourseManagement.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult DashboardPage()
        {
            return View();
        }
    }
}
