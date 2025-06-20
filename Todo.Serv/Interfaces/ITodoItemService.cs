using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.DTOs;
using Todo.Domain.Entities;
using TodoApi.DTOs;

namespace Todo.Serv.Interfaces
{
    public interface ITodoItemService
    {
        Task<List<TodoItemDto>> GetSortedItemsAsync(string userEmail, List<string> roles, string? sortField, string? sortDirection , string? titleFilter, bool? isCompletedFilter,DateTime? dueDateBefore);
        Task<TodoItem> GetByIdAsync(int id);
        Task<TodoItemDto> CreateAsync(CreateTodoItemDto dto , string userEmail);
        Task<TodoItemDto> UpdateAsync(UpdateTodoItemDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
