using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Todo.Domain.Entities.Identity;
using Todo.Domain.Services;
using Todo.Repo.identity;
using Todo.Serv;

namespace TodoApi.Extensions
{
    public static class IdentityServicesExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services , IConfiguration config) 
        {
            //DI fot JWT Token
            services.AddScoped<ITokenService, TokenService>();


            //DI for seeding users
            services.AddIdentity<Appuser, IdentityRole>()
                            .AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            
                
                .AddJwtBearer(Options =>
                 {
                     Options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ValidIssuer = config["JWT:Issuer"],
                         ValidAudience = config["JWT:Audience"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]))
                     };

                 });                                         
                       


           services.AddDbContext<AppIdentityDbContext>(Options =>
            {
                Options.UseSqlServer(config.GetConnectionString("IdentityConnection"));

            });

            return services;

        }
    }
}
