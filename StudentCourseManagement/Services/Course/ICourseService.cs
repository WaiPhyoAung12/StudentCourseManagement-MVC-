using StudentCourseManagement.Models.Courses;
using StudentCourseManagement.Models.Shared.ResultModel;

namespace StudentCourseManagement.Services.Course;

public interface ICourseService
{
    Task<Result<CourseModel>> CreateCourse(CourseRequestModel requestModel);
    Task<Result<CourseListResponseModel>> GetCourse(CourseListPaginationModel courseListPagination);
    Task<Result<CourseModel>> GetCourseById(int id);
    Task<Result<CourseModel>> UpdateCourse(CourseRequestModel requestModel);
    Task<Result<CourseModel>> Delete(int id);
}
