// Copyright (c) 2023 - Gesma94
// This code is licensed under MIT license (see LICENSE for details)

using System.IO;
using FantaAstaServer.Models;
using FantaAstaServer.Interfaces;
using System.Collections.Generic;

namespace FantaAstaServer.Services
{
    public class JsonConsumer : IJsonConsumer
    {
        public IEnumerable<Player> GetPlayerCatalog(string jsonPath)
        { 
            var jsonFileContent = File.ReadAllText(jsonPath);

            return System.Array.Empty<Player>();
        }
    }
}
