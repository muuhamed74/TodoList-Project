using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Services;

namespace Todo.Repo
{
    public class TodoItemRepository : ITodoItemRepository   
    {
        private readonly TodoDbContext _context;

        public TodoItemRepository(TodoDbContext context)
        {
            _context = context;
        }
        // for Admin to show al lists
        public async Task<IQueryable<TodoItem>> GetAllAsQueryable()
        {
            return _context.TodoItems.AsQueryable();
        }
        // for user to show his lists 
        public async Task<IQueryable<TodoItem>> GetAllForUserAsQueryable(string userEmail)
        {
            return _context.TodoItems
               .Where(item => item.UserEmail == userEmail)
               .AsQueryable();
        }

        public async Task<TodoItem> GetByIdAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task AddAsync(TodoItem item)
        {
            await _context.TodoItems.AddAsync(item);
        }

        public void Update(TodoItem item)
        {
            _context.TodoItems.Update(item);
        }

        public void Delete(TodoItem item)
        {
            _context.TodoItems.Remove(item);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

