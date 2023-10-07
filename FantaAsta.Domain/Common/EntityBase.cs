// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

namespace FantaAsta.Domain.Common;

public abstract class EntityBase
{
    public int Id { get; set; }

    public override bool Equals(object obj)
    {
        return obj is EntityBase compareTo && (ReferenceEquals(this, compareTo) || Id.Equals(compareTo.Id));
    }

    public override int GetHashCode()
    {
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        return (GetType().GetHashCode() * 10007) + Id.GetHashCode();
    }

    public static bool operator ==(EntityBase a, EntityBase b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(EntityBase a, EntityBase b)
    {
        return !(a == b);
    }
}