// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Application.Interfaces.Repositories;
using FantaAsta.Domain.Models;
using FantaAsta.Infrastructure.Common;
using FantaAsta.Infrastructure.DbContexts;
using Fluently.Interfaces;

namespace FantaAsta.Infrastructure.Repositories;

public class UserAuctionRepository : GenericRepository<UserAuctionEntity>, IUserAuctionRepository
{
    public UserAuctionRepository(PostgreSqlContext postgreSqlContext, IFluentlyContext fluentlyContext) : base(postgreSqlContext, fluentlyContext)
    {
    }

    public IEnumerable<UserAuctionEntity> GetAllByAuctionId(int auctionId)
    {
        return FluentlyContext.Query<UserAuctionEntity>(PostgreSqlContext.CreateCommand)
            .Where(x => x.AuctionId == auctionId);
    }
}