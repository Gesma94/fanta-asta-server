// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Models.Domain;
using FantaAstaServer.Interfaces.Repositories;

namespace FantaAstaServer.Services.Repositories
{
    public class OfferRepository : GenericRepository<OfferEntity>, IOfferRepository
    {
        public OfferRepository(FantaAstaDbContext fantaAstaDbContext) : base(fantaAstaDbContext)
        {
        }
    }
}
