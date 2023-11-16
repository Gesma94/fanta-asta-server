// Copyright (c) Gesma94. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Fluently.Mappers
{
    public class EntityMapper
    {
        public Type PocoType { get; set; }
        public string TableName { get; set; }
        public IList<PropertyMapper> PropertyMappers { get; } = new List<PropertyMapper>();

        public PropertyMapper GetMapperByColumnName(string columnName)
        {
            return PropertyMappers.SingleOrDefault(x => x.ColumnName == columnName);
        }
        
        public PropertyMapper GetMapperByPropertyName(string propertyName)
        {
            return PropertyMappers.SingleOrDefault(x => x.PropertyName == propertyName);
        }
    }
}