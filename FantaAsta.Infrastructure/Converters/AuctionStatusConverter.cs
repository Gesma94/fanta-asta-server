// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using System.Data;
using FantaAsta.Domain.Enums;
using Fluently.Common;

namespace FantaAsta.Infrastructure.Converters;

public class AuctionStatusConverter : FluentlyConverter<AuctionStatus>
{
    protected override (string, IDbDataParameter) ToDatabase(AuctionStatus pocoValue, Func<IDbDataParameter> dbParameterFactory, string parameterName)
    {
        var dbParameter = dbParameterFactory();
        
        dbParameter.ParameterName = parameterName;
        dbParameter.Value = pocoValue switch
        {
            AuctionStatus.Created => "created",
            AuctionStatus.Started => "started",
            AuctionStatus.Ended => "ended",
            _ => throw new InvalidOperationException($"unsupported '{pocoValue}' value")
        };

        return (parameterName + "::auction_status", dbParameter);
    }

    protected override AuctionStatus FromDatabase(IDataReader reader, int ordinal)
    {
        var stringValue = reader.GetString(ordinal);

        return stringValue switch
        {
            "created" => AuctionStatus.Created,
            "started" => AuctionStatus.Started,
            "ended" => AuctionStatus.Ended,
            _ => throw new InvalidOperationException("")
        };
    }
}