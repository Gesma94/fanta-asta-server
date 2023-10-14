// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantaAsta.Infrastructure.Configurations;

public class UserAuctionConfiguration : IEntityTypeConfiguration<UserAuctionEntity>
{
    public void Configure(EntityTypeBuilder<UserAuctionEntity> builder)
    {
        builder.ToTable("Users_Auctions");
        
        builder.HasKey(x => new { x.UserId, x.AuctionId });
        
        builder.Property(x => x.CreatedTime).HasColumnName("created_time");
        builder.Property(x => x.LastModifiedTime).HasColumnName("last_modified_time");
        builder.Property(x => x.AuctionId).HasColumnName("auction_id");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.IsAdmin).HasColumnName("is_admin");
    }
}
