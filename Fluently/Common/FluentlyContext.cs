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

        public IEnumerable<T> Query<T>(Func<IDbCommand> dbCommandFactory)
        {
            return _provider.Query<T>(dbCommandFactory);
        }

        public IEnumerable<T> Query<T>(Func<IDbCommand> dbCommandFactory, string sqlString)
        {
            return _provider.Query<T>(dbCommandFactory, sqlString);
        }

        public int Insert<T>(Func<IDbCommand> dbCommandFactory, T entity)
        {
            return _provider.Insert(dbCommandFactory, entity);
        }

        public int InsertRange<T>(Func<IDbCommand> dbCommandFactory, IEnumerable<T> entities)
        {
            return _provider.InsertRange(dbCommandFactory, entities);
        }

        public int Delete<T>(Func<IDbCommand> dbCommandFactory, T entity)
        {
            return _provider.Delete(dbCommandFactory, entity);
        }

        public int DeleteRange<T>(Func<IDbCommand> dbCommandFactory, IEnumerable<T> entities)
        {
            return _provider.DeleteRange(dbCommandFactory, entities);
        }

        public int Update<T>(Func<IDbCommand> dbCommandFactory, T entity)
        {
            return _provider.Update(dbCommandFactory, entity);
        }

        public int UpdateRange<T>(Func<IDbCommand> dbCommandFactory, IEnumerable<T> entities)
        {
            return _provider.UpdateRange(dbCommandFactory, entities);
        }
    }
}