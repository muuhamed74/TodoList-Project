using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entities;

namespace Todo.Domain.Services
{
    public interface ITodoItemRepository
    {
        // for Admin to show al lists
        Task<IQueryable<TodoItem>> GetAllAsQueryable();
        // for user to show specific list 
        Task<IQueryable<TodoItem>> GetAllForUserAsQueryable(string userEmail);
        Task<TodoItem> GetByIdAsync(int id);
        Task AddAsync(TodoItem item);
        void Update(TodoItem item);
        void Delete(TodoItem item);
        Task<bool> SaveChangesAsync();
    }
}
