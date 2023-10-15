// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Application.Interfaces.Repositories;
using FantaAsta.Domain.Models;
using FantaAsta.Infrastructure.Common;
using FantaAsta.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FantaAsta.Infrastructure.Repositories;

public class UserRecoveryGuidRepository : GenericRepository<UserRecoveryGuidEntity>, IUserRecoveryGuidRepository
{
    public UserRecoveryGuidRepository(PostgreSqlContext postgreSqlContext) : base(postgreSqlContext)
    {
    }

    public UserRecoveryGuidEntity GetByUserId(int userId)
    {
        return PostgreSqlContext.Set<UserRecoveryGuidEntity>().AsNoTracking().SingleOrDefault(x => x.UserId.Equals(userId));
    }
}