using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using StudentCourseManagement.Models.Batches;
using StudentCourseManagement.Models.Courses;
using StudentCourseManagement.Services.Batch;
using StudentCourseManagement.Services.Course;
using StudentCourseManagement.Services.Shared;
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

        [HttpGet]
        public async Task<IActionResult> GetBatchListPagination()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id is 0)
            {
                return NotFound();
            }
            var result = await _batchService.Delete(id);
            if (result.IsError)
                return NotFound();

            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> GetList()
        {
            var form = Request.Form;

            var pageSetting = form.GetPageSettingModel();
             BatchListPaginationModel requestModel = new();
            requestModel.PageSetting = pageSetting;

            var result = await _batchService.GetBatchListPagination(requestModel);

            if (result.IsError)
                return Json(new
                {
                    draw = pageSetting.Draw,
                });

            return Json(new
            {
                draw = pageSetting.Draw,
                recordsTotal = result.Data.TotalCount,       // total records without filter
                recordsFiltered = result.Data.BatchModels.Count,
                data = result.Data.BatchModels
            });
        }

        [HttpPost]
        public async Task<IActionResult>Edit(BatchResponseModel responseModel)
        {
            BatchRequestModel batchRequestModel = new();
            batchRequestModel = responseModel.Adapt<BatchRequestModel>();

            ValidationResult result = _validator.Validate(batchRequestModel);
            ModelState.Clear();
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View(responseModel);
            }

            var response = await _batchService.UpdateBatch(batchRequestModel);

            ViewBag.Message = response.Message;
            ViewBag.IsSuccess = response.IsSuccess;
            return View(responseModel);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _batchService.GetBatchById(id);
            if (response.IsError)
            {
                ViewBag.Message = response.Message;
                return View();
            }
            else
            {
                return View(response.Data);
            }
        }

    }
}
