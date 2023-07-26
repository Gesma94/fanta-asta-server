// Copyright (c) 2023 - Gesma94
// This code is licensed under MIT license (see LICENSE for details)

using System.Collections.Generic;
using FantaAstaServer.Models.Domain;

namespace FantaAstaServer.Interfaces
{
    public interface IJsonConsumer
    {
        public IEnumerable<FootballerEntity> GetPlayerCatalog(string jsonString);
    }
}
