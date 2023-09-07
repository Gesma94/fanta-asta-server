// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Common;
using FantaAstaServer.Interfaces;
using FantaAstaServer.Models.APIs;
using FantaAstaServer.Models.Domain;
using FantaAstaServer.Models.DTOs;
using FantaAstaServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace FantaAstaServer.Controllers
{
    [Route("api/v1/user/")]
    public class AuctionController : Controller
    {
        private readonly IDbUnitOfWork _dbUnitOfWork;


        public AuctionController(IDbUnitOfWork dbUnitOfWork)
            => (_dbUnitOfWork) = (dbUnitOfWork);


        /// <summary>
        /// Creates a new auction.
        /// </summary>
        [HttpPut]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateAuctionRequestDto createAuctionRequestDto)
        {
            try
            {
                var newAuctionEntity = new AuctionEntity()
            {
                CreationDate = DateTime.Now,
                Status = Enums.AuctionStatus.Created,
                Name = createAuctionRequestDto.Name,
                UserAmount = createAuctionRequestDto.UserAmount,
                InitialCredit = createAuctionRequestDto.InitialCredit,
                Mode = createAuctionRequestDto.AuctionMode,
                RealTimeTimeoutCallMs = createAuctionRequestDto.RealTimeTimeoutCallMs,
                GoalkeaperMaxAmount = createAuctionRequestDto.GoalkeaperMaxAmount,
                GoalkeaperMinAmount = createAuctionRequestDto.GoalkeaperMinAmount,
                DefenderMaxAmount = createAuctionRequestDto.DefenderMaxAmount,
                DefenderMinAmount = createAuctionRequestDto.DefenderMinAmount,
                MidfielderMinAmount = createAuctionRequestDto.MidfielderMinAmount,
                MidfielderMaxAmount = createAuctionRequestDto.MidfielderMaxAmount,
                StrikerMinAmount = createAuctionRequestDto.StrikerMinAmount,
                StrikerMaxAmount = createAuctionRequestDto.StrikerMaxAmount
            };

          
                await _dbUnitOfWork.Auctions.Create(newAuctionEntity);
                await _dbUnitOfWork.SaveChanges();

                return Ok("Connection OK");
            }
            catch (Exception ex)
            {
                return BadRequest("couldn't perform the operation");
            }
        }

        /// <summary>
        /// Starts an auction.
        /// </summary>
        [HttpPut]
        [Route("enter-lobby")]
        public async Task<IActionResult> EnterLobby([FromQuery] int userId, [FromQuery] int auctionId)
        {
            try
            {
                var auction = await _dbUnitOfWork.Auctions.Get(auctionId);
                var usersInAuction = await _dbUnitOfWork.UserActions.GetByAuctionId(auctionId);

                if (usersInAuction.Select(x => x.UserId).Contains(userId))
                {
                    return BadRequest("user already registered in auction");
                }

                if (auction.Status != Enums.AuctionStatus.InLobby)
                {
                    return BadRequest("cannot enter an auction if it's not in lobby state");
                }

                if (usersInAuction.Count() >= auction.UserAmount)
                {
                    return BadRequest("auction already full");
                }

                var userInAuction = new UserAuctionEntity() 
                { 
                    AuctionId = auctionId,
                    UserId = userId,
                    IsAdmin = false
                };

                await _dbUnitOfWork.UserActions.Create(userInAuction);
                await _dbUnitOfWork.SaveChanges();

                return Ok("Connection OK");
            }
            catch (Exception ex)
            {
                return BadRequest("couldn't perform the operation");
            }
        }

        /// <summary>
        /// Starts an auction.
        /// </summary>
        [HttpPut]
        [Route("start")]
        public async Task<IActionResult> Start([FromQuery] int auctionId)
        {
            try
            {
                var auction = await _dbUnitOfWork.Auctions.Get(auctionId);
                var usersInAuction = await _dbUnitOfWork.UserActions.GetByAuctionId(auctionId);

                if (auction.UserAmount != usersInAuction.Count())
                {
                    return BadRequest("no right amount of player");
                }

                // TODO Check that the admin is executing the operation

                // TODO Check that all users are in lobby (maybe to do via signalR)

                // TODO Make it random or user-defined
                auction.UsersOrder = usersInAuction.Select(x => x.Id).ToArray();
                auction.Status = Enums.AuctionStatus.Started;

                _dbUnitOfWork.Auctions.Update(auction);
                await _dbUnitOfWork.SaveChanges();

                return Ok("Connection OK");
            }
            catch (Exception ex)
            {
                return BadRequest("couldn't perform the operation");
            }
        }
    }
}