// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace FantaAstaServer.Tests.Tests.Services
{
    [TestClass]
    public class JsonConsumerTests
    {
        [TestClass]
        public class GetPlayerCatalog
        {
            [TestMethod]
            public void InvalidPath()
            {
                Assert.ThrowsException<FileNotFoundException>(() => new JsonConsumer().GetPlayerCatalog("TotallyInvalidPath"));
            }
        }
    }
}
