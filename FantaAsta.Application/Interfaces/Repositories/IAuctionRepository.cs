// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Models;
using FantaAsta.Application.Interfaces.Common;

namespace FantaAsta.Application.Interfaces.Repositories;

public interface IAuctionRepository : IRepository<AuctionEntity>
{
    AuctionEntity GetByName(string auctionName);
}