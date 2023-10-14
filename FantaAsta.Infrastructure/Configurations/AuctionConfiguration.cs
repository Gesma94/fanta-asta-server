// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantaAsta.Infrastructure.Configurations;

public class AuctionConfiguration : IEntityTypeConfiguration<AuctionEntity>
{
    public void Configure(EntityTypeBuilder<AuctionEntity> builder)
    {
        builder.ToTable("Auctions");
        
        builder.HasKey(x => x.Id).HasName("id");
        
        builder.Property(x => x.CreatedTime).HasColumnName("created_time");
        builder.Property(x => x.LastModifiedTime).HasColumnName("last_modified_time");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.UserSize).HasColumnName("user_size");
        builder.Property(x => x.InitialCredit).HasColumnName("initial_credit");
        builder.Property(x => x.Mode).HasColumnName("mode");
        builder.Property(x => x.CallOrder).HasColumnName("call_order");
        builder.Property(x => x.UsersOrder).HasColumnName("users_order");
        builder.Property(x => x.OfferBasedTimeoutCallMs).HasColumnName("offer_based_timeout_call_ms");
        builder.Property(x => x.GoalkeeperMinAmount).HasColumnName("goalkeeper_min_amount");
        builder.Property(x => x.GoalkeeperMaxAmount).HasColumnName("goalkeeper_max_amount");
        builder.Property(x => x.DefenderMinAmount).HasColumnName("defender_min_amount");
        builder.Property(x => x.DefenderMaxAmount).HasColumnName("defender_max_amount");
        builder.Property(x => x.MidfielderMinAmount).HasColumnName("midfielder_min_amount");
        builder.Property(x => x.MidfielderMaxAmount).HasColumnName("midfielder_max_amount");
        builder.Property(x => x.StrikerMinAmount).HasColumnName("striker_min_amount");
        builder.Property(x => x.StrikerMaxAmount).HasColumnName("striker_max_amount");
    }
}
