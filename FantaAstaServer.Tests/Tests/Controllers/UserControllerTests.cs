// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Enums;
using FantaAstaServer.Interfaces.Repositories;
using FantaAstaServer.Models.APIs;
using FantaAstaServer.Models.DTOs;
using FantaAstaServer.Tests.Sources;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace FantaAstaServer.Tests.Tests.Controllers
{
    public class UserControllerTests
    {
        [TestClass]
        [Ignore]
        public class Register
        {
            [TestMethod]
            [DataRow("it-IT", "Il parametro fornito non è valido")]
            [DataRow("en-US", "The provided parameter is not valid")]
            public void NullParameter_BadRequest(string culture, string expectedErrorMessage)
            {
                using var clientWrapper = new FantaAstaHttpClientWrapper();
                clientWrapper.Client.DefaultRequestHeaders.Add("Accept-Language", culture);
                HttpContent inputContent = new StringContent(JsonSerializer.Serialize<CreateUserDto>(null), Encoding.UTF8, "application/json");

                var result = clientWrapper.Client.PutAsync("/api/v1/user/register", inputContent).Result;

                Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
                Assert.AreEqual(expectedErrorMessage, result.Content.ReadAsStringAsync().Result);
            }

            [TestMethod]
            [DataRow("it-IT", "Il parametro fornito non è valido")]
            [DataRow("en-US", "The provided parameter is not valid")]
            public void UsernameNull_BadRequest(string culture, string expectedErrorMessage)
            {
                using var clientWrapper = new FantaAstaHttpClientWrapper();
                clientWrapper.Client.DefaultRequestHeaders.Add("Accept-Language", culture);
                HttpContent inputContent = new StringContent(JsonSerializer.Serialize(new CreateUserDto()
                {
                    Username = null,
                    City = "irrelevant",
                    Email = "irrelevant",
                    Password = "irrelevant",
                    FavouriteTeam = "irrelevant",
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Now)
                }), Encoding.UTF8, "application/json");

                var result = clientWrapper.Client.PutAsync("/api/v1/user/register", inputContent).Result;

                Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
                Assert.AreEqual(expectedErrorMessage, result.Content.ReadAsStringAsync().Result);
            }

            [TestMethod]
            [DataRow("it-IT", "Il parametro fornito non è valido")]
            [DataRow("en-US", "The provided parameter is not valid")]
            public void PasswordNull_BadRequest(string culture, string expectedErrorMessage)
            {
                using var clientWrapper = new FantaAstaHttpClientWrapper();
                clientWrapper.Client.DefaultRequestHeaders.Add("Accept-Language", culture);
                HttpContent inputContent = new StringContent(JsonSerializer.Serialize(new CreateUserDto()
                {
                    Password = null,
                    City = "irrelevant",
                    Email = "irrelevant",
                    Username = "irrelevant",
                    FavouriteTeam = "irrelevant",
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Now)
                }), Encoding.UTF8, "application/json");

                var result = clientWrapper.Client.PutAsync("/api/v1/user/register", inputContent).Result;

                Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
                Assert.AreEqual(expectedErrorMessage, result.Content.ReadAsStringAsync().Result);
            }

            [TestMethod]
            [DataRow("it-IT", "Il parametro fornito non è valido")]
            [DataRow("en-US", "The provided parameter is not valid")]
            public void EmailNull_BadRequest(string culture, string expectedErrorMessage)
            {
                using var clientWrapper = new FantaAstaHttpClientWrapper();
                clientWrapper.Client.DefaultRequestHeaders.Add("Accept-Language", culture);
                HttpContent inputContent = new StringContent(JsonSerializer.Serialize(new CreateUserDto()
                {
                    Email = null,
                    City = "irrelevant",
                    Password = "irrelevant",
                    Username = "irrelevant",
                    FavouriteTeam = "irrelevant",
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Now)
                }), Encoding.UTF8, "application/json");

                var result = clientWrapper.Client.PutAsync("/api/v1/user/register", inputContent).Result;

                Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
                Assert.AreEqual(expectedErrorMessage, result.Content.ReadAsStringAsync().Result);
            }

            [TestMethod]
            public void EmailAlreadyUsed_BadRequest()
            {
                using var clientWrapper = new FantaAstaHttpClientWrapper();
                HttpContent inputContent1 = new StringContent(JsonSerializer.Serialize(new CreateUserDto()
                {
                    City = "city",
                    Password = "password",
                    Username = "username1",
                    Email = "fake@gmail.com",
                    FavouriteTeam = "favourite team",
                    DateOfBirth = new DateOnly(1946, 8, 12)
                }), Encoding.UTF8, "application/json");
                HttpContent inputContent2 = new StringContent(JsonSerializer.Serialize(new CreateUserDto()
                {
                    City = "city",
                    Password = "password",
                    Username = "username2",
                    Email = "fake@gmail.com",
                    FavouriteTeam = "favourite team",
                    DateOfBirth = new DateOnly(1946, 8, 12)
                }), Encoding.UTF8, "application/json");

                clientWrapper.Client.PutAsync("/api/v1/user/register", inputContent1).Wait();
                var result = clientWrapper.Client.PutAsync("/api/v1/user/register", inputContent2).Result;

                Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
                var error = ReadContent<Error>(result.Content);

                Assert.AreEqual(ErrorCode.EmailAlreadyUsed, error.Code);
                Assert.AreEqual("Email is already used", error.Message);
            }

            [TestMethod]
            public void DuplicateUser_BadRequest()
            {
                using var clientWrapper = new FantaAstaHttpClientWrapper();
                HttpContent inputContent1 = new StringContent(JsonSerializer.Serialize(new CreateUserDto()
                {
                    City = "city",
                    Password = "password",
                    Username = "username",
                    Email = "fak1e@gmail.com",
                    FavouriteTeam = "favourite team",
                    DateOfBirth = new DateOnly(1946, 8, 12)
                }), Encoding.UTF8, "application/json");
                HttpContent inputContent2 = new StringContent(JsonSerializer.Serialize(new CreateUserDto()
                {
                    City = "city",
                    Password = "password",
                    Username = "username",
                    Email = "fake2@gmail.com",
                    FavouriteTeam = "favourite team",
                    DateOfBirth = new DateOnly(1946, 8, 12)
                }), Encoding.UTF8, "application/json");

                clientWrapper.Client.PutAsync("/api/v1/user/register", inputContent1).Wait();
                var result = clientWrapper.Client.PutAsync("/api/v1/user/register", inputContent2).Result;

                Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
                var error = ReadContent<Error>(result.Content);

                Assert.AreEqual(ErrorCode.UsernameAlreadyUsed, error.Code);
                Assert.AreEqual("Username is already used", error.Message);
            }

            private static T ReadContent<T>(HttpContent httpContent)
            {
                var stringContent = httpContent.ReadAsStringAsync().Result;
                return  JsonSerializer.Deserialize<T>(stringContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }
    }
}
