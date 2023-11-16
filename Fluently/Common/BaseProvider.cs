// Copyright (c) Gesma94. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Fluently.Interfaces;
using Fluently.Mappers;

namespace Fluently.Common
{
    public abstract class BaseProvider : IProvider
    {
        private readonly IEnumerable<EntityMapper> _mappers;
        private readonly IEnumerable<IFluentlyConverter> _converters;

        protected BaseProvider(IEnumerable<EntityMapper> mappers, IEnumerable<IFluentlyConverter> converters)
        {
            _mappers = mappers ?? throw new ArgumentNullException(nameof(mappers));
            _converters = converters  ?? throw new ArgumentNullException(nameof(converters));
        }

        public abstract IEnumerable<T> Query<T>(Func<IDbCommand> dbCommandFactory);

        public abstract IEnumerable<T> Query<T>(Func<IDbCommand> dbCommandFactory, string sqlString);

        public int Insert<T>(Func<IDbCommand> dbCommandFactory, T entity)
        {
            return InsertRange(dbCommandFactory, new[] { entity });
        }

        public abstract int InsertRange<T>(Func<IDbCommand> dbCommandFactory, IEnumerable<T> entities);

        public int Delete<T>(Func<IDbCommand> dbCommandFactory, T entity)
        {
            return DeleteRange(dbCommandFactory, new[] { entity });
        }

        public abstract int DeleteRange<T>(Func<IDbCommand> dbCommandFactory, IEnumerable<T> entities);

        public int Update<T>(Func<IDbCommand> dbCommandFactory, T entity)
        {
            return UpdateRange(dbCommandFactory, new[] { entity });
        }

        public abstract int UpdateRange<T>(Func<IDbCommand> dbCommandFactory, IEnumerable<T> entities);
        
        public abstract string GetTableName<T>();
        
        public abstract string GetColumnName<T>(string propertyName);

        protected abstract object GetPocoValue(Type propertyType, IDataRecord reader, int i);

        protected EntityMapper GetMapperOf<T>()
        {
            var mapBuilder = _mappers.Where(x => x.PocoType == typeof(T)).ToArray();

            if (mapBuilder.Length > 1)
            {
                throw new InvalidOperationException($"cannot have multiple mapper for type {typeof(T)}");
            }

            if (!mapBuilder.Any())
            {
                throw new InvalidOperationException($"no mapper registered for type {typeof(T)}");
            }

            return _mappers.Single();
        }
        
        protected object GetPocoValueBase<TEntity>(PropertyMapper propertyMapper, IDataReader reader, int i)
        {
            if (reader.IsDBNull(i))
            {
                return null;
            }
            
            var propertyInfo = GetPropertyInfo<TEntity>(propertyMapper.PropertyName);
            var customConverter = GetTypeConverter(propertyMapper.CustomConverter, propertyInfo.PropertyType);

            return customConverter != null
                ? customConverter.FromDatabaseValue(reader, i)
                : GetPocoValue(propertyInfo.PropertyType, reader, i);
        }
        
        protected (string, IDbDataParameter) GetDbValueBase<TEntity>(Func<IDbDataParameter> dbParameterFactory, TEntity entity, int index, PropertyMapper propertyMapper)
        {
            var propertyInfo = GetPropertyInfo<TEntity>(propertyMapper.PropertyName);
            
            var pocoValue = propertyInfo.GetValue(entity);
            var parameterName = $"@{propertyMapper.ColumnName}_{index}";
            var customConverter = GetTypeConverter(propertyMapper.CustomConverter, propertyInfo.PropertyType);

            return customConverter != null
                ? customConverter.ToDatabaseValue(pocoValue, dbParameterFactory, parameterName)
                : GetDbValue<TEntity>(dbParameterFactory, propertyInfo.PropertyType, pocoValue, parameterName);
        }

        protected abstract (string, IDbDataParameter) GetDbValue<TEntity>(Func<IDbDataParameter> dbParameterFactory, Type propertyType, object pocoValue, string parameterName);

        protected DbType GetDbType(Type propertyType)
        {
            return GetTypeToDbTypeMap().TryGetValue(propertyType, out var result)
                ? result
                : throw new InvalidOperationException($"cannot find DbType of type <{propertyType.Name}>");
        }

        protected abstract Dictionary<Type, DbType> GetTypeToDbTypeMap();

        protected static PropertyInfo GetPropertyInfo<TEntity>(string propertyName)
        {
            return typeof(TEntity).GetProperty(propertyName)
                ?? throw new InvalidOperationException($"cannot retrieve property '{propertyName}' in type <{typeof(TEntity).Name}>");
        }

        private IFluentlyConverter GetTypeConverter(IFluentlyConverter customConverter, Type type)
        {
            return customConverter ?? _converters.SingleOrDefault(x => x.GetEntityType() == type);
        }
    }
}