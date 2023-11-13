// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using System.Data;
using FantaAsta.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace FantaAsta.Application.Interfaces.Common;

public interface IUnitOfWork : IDisposable
{
    IAuctionRepository Auctions { get; }
    IBatchRepository Batches { get; }
    IFootballerRepository Footballers { get; }
    IFootballerUserRepository FootballerUsers { get; }
    IOfferRepository Offers { get; }
    IUserAuctionRepository UserAuctions { get; }
    IUserRecoveryGuidRepository UserRecoveryGuids { get; }
    IUserRepository Users { get; }
    IDbTransaction BeginTransaction();
    void RollbackTransaction();
    void Commit();
}
