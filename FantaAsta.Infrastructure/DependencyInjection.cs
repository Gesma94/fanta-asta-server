// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Application.Interfaces.Repositories;
using FantaAsta.Infrastructure.DbContexts;
using FantaAsta.Infrastructure.Options;
using FantaAsta.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FantaAsta.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddOptions<PostgreSqlOptions>();
        services.AddDbContext<PostgreSqlContext>();

        services.AddTransient<IAuctionRepository, AuctionRepository>();
        services.AddTransient<IBatchRepository, BatchRepository>();
        services.AddTransient<IFootballerRepository, FootballerRepository>();
        services.AddTransient<IFootballerUserRepository, FootballerUserRepository>();
        services.AddTransient<IOfferRepository, OfferRepository>();
        
        return services;
    }
}