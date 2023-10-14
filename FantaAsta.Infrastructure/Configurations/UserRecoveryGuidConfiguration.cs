// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantaAsta.Infrastructure.Configurations;

public class UserRecoveryGuidConfiguration : IEntityTypeConfiguration<UserRecoveryGuidEntity>
{
    public void Configure(EntityTypeBuilder<UserRecoveryGuidEntity> builder)
    {
        builder.ToTable("User_Recovery_Guids");
        
        builder.HasKey(x => x.Id).HasName("id");
        
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.Guid).HasColumnName("guid");
        builder.Property(x => x.Timestamp).HasColumnName("timestamp");
   }
}
