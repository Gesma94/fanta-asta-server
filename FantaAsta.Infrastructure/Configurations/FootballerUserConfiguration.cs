// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantaAsta.Infrastructure.Configurations;

public class FootballerUserConfiguration : IEntityTypeConfiguration<FootballerUserEntity>
{
    public void Configure(EntityTypeBuilder<FootballerUserEntity> builder)
    {
        builder.ToTable("Footballers_Users");
        
        builder.HasKey(x => x.Id).HasName("id");

        builder.Property(x => x.CreatedTime).HasColumnName("created_time");
        builder.Property(x => x.LastModifiedTime).HasColumnName("last_modified_time");
        builder.Property(x => x.FootballerId).HasColumnName("footballer_id");
        builder.Property(x => x.UserId).HasColumnName("user_id");
    }
}
