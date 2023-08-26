using DotNet.Testcontainers.Builders;
using FantaAstaServer.Controllers;
using FantaAstaServer.Interfaces.Services;
using FantaAstaServer.Models.APIs;
using FantaAstaServer.Models.Configurations;
using FantaAstaServer.Tests.Properties;
using FantaAstaServer.Tests.Sources;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Testcontainers.PostgreSql;

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]
namespace FantaAstaServer.Tests.Tests.Controllers
{
    
    [TestClass]
    public class UserControllerTests
    {
        private static FantaAstaApplicationFactory _factory = new FantaAstaApplicationFactory();

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _factory.Dispose();
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

    [TestClass]
    public class UserControllerTests2
    { 
        [TestMethod]
        public void AppleJuice()
        {
        Assert.AreEqual(2, 2);

        }
    }
}
