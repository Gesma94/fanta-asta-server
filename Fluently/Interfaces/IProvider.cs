// Copyright (c) Gesma94. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data;

namespace Fluently.Interfaces
{
    public interface IProvider
    {
        IEnumerable<T> Query<T>(Func<IDbCommand> dbCommandFactory);
        IEnumerable<T> Query<T>(Func<IDbCommand> dbCommandFactory, string sqlString);
        int Insert<T>(Func<IDbCommand> dbCommandFactory, T entity);
        int InsertRange<T>(Func<IDbCommand> dbCommandFactory, IEnumerable<T> entities);
        int Delete<T>(Func<IDbCommand> dbCommandFactory, T entity);
        int DeleteRange<T>(Func<IDbCommand> dbCommandFactory, IEnumerable<T> entities);
        int Update<T>(Func<IDbCommand> dbCommandFactory, T entity);
        int UpdateRange<T>(Func<IDbCommand> dbCommandFactory, IEnumerable<T> entities);
        string GetTableName<T>();
    }
}