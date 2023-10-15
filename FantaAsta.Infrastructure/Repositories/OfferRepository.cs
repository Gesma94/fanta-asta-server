// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Application.Interfaces.Repositories;
using FantaAsta.Domain.Models;
using FantaAsta.Infrastructure.Common;
using FantaAsta.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FantaAsta.Infrastructure.Repositories;

public class OfferRepository : GenericRepository<OfferEntity>, IOfferRepository
{
    public OfferRepository(PostgreSqlContext postgreSqlContext) : base(postgreSqlContext)
    {
    }

    public IQueryable<OfferEntity> GetAllByBatchId(int batchId)
    {
        return PostgreSqlContext.Offers.AsNoTracking().Where(x => x.BatchId.Equals(batchId));
    }

    public OfferEntity GetPreviousOffer(int currentOfferId)
    {
        return GetPreviousOffer(Get(currentOfferId));
    }

    public OfferEntity GetPreviousOffer(OfferEntity offerEntity)
    {
        return PostgreSqlContext.Offers.AsNoTracking().FirstOrDefault(x => x.Id.Equals(offerEntity.PreviousOfferId));
    }
}