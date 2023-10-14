// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantaAsta.Infrastructure.Configurations;

public class FootballerConfiguration : IEntityTypeConfiguration<FootballerEntity>
{
    public void Configure(EntityTypeBuilder<FootballerEntity> builder)
    {
        builder.ToTable("Footballers");
        
        builder.HasKey(x => x.Id).HasName("id");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.CreatedTime).HasColumnName("created_time");
        builder.Property(x => x.LastModifiedTime).HasColumnName("last_modified_time");
        builder.Property(x => x.AuctionId).HasColumnName("auction_id");
        builder.Property(x => x.FirstName).HasColumnName("first_name");
        builder.Property(x => x.LastName).HasColumnName("last_name");
        builder.Property(x => x.Price).HasColumnName("price");
        builder.Property(x => x.Role).HasColumnName("role");
    }
}
