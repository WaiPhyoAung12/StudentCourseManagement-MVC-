using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using StudentCourseManagement.Models.Courses;
using StudentCourseManagement.Models.Shared.PageSetting;
using StudentCourseManagement.Services.Course;
using System;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        [HttpGet]
        public async Task<IActionResult> GetCourseList()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetList()
        {
            var form = Request.Form;
            int draw = Convert.ToInt32(form["draw"]);

            int pageSize = Convert.ToInt32(form["length"]);
            int skip = Convert.ToInt32(form["start"]);
            string? searchValue = form["search[value]"];

            string sortColumn = form[$"columns[{form["order[0][column]"]}][data]"];
            string sortDirection = form["order[0][dir]"];

            var pageSetting = new PageSettingModel
            {
                PageNumber = (skip / pageSize) + 1,
                PageSize = pageSize,
                SearchTerm = searchValue,
                SortColumn = sortColumn,
                SortDirection = sortDirection
            };

            CourseListPaginationModel requestModel = new();
            requestModel.PageSetting = pageSetting;

            var result = await _courseService.GetCourse(requestModel);

            if (result.IsError)
                return Json(new
                {
                    draw = draw
                });

            return Json(new
            {
                draw = draw,
                recordsTotal = result.Data.TotalCount,       // total records without filter
                recordsFiltered = result.Data.CourseModels.Count,
                data = result.Data.CourseModels
            });
        }

        [HttpGet]
        public async Task<IActionResult>Edit(int id)
        {
            var response=await _courseService.GetCourseById(id);
            if(response.IsError)
            {
                ViewBag.Message = response.Message;
                return View();
            }
            else
            {
                return View(response.Data);
            }
        }

        [HttpGet]
        public async Task<IActionResult>Delete(int id)
        {
            if(id is 0)
            {
                return NotFound();
            }
            var result=await _courseService.Delete(id);
            if (result.IsError)
                return NotFound();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CourseModel courseModel)
        {
            CourseRequestModel courseRequestModel = new()
            {
                CourseCode = courseModel.CourseCode,
                CourseDescription = courseModel.CourseDescription,
                CourseTitle = courseModel.CourseTitle,
                Id = courseModel.Id,
            };
            ValidationResult result = _validator.Validate(courseRequestModel);
            ModelState.Clear();
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View(courseModel);
            }

            var response=await _courseService.UpdateCourse(courseRequestModel);

            ViewBag.Message = response.Message;
            ViewBag.IsSuccess = response.IsSuccess;
            return View(courseModel);
        }
    }
}
