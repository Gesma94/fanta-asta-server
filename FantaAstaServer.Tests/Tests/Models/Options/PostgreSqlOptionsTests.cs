// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Models.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FantaAstaServer.Tests.Tests.Models.Options
{
    public class PostgreSqlOptionsTests
    {

        [TestClass]
        public class GetConnectionString
        {
            [TestMethod]
            public void AllPropertiesAvailable_ExpectedOutput()
            {
                var postgreSqlOptions = new PostgreSqlOptions() { Database = "database", Id = "id", Password = "password", Port = 1234, Server = "server" };
                Assert.AreEqual("Port=1234;Username=id;Host=server;Password=password;Database=database", postgreSqlOptions.GetConnectionString());
            }

            [TestMethod]
            public void NullProperties_ExpectedOutput()
            {
                var postgreSqlOptions = new PostgreSqlOptions() { Database = null, Id = null, Password = null, Port = 1234, Server = null };
                Assert.AreEqual("Port=1234", postgreSqlOptions.GetConnectionString());
            }
        }
    }
}
