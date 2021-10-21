using MediatR;
using TodoList.Mvc.Models;

namespace TodoList.Mvc.Core.TodoModule.Aplication.TodoService.Update
{
    public class UpdateTodoCommand : IRequest<bool>
    {
        public TodoViewModel Todo { get; set; }
    }
}
