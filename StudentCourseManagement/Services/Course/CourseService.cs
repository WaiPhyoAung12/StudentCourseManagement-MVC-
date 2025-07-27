using StudentCourseManagement.Models.Courses;
using StudentCourseManagement.Models.Shared.ResultModel;
using System.Security.AccessControl;

namespace StudentCourseManagement.Services.Course;

public class CourseService : ICourseService
{
    private readonly CourseRepo _courseRepo;

    public CourseService(CourseRepo courseRepo)
    {
        _courseRepo = courseRepo;
    }
    public async Task<Result<CourseModel>> CreateCourse(CourseRequestModel requestModel)
    {
        var result=await _courseRepo.CreateCourse(requestModel);
        return result;
    }
}
