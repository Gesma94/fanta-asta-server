// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Common;
using FantaAstaServer.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace FantaAstaServer.Utils
{
    public static class LocalizedResourceUtils
    {
        public static void VerifyKeys(IEnumerable<CultureInfo> allSupportedCulturesInfo)
        {
            var allLocalizeResourceSets = new List<ResourceSet>();
            var resourceManager = new ResourceManager(typeof(LocalizedResources));
            var localizerKeys = typeof(LocalizerKey).GetFields(BindingFlags.Public | BindingFlags.Static).Where(x => x.IsLiteral);

            foreach(var cultureInfo in allSupportedCulturesInfo)
            {
                var resourceSet = resourceManager.GetResourceSet(cultureInfo, true, false);

                if (resourceSet == null)
                {
                    throw new InvalidOperationException($"Missing {cultureInfo.Name} culture localization file");
                }

                allLocalizeResourceSets.Add(resourceSet);
            }

            foreach(var localizerKey in localizerKeys)
            {
                foreach(var resourceSet in allLocalizeResourceSets)
                {
                    var rawConstantValue = localizerKey.GetRawConstantValue();

                    if (rawConstantValue == null)
                    {
                        throw new InvalidOperationException($"Cannot retrieve constant value of {localizerKey}");
                    }

                    if (rawConstantValue is not string stringConstantValue)
                    {
                        throw new InvalidOperationException($"Constant value of {localizerKey} is not a string");
                    }

                    var valueInResource = resourceSet.GetString(stringConstantValue);

                    if (valueInResource == null)
                    {
                        throw new InvalidOperationException($"Cannot find key '{localizerKey}' localize resx file");
                    }
                }
            }
        }
    }
}
