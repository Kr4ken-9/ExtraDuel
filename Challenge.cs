﻿using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtraConcentratedJuice.ExtraDuel
{
    public class Challenge
    {
        public UnturnedPlayer challenger;
        public UnturnedPlayer challenged;
        public Arena arena;
        public DateTime expiry;

        public Challenge(UnturnedPlayer c, UnturnedPlayer c2, Arena a, DateTime e)
        {
            challenger = c;
            challenged = c2;
            arena = a;
            expiry = e;
        }
    }
}
