using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;

namespace ExtraConcentratedJuice.ExtraDuel
{
    public class CommandRemoveArena : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "removearena";

        public string Help => "Removes an arena.";

        public string Syntax => "/removearena <arena name>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "extraduel.removearena" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
            if (args.Length != 1)
            {
                UnturnedChat.Say(caller, Syntax, UnityEngine.Color.red);
                return;
            }
            for (int i = 0; i < ExtraDuel.instance.arenaList.Count; i++)
            {
                if (ExtraDuel.instance.arenaList[i].name == args[0])
                {
                    ExtraDuel.instance.arenaList.RemoveAt(i);
                    UnturnedChat.Say(caller, Util.getTrans().Translate("extraduel_removearena_success"), UnityEngine.Color.green);
                    ExtraDuel.instance.SerializeArena(ExtraDuel.arenaPath);
                    return;
                }
            }
            UnturnedChat.Say(caller, Util.getTrans().Translate("extraduel_removearena_fail_not_found"), UnityEngine.Color.red);
        }
    }
}