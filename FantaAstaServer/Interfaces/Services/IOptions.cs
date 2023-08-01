﻿// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

namespace FantaAstaServer.Interfaces.Services
{
    public interface IConfigOptions
    {
        T GetConfigProperty<T>(string name);
    }
}
