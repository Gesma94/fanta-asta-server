// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Models.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FantaAstaServer.Controllers
{
    [Route("api/test/")]
    public class TestController : Controller
    {
        private readonly PostgreSqlConfig _postgreSqlConfig;


        public TestController(IOptions<PostgreSqlConfig> postgreSqlConfig)
            => _postgreSqlConfig = postgreSqlConfig.Value;


        [HttpGet]
        [Route("ping")]
        public IActionResult Ping()
        {
            return Ok("Connection OK");
        }

        [HttpGet]
        [Route("ping-auth")]
        [Authorize]
        public IActionResult PingAuthorized()
        {
            return Ok("Connection OK");
        }

        [HttpGet]
        [Route("test-secret")]
        public IActionResult TestSecret()
        {
            return Ok(_postgreSqlConfig.Server);
        }
    }
}
