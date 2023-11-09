// Copyright (c) Gesma94. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Fluently.Interfaces;

namespace Fluently.Mappers
{
    public class PropertyMapper
    {
        public string PropertyName { get; set; }
        public string ColumnName { get; set; }
        public bool IsColumnReadOnly { get; set; } = false;
        public IFluentlyConverter CustomConverter { get; set; }
        public bool IsKey { get; set; }
    }
}