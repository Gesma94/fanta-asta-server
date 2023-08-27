// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Models.APIs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace FantaAstaServer.Tests.Tests.Controllers
{
    public class UserControllerTests
    {
        [TestClass]
        public class Register
        {
            [TestMethod]
            [DataRow("/api/v1/user/register", "The provided parameter is not valid")]
            [DataRow("/api/v1/en-US/user/register", "The provided parameter is not valid")]
            [DataRow("/api/v1/it-IT/user/register", "Il parametro fornito non è valido")]
            public void NullParameter_BadRequest(string requestUri, string expectedErrorMessage)
            {
                using var clientWrapper = new FantaAstaHttpClientWrapper();
                HttpContent inputContent = new StringContent(JsonSerializer.Serialize<CreateUserDto>(null), Encoding.UTF8, "application/json");
                
                var result = clientWrapper.Client.PutAsync(requestUri, inputContent).Result;

                Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
                Assert.AreEqual(expectedErrorMessage, result.Content.ReadAsStringAsync().Result);
            }

            [TestMethod]
            [DataRow("/api/v1/user/register", "The provided parameter is not valid")]
            [DataRow("/api/v1/en-US/user/register", "The provided parameter is not valid")]
            [DataRow("/api/v1/it-IT/user/register", "Il parametro fornito non è valido")]
            public void UsernameNull_BadRequest(string requestUri, string expectedErrorMessage)
            {
                using var clientWrapper = new FantaAstaHttpClientWrapper();
                HttpContent inputContent = new StringContent(JsonSerializer.Serialize(new CreateUserDto()
                {
                    Username = null,
                    City = "irrelevant",
                    Email = "irrelevant",
                    Password = "irrelevant",
                    FavouriteTeam = "irrelevant",
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Now)
                }), Encoding.UTF8, "application/json");

                var result = clientWrapper.Client.PutAsync(requestUri, inputContent).Result;

                Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
                Assert.AreEqual(expectedErrorMessage, result.Content.ReadAsStringAsync().Result);
            }

            [TestMethod]
            [DataRow("/api/v1/user/register", "The provided parameter is not valid")]
            [DataRow("/api/v1/en-US/user/register", "The provided parameter is not valid")]
            [DataRow("/api/v1/it-IT/user/register", "Il parametro fornito non è valido")]
            public void PasswordNull_BadRequest(string requestUri, string expectedErrorMessage)
            {
                using var clientWrapper = new FantaAstaHttpClientWrapper();
                HttpContent inputContent = new StringContent(JsonSerializer.Serialize(new CreateUserDto()
                {
                    Password = null,
                    City = "irrelevant",
                    Email = "irrelevant",
                    Username = "irrelevant",
                    FavouriteTeam = "irrelevant",
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Now)
                }), Encoding.UTF8, "application/json");

                var result = clientWrapper.Client.PutAsync(requestUri, inputContent).Result;

                Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
                Assert.AreEqual(expectedErrorMessage, result.Content.ReadAsStringAsync().Result);
            }

            [TestMethod]
            [DataRow("/api/v1/user/register", "The provided parameter is not valid")]
            [DataRow("/api/v1/en-US/user/register", "The provided parameter is not valid")]
            [DataRow("/api/v1/it-IT/user/register", "Il parametro fornito non è valido")]
            public void EmailNull_BadRequest(string requestUri, string expectedErrorMessage)
            {
                using var clientWrapper = new FantaAstaHttpClientWrapper();
                HttpContent inputContent = new StringContent(JsonSerializer.Serialize(new CreateUserDto()
                {
                    Email = null,
                    City = "irrelevant",
                    Password = "irrelevant",
                    Username = "irrelevant",
                    FavouriteTeam = "irrelevant",
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Now)
                }), Encoding.UTF8, "application/json");

                var result = clientWrapper.Client.PutAsync(requestUri, inputContent).Result;

                Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
                Assert.AreEqual(expectedErrorMessage, result.Content.ReadAsStringAsync().Result);
            }
        }

        [TestMethod]
        public void AppleJuice()
        {
            using var clientWrapper = new FantaAstaHttpClientWrapper();

            string inputJson = JsonSerializer.Serialize(new CreateUserDto()
            {
                Username = "username",
                Password = "password",
                Email = "email",
                DateOfBirth = new DateOnly(),
                FavouriteTeam = "fav team",
                City = "city"
            });

            HttpContent inputContent = new StringContent(inputJson, Encoding.UTF8, "application/json");
            clientWrapper.Client.PutAsync("/api/v1/user/register", inputContent).Wait();           
        }

        [TestMethod]
        public void AppleJuice2()
        {
            using var clientWrapper = new FantaAstaHttpClientWrapper();
            string inputJson = JsonSerializer.Serialize(new CreateUserDto()
            {
                Username = "username",
                Password = "password",
                Email = "email",
                DateOfBirth = new DateOnly(),
                FavouriteTeam = "fav team",
                City = "city"
            });

            HttpContent inputContent = new StringContent(inputJson, Encoding.UTF8, "application/json");
            clientWrapper.Client.PutAsync("/api/v1/user/register", inputContent).Wait();
        }
    }
}
