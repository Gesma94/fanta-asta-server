// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MimeKit;

namespace FantaAstaServer.Tests.Tests.Services
{
    public class EmailSenderTests
    {
        [TestClass]
        public class CreateMimeMessage
        {
            [TestMethod]
            public void EmptyFromEmail_ThrowException()
            {
                Assert.ThrowsException<ArgumentException>(() => new EmailSender().CreateMimeMessage("", "irrelevant", "irrelevant", "irrelevant"));
            }

            [TestMethod]
            public void NullFromEmail_ThrowException()
            {
                Assert.ThrowsException<ArgumentException>(() => new EmailSender().CreateMimeMessage(null, "irrelevant", "irrelevant", "irrelevant"));
            }

            [TestMethod]
            public void EmptyToEmail_ThrowException()
            {
                Assert.ThrowsException<ArgumentException>(() => new EmailSender().CreateMimeMessage("irrelevant", "", "irrelevant", "irrelevant"));
            }

            [TestMethod]
            public void NullToEmail_ThrowException()
            {
                Assert.ThrowsException<ArgumentException>(() => new EmailSender().CreateMimeMessage("irrelevant", null, "irrelevant", "irrelevant"));
            }

            [TestMethod]
            public void ValidInput_ExpectedOutput()
            {
                var emailSender = new EmailSender();

                var mimeMessage = emailSender.CreateMimeMessage("fromEmail", "toEmail", "Subject", "bodyText");

                Assert.AreEqual("Subject", mimeMessage.Subject);
                Assert.IsInstanceOfType(mimeMessage.Body, typeof(TextPart));
                Assert.AreEqual("toEmail", mimeMessage.To.Single().ToString());
                Assert.AreEqual("fromEmail", mimeMessage.From.Single().ToString());

                if (mimeMessage.Body is TextPart textPart)
                {
                    Assert.AreEqual("bodyText", textPart.Text);
                    Assert.AreEqual("plain", textPart.ContentType.MediaSubtype);
                }
            }
        }
    }
}
