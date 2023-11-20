// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Models;
using Fluently.Builders;
using Fluently.Interfaces;

namespace FantaAsta.Infrastructure.Configurations;

public class AuctionConfiguration : IEntityConfigurator<AuctionEntity>
{
    public void Configure(EntityMapBuilder<AuctionEntity> builder)
    {
        builder.ToTable("Auctions");

        builder.HasKey(x => x.Id).HasColumnName("id");
        builder.HasProperty(x => x.CreatedTime).HasColumnName("created_time");
        builder.HasProperty(x => x.LastModifiedTime).HasColumnName("last_modified_time");
        builder.HasProperty(x => x.Status).HasColumnName("status");
        builder.HasProperty(x => x.Name).HasColumnName("name");
        builder.HasProperty(x => x.UserSize).HasColumnName("user_size");
        builder.HasProperty(x => x.InitialCredit).HasColumnName("initial_credit");
        builder.HasProperty(x => x.Mode).HasColumnName("mode");
        builder.HasProperty(x => x.CallOrder).HasColumnName("call_order");
        builder.HasProperty(x => x.UsersOrder).HasColumnName("users_order");
        builder.HasProperty(x => x.OfferBasedTimeoutCallMs).HasColumnName("offer_based_timeout_call_ms");
        builder.HasProperty(x => x.GoalkeeperMinAmount).HasColumnName("goalkeeper_min_amount");
        builder.HasProperty(x => x.GoalkeeperMaxAmount).HasColumnName("goalkeeper_max_amount");
        builder.HasProperty(x => x.DefenderMinAmount).HasColumnName("defender_min_amount");
        builder.HasProperty(x => x.DefenderMaxAmount).HasColumnName("defender_max_amount");
        builder.HasProperty(x => x.MidfielderMinAmount).HasColumnName("midfielder_min_amount");
        builder.HasProperty(x => x.MidfielderMaxAmount).HasColumnName("midfielder_max_amount");
        builder.HasProperty(x => x.StrikerMinAmount).HasColumnName("striker_min_amount");
        builder.HasProperty(x => x.StrikerMaxAmount).HasColumnName("striker_max_amount");
    }
}