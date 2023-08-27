// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using Microsoft.Extensions.Localization;

namespace FantaAstaServer.Interfaces
{
    public interface ILocalizer : IStringLocalizer<Resources.LocalizedResources>
    {
        string GetStringValue(string index);
    }
}
