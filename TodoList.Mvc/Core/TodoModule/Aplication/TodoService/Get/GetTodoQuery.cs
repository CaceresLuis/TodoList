using System;
using MediatR;
using TodoList.Mvc.Models;

namespace TodoList.Mvc.Core.TodoModule.Aplication.TodoService.Get
{
    public class GetTodoQuery : IRequest<TodoViewModel>
    {
        public Guid Id { get; set; }
    }
}
