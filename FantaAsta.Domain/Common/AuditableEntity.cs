// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

namespace FantaAsta.Domain.Common;

public abstract class AuditableEntity : EntityBase
{
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset LastModifiedTime { get; set; }
}