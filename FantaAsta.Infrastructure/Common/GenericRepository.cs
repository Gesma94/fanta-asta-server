// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Application.Interfaces.Common;
using FantaAsta.Domain.Common;
using FantaAsta.Infrastructure.DbContexts;
using Fluently.Interfaces;

namespace FantaAsta.Infrastructure.Common;

public abstract class GenericRepository<T> : IRepository<T> where T : EntityBase
{
    protected readonly PostgreSqlContext PostgreSqlContext;
    protected readonly IFluentlyContext FluentlyContext;
    
    protected GenericRepository(PostgreSqlContext postgreSqlContext, IFluentlyContext fluentlyContext)
    {
        FluentlyContext = fluentlyContext ?? throw new ArgumentNullException(nameof(fluentlyContext));
        PostgreSqlContext = postgreSqlContext ?? throw new ArgumentNullException(nameof(postgreSqlContext));
    }
    
    public int Add(T entity)
    {
        return FluentlyContext.Insert(PostgreSqlContext.CreateCommand, entity);
    }

    public int AddRange(IEnumerable<T> entities)
    {
        return FluentlyContext.InsertRange(PostgreSqlContext.CreateCommand, entities);
    }

    public int Update(T entity)
    {
        return FluentlyContext.Update(PostgreSqlContext.CreateCommand, entity);
    }

    public int UpdateRange(IEnumerable<T> entities)
    {
        return FluentlyContext.UpdateRange(PostgreSqlContext.CreateCommand, entities);
    }

    public int Delete(T entity)
    {
        return FluentlyContext.Delete(PostgreSqlContext.CreateCommand, entity);
    }

    public int Delete(int entityId)
    {
        return Delete(Get(entityId));
    }

    public int DeleteRange(IEnumerable<T> entities)
    {
        return FluentlyContext.DeleteRange(PostgreSqlContext.CreateCommand, entities);
    }

    public int DeleteRange(IEnumerable<int> entitiesIds)
    {
        return DeleteRange(entitiesIds.Select(Get));
    }
    
    public T Get(int entityId)
    {
        return FluentlyContext.Query<T>(PostgreSqlContext.CreateCommand).FirstOrDefault(x => x.Id.Equals(entityId));
    }

    public IEnumerable<T> GetAll()
    {
        return FluentlyContext.Query<T>(PostgreSqlContext.CreateCommand);
    }
}