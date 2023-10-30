// Copyright (c) Gesma94. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Data;
using Fluently.Interfaces;

namespace Fluently.Common
{
    public abstract class FluentlyConverter<T> : IFluentlyConverter
    {
        public Type GetEntityType()
        {
            return typeof(T);
        }

        public object FromDatabaseValue(IDataReader reader, int ordinal)
        {
            return FromDatabase(reader, ordinal);
        }

        public object ToDatabaseValue(object value)
        {
            if (!(value is T typedValue))
            {
                throw new Exception($"value '{value}' is not of type <{typeof(T).Name}>");
            }
            
            return ToDatabase(typedValue);
        }
        
        protected abstract object ToDatabase(T value);
        protected abstract T FromDatabase(IDataReader reader, int ordinal);
    }
}