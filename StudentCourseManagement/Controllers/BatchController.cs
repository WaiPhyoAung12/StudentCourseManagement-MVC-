using Microsoft.AspNetCore.Mvc;

namespace StudentCourseManagement.Controllers
{
    public class BatchController : Controller
    {
        public IActionResult CreateBatch()
        {
            return View();
        }
    }
}
