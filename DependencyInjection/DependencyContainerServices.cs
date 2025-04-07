using ContactList.Application.Contracts.Persistance;
using ContactList.Infrastructure.Persistance;
using ContactList.Infrastructure.Persistance.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.DependencyInjection
{
    public static class DependencyContainerServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //EF Core db context dependency
            services.AddDbContext<EFDbContext>(opt =>
               opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
              );

            //dapper context dependency
            services.AddScoped<DapperDbContext>();

            //set up jwt authenticatoin setting
            var JWTSetting = configuration.GetSection("JwtSettings");
            var SecretKey = Encoding.UTF8.GetBytes(JWTSetting["SecretKey"]);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(options =>
                  {
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuer = true,
                          ValidateAudience = true,
                          ValidateLifetime = true,
                          ValidIssuer = JWTSetting["Issuer"],
                          ValidAudience = JWTSetting["Audience"],
                          IssuerSigningKey = new SymmetricSecurityKey(SecretKey)
                      };

                      options.Events = new JwtBearerEvents
                      {
                          OnChallenge = context =>
                          {
                              context.Response.StatusCode = 401; // Unauthorized
                              context.Response.ContentType = "application/json";
                              return Task.CompletedTask;
                          }
                      };
                  });


            //repository services dependencies
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();

            return services;
        }
    }
}
