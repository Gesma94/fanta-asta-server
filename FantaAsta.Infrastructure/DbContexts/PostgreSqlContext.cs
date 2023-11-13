// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using System.Data;
using FantaAsta.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Npgsql;

namespace FantaAsta.Infrastructure.DbContexts;

public class PostgreSqlContext : IDisposable
{
    public PostgreSqlContext(IOptions<PostgreSqlOptions> postgreSqlConfig)
    {
        if (postgreSqlConfig?.Value == null)
        {
            throw new ArgumentNullException(nameof(postgreSqlConfig));
        }

        DbConnection = new NpgsqlConnection(postgreSqlConfig.Value.GetConnectionString());
        DbConnection.Open();
    }

    public IDbConnection DbConnection { get; private set; }
    public IDbTransaction DbTransaction { get; private set; }

    public IDbCommand CreateCommand()
    {
        var command = DbConnection.CreateCommand();

        if (DbTransaction != null)
        {
            command.Transaction = DbTransaction;
        }

        return command;
    }

    public IDbTransaction BeginTransaction()
    {
        if (DbTransaction != null)
        {
            throw new InvalidOperationException("cannot begin a transaction when one is already started");
        }
        
        return DbTransaction = DbConnection.BeginTransaction();
    }

    public void Commit()
    {
        if (DbTransaction == null)
        {
            throw new InvalidOperationException("cannot commit if no transaction has begun");
        }
        
        DbTransaction.Commit();
        DbTransaction.Dispose();

        DbTransaction = null;
    }

    public void Rollback()
    {
        if (DbTransaction == null)
        {
            throw new InvalidOperationException("cannot rollback if no transaction has begun");
        }
        
        DbTransaction.Rollback();
        DbTransaction.Dispose();

        DbTransaction = null;
    }
    
    public void Dispose()
    {
        if (DbTransaction != null)
        {
            DbTransaction.Rollback();
            DbTransaction.Dispose();
            DbTransaction = null;
        }

        if (DbConnection != null)
        {
            DbConnection.Close();
            DbConnection.Dispose();
            DbConnection = null;
        }
        
        GC.SuppressFinalize(this);
    }
}
