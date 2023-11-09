// Copyright (c) Gesma94. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Fluently.Interfaces;
using Fluently.Mappers;

namespace Fluently.Builders
{
    public sealed class PropertyMapBuilder<T> : IPropertyMapBuilder
    {
        private readonly PropertyMapper _propertyMapper = new PropertyMapper();
  
        public PropertyMapper GetPropertyMapper()
        {
            return _propertyMapper;
        }
        
        public PropertyMapBuilder<T> HasPropertyName(string propertyName)
        {
            _propertyMapper.PropertyName = propertyName;
            return this;
        }
        
        public PropertyMapBuilder<T> IsReadOnly()
        {
            _propertyMapper.IsColumnReadOnly = true;
            return this;
        }
        
        public PropertyMapBuilder<T> IsKey()
        {
            _propertyMapper.IsKey = true;
            return IsReadOnly();
        }

        public PropertyMapBuilder<T> HasColumnName(string columnName)
        {
            _propertyMapper.ColumnName = columnName;
            return this;
        }

        public PropertyMapBuilder<T> HasCustomConverter<TConverter>() where TConverter : IFluentlyConverter
        {
            _propertyMapper.CustomConverter = Activator.CreateInstance<TConverter>();
            return this;
        }
    }
}