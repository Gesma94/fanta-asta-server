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

        public abstract IEnumerable<T> Query<T>(IDbCommand dbCommand);

        public abstract IEnumerable<T> Query<T>(IDbCommand dbCommand, string sqlString);

        public abstract int Insert<T>(IDbCommand dbCommand, T entity);

        public abstract int InsertRange<T>(IDbCommand dbCommand, IEnumerable<T> entities);

        public abstract void Delete<T>(IDbCommand dbCommand, T entity);

        public abstract void DeleteRange<T>(IDbCommand dbCommand, IEnumerable<T> entities);

        public abstract void Update<T>(IDbCommand dbCommand, T entity);

        public abstract void UpdateRange<T>(IDbCommand dbCommand, IEnumerable<T> entities);
        
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
        
        protected object GetPocoValue<TEntity>(PropertyMapper propertyMapper, IDataReader reader, int i)
        {
            if (reader.IsDBNull(i))
            {
                return null;
            }
            
            var propertyInfo = GetPropertyInfo<TEntity>(propertyMapper.PropertyName);
            var customConverter = GetTypeConverter(propertyMapper.CustomConverter, propertyInfo.PropertyType);

            return customConverter != null
                ? customConverter.FromDatabaseValue(reader, i)
                : GetValue(propertyInfo.PropertyType, reader, i);
        }

        private static object GetValue(Type propertyType, IDataRecord reader, int i)
        {
            if (reader.IsDBNull(i))
            {
                return null;
            }
            
            if (propertyType == typeof(short))
            {
                return reader.GetInt16(i);
            }
            if (propertyType == typeof(int))
            {
                return reader.GetInt32(i);
            }
            if (propertyType == typeof(string))
            {
                return reader.GetString(i);
            }
            if (propertyType == typeof(bool))
            {
                return reader.GetBoolean(i);
            }
            if (propertyType == typeof(byte))
            {
                return reader.GetByte(i);
            }
            if (propertyType == typeof(DateTimeOffset))
            {
                return new DateTimeOffset(reader.GetDateTime(i));
            }

            var value = reader.GetValue(i);

            if (propertyType.IsInstanceOfType(value))
            {
                return value;
            }

            throw new InvalidOperationException($"unsupported <{propertyType.Name}> type");
        }

        internal protected static PropertyInfo GetPropertyInfo<TEntity>(string propertyName)
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