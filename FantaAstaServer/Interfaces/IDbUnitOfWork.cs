// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace FantaAstaServer.Interfaces
{
    public interface IDbUnitOfWork
    {
        IAuctionRepository Auctions { get; }
        IUserRepository Users { get; }
        IBatchRepository Batches { get; }
        IFootballerRepository Footballers { get; }
        IOfferRepository Offers { get; }
        IUserAuctionRepository UserAuctions { get; }
        IDbContextTransaction BeginTransaction();
        Task<int> SaveChanges();
    }
}
