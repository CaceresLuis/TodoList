using System;
using MediatR;
using TodoList.Mvc.Models;
using System.Threading.Tasks;
using TodoList.Mvc.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using TodoList.Mvc.Models.Entity;
using TodoList.Mvc.Core.Exepctions;
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
            try
            {
                TodoViewModel todo = await _mediator.Send(new GetTodoQuery { Id = id });
                return View(todo);
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error.ToString();
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TodoViewModel todo)
        {
            try
            {
                await _mediator.Send(new CreateTodoCommand { Todo = todo });
                TempData["Title"] = "Created";
                TempData["Message"] = $"The task {todo.Title} was created";
                TempData["State"] = State.success.ToString();

                return RedirectToAction(nameof(Index));
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error.ToString();
                return View();
            }
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                TodoViewModel todo = await _mediator.Send(new GetTodoQuery { Id = id });
                return View(todo);
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error.ToString();
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, TodoViewModel todo)
        {
            todo.Id = id;
            try
            {
                await _mediator.Send(new UpdateTodoCommand { Todo = todo });
                TempData["Title"] = "Updatede";
                TempData["Message"] = $"The task {todo.Title} was updated";
                TempData["State"] = State.success.ToString();

                return RedirectToAction(nameof(Index));
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error.ToString();
                return View(todo);
            }
            
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteTodoCommand { Id = id });
                TempData["Title"] = "Deleted";
                TempData["Message"] = "The task was deleted";
                TempData["State"] = State.success.ToString();

                return RedirectToAction(nameof(Index));
            }
            catch (ExceptionHandler e)
            {
                TempData["Title"] = e.Error.Title;
                TempData["Message"] = e.Error.Message;
                TempData["State"] = State.error.ToString();
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
