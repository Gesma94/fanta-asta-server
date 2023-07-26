// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using System.IO;
using FantaAstaServer.Models;
using FantaAstaServer.Interfaces;
using System.Collections.Generic;
using FantaAstaServer.Models.Domain;

namespace FantaAstaServer.Services
{
    public class JsonConsumer : IJsonConsumer
    {
        public IEnumerable<FootballerEntity> GetPlayerCatalog(string jsonPath)
        { 
            var jsonFileContent = File.ReadAllText(jsonPath);

            return System.Array.Empty<FootballerEntity>();
        }
    }
}
