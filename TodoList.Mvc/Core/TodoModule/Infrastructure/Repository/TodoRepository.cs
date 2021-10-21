using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TodoList.Mvc.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace TodoList.Mvc.Core.TodoModule.Infrastructure.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DataContext _dataContext;

        public TodoRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Todo>> GetTodos()
        {
            return await _dataContext.Todos.ToListAsync();
        }

        public async Task<bool> AddTodo(Todo todo)
        {
            _dataContext.Todos.Add(todo);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<Todo> GetTodo(int id)
        {
            return await _dataContext.Todos.FindAsync(id);
        }

        public async Task<List<Todo>> GetTodoContain(string text)
        {
            return await _dataContext.Todos.Where(t => t.Title.Contains(text)).ToListAsync();
        }

        public async Task<bool> UpdateTodo(Todo todo)
        {
            _dataContext.Todos.Update(todo);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTodo(Todo todo)
        {
            _dataContext.Todos.Remove(todo);
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
