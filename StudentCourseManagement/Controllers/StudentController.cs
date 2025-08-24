using Microsoft.AspNetCore.Mvc;

namespace StudentCourseManagement.Controllers;

public class StudentController : Controller
{
    public IActionResult GetStudentListByPagination()
    {
        return View();
    }
}
