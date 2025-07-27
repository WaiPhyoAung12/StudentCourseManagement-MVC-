using Microsoft.AspNetCore.Mvc;

namespace StudentCourseManagement.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult LoginPage()
        {
            return View();
        }
    }
}
