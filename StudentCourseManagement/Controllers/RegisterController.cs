using Microsoft.AspNetCore.Mvc;

namespace StudentCourseManagement.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult StudentRegister()
        {
            return View();
        }
        public IActionResult AdminRegister()
        {
            return View();
        }
    }
}
