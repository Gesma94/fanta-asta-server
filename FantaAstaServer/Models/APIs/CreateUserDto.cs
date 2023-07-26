// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using System;

namespace FantaAstaServer.Models.APIs
{
    public record CreateUserDto(string Username, string Password, string Email, DateOnly DateOfBirth, string FavouriteTeam, string City);
}