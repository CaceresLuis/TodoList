using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TodoList.Mvc.Core.Enums;
using TodoList.Mvc.Models.Entity;
using TodoList.Mvc.Core.Exepctions;
using TodoList.Mvc.Core.TodoModule.Infrastructure.Repository;

namespace TodoList.Mvc.Core.TodoModule.Aplication.TodoService.Delete
{
    public class DeleteTodoHandler : IRequestHandler<DeleteTodoCommand, bool>
    {
        private readonly ITodoRepository _todoRepository;

        public DeleteTodoHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            Todo todo = await _todoRepository.GetTodo(request.Id);
            if (todo == null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "Task does exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            return await _todoRepository.DeleteTodo(todo);
        }
    }
}
