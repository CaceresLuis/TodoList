using System;
using MediatR;

namespace TodoList.Mvc.Core.TodoModule.Aplication.TodoService.Delete
{
    public class DeleteTodoCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
