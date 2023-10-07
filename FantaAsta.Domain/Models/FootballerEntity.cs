// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Common;
using FantaAsta.Domain.Enums;

namespace FantaAsta.Domain.Models;

public class FootballerEntity : AuditableEntity
{
    public int AuctionId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Price { get; set; }
    public FootballerRole Role { get; set; }
}