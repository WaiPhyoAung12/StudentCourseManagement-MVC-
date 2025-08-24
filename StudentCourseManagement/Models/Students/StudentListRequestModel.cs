using StudentCourseManagement.Models.Shared.PageSetting;

namespace StudentCourseManagement.Models.Students
{
    public class StudentListRequestModel
    {
        public PageSettingModel PageSetting { get; set; }
        public bool IsEnrolledStudent { get; set; }
    }
}
