// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Enums;

namespace FantaAstaServer.Models.Domain
{
    public class FootballerEntity : EntityBase
    {
        public int AuctionId { get; set; }
        public int UserOwnerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Price { get; set; }
        public FootballerRole Role { get; set; }
    }
}
