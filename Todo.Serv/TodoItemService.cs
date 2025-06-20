using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Todo.Domain.DTOs;
using Todo.Domain.Entities;
using Todo.Domain.Entities.Identity;
using Todo.Domain.Services;
using Todo.Serv.Interfaces;
using TodoApi.DTOs;
using Microsoft.EntityFrameworkCore;


namespace Todo.Serv
{
    public class TodoItemService : ITodoItemService
    {


        private readonly ITodoItemRepository _repo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly UserManager<Appuser> _userManager;

        public TodoItemService(ITodoItemRepository repo, IHttpContextAccessor httpContextAccessor , IMapper mapper , UserManager<Appuser> userManager)
        {
            _repo = repo;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userManager = userManager;
        }

        //for sorting
        private IQueryable<TodoItem> ApplySorting(IQueryable<TodoItem> query, string? sortField, string? sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortField))
                return query;


            bool ascending = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase);

            return sortField.ToLower() switch
            {
                "title" => ascending ? query.OrderBy(t => t.Title) : query.OrderByDescending(t => t.Title),
                "duedate" => ascending ? query.OrderBy(t => t.DueDate) : query.OrderByDescending(t => t.DueDate),
                "iscompleted" => ascending ? query.OrderBy(t => t.IsCompleted) : query.OrderByDescending(t => t.IsCompleted),
                _ => query
            };
        }





        private string GetCurrentUserEmail()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
        }






        public async Task<List<TodoItemDto>> GetSortedItemsAsync(string userEmail, List<string> roles, string? sortField, string? sortDirection ,
            string? titleFilter,bool? isCompletedFilter,DateTime? dueDateBefore)
        {
            IQueryable<TodoItem> query;

            if (roles.Contains("Admin"))
            {
                query = await _repo.GetAllAsQueryable(); 
            }
            else
            {
                query = await _repo.GetAllForUserAsQueryable(userEmail);
            }

            //for filtration
            if (!string.IsNullOrWhiteSpace(titleFilter))
                query = query.Where(t => t.Title.Contains(titleFilter));

            if (isCompletedFilter.HasValue)
                query = query.Where(t => t.IsCompleted == isCompletedFilter.Value);

            if (dueDateBefore.HasValue)
                query = query.Where(t => t.DueDate < dueDateBefore.Value);


            //for impleminting sort 
            query = ApplySorting(query, sortField, sortDirection);

            var result = await query.ToListAsync();

            return _mapper.Map<List<TodoItemDto>>(result);
        }





        public async Task<TodoItem> GetByIdAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null || item.UserEmail != GetCurrentUserEmail())
                throw new UnauthorizedAccessException("You are not allowed to access this item.");
            return item;
        }





        public async Task<TodoItemDto> CreateAsync(CreateTodoItemDto dto, string userEmail)
        {
            var email = GetCurrentUserEmail();
            var user = await _userManager.FindByEmailAsync(email);
            if (string.IsNullOrEmpty(userEmail))
                throw new Exception("User email is null");

            var item = _mapper.Map<TodoItem>(dto);
            item.UserEmail = userEmail;
            item.CreatedAt = DateTime.UtcNow;

            await _repo.AddAsync(item);
            await _repo.SaveChangesAsync();

            return _mapper.Map<TodoItemDto>(item);
        }





        public async Task<TodoItemDto> UpdateAsync(UpdateTodoItemDto dto)
        {
            var existing = await _repo.GetByIdAsync(dto.Id);
            if (existing == null || existing.UserEmail != GetCurrentUserEmail())
                throw new UnauthorizedAccessException("You are not allowed to update this item.");

            _mapper.Map(dto, existing);

            var saved = await _repo.SaveChangesAsync();

            if (!saved)
                return null;


            return _mapper.Map<TodoItemDto>(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null || item.UserEmail != GetCurrentUserEmail())
                throw new UnauthorizedAccessException("You are not allowed to delete this item.");

            _repo.Delete(item);
            return await _repo.SaveChangesAsync();
        }

        
    }
}