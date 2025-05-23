
using ContactList.Application;
using ContactList.DependencyInjection;
using ContactList.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;


namespace ContactList.Peresentation
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add infrastructure services to the container.
            builder.Services.AddInfrastructureServices(builder.Configuration);

            //add mediatR dependency
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly));
            //add dependency to AutoMapper
            builder.Services.AddAutoMapper(typeof(ApplicationAssemblyReference).Assembly);


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //automatic jobs during running app for the first time
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                //Auto create database if not exist
                var dbContext = services.GetRequiredService<EFDbContext>();
                await dbContext.Database.MigrateAsync();
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            await app.RunAsync();
        }
    }
}
