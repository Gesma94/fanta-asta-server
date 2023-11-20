// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Application.Interfaces.Repositories;
using FantaAsta.Domain.Models;
using FantaAsta.Infrastructure.Common;
using FantaAsta.Infrastructure.DbContexts;
using Fluently.Interfaces;

namespace FantaAsta.Infrastructure.Repositories;

public class AuctionRepository : GenericRepository<AuctionEntity>, IAuctionRepository 
{
    public AuctionRepository(PostgreSqlContext postgreSqlContext, IFluentlyContext fluentlyContext) : base(postgreSqlContext, fluentlyContext)
    {
    }

    public AuctionEntity GetByName(string auctionName)
    {
        return FluentlyContext.Query<AuctionEntity>(PostgreSqlContext.CreateCommand)
            .SingleOrDefault(x => x.Name == auctionName);
    }
}