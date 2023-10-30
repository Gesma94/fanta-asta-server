// Copyright (c) Gesma94. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data;
using Fluently.Common;
using Fluently.Interfaces;
using Fluently.Mappers;

namespace Fluently.Providers
{
    public class PostgreSqlProvider : BaseProvider
    {
        public PostgreSqlProvider(IEnumerable<EntityMapper> mappers, IEnumerable<IFluentlyConverter> converters) : base(mappers, converters)
        {
        }
        
        public override IEnumerable<T> Query<T>(IDbCommand dbCommand)
        {
            return Query<T>(dbCommand, $"SELECT * FROM \"{GetMapperOf<T>().TableName}\"");
        }

        public override IEnumerable<T> Query<T>(IDbCommand dbCommand, string sqlString)
        {
            var result = new List<T>();
            var shouldCloseConnection = false;
            var mapBuilder = GetMapperOf<T>();

            dbCommand.CommandText = sqlString;

            if (dbCommand.Connection.State != ConnectionState.Open)
            {
                dbCommand.Connection.Open();
                shouldCloseConnection = true;
            }

            using (var reader = dbCommand.ExecuteReader(shouldCloseConnection ? CommandBehavior.CloseConnection : CommandBehavior.Default))
            {
                while (reader.Read())
                {
                    var entity = Activator.CreateInstance<T>();

                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var columnName = reader.GetName(i);
                        var propertyBuilder = mapBuilder.GetMapperByColumnName(columnName);

                        if (propertyBuilder == null)
                        {
                            continue;
                        }

                        var propertyInfo = GetPropertyInfo<T>(propertyBuilder.PropertyName);
                        if (propertyInfo == null)
                        {
                            throw new InvalidOperationException(
                                $"property '{propertyBuilder.PropertyName}' not found in type <{nameof(T)}>");
                        }

                        propertyInfo.SetValue(entity, GetPocoValue<T>(propertyBuilder, reader, i));
                    }

                    result.Add(entity);
                }
            }
            
            return result;
        }

        public override int Insert<T>(IDbCommand dbCommand, T entity)
        {
            throw new NotImplementedException();
        }

        public override int InsertRange<T>(IDbCommand dbCommand, IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public override void Delete<T>(IDbCommand dbCommand, T entity)
        {
            throw new NotImplementedException();
        }

        public override void DeleteRange<T>(IDbCommand dbCommand, IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public override void Update<T>(IDbCommand dbCommand, T entity)
        {
            throw new NotImplementedException();
        }

        public override void UpdateRange<T>(IDbCommand dbCommand, IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}