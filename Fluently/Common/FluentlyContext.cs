// Copyright (c) Gesma94. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data;
using Fluently.Interfaces;

namespace Fluently.Common
{
    public class FluentlyContext : IFluentlyContext
    {
        private readonly IProvider _provider;

        public FluentlyContext(IProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public IEnumerable<T> Query<T>(IDbCommand dbCommand)
        {
            return _provider.Query<T>(dbCommand);
        }

        public IEnumerable<T> Query<T>(IDbCommand dbCommand, string sqlString)
        {
            return _provider.Query<T>(dbCommand, sqlString);
        }

        public int Insert<T>(IDbCommand dbCommand, T entity)
        {
            return _provider.Insert(dbCommand, entity);
        }

        public int InsertRange<T>(IDbCommand dbCommand, IEnumerable<T> entities)
        {
            return _provider.InsertRange(dbCommand, entities);

        }

        public void Delete<T>(IDbCommand dbCommand, T entity)
        {
            _provider.Delete(dbCommand, entity);
        }

        public void DeleteRange<T>(IDbCommand dbCommand, IEnumerable<T> entities)
        {
            _provider.DeleteRange(dbCommand, entities);
        }

        public void Update<T>(IDbCommand dbCommand, T entity)
        {
            _provider.Update(dbCommand, entity);
        }

        public void UpdateRange<T>(IDbCommand dbCommand, IEnumerable<T> entities)
        {
            _provider.UpdateRange(dbCommand, entities);
        }
    }
}