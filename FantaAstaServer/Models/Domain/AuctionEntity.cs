// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using System;
using FantaAstaServer.Enums;

namespace FantaAstaServer.Models.Domain
{
    public class AuctionEntity : EntityBase
    {
        public DateTime CreationDate { get; set; }
        public AuctionStatus Status { get; set; }
        public string Name { get; set; }
        public int UserAmount { get; set; }
        public int InitialCredit { get; set; }
        public AuctionMode Mode { get; set; }
        public int[] UsersOrder { get; set; }
        public int RealTimeTimeoutCallMs { get; set; }
        public int GoalkeaperMinAmount { get; set; }
        public int GoalkeaperMaxAmount { get; set; }
        public int DefenderMinAmount { get; set; }
        public int DefenderMaxAmount { get; set; }
        public int MidfielderMinAmount { get; set; }
        public int MidfielderMaxAmount { get; set; }
        public int StrikerMinAmount { get; set; }
        public int StrikerMaxAmount { get; set; }
    }
}
