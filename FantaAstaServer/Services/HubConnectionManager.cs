// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using System;
using FantaAstaServer.Interfaces;
using System.Collections.Generic;

namespace FantaAstaServer.Services
{
    public class HubConnectionManager : IHubConnectionManager
    {
        private readonly Dictionary<string, IList<string>> _store = new();

        public IEnumerable<string> GetGroups(string connectionId)
        {
            return _store.TryGetValue(connectionId, out var groups) ? groups : Array.Empty<string>();
        }

        public void Register(string connectionId, string lobbyGroupName)
        {
            if (!_store.ContainsKey(connectionId)) 
            {
                _store.Add(connectionId, new List<string>());
            }

            if (!_store[connectionId].Contains(lobbyGroupName))
            {
                _store[connectionId].Add(lobbyGroupName);
            }
        }

        public void Remove(string connectionId)
        {
            if (_store.ContainsKey(connectionId)) 
            {
                _store.Remove(connectionId);
            }
        }

        public void RemoveFromGroup(string connectionId, string groupName)
        {
            if (_store.ContainsKey(connectionId) && _store[connectionId].Contains(groupName))
            {
                _store[connectionId].Remove(groupName);
            }
        }
    }
}
