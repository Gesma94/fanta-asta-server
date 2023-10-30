// Copyright (c) Gesma94. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Fluently.Builders;
using Fluently.Interfaces;

namespace Fluently.Common
{
    internal class ProxyConfigurator
    {
        internal EntityMapBuilder<T> ApplyConfigurator<T>(IEntityConfigurator<T> configurator)
        {
            var entityMapBuilder = new EntityMapBuilder<T>();
            
            configurator.Configure(entityMapBuilder);

            return entityMapBuilder;
        }
    }
}