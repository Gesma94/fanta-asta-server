// Copyright (c) 2023 - Gesma94
// This code is licensed under MIT license (see LICENSE for details)

using Microsoft.AspNetCore.Mvc;

namespace FantaAstaServer.Controllers
{
    [Route("api/test/")]
    public class TestController : Controller
    {
        [HttpGet]
        [Route("ping")]
        public IActionResult Ping()
        {
            return Ok("Connection OK");
        }
    }
}
