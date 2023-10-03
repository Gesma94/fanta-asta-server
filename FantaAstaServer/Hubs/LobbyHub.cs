using FantaAstaServer.Enums;
using FantaAstaServer.Extensions;
using FantaAstaServer.Interfaces;
using FantaAstaServer.Models.APIs;
using FantaAstaServer.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantaAstaServer.Hubs
{
    [Authorize]
    public class LobbyHub : Hub
    {
        private readonly IDbUnitOfWork _dbUnitOfWork;
        private readonly IHubConnectionManager _connectionManager;

        public LobbyHub(IHubConnectionManager connectionManager, IDbUnitOfWork dbUnitOfWork)
            => (_connectionManager, _dbUnitOfWork) = (connectionManager, dbUnitOfWork);

        [HubMethodName("join/{auctionId}")]
        public async Task<HubMethodResult> JoinAuction(int auctionId)
        {
            var userId = Context.GetUserIdFromCookieClaim();
            var user = await _dbUnitOfWork.Users.Get(userId);
            var auction = await _dbUnitOfWork.Auctions.Get(auctionId);
            var userAuctions = await _dbUnitOfWork.UserAuctions.GetByAuctionId(auctionId);

            if (auction.Status == AuctionStatus.Ended)
            {
                return new HubMethodResult(new Error(ErrorCode.AuctionEnded, $"Auction '{auction.Name}' has ended"));
            }

            if (!userAuctions.Any(x => x.UserId.Equals(userId)))
            {
                return new HubMethodResult(new Error(ErrorCode.UserNotRegisteredInAuction, $"User '{user.Username}' does not partecipate to auction '{auction.Name}'"));
            }

            var lobbyGroupName = GetLobbyGroupName(auctionId);

            if (lobbyGroupName.Contains(Context.ConnectionId))
            {
                return new HubMethodResult(new Error(ErrorCode.UserNotRegisteredInAuction, $"User '{user.Username}' already in lobby"));
            }

            _connectionManager.Register(Context.ConnectionId, lobbyGroupName);
            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyGroupName);
            await Clients.Group(lobbyGroupName).SendAsync("UserJoined", userId);

            return HubMethodResult.Success;
        }

        [HubMethodName("leave/{auctionId}")]
        public async void LeaveLobby(int auctionId)
        {
            var groupName = GetLobbyGroupName(auctionId);
            var userId = Context.GetUserIdFromCookieClaim();

            _connectionManager.RemoveFromGroup(Context.ConnectionId, groupName);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetLobbyGroupName(auctionId));
            await Clients.Group(GetLobbyGroupName(auctionId)).SendAsync("UserLeft", userId);
        }


        public async override Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.GetUserIdFromCookieClaim();
            var groupNames = _connectionManager.GetGroups(Context.ConnectionId);

            _connectionManager.Remove(Context.ConnectionId);

            foreach(var groupName in groupNames)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
                await Clients.Group(groupName).SendAsync("UserLeft", userId);
            }
        }

        internal string GetLobbyGroupName(int auctionId)
        {
            return $"Lobby_{auctionId}";
        }
    }
}
