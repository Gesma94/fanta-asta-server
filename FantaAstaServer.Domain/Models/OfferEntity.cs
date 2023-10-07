// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Domain.Common;

namespace FantaAstaServer.Domain.Models;

public class OfferEntity : AuditableEntity
{
    public int BatchId { get; set; }
    public int UserId { get; set; }
    public int PreviousOfferId { get; set; }
    public int OfferedAmount { get; set; }
}