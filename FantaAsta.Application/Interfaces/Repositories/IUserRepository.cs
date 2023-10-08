// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Application.Interfaces.Common;
using FantaAsta.Domain.Models;

namespace FantaAsta.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<UserEntity>
{
    Task<UserEntity> GetByEmailAsync(string email);
    Task<UserEntity> GetByUsernameAsync(string username);
}