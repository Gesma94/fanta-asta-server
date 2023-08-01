// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using System;

namespace FantaAstaServer.Interfaces.Services
{
    public interface IPasswordHasher
    {
        string ComputeHash(string password, string salt, string pepper = null, int? iterations = null);
    }
}
