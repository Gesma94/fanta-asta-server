// Copyright (c) Gesma94. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Fluently.Common;
using Fluently.Interfaces;
using Fluently.Mappers;

namespace Fluently.Providers
{
    public class PostgreSqlProvider : BaseProvider
    {
        private static Dictionary<Type, DbType> _typeToDbTypeMap;
        
        public PostgreSqlProvider(IEnumerable<EntityMapper> mappers, IEnumerable<IFluentlyConverter> converters) : base(mappers, converters)
        {
        }
        
        public override IEnumerable<T> Query<T>(Func<IDbCommand> dbCommandFactory)
        {
            return Query<T>(dbCommandFactory, $"SELECT * FROM \"{GetMapperOf<T>().TableName}\"");
        }

        public override IEnumerable<T> Query<T>(Func<IDbCommand> dbCommandFactory, string sqlString)
        {
            using var dbCommand = dbCommandFactory();
            var result = new List<T>();
            var mapBuilder = GetMapperOf<T>();

            dbCommand.CommandText = sqlString;
            
            using var reader = dbCommand.ExecuteReader();
        
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
                        throw new InvalidOperationException($"property '{propertyBuilder.PropertyName}' not found in type <{nameof(T)}>");
                    }

                    propertyInfo.SetValue(entity, GetPocoValueBase<T>(propertyBuilder, reader, i));
                }

                result.Add(entity);
            }

            return result;
        }

        public override int InsertRange<T>(Func<IDbCommand> dbCommandFactory, IEnumerable<T> entities)
        {
            entities = entities.ToArray();
            
            var mapBuilder = GetMapperOf<T>();
            var writableColumns = mapBuilder.PropertyMappers.Where(x => !x.IsColumnReadOnly).ToArray();
            var columnsName = writableColumns.Select(x => x.ColumnName);
            var valueStatements = new List<string>();
            using var dbCommand = dbCommandFactory();

            for (var i=0; i<entities.Count(); i++)
            {
                var valueStatement = new List<string>();
                
                for (var y = 0; y < writableColumns.Length; y++)
                {
                    var (placeholder, dbParameter) = GetDbValueBase(dbCommand.CreateParameter, entities.ElementAt(i), i, writableColumns[y]);
                    
                    valueStatement.Add(placeholder);
                    dbCommand.Parameters.Add(dbParameter);
                }
                
                valueStatements.Add($"({string.Join(", ", valueStatement)})");
            }

            dbCommand.CommandText = $"INSERT INTO \"{mapBuilder.TableName}\" ({string.Join(", ", columnsName)}) VALUES {string.Join(", ", valueStatements)}";

            return dbCommand.ExecuteNonQuery();
           
        }

        public override int DeleteRange<T>(Func<IDbCommand> dbCommandFactory, IEnumerable<T> entities)
        {
            using var dbCommand = dbCommandFactory();

            entities = entities.ToArray();
            var mapBuilder = GetMapperOf<T>();
            var columnKey = mapBuilder.PropertyMappers.Single(x => x.IsKey);
            var placeholders = new List<string>();
            
            for (var i=0; i<entities.Count(); i++)
            {
                var (placeholder, dbParameter) = GetDbValueBase(dbCommand.CreateParameter, entities.ElementAt(i), i, columnKey);
                
                placeholders.Add(placeholder);
                dbCommand.Parameters.Add(dbParameter);
            }
                
            dbCommand.CommandText = $"DELETE FROM \"{mapBuilder.TableName}\" WHERE {columnKey.ColumnName} IN ({string.Join(", ", placeholders)});";

            return dbCommand.ExecuteNonQuery();
        }
        
        public override int UpdateRange<T>(Func<IDbCommand> dbCommandFactory, IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
        
        protected override object GetPocoValue(Type propertyType, IDataRecord reader, int i)
        {
            if (reader.IsDBNull(i))
            {
                return null;
            }

            if (propertyType == typeof(DateTimeOffset))
            {
                return new DateTimeOffset(reader.GetDateTime(i));
            }

            return reader.GetValue(i);
        }

        protected override (string, IDbDataParameter) GetDbValue<TEntity>(Func<IDbDataParameter> dbParameterFactory, Type propertyType, object pocoValue, string parameterName)
        {
            var dbParameter = dbParameterFactory();
            
            dbParameter.ParameterName = parameterName;
            dbParameter.DbType = GetDbType(propertyType);
            dbParameter.Value = pocoValue ?? DBNull.Value;
            
            return (parameterName, dbParameter);
        }

        protected override Dictionary<Type, DbType> GetTypeToDbTypeMap()
        {
            if (_typeToDbTypeMap != null)
            {
                return _typeToDbTypeMap;
            }
            
            return _typeToDbTypeMap = new Dictionary<Type, DbType>
            {
                [typeof(byte)] = DbType.Byte,
                [typeof(sbyte)] = DbType.SByte,
                [typeof(short)] = DbType.Int16,
                [typeof(ushort)] = DbType.UInt16,
                [typeof(int)] = DbType.Int32,
                [typeof(uint)] = DbType.UInt32,
                [typeof(long)] = DbType.Int64,
                [typeof(ulong)] = DbType.UInt64,
                [typeof(float)] = DbType.Single,
                [typeof(double)] = DbType.Double,
                [typeof(decimal)] = DbType.Decimal,
                [typeof(bool)] = DbType.Boolean,
                [typeof(string)] = DbType.String,
                [typeof(char)] = DbType.StringFixedLength,
                [typeof(Guid)] = DbType.Guid,
                [typeof(DateTime)] = DbType.DateTime,
                [typeof(DateTimeOffset)] = DbType.DateTimeOffset,
                [typeof(TimeSpan)] = DbType.Time,
                [typeof(int[])] = DbType.Object,
                [typeof(byte[])] = DbType.Binary,
                [typeof(byte?)] = DbType.Byte,
                [typeof(sbyte?)] = DbType.SByte,
                [typeof(short?)] = DbType.Int16,
                [typeof(ushort?)] = DbType.UInt16,
                [typeof(int?)] = DbType.Int32,
                [typeof(uint?)] = DbType.UInt32,
                [typeof(long?)] = DbType.Int64,
                [typeof(ulong?)] = DbType.UInt64,
                [typeof(float?)] = DbType.Single,
                [typeof(double?)] = DbType.Double,
                [typeof(decimal?)] = DbType.Decimal,
                [typeof(bool?)] = DbType.Boolean,
                [typeof(char?)] = DbType.StringFixedLength,
                [typeof(Guid?)] = DbType.Guid,
                [typeof(DateTime?)] = DbType.DateTime,
                [typeof(DateTimeOffset?)] = DbType.DateTimeOffset,
                [typeof(TimeSpan?)] = DbType.Time,
                [typeof(object)] = DbType.Object
            };
        }
    }
}