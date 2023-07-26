// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

namespace FantaAstaServer.Models.Settings
{
    public class PostgreSqlConfig
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
    }
}