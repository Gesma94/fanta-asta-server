// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Application.Interfaces.Repositories;
using FantaAsta.Domain.Models;
using FantaAsta.Infrastructure.Common;
using FantaAsta.Infrastructure.DbContexts;
using Fluently.Interfaces;

namespace FantaAsta.Infrastructure.Repositories;

public class FootballerRepository : GenericRepository<FootballerEntity>, IFootballerRepository
{
    public FootballerRepository(PostgreSqlContext postgreSqlContext, IFluentlyContext fluentlyContext) : base(postgreSqlContext, fluentlyContext)
    {
    }

    public IEnumerable<FootballerEntity> GetAllByAuctionId(int auctionId)
    {                
        return FluentlyContext.Query<FootballerEntity>(PostgreSqlContext.CreateCommand).Where(x => x.AuctionId == auctionId);
    }
}