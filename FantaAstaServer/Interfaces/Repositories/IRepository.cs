// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using System.Linq;
using System.Threading.Tasks;
using FantaAstaServer.Models.Domain;

namespace FantaAstaServer.Interfaces.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<bool> Create(T entity);
        bool Update(T entity);
        Task<bool> Delete(int key);
        Task<T> Get(int key);
        IQueryable<T> GetAll();
    }
}
