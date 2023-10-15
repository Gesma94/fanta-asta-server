// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FantaAsta.Application.Interfaces.Common;

public interface IRepository<T> where T : EntityBase
{
    EntityEntry<T> Add(T entity);
    void AddRange(IEnumerable<T> entities);
    EntityEntry<T> Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
    EntityEntry<T> Delete(T entity);
    EntityEntry<T> Delete(int entityId);
    void DeleteRange(IEnumerable<T> entities);
    void DeleteRange(IEnumerable<int> entitiesIds);
    T Get(int entityId);
    IQueryable<T> GetAll();
}