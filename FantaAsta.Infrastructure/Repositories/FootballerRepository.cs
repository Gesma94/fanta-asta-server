// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Application.Interfaces.Repositories;
using FantaAsta.Domain.Models;
using FantaAsta.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FantaAsta.Infrastructure.Repositories;

public class FootballerRepository : GenericRepository<FootballerEntity>, IFootballerRepository
{
    public FootballerRepository(PostgreSqlContext postgreSqlContext) : base(postgreSqlContext)
    {
    }

    public IQueryable<FootballerEntity> GetAllByAuctionId(int auctionId)
    {
        return PostgreSqlContext.Footballers.AsNoTracking().Where(x => x.AuctionId.Equals(auctionId));
    }
}