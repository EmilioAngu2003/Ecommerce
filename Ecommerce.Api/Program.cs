using Ecommerce.Core.Interfaces;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ECommerceDbContext>(options =>
            {
                string stringConnection = builder.Configuration.GetConnectionString("DockerConnection");
                options.UseSqlServer(stringConnection);
            });

            // Registrar el repositorio
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Add services to the container.

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
