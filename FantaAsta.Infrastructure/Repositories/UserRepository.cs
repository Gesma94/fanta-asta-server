// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Application.Interfaces.Repositories;
using FantaAsta.Domain.Models;
using FantaAsta.Infrastructure.Common;
using FantaAsta.Infrastructure.DbContexts;
using Fluently.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FantaAsta.Infrastructure.Repositories;

public class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
    public UserRepository(PostgreSqlContext postgreSqlContext, IFluentlyContext fluentlyContext) : base(postgreSqlContext, fluentlyContext)
    {
    }

    public UserEntity GetByEmail(string email)
    {
        return FluentlyContext.Query<UserEntity>(PostgreSqlContext.CreateCommand)
            .SingleOrDefault(x => x.Email == email);
    }

    public UserEntity GetByUsername(string username)
    {
        return FluentlyContext.Query<UserEntity>(PostgreSqlContext.CreateCommand)
            .SingleOrDefault(x => x.Username == username);
    }
}