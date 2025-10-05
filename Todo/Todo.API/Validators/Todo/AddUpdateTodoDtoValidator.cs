using FluentValidation;
using Todo.BusinessLogic.Dtos.Todo;
using Todo.Utilities;

namespace Todo.API.Validators.Todo
{
    public class AddUpdateTodoDtoValidator : AbstractValidator<AddUpdateTodoDto>
    {
        public AddUpdateTodoDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(300).WithMessage("Title cannot exceed 300 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => (int)x.Status)
                .InclusiveBetween((int)TodoStatus.Pending, (int)TodoStatus.Archived)
                .WithMessage("Invalid status. Allowed values are: Pending = 0, InProgress = 1, Completed = 2, Archived = 3.");
        }
    }
}
