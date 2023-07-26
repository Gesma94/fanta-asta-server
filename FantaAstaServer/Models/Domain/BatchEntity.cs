// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Enums;
using System;

namespace FantaAstaServer.Models.Domain
{
    public class BatchEntity : EntityBase
    {
        public int AuctionId { get; set; }
        public DateTime CreationDate { get; set; }
        public int FootballerId { get; set; }
        public int InitialCost { get; set; }
        public int CurrentCost { get; set; }
        public int CurrentOwnerUserId { get; set; }
        public BatchStatus Status { get; set; }
        public int[] FoldedUsersId { get; set; }
        public int NextCallerUserId { get; set; }
        public int WinnerUserId { get; set; }
    }
}
