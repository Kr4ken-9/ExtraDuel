using Rocket.API;
using Rocket.API.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ExtraConcentratedJuice.ExtraDuel
{
    public static class Util
    {
        public static ExtraDuelConfig getConfig()
        {
            return ExtraDuel.instance.Configuration.Instance;
        }
        public static TranslationList getTrans()
        {
            return ExtraDuel.instance.Translations.Instance;
        }
        public static Vector2 Vector2(Vector3 p)
        {
            return new Vector2(p.x, p.z);
        }
        
    }
}
