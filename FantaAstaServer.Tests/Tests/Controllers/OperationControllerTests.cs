// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Controllers;
using FantaAstaServer.Services;
using FantaAstaServer.Tests.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace FantaAstaServer.Tests.Tests.Controllers
{
    [TestClass]
    public class OperationControllerTests
    {
        [TestClass]
        public class CreateAuction
        {
            [TestMethod]
            public void AppleJuice()
            {
                var operationController = new OperationController();

                using var stream = new MemoryStream();
                stream.Write(Resources.OnePlayer);
                stream.Seek(0, SeekOrigin.Begin);
                var formFile = new FormFile(stream, 0, stream.Length, "players", "onePlayer.json");
                var formFileCollection = new FormFileCollection() { formFile  };
                var formCollection = new FormCollection(new Dictionary<string, StringValues>(), formFileCollection);

                operationController.CreateAuction(formCollection);

                Assert.ThrowsException<FileNotFoundException>(() => new JsonConsumer().GetPlayerCatalog("TotallyInvalidPath"));
            }
        }
    }
}
