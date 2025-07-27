using FluentValidation;

namespace StudentCourseManagement.Models.Courses;

public class CourseRequestValidator:AbstractValidator<CourseRequestModel>
{
    public CourseRequestValidator()
    {
        RuleFor(x => x.CourseCode)
            .NotEmpty().WithMessage("Course Code is required!")
            .NotNull().WithMessage("Course Code is required!");


        RuleFor(x => x.CourseTitle)
            .NotEmpty().WithMessage("Course Title is required!")
            .NotNull().WithMessage("Course Title is required!");

        RuleFor(x => x.CourseDescription)
            .NotEmpty().WithMessage("Course Description is required!")
            .NotNull().WithMessage("Course Description is required!");
    }
}
