// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Application.Interfaces.Repositories;
using FantaAsta.Domain.Models;
using FantaAsta.Infrastructure.Common;
using FantaAsta.Infrastructure.DbContexts;
using Fluently.Interfaces;

namespace FantaAsta.Infrastructure.Repositories;

public class BatchRepository : GenericRepository<BatchEntity>, IBatchRepository
{
    public BatchRepository(PostgreSqlContext postgreSqlContext, IFluentlyContext fluentlyContext) : base(postgreSqlContext, fluentlyContext)
    {
    }

    public IEnumerable<BatchEntity> GetAllByAuctionId(int auctionId)
    { 
        return FluentlyContext.Query<BatchEntity>(PostgreSqlContext.CreateCommand).Where(x => x.AuctionId == auctionId);
    }

    public BatchEntity GetByFootballerId(int footballerId)
    {
        return FluentlyContext.Query<BatchEntity>(PostgreSqlContext.CreateCommand).SingleOrDefault(x => x.FootballerId == footballerId);
    }
}
