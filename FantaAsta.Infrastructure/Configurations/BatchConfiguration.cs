// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantaAsta.Infrastructure.Configurations;

public class BatchConfiguration : IEntityTypeConfiguration<BatchEntity>
{
    public void Configure(EntityTypeBuilder<BatchEntity> builder)
    {
        builder.ToTable("Batches");
        
        builder.HasKey(x => x.Id).HasName("id");

        builder.Property(x => x.CreatedTime).HasColumnName("created_time");
        builder.Property(x => x.LastModifiedTime).HasColumnName("last_modified_time");
        builder.Property(x => x.AuctionId).HasColumnName("auction_id");
        builder.Property(x => x.FootballerId).HasColumnName("footballer_id");
        builder.Property(x => x.InitialCost).HasColumnName("initial_cost");
        builder.Property(x => x.LastCallerOffer).HasColumnName("last_caller_offer");
        builder.Property(x => x.LastCallerUserId).HasColumnName("last_caller_user_id");
        builder.Property(x => x.NextCallerUserId).HasColumnName("next_caller_user_id");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.FoldedUsersId).HasColumnName("folded_users_id");
    }
}
