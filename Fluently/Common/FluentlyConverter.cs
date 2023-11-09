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

        public (string, IDbDataParameter) ToDatabaseValue(object pocoValue, Func<IDbDataParameter> dbParameterFactory, string parameterName)
        {
            if (pocoValue is not T typedValue)
            {
                throw new Exception($"value '{pocoValue}' is not of type <{typeof(T).Name}>");
            }
            
            return ToDatabase(typedValue, dbParameterFactory, parameterName);
        }
        
        protected abstract (string, IDbDataParameter) ToDatabase(T pocoValue, Func<IDbDataParameter> dbParameterFactory, string parameterName);
        protected abstract T FromDatabase(IDataReader reader, int ordinal);
    }
}