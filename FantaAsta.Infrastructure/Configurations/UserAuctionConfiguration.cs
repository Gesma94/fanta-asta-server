﻿// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Models;
using Fluently.Builders;
using Fluently.Interfaces;

namespace FantaAsta.Infrastructure.Configurations;

public class UserAuctionConfiguration : IEntityConfigurator<UserAuctionEntity>
{
    public void Configure(EntityMapBuilder<UserAuctionEntity> builder)
    {
        builder.ToTable("Users_Auctions");
        
        builder.HasKey(x => new { x.UserId, x.AuctionId });
        builder.HasProperty(x => x.CreatedTime).HasColumnName("created_time");
        builder.HasProperty(x => x.LastModifiedTime).HasColumnName("last_modified_time");
        builder.HasProperty(x => x.AuctionId).HasColumnName("auction_id");
        builder.HasProperty(x => x.UserId).HasColumnName("user_id");
        builder.HasProperty(x => x.IsAdmin).HasColumnName("is_admin");
    }
}
