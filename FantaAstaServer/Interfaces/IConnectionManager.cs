using System.Collections.Generic;

namespace FantaAstaServer.Interfaces
{
    public interface IHubConnectionManager
    {
        IEnumerable<string> GetGroups(string connectionId);
        void Register(string connectionId, string lobbyGroupName);
        void Remove(string connectionId);
        void RemoveFromGroup(string connectionId, string groupName);
    }
}
