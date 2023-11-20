// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Models;
using Fluently.Builders;
using Fluently.Interfaces;

namespace FantaAsta.Infrastructure.Configurations;

public class FootballerUserConfiguration : IEntityConfigurator<FootballerUserEntity>
{
    public void Configure(EntityMapBuilder<FootballerUserEntity> builder)
    {
        builder.ToTable("Footballers_Users");
        
        builder.HasKey(x => x.Id).HasColumnName("id");
        builder.HasProperty(x => x.CreatedTime).HasColumnName("created_time");
        builder.HasProperty(x => x.LastModifiedTime).HasColumnName("last_modified_time");
        builder.HasProperty(x => x.FootballerId).HasColumnName("footballer_id");
        builder.HasProperty(x => x.UserId).HasColumnName("user_id");
    }
}
