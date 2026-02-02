
using Microsoft.EntityFrameworkCore;
using apisApp.Data;
using Microsoft.AspNetCore.Builder;

namespace apisApp
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
            builder.Services.AddDbContext<apisApp.Data.MyDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaltConnection")));
            builder.Services.AddScoped<apisApp.Services.Interfaces.IUserService, apisApp.Services.UserService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(options =>
                {
                    options.RouteTemplate = "docs/{documentName}/swagger.json";
                });
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/docs/v1/swagger.json", "User API v1");
                    options.RoutePrefix = "docs";
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
