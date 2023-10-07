// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Domain.Common;

namespace FantaAstaServer.Domain.Models;

public class FootballerUserEntity : AuditableEntity
{
    public int UserId { get; set; }
    public int FootballerId { get; set; }
}