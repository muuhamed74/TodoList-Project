using AutoMapper;
using Todo.Domain.DTOs;
using Todo.Domain.Entities;
using TodoApi.DTOs;

namespace TodoApi.helpers
{
    public class MappingProfiles : Profile

    {
        public MappingProfiles()
        {
            CreateMap<CreateTodoItemDto, TodoItem>();
            CreateMap<TodoItem, TodoItemDto>();

            CreateMap<UpdateTodoItemDto, TodoItem>();
            
        }
    }
}
