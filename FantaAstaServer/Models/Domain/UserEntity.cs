﻿// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.JsonConverters;
using System;
using System.Text.Json.Serialization;

namespace FantaAstaServer.Models.Domain
{
    public class UserEntity : EntityBase 
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Guid? ResetPasswordGuid { get; set; }
        public DateTime? ResetPasswordTimeStamp { get; set; }
        public DateTime CreationDate { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly? DateOfBirth { get; set; }
        public string FavouriteTeam { get; set; }
        public string City { get; set; }
    }
}
