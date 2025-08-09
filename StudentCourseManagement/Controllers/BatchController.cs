using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using StudentCourseManagement.Models.Batches;
using StudentCourseManagement.Services.Batch;
using StudentCourseManagement.Services.Course;
using System.Threading.Tasks;

namespace StudentCourseManagement.Controllers
{
    public class BatchController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IBatchService _batchService;
        private readonly IValidator<BatchRequestModel> _validator;

        public BatchController(ICourseService courseService,IBatchService batchService, IValidator<BatchRequestModel> validator)
        {
            _courseService = courseService;
            _batchService = batchService;
            _validator = validator;
        }
        [HttpGet]
        public IActionResult CreateBatch()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBatch(BatchRequestModel requestModel)
        {
            ValidationResult result = _validator.Validate(requestModel);
            ModelState.Clear();
            if (!result.IsValid) 
            { 
                result.AddToModelState(this.ModelState);
                return View(requestModel);
            }
            var response = await _batchService.CreateBatch(requestModel);
            ViewBag.Message = response.Message;
            ViewBag.IsSuccess = response.IsSuccess;
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetCourseList()
        {
            var response = await _courseService.GetCourseList();
            if (response.IsError)
                return null;

            return Json(response.Data);
        }
    }
}
