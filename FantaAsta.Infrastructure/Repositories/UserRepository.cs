// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Application.Interfaces.Repositories;
using FantaAsta.Domain.Models;
using FantaAsta.Infrastructure.Common;
using FantaAsta.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FantaAsta.Infrastructure.Repositories;

public class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
    public UserRepository(PostgreSqlContext postgreSqlContext) : base(postgreSqlContext)
    {
    }

    public UserEntity GetByEmail(string email)
    {
        return PostgreSqlContext.Set<UserEntity>().AsNoTracking().SingleOrDefault(x => x.Email.Equals(email));
    }

    public UserEntity GetByUsername(string username)
    {
        return PostgreSqlContext.Set<UserEntity>().AsNoTracking().SingleOrDefault(x => x.Username.Equals(username));
    }
}