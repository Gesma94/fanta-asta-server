// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Models.Domain;
using FantaAstaServer.Interfaces.Repositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FantaAstaServer.Services.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        public UserRepository(FantaAstaDbContext fantaAstaDbContext) : base(fantaAstaDbContext)
        {
        }

        public Task<UserEntity> GetByEmail(string email)
        {
            return _fantaAstaDbContext.Set<UserEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public Task<UserEntity> GetByUsername(string username)
        {
            return _fantaAstaDbContext.Set<UserEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Username.Equals(username));
        }
    }
}
