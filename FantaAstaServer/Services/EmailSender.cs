// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using FantaAstaServer.Interfaces.Services;
using System;

namespace FantaAstaServer.Services
{
    public class EmailSender : IEmailSender
    {
        public MimeMessage CreateMimeMessage(string fromEmail, string toEmail, string subject, string bodyText)
        {
            if (string.IsNullOrEmpty(fromEmail))
            {
                throw new ArgumentException("from email cannot be null nor empty");
            }

            if (string.IsNullOrEmpty(toEmail))
            {
                throw new ArgumentException("to email cannot be null nor empty");
            }

            var mimeMessage = new MimeMessage { Subject = subject };

            mimeMessage.To.Add(MailboxAddress.Parse(toEmail));
            mimeMessage.From.Add(MailboxAddress.Parse(fromEmail));
            mimeMessage.Body = new TextPart(TextFormat.Html) { Text = bodyText };

            return mimeMessage;
        }

        public void SendSslEmail(string host, string username, string password, MimeMessage mimeMessage)
        {
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentException("host cannot be null nor empty");
            }

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("username cannot be null nor empty");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("password cannot be null nor empty");
            }

            if (mimeMessage == null)
            {
                throw new ArgumentException("mime message cannot be null");
            }

            using var smtp = new SmtpClient();

            smtp.Connect(host, 465, true);
            smtp.Authenticate(username, password);
            smtp.Send(mimeMessage);

            smtp.Disconnect(true);
        }
    }
}
