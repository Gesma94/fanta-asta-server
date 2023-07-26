// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using System;

namespace FantaAstaServer.Models.Domain
{
    public class OfferEntity : EntityBase
    {
        public int BatchId { get; set; }
        public int UserId { get; set; }
        public int PreviousAmount { get; set; }
        public int OfferedAmount { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
