// Copyright (c) Gesma94. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Reflection;
using Fluently.Common;
using Fluently.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Fluently
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFluently(this IServiceCollection services)
        {
            services.AddSingleton<IFluentlyContext, FluentlyContext>();
            return services;
        }
        
        public static IServiceCollection AddFluently<T>(this IServiceCollection services) where T : class, IProvider, new()
        {
            services.AddSingleton<IFluentlyContext, FluentlyContext>();
            services.AddSingleton<IProvider, T>();
            
            return services;
        }
        
        public static IServiceCollection AddFluently(this IServiceCollection services, Assembly assembly, bool loadConverters = true, bool loadConfigurators = true)
        {
            services.AddFluently();

            if (loadConverters)
            {
                services.AddConvertersFromAssembly(assembly);
            }

            if (loadConfigurators)
            {
                services.AddConfiguratorsFromAssembly(assembly);
            }

            return services;
        }
        
        public static IServiceCollection AddFluently<T>(this IServiceCollection services, Assembly assembly, bool loadConverters = true, bool loadConfigurators = true)  where T : class, IProvider, new()
        {
            services.AddFluently<T>();

            if (loadConverters)
            {
                services.AddConvertersFromAssembly(assembly);
            }

            if (loadConfigurators)
            {
                services.AddConfiguratorsFromAssembly(assembly);
            }

            return services;
        }
        
        public static IServiceCollection AddConvertersFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAbstract)
                {
                    continue;
                }
                
                if (type.GetConstructor(Type.EmptyTypes) == null)
                {
                    continue;
                }
                
                if (typeof(IFluentlyConverter).IsAssignableFrom(type))
                {
                    var objectConverter = Activator.CreateInstance(type);

                    if (!(objectConverter is IFluentlyConverter converter))
                    {
                        throw new InvalidOperationException($"created builder is not a valid {nameof(IEntityMapBuilder)}");
                    }

                    services.AddSingleton(converter);
                }
            }
            
            return services;
        }

        public static IServiceCollection AddConfiguratorsFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var proxyConfigurator = new ProxyConfigurator();
            var applyConfiguratorMethod = typeof(ProxyConfigurator).GetMethod(nameof(ProxyConfigurator.ApplyConfigurator), BindingFlags.NonPublic | BindingFlags.Instance);

            if (applyConfiguratorMethod == null)
            {
                throw new InvalidOperationException($"cannot find '{nameof(ProxyConfigurator.ApplyConfigurator)}' method");
            }
        
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAbstract)
                {
                    continue;
                }

                if (type.GetConstructor(Type.EmptyTypes) == null)
                {
                    continue;
                }

                foreach (var @interface in type.GetInterfaces())
                {
                    if (!@interface.IsGenericType)
                    {
                        continue;
                    }
                
                    if (@interface.GetGenericTypeDefinition() != typeof(IEntityConfigurator<>))
                    {
                        continue;
                    }

                    var configurator = Activator.CreateInstance(type);
                    var entityType = @interface.GenericTypeArguments[0];
                    var genericMethod = applyConfiguratorMethod.MakeGenericMethod(entityType);

                    var objBuilder = genericMethod.Invoke(proxyConfigurator, new[] { configurator });

                    if (!(objBuilder is IEntityMapBuilder builder))
                    {
                        throw new InvalidOperationException($"created builder is not a valid {nameof(IEntityMapBuilder)}");
                    }
                    
                    services.AddSingleton(builder.GetEntityMapper());
                }
            }
            
            return services;
        }
    }
}