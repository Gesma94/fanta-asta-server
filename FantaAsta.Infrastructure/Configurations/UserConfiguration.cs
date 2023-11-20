// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Models;
using Fluently.Builders;
using Fluently.Interfaces;

namespace FantaAsta.Infrastructure.Configurations;

public class UserConfiguration : IEntityConfigurator<UserEntity>
{
    public void Configure(EntityMapBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(x => x.Id).HasColumnName("id");
        builder.HasProperty(x => x.CreatedTime).HasColumnName("created_time");
        builder.HasProperty(x => x.LastModifiedTime).HasColumnName("last_modified_time");
        builder.HasProperty(x => x.Email).HasColumnName("email");
        builder.HasProperty(x => x.Username).HasColumnName("username");
        builder.HasProperty(x => x.Password).HasColumnName("password");
        builder.HasProperty(x => x.DateOfBirth).HasColumnName("date_of_birth");
        builder.HasProperty(x => x.City).HasColumnName("city");
        builder.HasProperty(x => x.FavouriteTeam).HasColumnName("favourite_team");
    }
}
