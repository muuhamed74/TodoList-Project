using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities.Identity;
using Todo.Domain.Services;
using Todo.Repo;
using Todo.Repo.identity;
using Todo.Serv;
using Todo.Serv.Interfaces;
using TodoApi.helpers;

namespace TodoApi.Extensions
{
    public static class TodoServiceExtentions
    {
        public static IServiceCollection AddTodoServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<TodoDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<ITodoItemRepository, TodoItemRepository>();
            services.AddScoped<ITodoItemService, TodoItemService>();

            services.AddAutoMapper(typeof(MappingProfiles));

            

            return services;
        }

    }
}
