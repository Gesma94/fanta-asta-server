// Copyright (c) 2023 - Gesma94
// This code is licensed under MIT license (see LICENSE for details)

using FantaAstaServer.Models;
using System.Collections.Generic;

namespace FantaAstaServer.Interfaces
{
    public interface IJsonConsumer
    {
        public IEnumerable<Player> GetPlayerCatalog(string jsonString);
    }
}
