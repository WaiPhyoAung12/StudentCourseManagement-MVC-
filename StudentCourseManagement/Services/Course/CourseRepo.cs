using Mapster;
using StudentCourseManagement.AppDbContextModels;
using StudentCourseManagement.Constants;
using StudentCourseManagement.Models.Courses;
using StudentCourseManagement.Models.Shared.ResultModel;

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
}
