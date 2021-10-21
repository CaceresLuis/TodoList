using MediatR;
using TodoList.Mvc.Models;
using System.Collections.Generic;

namespace TodoList.Mvc.Core.TodoModule.Aplication.TodoService.List
{
    public class ListTodoQuery : IRequest<List<TodoViewModel>>
    {
    }
}
