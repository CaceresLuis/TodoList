using MediatR;
using System.Threading;
using TodoList.Mvc.Models;
using System.Threading.Tasks;
using TodoList.Mvc.Models.Entity;
using TodoList.Mvc.Core.TodoModule.Infrastructure.Repository;

namespace TodoList.Mvc.Core.TodoModule.Aplication.TodoService.Update
{
    public class UpdateTodoHandler : IRequestHandler<UpdateTodoCommand, bool>
    {
        private readonly ITodoRepository _todoRepository;

        public UpdateTodoHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<bool> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            TodoViewModel todoR = request.Todo;
            Todo todo = await _todoRepository.GetTodo(todoR.Id);

            todo.Title = todoR.Title ?? todo.Title;
            todo.Description = todoR.Description ?? todo.Description;
            todo.State = todoR.State ?? todo.State;

            return await _todoRepository.UpdateTodo(todo);
        }
    }
}
