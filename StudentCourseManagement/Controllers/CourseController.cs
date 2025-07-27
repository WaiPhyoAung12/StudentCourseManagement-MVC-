using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using StudentCourseManagement.Models.Courses;
using StudentCourseManagement.Services.Course;
using System;
using System.Threading.Tasks;

namespace StudentCourseManagement.Controllers
{
    public class CourseController : Controller
    {
        private readonly IValidator<CourseRequestModel> _validator;
        private readonly ICourseService _courseService;

        public CourseController(IValidator<CourseRequestModel> validator,ICourseService courseService)
        {
            _validator = validator;
            _courseService = courseService;
        }
        [HttpGet]
        public IActionResult ViewDetails()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateCourse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseRequestModel requestModel)
        {
            ValidationResult result = _validator.Validate(requestModel);
            ModelState.Clear();
            if (!result.IsValid)
            {                
                result.AddToModelState(this.ModelState);
                return View(requestModel);
            }
            var response=await _courseService.CreateCourse(requestModel);
            ViewBag.Message = response.Message;
            ViewBag.IsSuccess=response.IsSuccess;
            return View(requestModel);
        }
    }
}
