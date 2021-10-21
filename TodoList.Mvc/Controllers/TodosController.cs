using System;
using MediatR;
using System.Linq;
using TodoList.Mvc.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Mvc.Models.Entity;
using Microsoft.EntityFrameworkCore;
using TodoList.Mvc.Core.TodoModule.Aplication.TodoService.Create;
using TodoList.Mvc.Core.TodoModule.Aplication.TodoService.List;

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

        public async Task<IActionResult> Details(Guid id)
        {
            Todo todo = await _context.Todos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
                return NotFound();

            return View(todo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoViewModel todo)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new CreateTodoCommand { Todo = todo });
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            Todo todo = await _context.Todos.FindAsync(id);
            if (todo == null)
                return NotFound();

            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Todo todo)
        {
            todo.Id = id;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoExists(todo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            Todo todo = await _context.Todos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
                return NotFound();

            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            Todo todo = await _context.Todos.FindAsync(id);
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoExists(Guid id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }
    }
}
