using MediatR;
using AutoMapper;
using System.Threading;
using TodoList.Mvc.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using TodoList.Mvc.Models.Entity;
using TodoList.Mvc.Core.TodoModule.Infrastructure.Repository;

namespace TodoList.Mvc.Core.TodoModule.Aplication.TodoService.List
{
    public class ListTodoHandler : IRequestHandler<ListTodoQuery, List<TodoViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ITodoRepository _todoRepository;

        public ListTodoHandler(IMapper mapper, ITodoRepository todoRepository)
        {
            _mapper = mapper;
            _todoRepository = todoRepository;
        }

        public async Task<List<TodoViewModel>> Handle(ListTodoQuery request, CancellationToken cancellationToken)
        {
            List<Todo> todos = await _todoRepository.GetTodos();

            return _mapper.Map<List<TodoViewModel>>(todos);
        }
    }
}
