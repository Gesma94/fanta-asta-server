// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Enums;

namespace FantaAstaServer.Models.DTOs
{
    public class UserAuctionDetailsDto
    {
        public string Name { get; set; }

        public int UserAmount { get; set; }

        public int CurrentUserAmount { get; set; }

        public AuctionStatus Status { get; set; }
    }
}
