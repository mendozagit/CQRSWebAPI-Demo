using CQRSWebAPI_Demo.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using CQRSWebAPI_Demo.PipelineBehaviours;
using FluentValidation;

namespace CQRSWebAPI_Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(
                    @"Server=.\SQLEXPRESS;Database=Corsdb;Trusted_Connection=True;",
                    b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

            //Add  FluentValidatios from  Assembly
            //This essentialy registers all the validators that are available within the Assembly
            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));


            // Add  MediatR from  Assembly
            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}