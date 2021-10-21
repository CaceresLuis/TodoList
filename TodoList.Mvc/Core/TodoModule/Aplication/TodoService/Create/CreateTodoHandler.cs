using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using TodoList.Mvc.Models.Entity;
using TodoList.Mvc.Core.TodoModule.Infrastructure.Repository;
using TodoList.Mvc.Core.Exepctions;
using System.Net;
using TodoList.Mvc.Core.Enums;

namespace TodoList.Mvc.Core.TodoModule.Aplication.TodoService.Create
{
    public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly ITodoRepository _todoRepository;

        public CreateTodoHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _todoRepository = todoRepository;
        }

        public async Task<bool> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            if(await _todoRepository.GetTodo(request.Todo.Title) != null)
            throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = $"There is already a task with the title {request.Todo.Title}",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            Todo todo = _mapper.Map<Todo>(request.Todo);
            return await _todoRepository.AddTodo(todo);
        }
    }
}
