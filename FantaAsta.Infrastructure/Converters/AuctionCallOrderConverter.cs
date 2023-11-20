// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using System.Data;
using FantaAsta.Domain.Enums;
using Fluently.Common;

namespace FantaAsta.Infrastructure.Converters;

public class AuctionCallOrderConverter : FluentlyConverter<AuctionCallOrder>
{
    protected override (string, IDbDataParameter) ToDatabase(AuctionCallOrder pocoValue, Func<IDbDataParameter> dbParameterFactory, string parameterName)
    {
        var dbParameter = dbParameterFactory();
        
        dbParameter.ParameterName = parameterName;
        dbParameter.Value = pocoValue switch
        {
            AuctionCallOrder.ByRole => "by_role",
            AuctionCallOrder.Unordered => "unordered",
            _ => throw new InvalidOperationException($"unsupported '{pocoValue}' value")
        };

        return (parameterName + "::auction_call_order", dbParameter);
    }

    protected override AuctionCallOrder FromDatabase(IDataReader reader, int ordinal)
    {
        var stringValue = reader.GetString(ordinal);

        return stringValue switch
        {
            "by_role" => AuctionCallOrder.ByRole,
            "unordered" => AuctionCallOrder.Unordered,
            _ => throw new InvalidOperationException("")
        };
    }
}