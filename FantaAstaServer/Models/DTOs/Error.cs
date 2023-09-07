// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Enums;

namespace FantaAstaServer.Models.DTOs
{
    public class Error
    {
        public Error(ErrorCode code, string message) => (Code, Message) = (code, message);


        public ErrorCode Code { get; set; }
        public string Message { get; set; }
    }
}
