// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Models.Domain;
using FantaAstaServer.Models.DTOs;
using System.Threading.Tasks;

namespace FantaAstaServer.Interfaces.Services.Mappers
{
    public interface IAuctionMapper
    {
        Task<UserAuctionDetailsDto> ToUserAuctionDetailsDto(AuctionEntity auctionEntity);
    }
}
