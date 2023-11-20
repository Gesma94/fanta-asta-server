// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using System.Data;
using FantaAsta.Domain.Enums;
using Fluently.Common;

namespace FantaAsta.Infrastructure.Converters;

public class AuctionModeConverter: FluentlyConverter<AuctionMode>
{
    protected override (string, IDbDataParameter) ToDatabase(AuctionMode pocoValue, Func<IDbDataParameter> dbParameterFactory, string parameterName)
    {
        var dbParameter = dbParameterFactory();
        
        dbParameter.ParameterName = parameterName;
        dbParameter.Value = pocoValue switch
        {
            AuctionMode.TurnBased => "turn_based",
            AuctionMode.OfferBased => "offer_based",
            _ => throw new InvalidOperationException($"unsupported '{pocoValue}' value")
        };

        return (parameterName + "::auction_mode", dbParameter);
    }

    protected override AuctionMode FromDatabase(IDataReader reader, int ordinal)
    {
        var stringValue = reader.GetString(ordinal);

        return stringValue switch
        {
            "turn_based" => AuctionMode.TurnBased,
            "offer_based" => AuctionMode.OfferBased,
            _ => throw new InvalidOperationException("")
        };
    }
}