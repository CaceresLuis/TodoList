using MediatR;
using AutoMapper;
using System.Threading;
using TodoList.Mvc.Models;
using System.Threading.Tasks;
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
            Models.Entity.Todo todo = await _todoRepository.GetTodo(request.Id);
            return _mapper.Map<TodoViewModel>(todo);
        }
    }
}
