// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Application.Interfaces.Repositories;
using FantaAsta.Domain.Models;
using FantaAsta.Infrastructure.Common;
using FantaAsta.Infrastructure.DbContexts;
using Fluently.Interfaces;

namespace FantaAsta.Infrastructure.Repositories;

public class OfferRepository : GenericRepository<OfferEntity>, IOfferRepository
{
    public OfferRepository(PostgreSqlContext postgreSqlContext, IFluentlyContext fluentlyContext) : base(postgreSqlContext, fluentlyContext)
    {
    }

    public IEnumerable<OfferEntity> GetAllByBatchId(int batchId)
    {
        return FluentlyContext.Query<OfferEntity>(PostgreSqlContext.CreateCommand).Where(x => x.BatchId == batchId);
    }

    public OfferEntity GetPreviousOffer(int currentOfferId)
    {
        return GetPreviousOffer(Get(currentOfferId));
    }

    public OfferEntity GetPreviousOffer(OfferEntity offerEntity)
    {
        return FluentlyContext.Query<OfferEntity>(PostgreSqlContext.CreateCommand)
            .SingleOrDefault(x => x.Id == offerEntity.Id);
    }
}