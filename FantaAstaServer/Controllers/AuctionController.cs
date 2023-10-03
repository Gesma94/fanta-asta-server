// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Enums;
using FantaAstaServer.Extensions;
using FantaAstaServer.Hubs;
using FantaAstaServer.Interfaces;
using FantaAstaServer.Interfaces.Services.Mappers;
using FantaAstaServer.Models.APIs;
using FantaAstaServer.Models.Domain;
using FantaAstaServer.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantaAstaServer.Interfaces.Services;

namespace FantaAstaServer.Controllers
{
    [Authorize]
    [Route("api/v1/auction/")]
    public class AuctionController : Controller
    {
        private readonly IDbUnitOfWork _dbUnitOfWork;
        private readonly IAuctionMapper _auctionMapper;
        private readonly IHubContext<AuctionHub> _auctionHub;
        private readonly IFormFileReader _formFileReader;

        public AuctionController(IDbUnitOfWork dbUnitOfWork, IAuctionMapper auctionMapper, IFormFileReader formFileReader, IHubContext<AuctionHub> auctionHub)
            => (_dbUnitOfWork, _auctionMapper, _formFileReader, _auctionHub) = (dbUnitOfWork, auctionMapper, formFileReader, auctionHub);


        /// <summary>
        /// Creates a new auction
        /// </summary>
        /// <param name="createAuctionRequestDto">Specifies all the details needed to create a new auction</param>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateAuctionRequestDto createAuctionRequestDto)
        {
            var adminUser = await _dbUnitOfWork.Users.GetByUsername(createAuctionRequestDto.AdminUsername);

            if (adminUser == null)
            {
                return BadRequest(new Error(ErrorCode.UsernameNotFound, $"Username '{createAuctionRequestDto.AdminUsername}' not found"));
            }

            var dbTransaction = _dbUnitOfWork.BeginTransaction();

            var newAuctionEntity = new AuctionEntity()
            {
                CreationDate = DateTime.UtcNow,
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

            DateTime.SpecifyKind(newAuctionEntity.CreationDate, DateTimeKind.Utc);

            await _dbUnitOfWork.Auctions.Create(newAuctionEntity);
            await _dbUnitOfWork.SaveChanges();

            var newFootballers = _formFileReader.GetJsonFootballers(createAuctionRequestDto.FootballersFile)
                .Select(x => new FootballerEntity()
                {
                    Role = x.Role,
                    Price = x.Price,
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    AuctionId = newAuctionEntity.Id
                });

            await _dbUnitOfWork.Footballers.Create(newFootballers);
            
            var newUserAuctionEntity = new UserAuctionEntity()
            {
                IsAdmin = true,
                UserId = adminUser.Id,
                AuctionId = newAuctionEntity.Id
            };

            await _dbUnitOfWork.UserAuctions.Create(newUserAuctionEntity);          
            await _dbUnitOfWork.SaveChanges();

            await dbTransaction.CommitAsync();

            return Ok();
        }

        /// <summary>
        /// Gets all the auctions that the user identified by <paramref name="userId"/> has joined
        /// </summary>
        /// <param name="userId">The id of the user for which the auctions are required</param>
        [HttpGet]
        [Route("/api/v1/auctions/user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var auctions = new List<AuctionEntity>();
            var userAuctions = await _dbUnitOfWork.UserAuctions.GetByUserId(userId);

            foreach (var userAuction in userAuctions)
            {
                auctions.Add(await _dbUnitOfWork.Auctions.Get(userAuction.AuctionId));
            }

            var auctionsDetails = auctions.Select(x => _auctionMapper.ToUserAuctionDetailsDto(x));

            return Ok(auctionsDetails);          
        }

        /// <summary>
        /// Allows the authorized user to join the auction with id <paramref name="auctionId"/>
        /// </summary>
        /// <param name="auctionId">The id of the auction that the user wants to join</param>
        [HttpPost]
        [Route("join/{auctionId}")]
        public async Task<IActionResult> Join(int auctionId)
        {
            var userId = HttpContext.GetUserIdFromCookieClaim();
            var user = await _dbUnitOfWork.Users.Get(userId);
            var auction = await _dbUnitOfWork.Auctions.Get(auctionId);
            var usersInAuction = await _dbUnitOfWork.UserAuctions.GetByAuctionId(auctionId);

            if (usersInAuction.Select(x => x.UserId).Contains(userId))
            {
                return BadRequest(new Error(ErrorCode.UserAlreadyInAuction, $"User '{user.Username}' already joined auction '{auction.Name}'"));
            }

            if (auction.Status != AuctionStatus.Created)
            {
                return BadRequest(new Error(ErrorCode.AuctionEnded, $"Cannot join auction '{auction.Name}' because is not in a proper state"));
            }

            if (usersInAuction.Count() >= auction.UserAmount)
            {
                return BadRequest(new Error(ErrorCode.AuctionFull, $"Cannot join auction '{auction.Name}' because is already full"));
            }

            var userInAuction = new UserAuctionEntity() { IsAdmin = false, UserId = userId, AuctionId = auctionId };

            await _dbUnitOfWork.UserAuctions.Create(userInAuction);
            await _dbUnitOfWork.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Starts the auction identified by <paramref name="auctionId"/>
        /// </summary>
        /// <param name="auctionId">The id of the auction that the user wants to start</param>
        [HttpPatch]
        [Route("start/{auctionId}")]
        public async Task<IActionResult> Start(int auctionId)
        {
            var userId = HttpContext.GetUserIdFromCookieClaim();
            var user = await _dbUnitOfWork.Users.Get(userId);
            var auction = await _dbUnitOfWork.Auctions.Get(auctionId);
            var usersInAuction = await _dbUnitOfWork.UserAuctions.GetByAuctionId(auctionId);

            if (auction.UserAmount != usersInAuction.Count())
            {
                return BadRequest(new Error(ErrorCode.NotEnoughUsers, $"Auction '{auction.Name}' cannot be started because there is no enough users registered"));
            }

            if (usersInAuction.Any(x => x.UserId == userId && x.IsAdmin))
            {
                return BadRequest(new Error(ErrorCode.OnlyAdminCanStart, $"Auction '{auction.Name}' cannot be started unless from its administrator"));
            }

            // TODO: Check that lobby is full

            auction.Status = AuctionStatus.Started;
            auction.UsersOrder = usersInAuction.Select(x => x.Id).ToArray();

            _dbUnitOfWork.Auctions.Update(auction);
            await _dbUnitOfWork.SaveChanges();

            return Ok();
        }
    }
}