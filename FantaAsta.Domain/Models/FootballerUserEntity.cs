﻿// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Common;

namespace FantaAsta.Domain.Models;

public sealed class FootballerUserEntity : AuditableEntity
{
    public int UserId { get; set; }
    public int FootballerId { get; set; }
}