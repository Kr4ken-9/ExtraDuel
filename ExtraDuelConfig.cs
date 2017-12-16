using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtraConcentratedJuice.ExtraDuel
{
    public class ExtraDuelConfig : IRocketPluginConfiguration
    {
        public int updateTime;
        public void LoadDefaults()
        {
            updateTime = 1;
        }
    }
}
