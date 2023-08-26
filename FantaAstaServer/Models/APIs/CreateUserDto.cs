// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.JsonConverters;
using System;
using System.Text.Json.Serialization;

namespace FantaAstaServer.Models.APIs
{
    //public record CreateUserDto(string Username, string Password, string Email, [property:JsonConverter(typeof(DateOnlyJsonConverter))] DateOnly DateOfBirth, string FavouriteTeam, string City);
    public class CreateUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly DateOfBirth { get; set; }
        public string FavouriteTeam { get; set; }
        public string City { get; set; }
    }
}