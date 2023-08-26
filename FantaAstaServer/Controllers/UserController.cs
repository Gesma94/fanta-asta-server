﻿// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Common.Constants;
using FantaAstaServer.Interfaces;
using FantaAstaServer.Interfaces.Services;
using FantaAstaServer.Models.APIs;
using FantaAstaServer.Models.Options;
using FantaAstaServer.Models.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FantaAstaServer.Controllers
{
    [Route("api/v1/user/")]
    public class UserController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly IDbUnitOfWork _dbUnitOfWork;
        private readonly IPasswordHasher _passwordHasher;


        public UserController(IEmailSender emailSender, IPasswordHasher passwordHasher, IDbUnitOfWork dbUnitOfWork)
            => (_emailSender, _passwordHasher, _dbUnitOfWork) = (emailSender, passwordHasher, dbUnitOfWork);


        /// <summary>
        /// Allows to register a new user in the system
        /// </summary>
        /// <param name="createUserDto"></param>
        [HttpPut]
        [Route("register")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
        {
            var newUserEntity = new UserEntity()
            {
                Username = createUserDto.Username,
                Email = createUserDto.Email,
                Password = _passwordHasher.ComputeHash(createUserDto.Password, createUserDto.Email),
                City = createUserDto.City,
                FavouriteTeam = createUserDto.FavouriteTeam,
                DateOfBirth = createUserDto.DateOfBirth,
                CraetionDate = DateTime.UtcNow
            };

            await _dbUnitOfWork.Users.Create(newUserEntity);
            await _dbUnitOfWork.SaveChanges();

            return Ok("Connection OK");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(string username, string password)
        {

            var user = await _dbUnitOfWork.Users.GetByUsername(username);

            if (user.Password == _passwordHasher.ComputeHash(password, user.Email))
            {
                var claimIdentity = new ClaimsIdentity(new List<Claim>() { new(ClaimTypes.Name, username) }, "Login");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));

                return Ok("Logged");
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("request-reset-password")]
        public async Task<IActionResult> RequestResetPassword([FromServices] IOptions<SmtpOptions> smtpOptionsWrapper, ResetPasswordRequestDto resetPasswordRequestDto)
        {
            var smtpOptions = smtpOptionsWrapper.Value;
            var user = await _dbUnitOfWork.Users.GetByEmail(resetPasswordRequestDto.Email);

            if (user == null)
            {
                throw new InvalidOperationException("user does not exist");
            }

            try
            {
                var subject = "FantaAsta: Recovery your password";
                var textBody = "Click <a href=\"https://www.google.com\">here</a> to reset your password";
                var mimeMessage = _emailSender.CreateMimeMessage(smtpOptions.Username, resetPasswordRequestDto.Email, subject, textBody);

                user.ResetPasswordGuid = Guid.NewGuid();
                user.ResetPasswordTimeStamp = DateTime.UtcNow;
                DateTime.SpecifyKind(user.ResetPasswordTimeStamp.Value, DateTimeKind.Utc);

                _dbUnitOfWork.Users.Update(user);
                await _dbUnitOfWork.SaveChanges();

                _emailSender.SendSslEmail(smtpOptions.Host, smtpOptions.Username, smtpOptions.Password, mimeMessage);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _dbUnitOfWork.Users.GetByEmail(resetPasswordDto.Email);

            if (user == null)
            {
                throw new InvalidOperationException("user does not exist");
            }

            if (!user.ResetPasswordGuid.HasValue)
            {
                throw new InvalidOperationException("user has not request a new password");
            }

            if (!user.ResetPasswordTimeStamp.HasValue)
            {
                throw new InvalidOperationException("user has not request a new password");
            }

            var resetPasswordGuid = user.ResetPasswordGuid.Value;
            var resetPasswordTimestamp = user.ResetPasswordTimeStamp.Value;

            if (resetPasswordGuid.ToString() != resetPasswordDto.ResetPasswordGuid)
            {
                throw new InvalidOperationException("password guid missmatch");
            }
            
            if ((DateTime.UtcNow - resetPasswordTimestamp).TotalHours > 2)
            {
                try
                {
                    user.ResetPasswordGuid = null;
                    user.ResetPasswordTimeStamp = null;

                    _dbUnitOfWork.Users.Update(user);
                    await _dbUnitOfWork.SaveChanges();
                }
                catch (Exception)
                {

                }

                throw new InvalidOperationException("user new password request has expired");
            }

            try
            {
                user.ResetPasswordGuid = null;
                user.ResetPasswordTimeStamp = null;
                user.Password = _passwordHasher.ComputeHash(resetPasswordDto.NewPassword, user.Email);

                _dbUnitOfWork.Users.Update(user);
                await _dbUnitOfWork.SaveChanges();
             
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
