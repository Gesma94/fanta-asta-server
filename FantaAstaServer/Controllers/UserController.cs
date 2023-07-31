// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Common.Constants;
using FantaAstaServer.Interfaces;
using FantaAstaServer.Interfaces.Services;
using FantaAstaServer.Models.APIs;
using FantaAstaServer.Models.Configurations;
using FantaAstaServer.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace FantaAstaServer.Controllers
{
    [Route("v1/user/")]
    public class UserController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly IDbUnitOfWork _dbUnitOfWork;
        private readonly IConfiguration _configuration;


        public UserController(IConfiguration configuration, IEmailSender emailSender, IDbUnitOfWork dbUnitOfWork)
            => (_configuration, _emailSender, _dbUnitOfWork) = (configuration, emailSender, dbUnitOfWork);


        /// <summary>
        /// Allows to register a new user in the system
        /// </summary>
        /// <param name="createUserDto"></param>
        [HttpGet]
        [Route("register")]
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


        [HttpPost]
        [Route("request-reset-password")]
        public async Task<IActionResult> RequestResetPassword([FromBody] ResetPasswordRequestDto resetPasswordRequestDto)
        {
            var user = await _dbUnitOfWork.Users.GetByEmail(resetPasswordRequestDto.Email);
            var smptConfig = _configuration.GetSection(Constants.SmtpConfigKey).Get<SmtpConfig>();

            if (user == null)
            {
                throw new InvalidOperationException("user does not exist");
            }

            try
            {
                var subject = "FantaAsta: Recovery your password";
                var textBody = "Click <a href=\"https://www.google.com\">here</a> to reset your password";
                var mimeMessage = _emailSender.CreateMimeMessage(smptConfig.Username, resetPasswordRequestDto.Email, subject, textBody);

                user.ResetPasswordGuid = Guid.NewGuid();
                user.ResetPasswordTimeStamp = DateTime.UtcNow;
                DateTime.SpecifyKind(user.ResetPasswordTimeStamp.Value, DateTimeKind.Utc);

                _dbUnitOfWork.Users.Update(user);
                await _dbUnitOfWork.SaveChanges();

                _emailSender.SendSslEmail(smptConfig.Host, smptConfig.Username, smptConfig.Password, mimeMessage);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
