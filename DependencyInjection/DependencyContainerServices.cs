using ContactList.Application.Contracts.Persistance;
using ContactList.Infrastructure.Persistance;
using ContactList.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            //repository services dependencies
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();

            return services;
        }
    }
}
