// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Application.Interfaces.Common;
using FantaAsta.Domain.Common;
using FantaAsta.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FantaAsta.Infrastructure.Repositories;

public class GenericRepository<T> : IRepository<T> where T : EntityBase
{
    private readonly PostgreSqlContext _postgreSqlContext;
    
    public GenericRepository(PostgreSqlContext postgreSqlContext)
    {
        _postgreSqlContext = postgreSqlContext ?? throw new ArgumentNullException(nameof(postgreSqlContext));
    }
    
    public EntityEntry<T> Add(T entity)
    {
        return _postgreSqlContext.Set<T>().Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _postgreSqlContext.Set<T>().AddRange(entities);
    }

    public EntityEntry<T> Update(T entity)
    {
        return _postgreSqlContext.Set<T>().Update(entity);
    }

    public void UpdateRange(IEnumerable<T> entities)
    {
        _postgreSqlContext.Set<T>().UpdateRange(entities);
    }

    public EntityEntry<T> Delete(T entity)
    {
        return _postgreSqlContext.Set<T>().Remove(entity);
    }

    public EntityEntry<T> Delete(int entityId)
    {
        return _postgreSqlContext.Set<T>().Remove(Get(entityId));
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        _postgreSqlContext.Set<T>().RemoveRange(entities);
    }

    public void DeleteRange(IEnumerable<int> entitiesIds)
    {
        _postgreSqlContext.Set<T>().RemoveRange(entitiesIds.Select(Get));
    }
    
    public T Get(int entityId)
    {
        return _postgreSqlContext.Set<T>().AsNoTracking().FirstOrDefault(x => x.Id.Equals(entityId));
    }

    public IQueryable<T> GetAll()
    {
        return _postgreSqlContext.Set<T>().AsNoTracking();
    }
}