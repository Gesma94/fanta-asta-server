// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Interfaces;
using FantaAstaServer.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace FantaAstaServer.Services
{
    public class DbUnitOfWork : IDbUnitOfWork, IDisposable
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuctionRepository _auctionRepository;
        private readonly IBatchRepository _batchRepository;
        private readonly IFootballerRepository _footballerRepository;
        private readonly IOfferRepository _offerRepository;
        private readonly IUserActionRepository _userActionRepository;
        private readonly FantaAstaDbContext _fantaAstaDbContext;


        public DbUnitOfWork(FantaAstaDbContext fantaAstaDbContext, IAuctionRepository auctionRepository, IBatchRepository batchRepository,
            IFootballerRepository footballerRepository, IOfferRepository offerRepository, IUserActionRepository userActionRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _auctionRepository = auctionRepository;
            _fantaAstaDbContext = fantaAstaDbContext;
            _batchRepository = batchRepository;
            _footballerRepository= footballerRepository;
            _userActionRepository = userActionRepository;
            _offerRepository= offerRepository;
        }


        public IAuctionRepository Auctions => _auctionRepository;
        public IUserRepository Users => _userRepository;
        public IBatchRepository Batches => _batchRepository;
        public IFootballerRepository Footballers => _footballerRepository;
        public IOfferRepository Offers => _offerRepository;
        public IUserActionRepository UserActions => _userActionRepository;


        public async Task<int> SaveChanges()
        {
            return await _fantaAstaDbContext.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            _fantaAstaDbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
