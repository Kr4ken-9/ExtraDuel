using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;

namespace ExtraConcentratedJuice.ExtraDuel
{
    public class CommandArenaList : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "arenalist";

        public string Help => "List all arenas.";

        public string Syntax => "/arenalist";

        public List<string> Aliases => new List<string>() { "listarena", "arenas" };

        public List<string> Permissions => new List<string>() { "extraduel.arenalist" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
            if (ExtraDuel.instance.arenaList.Count <= 0)
            {
                UnturnedChat.Say(caller, Util.getTrans().Translate("extraduel_arenalist_none"), UnityEngine.Color.yellow);
                return;
            }
            foreach (Arena a in ExtraDuel.instance.arenaList)
            {
                UnturnedChat.Say(caller, Util.getTrans().Translate("extraduel_arenalist_message", a.name, a.pos1.ToString()), UnityEngine.Color.green);
            }
        }
    }
}