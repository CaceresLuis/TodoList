using MediatR;
using System.Threading;
using TodoList.Mvc.Models;
using System.Threading.Tasks;
using TodoList.Mvc.Models.Entity;
using TodoList.Mvc.Core.TodoModule.Infrastructure.Repository;
using TodoList.Mvc.Core.Exepctions;
using System.Net;
using TodoList.Mvc.Core.Enums;

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
            if (todo.Title != todoR.Title)
            {
                if (await _todoRepository.GetTodo(todoR.Title) != null)
                    throw new ExceptionHandler(HttpStatusCode.BadRequest,
                        new Error
                        {
                            Code = "Error",
                            Message = $"There is already a task with the title {todoR.Title}",
                            Title = "Error",
                            State = State.error,
                            IsSuccess = false
                        });
            }

            todo.Title = todoR.Title ?? todo.Title;
            todo.Description = todoR.Description ?? todo.Description;
            todo.State = todoR.State ?? todo.State;

            return await _todoRepository.UpdateTodo(todo);
        }
    }
}
