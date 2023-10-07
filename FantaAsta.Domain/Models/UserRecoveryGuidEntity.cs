// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Common;

namespace FantaAsta.Domain.Models;

public class UserRecoveryGuidEntity : EntityBase
{
    public Guid Guid { get; set; }
    public DateTime Timestamp { get; set; }
}