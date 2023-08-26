// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Interfaces.Services;
using System.Text;
using System;
using FantaAstaServer.Models.Configurations;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace FantaAstaServer.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly PasswordHasherConfig _passwordHasherConfig;


        public PasswordHasher(IOptions<PasswordHasherConfig> passwordHasherConfig) => _passwordHasherConfig = passwordHasherConfig.Value;


        public string ComputeHash(string password, string salt, string pepper = null, int? iterations = null)
        {
            if (password == null)
            {
                throw new ArgumentException("password to hash cannot be null");
            }

            if (salt == null)
            {
                throw new ArgumentException("salt value cannot be null");
            }

            if (iterations <= 0)
            {
                return password;
            }

            
            pepper ??= _passwordHasherConfig.Pepper;
            iterations ??= _passwordHasherConfig.Iterations;

            using var sha512 = SHA512.Create();
            var hashInput = $"{password}{salt}{pepper}";
            var byteValue = Encoding.UTF8.GetBytes(hashInput);
            var byteHash = sha512.ComputeHash(byteValue);
            var hash = Convert.ToBase64String(byteHash);

            return ComputeHash(hash, salt, pepper, iterations - 1);
        }
    }
}
