// Copyright (c) Gesma94. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Linq.Expressions;
using Fluently.Interfaces;
using Fluently.Mappers;

namespace Fluently.Builders
{
    public sealed class EntityMapBuilder<T> : IEntityMapBuilder
    {
        private readonly EntityMapper _entityMapper = new EntityMapper();
        
        public EntityMapper GetEntityMapper()
        {
            return _entityMapper;
        }
        
        public EntityMapBuilder<T> ToTable(string tableName)
        {
            _entityMapper.PocoType = typeof(T);
            _entityMapper.TableName = tableName;
        
            return this;
        }
        
        public PropertyMapBuilder<TProperty> HasKey<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            var propertyBuilder = new PropertyMapBuilder<TProperty>();

            propertyBuilder.IsKey();
            propertyBuilder.HasPropertyName(GetPropertyName(propertyExpression));
            _entityMapper.PropertyMappers.Add(propertyBuilder.GetPropertyMapper());
        
            return propertyBuilder;
        }

        public PropertyMapBuilder<TProperty> HasProperty<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            var propertyBuilder = new PropertyMapBuilder<TProperty>();

            propertyBuilder.HasPropertyName(GetPropertyName(propertyExpression));
            _entityMapper.PropertyMappers.Add(propertyBuilder.GetPropertyMapper());
        
            return propertyBuilder;
        }

        private static string GetPropertyName<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
            {
                throw new InvalidOperationException($"given expression is not a {nameof(MemberExpression)}");
            }

            return memberExpression.Member.Name;
        }
    }
}