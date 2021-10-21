using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TodoList.Mvc.Models.Entity;

namespace TodoList.Mvc.Core.TodoModule.Infrastructure.Repository
{
    public interface ITodoRepository
    {
        Task<bool> AddTodo(Todo todo);
        Task<bool> DeleteTodo(Todo todo);
        Task<Todo> GetTodo(Guid id);
        Task<List<Todo>> GetTodoContain(string text);
        Task<List<Todo>> GetTodos();
        Task<bool> UpdateTodo(Todo todo);
    }
}