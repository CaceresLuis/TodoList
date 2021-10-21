using MediatR;
using TodoList.Mvc.Models;

namespace TodoList.Mvc.Core.TodoModule.Aplication.TodoService.Create
{
    public class CreateTodoCommand : IRequest<bool>
    {
        public TodoViewModel Todo { get; set; }
    }
}
