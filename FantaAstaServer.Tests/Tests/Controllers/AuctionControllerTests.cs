// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Models.APIs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace FantaAstaServer.Tests.Tests.Controllers
{
    public class AuctionControllerTests
    {
        [TestClass]
        public class Create
        {
            [TestMethod]
            [Ignore]
            public void InvalidUsername_BadRequest()
            {
                using var clientWrapper = new FantaAstaHttpClientWrapper();
                HttpContent inputContent = new StringContent(JsonSerializer.Serialize(new CreateAuctionRequestDto()), Encoding.UTF8, "application/json");

                var result = clientWrapper.Client.PutAsync("/api/v1/auction/create", inputContent).Result;

                Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
                //Assert.AreEqual(expectedErrorMessage, result.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
