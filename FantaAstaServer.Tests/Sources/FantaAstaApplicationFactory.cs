// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Models.Configurations;
using FantaAstaServer.Tests.Properties;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data.Common;
using System.Diagnostics;
using Testcontainers.PostgreSql;

namespace FantaAstaServer.Tests.Sources
{
    public class FantaAstaApplicationFactory : WebApplicationFactory<Program>
    {
        private PostgreSqlContainer _postgreSqlBuilder;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var _postgreSqlBuilder = new PostgreSqlBuilder()
                .WithUsername("Gesma")
                //.WithPortBinding(5555, 5432)
                .Build();

            _postgreSqlBuilder.StartAsync().Wait();
            var npgsqlConnectionStringBuilder = new NpgsqlConnectionStringBuilder(_postgreSqlBuilder.GetConnectionString());

            Console.WriteLine($"Port is: {npgsqlConnectionStringBuilder.ConnectionString}");

            var commandPost = Resources.Db;

            using DbCommand command = new NpgsqlCommand();
            using DbConnection connection = new NpgsqlConnection(_postgreSqlBuilder.GetConnectionString());

            connection.Open();

            command.Connection = connection;
            command.CommandText = commandPost;
            command.ExecuteNonQuery();

            connection.Close();
            
            builder.ConfigureTestServices(services =>
            {
                services.PostConfigure<PostgreSqlConfig>(config => {

                    config.Port = npgsqlConnectionStringBuilder.Port;
                    config.Server = npgsqlConnectionStringBuilder.Host;
                    config.Id = npgsqlConnectionStringBuilder.Username;
                    config.Database = npgsqlConnectionStringBuilder.Database;
                    config.Password = npgsqlConnectionStringBuilder.Password;
                });
            });
        }

        protected override void Dispose(bool disposing)
        {
            //_postgreSqlBuilder.DisposeAsync().AsTask().Wait();
            base.Dispose(disposing);
        }
    }
}
