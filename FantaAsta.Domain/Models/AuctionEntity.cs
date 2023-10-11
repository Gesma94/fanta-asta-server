// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Common;
using FantaAsta.Domain.Enums;

namespace FantaAsta.Domain.Models;

public sealed class AuctionEntity : AuditableEntity
{
    public AuctionStatus Status { get; set; }
    public string Name { get; set; }
    public int UserSize { get; set; }
    public int InitialCredit { get; set; }
    public AuctionMode Mode { get; set; }
    public AuctionCallOrder CallOrder { get; set; }
    public int[] UsersOrder { get; set; }
    public int OfferBasedTimeoutCallMs { get; set; }
    public int GoalkeeperMinAmount { get; set; }
    public int GoalkeeperMaxAmount { get; set; }
    public int DefenderMinAmount { get; set; }
    public int DefenderMaxAmount { get; set; }
    public int MidfielderMinAmount { get; set; }
    public int MidfielderMaxAmount { get; set; }
    public int StrikerMinAmount { get; set; }
    public int StrikerMaxAmount { get; set; }
}