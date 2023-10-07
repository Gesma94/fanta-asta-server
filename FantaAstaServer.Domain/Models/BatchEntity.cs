// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Domain.Common;
using FantaAstaServer.Domain.Enums;

namespace FantaAstaServer.Domain.Models;

public class BatchEntity : AuditableEntity
{
    public int AuctionId { get; set; }
    public int FootballerId { get; set; }
    public int InitialCost { get; set; }
    public int LastCallerOffer { get; set; }
    public int LastCallerUserId { get; set; }
    public BatchStatus Status { get; set; }
    public int[] FoldedUsersId { get; set; }
    public int NextCallerUserId { get; set; }
}