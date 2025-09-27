using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Data.Repositories;
using ECommerce.Infrastructure.Services;
using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Services;
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

        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
