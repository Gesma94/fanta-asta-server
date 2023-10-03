// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Interfaces;
using FantaAstaServer.Interfaces.Services.Mappers;
using FantaAstaServer.Models.Domain;
using FantaAstaServer.Models.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace FantaAstaServer.Services.Mappers
{
    public class AuctionMapper : IAuctionMapper
    {
        private readonly IDbUnitOfWork _dbUnitOfWork;


        public AuctionMapper(IDbUnitOfWork dbUnitOfWork)
            => (_dbUnitOfWork) = (dbUnitOfWork);


        public async Task<UserAuctionDetailsDto> ToUserAuctionDetailsDto(AuctionEntity auctionEntity)
        {
            var auctionUsers = await _dbUnitOfWork.UserAuctions.GetByAuctionId(auctionEntity.Id);

            return new UserAuctionDetailsDto() 
            {
                Name = auctionEntity.Name,
                Status = auctionEntity.Status,
                UserAmount = auctionEntity.UserAmount,
                CurrentUserAmount = auctionUsers.Count(),
            };

        }
    }
}
