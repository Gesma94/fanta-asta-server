// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Interfaces;
using FantaAstaServer.Models;
using FantaAstaServer.Models.Domain;
using FantaAstaServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace FantaAstaServer.Controllers
{
    [Route("api/operation/")]
    public class OperationController : Controller
    {
        private readonly IDbUnitOfWork _dbUnitOfWork;

        public OperationController(IDbUnitOfWork dbUnitOfWork) => _dbUnitOfWork = dbUnitOfWork;

        [HttpPost]
        [Route("create-auction")]
        public IActionResult CreateAuction(FormCollection formCollection)
        {
            var formFile = formCollection.Files.GetFile("players");

            using var stream = formFile.OpenReadStream();
            using var streamReader = new StreamReader(stream);

            var fileContent = streamReader.ReadToEnd();

                        var players = JsonSerializer.Deserialize<IEnumerable<FootballerEntity>>(fileContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return Ok("Connection OK");
        }

        [HttpPost]
        [Route("test-auction")]
        public IActionResult TestRead()
        {
            var auctions = _dbUnitOfWork.Auctions.GetAll();
            return Ok(auctions);
        }
    }
}
