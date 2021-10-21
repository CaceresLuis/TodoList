using System;
using MediatR;
using TodoList.Mvc.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Mvc.Models.Entity;
using TodoList.Mvc.Core.TodoModule.Aplication.TodoService.Get;
using TodoList.Mvc.Core.TodoModule.Aplication.TodoService.List;
using TodoList.Mvc.Core.TodoModule.Aplication.TodoService.Create;
using TodoList.Mvc.Core.TodoModule.Aplication.TodoService.Update;
using TodoList.Mvc.Core.TodoModule.Aplication.TodoService.Delete;

namespace TodoList.Mvc.Controllers
{
    public class TodosController : Controller
    {
        private readonly DataContext _context;

        private readonly IMediator _mediator;

        public TodosController(DataContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _mediator.Send(new ListTodoQuery()));
        }

        public async Task<ActionResult> Details(Guid id)
        {
            TodoViewModel todo = await _mediator.Send(new GetTodoQuery { Id = id });
            if (todo == null)
                return NotFound();

            return View(todo);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TodoViewModel todo)
        {
            await _mediator.Send(new CreateTodoCommand { Todo = todo });

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            TodoViewModel todo = await _mediator.Send(new GetTodoQuery { Id = id });
            if (todo == null)
                return NotFound();

            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, TodoViewModel todo)
        {
            todo.Id = id;
            await _mediator.Send(new UpdateTodoCommand { Todo = todo });

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            TodoViewModel todo = await _mediator.Send(new GetTodoQuery { Id = id });
            if (todo == null)
                return NotFound();

            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await _mediator.Send(new DeleteTodoCommand { Id = id });
            return RedirectToAction(nameof(Index));
        }
    }
}
