using FluentValidation;
using TodoList.Mvc.Models;

namespace TodoList.Mvc.Core.Validations
{
    public class ConfigValidations
    {}

    public class TodoValidation : AbstractValidator<TodoViewModel>
    {
        public TodoValidation()
        {
            RuleFor(t => t.Title)
                .NotEmpty()
                .WithMessage("The Title is requiered");
            RuleFor(t => t.Description)
                .NotEmpty()
                .WithMessage("The Description is requiered");
            RuleFor(t => t.State)
                .NotEmpty()
                .WithMessage("The State is requiered");
        }
    }
}
