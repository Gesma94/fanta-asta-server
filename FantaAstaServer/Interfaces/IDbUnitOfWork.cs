// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Interfaces.Repositories;
using System.Threading.Tasks;

namespace FantaAstaServer.Interfaces
{
    public interface IDbUnitOfWork
    {
        IAuctionRepository Auctions { get; }
        IUserRepository Users { get; }
        Task<int> SaveChanges();
    }
}
