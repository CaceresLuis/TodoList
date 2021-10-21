using AutoMapper;
using TodoList.Mvc.Models;
using TodoList.Mvc.Models.Entity;

namespace TodoList.Mvc.Core.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Todo, TodoViewModel>().ReverseMap();
        } 
    }
}
