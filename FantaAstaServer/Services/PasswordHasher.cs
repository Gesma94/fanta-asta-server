// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Interfaces.Services;
using System.Text;
using System;
using FantaAstaServer.Models.Configurations;
using FantaAstaServer.Common.Constants;
using System.Security.Cryptography;

namespace FantaAstaServer.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly IConfigOptions _configOptions;


        public PasswordHasher(IConfigOptions configOptions) => _configOptions = configOptions;


        public string ComputeHash(string password, string salt, string pepper = null, int? iterations = null)
        {
            var passwordHasherConfig = _configOptions.GetConfigProperty<PasswordHasherConfig>(Constants.PasswordHasherConfigKey);
            
            pepper ??= passwordHasherConfig.Pepper;
            iterations ??= passwordHasherConfig.Iterations;

            if (iterations <= 0)
            {
                return password;
            }

            using var sha256 = SHA512.Create();
            var hashInput = $"{password}{salt}{pepper}";
            var byteValue = Encoding.UTF8.GetBytes(hashInput);
            var byteHash = sha256.ComputeHash(byteValue);
            var hash = Convert.ToBase64String(byteHash);

            return ComputeHash(hash, salt, pepper, iterations - 1);
        }
    }
}
