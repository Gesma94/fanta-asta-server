// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Common.Constants;
using FantaAstaServer.Models.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FantaAstaServer.Controllers
{
    [Route("api/test/")]
    public class TestController : Controller
    {
        private readonly IConfiguration _configuration;

        public TestController(IConfiguration configuration) => _configuration = configuration;


        [HttpGet]
        [Route("ping")]
        public IActionResult Ping()
        {
            return Ok("Connection OK");
        }

        [HttpGet]
        [Route("test-secret")]
        public IActionResult TestSecret()
        {
            var postgreSqlConfig = _configuration.GetSection(Constants.PostgresqlConfigKey).Get<PostgreSqlConfig>();
            return Ok(postgreSqlConfig.Server);
        }
    }
}
