// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Application.Interfaces.Repositories;
using FantaAsta.Domain.Models;
using FantaAsta.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FantaAsta.Infrastructure.Repositories;

public class AuctionRepository : GenericRepository<AuctionEntity>, IAuctionRepository 
{
    public AuctionRepository(PostgreSqlContext postgreSqlContext) : base(postgreSqlContext)
    {
    }

    public AuctionEntity GetByName(string auctionName)
    {
        return PostgreSqlContext.Auctions.AsNoTracking().FirstOrDefault(x => x.Name.Equals(auctionName));
    }
}