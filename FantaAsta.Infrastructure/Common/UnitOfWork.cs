// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using System.Data;
using FantaAsta.Application.Interfaces.Common;
using FantaAsta.Application.Interfaces.Repositories;
using FantaAsta.Infrastructure.DbContexts;

namespace FantaAsta.Infrastructure.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly PostgreSqlContext _postgreSqlContext;

    public UnitOfWork(PostgreSqlContext postgreSqlContext, IAuctionRepository auctionRepository,
        IBatchRepository batchRepository, IFootballerRepository footballerRepository,
        IFootballerUserRepository footballerUserRepository, IOfferRepository offerRepository,
        IUserAuctionRepository userAuctionRepository, IUserRecoveryGuidRepository userRecoveryGuidRepository,
        IUserRepository userRepository)
    {
        _postgreSqlContext = postgreSqlContext;
        Auctions = auctionRepository;
        Batches = batchRepository;
        Footballers = footballerRepository;
        FootballerUsers = footballerUserRepository;
        Offers = offerRepository;
        UserAuctions = userAuctionRepository;
        UserRecoveryGuids = userRecoveryGuidRepository;
        Users = userRepository;
    }

    public IAuctionRepository Auctions { get; }
    public IBatchRepository Batches { get; }
    public IFootballerRepository Footballers { get; }
    public IFootballerUserRepository FootballerUsers { get; }
    public IOfferRepository Offers { get; }
    public IUserAuctionRepository UserAuctions { get; }
    public IUserRecoveryGuidRepository UserRecoveryGuids { get; }
    public IUserRepository Users { get; }

    public IDbTransaction BeginTransaction()
    {
        return _postgreSqlContext.BeginTransaction();
    }

    public void RollbackTransaction()
    {
        _postgreSqlContext.Rollback();
    }

    public void Commit()
    {
        _postgreSqlContext.Commit();
    }

    public void Dispose()
    {
        _postgreSqlContext.Dispose();
        GC.SuppressFinalize(this);
    }
}