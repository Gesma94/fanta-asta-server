// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.JsonConverters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FantaAstaServer.Models.APIs
{
    public class CreateUserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly DateOfBirth { get; set; }

        public string FavouriteTeam { get; set; }

        public string City { get; set; }
    }
}