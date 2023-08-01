// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace FantaAstaServer.Services
{
    public class ConfigOptions : IConfigOptions
    {
        private readonly IConfiguration _configuration;


        public ConfigOptions(IConfiguration configuration) => _configuration = configuration;


        public T GetConfigProperty<T>(string name)
        {
            return _configuration.GetSection(name).Get<T>();
        }
    }
}
