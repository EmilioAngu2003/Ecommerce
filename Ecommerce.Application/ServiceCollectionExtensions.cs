﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ECommerce.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetProductsQueryHandler).Assembly));

        return services;
    }
}