using System;

namespace TodoList.Mvc.Models
{
    public class TodoViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
    }
}
