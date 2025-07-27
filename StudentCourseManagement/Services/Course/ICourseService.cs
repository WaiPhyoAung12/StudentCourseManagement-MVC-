using StudentCourseManagement.Models.Courses;
using StudentCourseManagement.Models.Shared.ResultModel;

namespace StudentCourseManagement.Services.Course;

public interface ICourseService
{
    Task<Result<CourseModel>> CreateCourse(CourseRequestModel requestModel);
}
