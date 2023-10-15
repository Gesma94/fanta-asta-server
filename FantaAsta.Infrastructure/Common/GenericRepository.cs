// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Application.Interfaces.Common;
using FantaAsta.Domain.Common;
using FantaAsta.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FantaAsta.Infrastructure.Common;

public class GenericRepository<T> : IRepository<T> where T : EntityBase
{
    protected readonly PostgreSqlContext PostgreSqlContext;
    
    public GenericRepository(PostgreSqlContext postgreSqlContext)
    {
        PostgreSqlContext = postgreSqlContext ?? throw new ArgumentNullException(nameof(postgreSqlContext));
    }
    
    public EntityEntry<T> Add(T entity)
    {
        return PostgreSqlContext.Set<T>().Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        PostgreSqlContext.Set<T>().AddRange(entities);
    }

    public EntityEntry<T> Update(T entity)
    {
        return PostgreSqlContext.Set<T>().Update(entity);
    }

    public void UpdateRange(IEnumerable<T> entities)
    {
        PostgreSqlContext.Set<T>().UpdateRange(entities);
    }

    public EntityEntry<T> Delete(T entity)
    {
        return PostgreSqlContext.Set<T>().Remove(entity);
    }

    public EntityEntry<T> Delete(int entityId)
    {
        return PostgreSqlContext.Set<T>().Remove(Get(entityId));
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        PostgreSqlContext.Set<T>().RemoveRange(entities);
    }

    public void DeleteRange(IEnumerable<int> entitiesIds)
    {
        PostgreSqlContext.Set<T>().RemoveRange(entitiesIds.Select(Get));
    }
    
    public T Get(int entityId)
    {
        return PostgreSqlContext.Set<T>().AsNoTracking().FirstOrDefault(x => x.Id.Equals(entityId));
    }

    public IQueryable<T> GetAll()
    {
        return PostgreSqlContext.Set<T>().AsNoTracking();
    }
}