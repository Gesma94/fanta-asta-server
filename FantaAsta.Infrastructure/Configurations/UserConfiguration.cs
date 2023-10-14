// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FantaAsta.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id).HasName("id");

        builder.Property(x => x.CreatedTime).HasColumnName("created_time");
        builder.Property(x => x.LastModifiedTime).HasColumnName("last_modified_time");
        builder.Property(x => x.Email).HasColumnName("email");
        builder.Property(x => x.Username).HasColumnName("username");
        builder.Property(x => x.Password).HasColumnName("password");
        builder.Property(x => x.DateOfBirth).HasColumnName("date_of_birth");
        builder.Property(x => x.City).HasColumnName("city");
        builder.Property(x => x.FavouriteTeam).HasColumnName("favourite_team");
    }
}
