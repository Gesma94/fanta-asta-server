// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Application.Interfaces.Repositories;
using FantaAsta.Domain.Models;
using FantaAsta.Infrastructure.Common;
using FantaAsta.Infrastructure.DbContexts;
using Fluently.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FantaAsta.Infrastructure.Repositories;

public class UserRecoveryGuidRepository : GenericRepository<UserRecoveryGuidEntity>, IUserRecoveryGuidRepository
{
    public UserRecoveryGuidRepository(PostgreSqlContext postgreSqlContext, IFluentlyContext fluentlyContext) : base(postgreSqlContext, fluentlyContext)
    {
    }

    public UserRecoveryGuidEntity GetByUserId(int userId)
    {
        return FluentlyContext.Query<UserRecoveryGuidEntity>(PostgreSqlContext.CreateCommand)
            .SingleOrDefault(x => x.UserId == userId);
    }
}