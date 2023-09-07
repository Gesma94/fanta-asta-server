// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Models.Domain;
using FantaAstaServer.Interfaces.Repositories;
using System.Threading.Tasks;
using System.Linq;

namespace FantaAstaServer.Services.Repositories
{
    public class UserActionRepository : GenericRepository<UserAuctionEntity>, IUserActionRepository
    {
        public UserActionRepository(FantaAstaDbContext fantaAstaDbContext) : base(fantaAstaDbContext)
        {
        }

        public Task<IQueryable<UserAuctionEntity>> GetByUserId(int userId)
        {
            return Task.FromResult(GetAll().Where(x => x.UserId == userId));
        }

        public Task<IQueryable<UserAuctionEntity>> GetByAuctionId(int auctionId)
        {
            return Task.FromResult(GetAll().Where(x => x.AuctionId == auctionId));
        }
    }
}
