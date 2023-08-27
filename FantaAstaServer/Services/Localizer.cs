// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Interfaces;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;

namespace FantaAstaServer.Services
{
    public class Localizer : ILocalizer
    {
        private readonly IStringLocalizer<Resources.LocalizedResources> _stringLocalizer;

        public Localizer(IStringLocalizer<Resources.LocalizedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer ?? throw new ArgumentNullException(nameof(stringLocalizer));
        }

        public LocalizedString this[string name] => _stringLocalizer[name];

        public LocalizedString this[string name, params object[] arguments] => _stringLocalizer[name, arguments];

        public string GetStringValue(string name)
        {
            return this[name].Value;
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => _stringLocalizer.GetAllStrings(includeParentCultures);
    }
}
