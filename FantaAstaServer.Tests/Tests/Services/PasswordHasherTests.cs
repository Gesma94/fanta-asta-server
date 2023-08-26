// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Models.Options;
using FantaAstaServer.Services;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace FantaAstaServer.Tests.Tests.Services
{
    public class PasswordHasherTests
    {
        [TestClass]
        public class ComputeHash
        {
            private const int _iterations = 7;
            private const string _pepper = "pepper";


            [TestMethod]
            public void NullParameter_ThrowException()
            {
                Assert.ThrowsException<ArgumentNullException>(() => new PasswordHasher(null));
            }

            [TestMethod]
            public void NullOptions_ThrowException()
            {
                Assert.ThrowsException<ArgumentNullException>(() => new PasswordHasher(Options.Create<PasswordHasherOptions>(null)));
            }

            [TestMethod]
            public void IterationsZero_PlainPassword()
            {
                Assert.AreEqual("password", new PasswordHasher(Options.Create(new PasswordHasherOptions())).ComputeHash("password", "irrelevant", iterations: 0));
            }

            [TestMethod]
            public void PasswordNull_ThrowException()
            {
                Assert.ThrowsException<ArgumentException>(() => new PasswordHasher(Options.Create(new PasswordHasherOptions())).ComputeHash(null, "irrelevant"));
            }

            [TestMethod]
            public void SaltNull_ThrowException()
            {
                Assert.ThrowsException<ArgumentException>(() => new PasswordHasher(Options.Create(new PasswordHasherOptions())).ComputeHash("irrelevant", null));
            }

            [TestMethod]
            [DataRow(1, "xtxP6vwZi79qqSrbDf3dN8Iauz0IbzVkJY3LBp9RU94Om7oSFvsKnrbaW2UXaneFAOW4x3d0XTfe4T7gYF+jDA==")]
            [DataRow(2, "Z5zNb1YfbayIlqLBPmPQmjnHDt8Mdi8Y15gC0ske0O4UhAPZ7KiUDvg5s+gifC8Rd9EbVxXzRsS3H9XCaCu3yA==")]
            [DataRow(3, "Zg3MI5MiV8d+81qrlViNIW7jfX8qTpsdvqJxMgWbQuWKT6eL33tQbtYPeOwKevF5WvOzoAdctn8jni71qjv4tw==")]
            public void SameInputsIncreasingIterations_ExpectedOutput(int iterations, string expectedOutput)
            {
                var configOptions = Mock.Of<IOptions<PasswordHasherOptions>>();

                Mock.Get(configOptions)
                    .SetupGet(x => x.Value)
                    .Returns(new PasswordHasherOptions() { Iterations = iterations, Pepper = _pepper });

                Assert.AreEqual(expectedOutput, new PasswordHasher(configOptions).ComputeHash("password", "salt"));
            }

            [TestMethod]
            [DataRow("salt1", "tnCsYt5IrkBmDmXni4jYPcuDFgRajLTZgI5xLI3sfsS8XJmAN2RmDaCdhIRFZ/2UjieY+TPZzbaYotiN2R7oTQ==")]
            [DataRow("salt2", "7iOoEGXyJN1cD2RF6pKGDm1rllBuRAfSov1VqVp/g8QUOgJmhK32mngHfhZECtrwfm9fC5cEAiLixCPoUgZNSQ==")]
            public void SamePasswordDifferentSalt_DifferentHashedPassword(string salt, string expectedOutput)
            {
                var configOptions = Mock.Of<IOptions<PasswordHasherOptions>>();

                Mock.Get(configOptions)
                    .SetupGet(x => x.Value)
                    .Returns(new PasswordHasherOptions() { Iterations = _iterations, Pepper = _pepper });

                Assert.AreEqual(expectedOutput, new PasswordHasher(configOptions).ComputeHash("password", salt));
            }

            [TestMethod]
            public void SamePasswordSameSalt_SameHashedPassword()
            {
                var configOptions = Mock.Of<IOptions<PasswordHasherOptions>>();

                Mock.Get(configOptions)
                    .SetupGet(x => x.Value)
                    .Returns(new PasswordHasherOptions() { Iterations = _iterations, Pepper = _pepper });

                var hashedPasswordOne = new PasswordHasher(configOptions).ComputeHash("password", "salt");
                var hashedPasswordTwo = new PasswordHasher(configOptions).ComputeHash("password", "salt");

                Assert.AreEqual(hashedPasswordOne, hashedPasswordTwo);
            }

            [TestMethod]
            public void EmptyPassword_ExpectedOutput()
            {
                var configOptions = Mock.Of<IOptions<PasswordHasherOptions>>();

                Mock.Get(configOptions)
                    .SetupGet(x => x.Value)
                    .Returns(new PasswordHasherOptions() { Iterations = _iterations, Pepper = _pepper });

                Assert.AreEqual("N2BHpP+pLR0Ala+c93BF0zO2P1ud8J3yO5zm7GGEcKv53IKEbaOUkxLQ1Ve3rKVArKNQuvREH7Q8vdnWs/lG9g==", new PasswordHasher(configOptions).ComputeHash("", "salt"));
            }
        }
    }
}
