// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using Npgsql;

namespace FantaAsta.Infrastructure.Options;

public class PostgreSqlOptions
{
    public int Port { get; set; }
    public string Username { get; set; }
    public string Host { get; set; }
    public string Password { get; set; }
    public string Database { get; set; }

    public NpgsqlConnectionStringBuilder GetNpgsqlConnectionStringBuilder()
    {
        var npgsqlConnectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Port = Port,
            Host = Host,
            Username = Username,
            Password = Password,
            Database = Database
        };

        return npgsqlConnectionStringBuilder;
    }

    public string GetConnectionString()
    {
        return GetNpgsqlConnectionStringBuilder().ConnectionString;
    }
}