using System;

namespace TodoList.Mvc.Models.Entity
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string State { get; set; }
        public string Description { get; set; }
    }
}
