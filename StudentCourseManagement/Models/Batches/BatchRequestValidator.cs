using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;

namespace StudentCourseManagement.Models.Batches;

public class BatchRequestValidator:AbstractValidator<BatchRequestModel>
{
    public BatchRequestValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEqual(0).WithMessage("Course is required!");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required!")
            .NotNull().WithMessage("Title is required!")
            .MaximumLength(50).WithMessage("Title cannot exceed more than 50 characters");

        RuleFor(x=>x.Description)
            .NotEmpty().WithMessage("Description is required!")
            .NotNull().WithMessage("Description is required!")
            .MaximumLength(50).WithMessage("Description cannot exceed more than 50 characters");

        RuleFor(x => x.CreditHour)
            .NotEqual(0).WithMessage("Credit hour is required!");

        RuleFor(x=>x.Capacity)
            .NotEqual(0).WithMessage("Capacity is required!");

        RuleFor(x => x.StartDate)
            .NotNull().WithMessage("Start date is required!");

    }
}
