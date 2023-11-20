// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using System.Reflection;
using FantaAsta.Application.Interfaces.Common;
using FantaAsta.Application.Interfaces.Repositories;
using FantaAsta.Infrastructure.Common;
using FantaAsta.Infrastructure.DbContexts;
using FantaAsta.Infrastructure.Options;
using FantaAsta.Infrastructure.Repositories;
using Fluently;
using Microsoft.Extensions.DependencyInjection;

namespace FantaAsta.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<PostgreSqlContext>();
        services.AddOptions<PostgreSqlOptions>();

        services.AddFluently(Assembly.GetExecutingAssembly());
        
        services.AddTransient<IAuctionRepository, AuctionRepository>();
        services.AddTransient<IBatchRepository, BatchRepository>();
        services.AddTransient<IFootballerRepository, FootballerRepository>();
        services.AddTransient<IFootballerUserRepository, FootballerUserRepository>();
        services.AddTransient<IOfferRepository, OfferRepository>();
        services.AddTransient<IUserAuctionRepository, UserAuctionRepository>();
        services.AddTransient<IUserRecoveryGuidRepository, UserRecoveryGuidRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }
}