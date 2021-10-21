using MediatR;
using AutoMapper;
using System.Net;
using System.Threading;
using TodoList.Mvc.Models;
using System.Threading.Tasks;
using TodoList.Mvc.Core.Enums;
using TodoList.Mvc.Models.Entity;
using TodoList.Mvc.Core.Exepctions;
using TodoList.Mvc.Core.TodoModule.Infrastructure.Repository;

namespace TodoList.Mvc.Core.TodoModule.Aplication.TodoService.Get
{
    public class GetTodoHandler : IRequestHandler<GetTodoQuery, TodoViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ITodoRepository _todoRepository;

        public GetTodoHandler(ITodoRepository todoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _todoRepository = todoRepository;
        }

        public async Task<TodoViewModel> Handle(GetTodoQuery request, CancellationToken cancellationToken)
        {
            Todo todo = await _todoRepository.GetTodo(request.Id);
            if(todo == null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "Task does exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            return _mapper.Map<TodoViewModel>(todo);
        }
    }
}
