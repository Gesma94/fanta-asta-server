// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

namespace FantaAstaServer.Models.Options
{
    public class PasswordHasherOptions
    {
        public string Pepper { get; set; }
        public int Iterations { get; set; }
    }
}