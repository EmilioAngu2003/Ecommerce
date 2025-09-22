using Ecommerce.Core.Interfaces;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<ECommerceDbContext>(options =>
        {
            string stringConnection = configuration.GetConnectionString("DockerConnection");
            options.UseSqlServer(stringConnection);
        });

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ECommerceDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}
