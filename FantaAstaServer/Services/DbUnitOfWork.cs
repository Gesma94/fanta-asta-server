// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Interfaces;
using FantaAstaServer.Interfaces.Repositories;
using FantaAstaServer.Services.Repositories;
using System;
using System.Threading.Tasks;

namespace FantaAstaServer.Services
{
    public class DbUnitOfWork : IDbUnitOfWork, IDisposable
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuctionRepository _auctionRepository;
        private readonly FantaAstaDbContext _fantaAstaDbContext;


        public DbUnitOfWork(FantaAstaDbContext fantaAstaDbContext, IAuctionRepository auctionRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _auctionRepository = auctionRepository;
            _fantaAstaDbContext = fantaAstaDbContext;
        }


        public IAuctionRepository Auctions => _auctionRepository;

        public IUserRepository Users => _userRepository;


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
