// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Models;
using Fluently.Builders;
using Fluently.Interfaces;

namespace FantaAsta.Infrastructure.Configurations;

public class FootballerConfiguration : IEntityConfigurator<FootballerEntity>
{
    public void Configure(EntityMapBuilder<FootballerEntity> builder)
    {
        builder.ToTable("Footballers");
        
        builder.HasKey(x => x.Id).HasColumnName("id");
        builder.HasProperty(x => x.CreatedTime).HasColumnName("created_time");
        builder.HasProperty(x => x.LastModifiedTime).HasColumnName("last_modified_time");
        builder.HasProperty(x => x.AuctionId).HasColumnName("auction_id");
        builder.HasProperty(x => x.FirstName).HasColumnName("first_name");
        builder.HasProperty(x => x.LastName).HasColumnName("last_name");
        builder.HasProperty(x => x.Price).HasColumnName("price");
        builder.HasProperty(x => x.Role).HasColumnName("role");
    }
}
