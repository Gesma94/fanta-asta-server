// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace FantaAstaServer.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger _logger;


        public ErrorController(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [Route("/error")]
        public IActionResult Error()
        {
            var exceptionHandlerContext = HttpContext.Features.Get<IExceptionHandlerFeature>();

            _logger.LogError(exceptionHandlerContext.Error, exceptionHandlerContext.Error.Message);
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
