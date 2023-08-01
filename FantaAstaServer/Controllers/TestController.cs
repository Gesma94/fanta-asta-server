// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Common.Constants;
using FantaAstaServer.Interfaces.Services;
using FantaAstaServer.Models.Configurations;
using Microsoft.AspNetCore.Mvc;

namespace FantaAstaServer.Controllers
{
    [Route("api/test/")]
    public class TestController : Controller
    {
        private readonly IConfigOptions _configOptions;

        public TestController(IConfigOptions configOptions) => _configOptions = configOptions;


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
            var postgreSqlConfig = _configOptions.GetConfigProperty<PostgreSqlConfig>(Constants.PostgresqlConfigKey);
            return Ok(postgreSqlConfig.Server);
        }
    }
}
