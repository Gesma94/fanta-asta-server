// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Application.Interfaces.Repositories;
using FantaAsta.Domain.Models;
using FantaAsta.Infrastructure.Common;
using FantaAsta.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FantaAsta.Infrastructure.Repositories;

public class BatchRepository : GenericRepository<BatchEntity>, IBatchRepository
{
    public BatchRepository(PostgreSqlContext postgreSqlContext) : base(postgreSqlContext)
    {
    }

    public IQueryable<BatchEntity> GetAllByAuctionId(int auctionId)
    {
        return PostgreSqlContext.Batches.AsNoTracking().Where(x => x.AuctionId.Equals(auctionId));
    }

    public BatchEntity GetByFootballerId(int footballerId)
    {
        return PostgreSqlContext.Batches.AsNoTracking().FirstOrDefault(x => x.FootballerId.Equals(footballerId));
    }
}
