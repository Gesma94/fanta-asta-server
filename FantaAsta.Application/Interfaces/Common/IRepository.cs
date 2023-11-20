// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Common;

namespace FantaAsta.Application.Interfaces.Common;

public interface IRepository<T> where T : EntityBase
{
    int Add(T entity);
    int AddRange(IEnumerable<T> entities);
    int Update(T entity);
    int UpdateRange(IEnumerable<T> entities);
    int Delete(T entity);
    int Delete(int entityId);
    int DeleteRange(IEnumerable<T> entities);
    int DeleteRange(IEnumerable<int> entitiesIds);
    T Get(int entityId);
    IEnumerable<T> GetAll();
}