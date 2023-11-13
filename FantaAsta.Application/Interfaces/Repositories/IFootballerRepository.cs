// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Application.Interfaces.Common;
using FantaAsta.Domain.Models;

namespace FantaAsta.Application.Interfaces.Repositories;

public interface IFootballerRepository : IRepository<FootballerEntity>
{
    IEnumerable<FootballerEntity> GetAllByAuctionId(int auctionId);
}