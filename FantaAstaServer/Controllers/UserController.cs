// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Interfaces;
using FantaAstaServer.Models.APIs;
using FantaAstaServer.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FantaAstaServer.Controllers
{
    [Route("v1/user/")]
    public class UserController : Controller
    {
        private readonly IDbUnitOfWork _dbUnitOfWork;


        public UserController(IDbUnitOfWork dbUnitOfWork) => _dbUnitOfWork = dbUnitOfWork;


        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> Create(CreateUserDto createUserDto)
        {
            var newUserEntity = new UserEntity()
            {
                Username = createUserDto.Username,
                Email = createUserDto.Email,
                Password = createUserDto.Password,
                City = createUserDto.City,
                FavouriteTeam = createUserDto.FavouriteTeam,
                DateOfBirth = createUserDto.DateOfBirth,
                CraetionDate = DateTime.UtcNow
            };

            await _dbUnitOfWork.Users.Create(newUserEntity);
            await _dbUnitOfWork.SaveChanges();

            return Ok("Connection OK");
        }
    }
}
