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

    public async Task<Result<CourseModel>> Delete(int id)
    {
        var result=await _courseRepo.Delete(id);
        return result;
    }

    public async Task<Result<CourseListResponseModel>> GetCourse(CourseListPaginationModel courseListPagination)
    {
        var result = await _courseRepo.GetCourse(courseListPagination);
        return result;
    }

    public async Task<Result<CourseModel>> GetCourseById(int id)
    {
        var result=await _courseRepo.GetCourseById(id);
        return result;
    }

    public async Task<Result<CourseModel>> UpdateCourse(CourseRequestModel requestModel)
    {
        var result=await _courseRepo.UpdateCourse(requestModel);
        return result;
    }
}
