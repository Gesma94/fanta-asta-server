// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Linq;

namespace FantaAstaServer.Hubs
{
    public class AuctionHub : Hub
    {

        private readonly IDbUnitOfWork _dbUnitOfWork;


        public AuctionHub(IDbUnitOfWork dbUnitOfWork)
            => (_dbUnitOfWork) = (dbUnitOfWork);


        public async void JoinAuction(int userId, int auctionId)
        {
            var auction = await _dbUnitOfWork.Auctions.Get(auctionId);

            if (auction.Status != Enums.AuctionStatus.Created)
            {
                // cannot join lobby if auction is not in created state
                return;
            }

            var allUserActions = await _dbUnitOfWork.UserAuctions.GetByAuctionId(auctionId);

            if (!allUserActions.Any(x => x.UserId.Equals(userId)))
            {
                // cannot join lobby if user does not belong to it
                return;
            }

            
            await Groups.AddToGroupAsync(Context.ConnectionId, GetLobbyGroupName(auctionId));
            await Clients.Group(GetLobbyGroupName(auctionId)).SendAsync("UserJoined", userId);
        }

        public async void LeaveLobby(int userId, int auctionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetLobbyGroupName(auctionId));
            await Clients.Group(GetLobbyGroupName(auctionId)).SendAsync("UserLeft", userId);
        }


        internal string GetLobbyGroupName(int auctionId)
        {
            return $"Lobby_{auctionId}";
        }
    }
}
