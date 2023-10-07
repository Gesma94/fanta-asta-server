// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Common;

namespace FantaAsta.Domain.Models;

public class UserAuctionEntity : AuditableEntity
{
    public int UserId { get; set; }
    public int AuctionId { get; set; }
    public bool IsAdmin { get; set; }
}