// Copyright (c) Gesma94. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Data;

namespace Fluently.Interfaces
{
    public interface IProvider
    {
        IEnumerable<T> Query<T>(IDbCommand dbCommand);
        IEnumerable<T> Query<T>(IDbCommand dbCommand, string sqlString);
        int Insert<T>(IDbCommand dbCommand, T entity);
        int InsertRange<T>(IDbCommand dbCommand, IEnumerable<T> entities);
        void Delete<T>(IDbCommand dbCommand, T entity);
        void DeleteRange<T>(IDbCommand dbCommand, IEnumerable<T> entities);
        void Update<T>(IDbCommand dbCommand, T entity);
        void UpdateRange<T>(IDbCommand dbCommand, IEnumerable<T> entities);
    }
}