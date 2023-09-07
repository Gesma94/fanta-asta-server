// Copyright(c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Models.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace FantaAstaServer.Interfaces.Repositories
{
    public interface IUserActionRepository : IRepository<UserAuctionEntity>
    {
        Task<IQueryable<UserAuctionEntity>> GetByUserId(int userId);
        Task<IQueryable<UserAuctionEntity>> GetByAuctionId(int auctionId);
    }
}
