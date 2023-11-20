// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Models;
using Fluently.Builders;
using Fluently.Interfaces;

namespace FantaAsta.Infrastructure.Configurations;

public class BatchConfiguration  : IEntityConfigurator<BatchEntity>
{
    public void Configure(EntityMapBuilder<BatchEntity> builder)
    {
        builder.ToTable("Batches");
        
        builder.HasKey(x => x.Id).HasColumnName("id");
        builder.HasProperty(x => x.CreatedTime).HasColumnName("created_time");
        builder.HasProperty(x => x.LastModifiedTime).HasColumnName("last_modified_time");
        builder.HasProperty(x => x.AuctionId).HasColumnName("auction_id");
        builder.HasProperty(x => x.FootballerId).HasColumnName("footballer_id");
        builder.HasProperty(x => x.InitialCost).HasColumnName("initial_cost");
        builder.HasProperty(x => x.LastCallerOffer).HasColumnName("last_caller_offer");
        builder.HasProperty(x => x.LastCallerUserId).HasColumnName("last_caller_user_id");
        builder.HasProperty(x => x.NextCallerUserId).HasColumnName("next_caller_user_id");
        builder.HasProperty(x => x.Status).HasColumnName("status");
        builder.HasProperty(x => x.FoldedUsersId).HasColumnName("folded_users_id");
    }
}
