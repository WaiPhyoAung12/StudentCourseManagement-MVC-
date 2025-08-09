using Mapster;
using Microsoft.EntityFrameworkCore;
using StudentCourseManagement.AppDbContextModels;
using StudentCourseManagement.Constants;
using StudentCourseManagement.Models.Courses;
using StudentCourseManagement.Models.Shared.ResultModel;
using StudentCourseManagement.Services.Shared;
using System.Linq.Dynamic.Core;

namespace StudentCourseManagement.Services.Course;

public class CourseRepo
{
    private readonly AppDbContext _appDbContext;

    public CourseRepo(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Result<CourseModel>>CreateCourse(CourseRequestModel requestModel)
    {
        CourseRequestValidator validator=new CourseRequestValidator();
        var validationResult=validator.Validate(requestModel);
        if (!validationResult.IsValid)
            return Result<CourseModel>.Fail(validationResult.Errors.FirstOrDefault().ErrorMessage);

        TblCourse course=new TblCourse();
        course = requestModel.Adapt<TblCourse>();
        course.CreatedUserId = "testinguser";
        course.CreatedDateTime=DateTime.Now;
        await _appDbContext.TblCourses.AddAsync(course);
        var result = _appDbContext.SaveChangesAsync();

        return await result <= 0 
            ? Result<CourseModel>.Fail(ConstantMessages.ErrorCreate) 
            : Result<CourseModel>.Success(ConstantMessages.SuccessCreate);
    }

    public async Task<Result<CourseListResponseModel>> GetCourse(CourseListPaginationModel courseListPagination)
    {     
        var pageSettingModel=courseListPagination.PageSetting;
        var searchItem = pageSettingModel.SearchTerm;

        var query =_appDbContext.TblCourses.Where(x => x.DeleteFlag == false);

        if (!string.IsNullOrEmpty(searchItem))
        {
            query = query.Where(x => x.DeleteFlag!=true && x.CourseTitle.Contains(searchItem)
        || x.CourseCode.Contains(searchItem)
        || x.CourseDescription.Contains(searchItem));
        }
        int totalCount=query.ToList().Count;
        query = query.GetPagination(pageSettingModel);

        var datalist = query.ToList();

        if (datalist.Count <= 0)
            return Result<CourseListResponseModel>.Fail(ConstantMessages.DataNotFound);

        CourseListResponseModel courseList = new();
        courseList.CourseModels = datalist.Adapt<List<CourseModel>>();
        courseList.TotalCount= totalCount;
        return Result<CourseListResponseModel>.Success(ConstantMessages.Success,courseList);
    }

    public async Task<Result<CourseModel>>GetCourseById(int id)
    {
        if(id is 0)
            return Result<CourseModel>.Fail(ConstantMessages.DataNotFound);

        var result=await _appDbContext.TblCourses.FirstOrDefaultAsync(x=>x.Id==id && x.DeleteFlag==false);

        if (result is null)
            return Result<CourseModel>.Fail(ConstantMessages.DataNotFound);

        CourseModel courseModel = result.Adapt<CourseModel>();

        return Result<CourseModel>.Success(ConstantMessages.Success,courseModel);
    }

    public async Task<Result<CourseModel>>UpdateCourse(CourseRequestModel requestModel)
    {
        var result = await _appDbContext.TblCourses
            .Where(x=>x.Id==requestModel.Id)
            .ExecuteUpdateAsync(x => x
            .SetProperty(c => c.CourseCode, requestModel.CourseCode)
            .SetProperty(c => c.CourseDescription, requestModel.CourseDescription)
            .SetProperty(c => c.CourseTitle, requestModel.CourseTitle)
            .SetProperty(c => c.UpdatedDateTime, DateTime.Now)
            .SetProperty(c => c.UpdatedUserId, "Testing")
            );

        if(result<=0)
            return Result<CourseModel>.Fail(ConstantMessages.ErrorUpdate);

        return Result<CourseModel>.Success(ConstantMessages.SuccessUpdate);
    }

    public async Task<Result<CourseModel>>Delete(int id)
    {
        if (id is 0)
            return Result<CourseModel>.Fail(ConstantMessages.DataNotFound);

        var result = await _appDbContext.TblCourses.Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => x
            .SetProperty(c => c.DeleteFlag, true)
            .SetProperty(c => c.UpdatedDateTime, DateTime.Now)
            .SetProperty(c => c.UpdatedUserId, "Testing")
            );

        if (result <= 0)
            return Result<CourseModel>.Fail(ConstantMessages.ErrorUpdate);

        return Result<CourseModel>.Success(ConstantMessages.SuccessUpdate);
    }

    public async Task<Result<List<CourseModel>>> GetCourseList()
    {
        var courseListModel = await _appDbContext.TblCourses
                        .Where(x => x.DeleteFlag == false)
                        .ToListAsync();

        if (courseListModel.Count <= 0)
            return Result<List<CourseModel>>.Fail(ConstantMessages.DataNotFound);

        List<CourseModel> courseList = new();
        courseList = courseListModel.Adapt<List<CourseModel>>();

        return Result<List<CourseModel>>.Success(ConstantMessages.Success,courseList);
    }
}
