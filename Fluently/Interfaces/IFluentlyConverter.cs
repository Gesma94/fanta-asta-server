// Copyright (c) Gesma94. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.ComponentModel;
using System.Data;

namespace Fluently.Interfaces
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IFluentlyConverter
    {
        Type GetEntityType();
        object FromDatabaseValue(IDataReader reader, int ordinal);
        (string, IDbDataParameter) ToDatabaseValue(object pocoValue, Func<IDbDataParameter> dbParameterFactory, string parameterName);
    }
}