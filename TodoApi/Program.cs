
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Todo.Domain.Entities.Identity;
using Todo.Repo.identity;
using TodoApi.Extensions;

namespace TodoApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpContextAccessor();

            // for extensions
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddTodoServices(builder.Configuration);







            var app = builder.Build();

            
            //seeding admin
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<Appuser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await AppIdentityDbContextSeed.SeedUserAsync(userManager, roleManager);
            }

            // Configure the HTTP request pipeline.


            // 3awez a3mlo
            app.UseExceptionHandler("/error");


            app.UseHttpsRedirection();
            //app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

           
           
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.MapControllers();

            app.Run();         
            
           

        }
    }
}
