// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Domain.Common;

namespace FantaAstaServer.Domain.Models;

public class UserRecoveryGuidEntity : EntityBase
{
    public Guid Guid { get; set; }
    public DateTime Timestamp { get; set; }
}