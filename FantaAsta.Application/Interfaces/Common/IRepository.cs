// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Common;

namespace FantaAsta.Application.Interfaces.Common;

public interface IRepository<T> where T : EntityBase
{
    Task<bool> CreateAsync(T entity);
    Task<bool> CreateAsync(IEnumerable<T> entities);
    Task<bool> UpdateAsync(T entity);
    Task<bool> UpdateAsync(IEnumerable<T> entities);
    Task<bool> DeleteAsync(T entity);
    Task<bool> DeleteAsync(int entityId);
    Task<bool> DeleteAsync(IEnumerable<T> entities);
    Task<bool> DeleteAsync(IEnumerable<int> entitiesIds);
    Task<T> GetAsync(int entityId);
    Task<IQueryable<T>> GetAllAsync();
}