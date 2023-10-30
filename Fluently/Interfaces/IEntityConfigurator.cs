// Copyright (c) Gesma94. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Fluently.Builders;

namespace Fluently.Interfaces
{
    public interface IEntityConfigurator<T>
    {
        void Configure(EntityMapBuilder<T> builder);
    }
}