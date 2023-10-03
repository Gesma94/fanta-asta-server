// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using System.Collections.Generic;
using FantaAstaServer.Interfaces.Repositories;
using FantaAstaServer.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FantaAstaServer.Services.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : EntityBase
    {
        protected readonly FantaAstaDbContext _fantaAstaDbContext;


        public GenericRepository(FantaAstaDbContext fantaAstaDbContext)
        {
            _fantaAstaDbContext = fantaAstaDbContext;
        }


        public async Task<bool> Create(T entity)
        {
            await _fantaAstaDbContext.Set<T>().AddAsync(entity);
            return true;
        }
        
        public async Task<bool> Create(IEnumerable<T> entities)
        {
            await _fantaAstaDbContext.Set<T>().AddRangeAsync(entities);
            return true;
        }
        
        public async Task<bool> Delete(int key)
        {
            _fantaAstaDbContext.Set<T>().Remove(await Get(key));
            return true;
        }

        public async Task<T> Get(int key)
        {
            return await _fantaAstaDbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(key));
        }

        public IQueryable<T> GetAll()
        {
            return _fantaAstaDbContext.Set<T>().AsNoTracking();
        }

        public bool Update(T entity)
        {
            _fantaAstaDbContext.Set<T>().Update(entity);
            return true;
        }
    }
}
