// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Models;
using Fluently.Builders;
using Fluently.Interfaces;

namespace FantaAsta.Infrastructure.Configurations;

public class UserRecoveryGuidConfiguration : IEntityConfigurator<UserRecoveryGuidEntity>
{
    public void Configure(EntityMapBuilder<UserRecoveryGuidEntity> builder)
    {
        builder.ToTable("User_Recovery_Guids");
        
        builder.HasKey(x => x.Id).HasColumnName("id");
        builder.HasProperty(x => x.UserId).HasColumnName("user_id");
        builder.HasProperty(x => x.Guid).HasColumnName("guid");
        builder.HasProperty(x => x.Timestamp).HasColumnName("timestamp");
    }
}
