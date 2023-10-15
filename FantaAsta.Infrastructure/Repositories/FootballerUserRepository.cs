﻿// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Application.Interfaces.Repositories;
using FantaAsta.Domain.Models;
using FantaAsta.Infrastructure.Common;
using FantaAsta.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FantaAsta.Infrastructure.Repositories;

public class FootballerUserRepository : GenericRepository<FootballerUserEntity>, IFootballerUserRepository
{
    public FootballerUserRepository(PostgreSqlContext postgreSqlContext) : base(postgreSqlContext)
    {
    }

    public FootballerUserEntity GetByFootballerId(int footballerId)
    {
        return PostgreSqlContext.FootballerUsers.AsNoTracking().FirstOrDefault(x => x.FootballerId.Equals(footballerId));
    }
    
    
}