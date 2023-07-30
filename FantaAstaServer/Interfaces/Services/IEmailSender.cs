// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using MimeKit;

namespace FantaAstaServer.Interfaces.Services
{
    public interface IEmailSender
    {
        MimeMessage CreateMimeMessage(string fromEmail, string toEmail, string subject, string bodyText);

        void SendSslEmail(string host, string username, string password, MimeMessage mimeMessage);
    }
}
