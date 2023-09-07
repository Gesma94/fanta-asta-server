// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

namespace FantaAstaServer.Common.Constants
{
    public class Constants
    {
        public const string SmtpConfigKey = "smtp";
        public const string PostgresqlConfigKey = "postgresql";
        public const string PasswordHasherConfigKey = "passwordHasher";

        public class ErrorCode
        {
            public const string ERR_EMAIL_ALREADY_USED = nameof(ERR_USERNAME_ALREADY_USED);
            public const string ERR_USERNAME_ALREADY_USED = nameof(ERR_USERNAME_ALREADY_USED);
        }
    }
}
