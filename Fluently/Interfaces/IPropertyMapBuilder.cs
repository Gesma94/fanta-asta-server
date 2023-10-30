// Copyright (c) Gesma94. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Fluently.Mappers;

namespace Fluently.Interfaces
{
    public interface IPropertyMapBuilder
    {
        PropertyMapper GetPropertyMapper();
    }
}