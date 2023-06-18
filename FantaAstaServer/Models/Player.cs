// Copyright (c) 2023 - Gesma94
// This code is licensed under MIT license (see LICENSE for details)

using FantaAstaServer.Enums;

namespace FantaAstaServer.Models
{
    public record Player(string FirstName, string LastName, string Team, Role Role, int Quotation);
}
