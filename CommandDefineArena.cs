using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;

namespace ExtraConcentratedJuice.ExtraDuel
{
    public class CommandDefineArena : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "definearena";

        public string Help => "Define an arena with the current set positions.";

        public string Syntax => "/definearena <name>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "extraduel.definearena" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
            if (args.Length != 1)
            {
                UnturnedChat.Say(caller, Syntax, UnityEngine.Color.red);
                return;
            }
            UnturnedPlayer uPlayer = (UnturnedPlayer)caller;
            ExtraPlayer player = uPlayer.GetComponent<ExtraPlayer>();
            if (player.selectedPos1 == null || player.selectedPos2 == null)
            {
                UnturnedChat.Say(caller, Util.getTrans().Translate("extraduel_no_position_set"), UnityEngine.Color.red);
                return;
            }
            Arena newArena = new Arena(player.selectedPos1, player.selectedPos2, args[0]);
            foreach (Arena a in ExtraDuel.instance.arenaList)
            {
                if (a.rect.Overlaps(newArena.rect))
                {
                    UnturnedChat.Say(caller, Util.getTrans().Translate("extraduel_definearena_fail_overlap"), UnityEngine.Color.red);
                    return;
                }
                if (a.name == newArena.name)
                {
                    UnturnedChat.Say(caller, Util.getTrans().Translate("extraduel_definearena_fail_same_name"), UnityEngine.Color.red);
                    return;
                }
            }
            ExtraDuel.instance.arenaList.Add(newArena);
            UnturnedChat.Say(caller, Util.getTrans().Translate("extraduel_definearena_success"), UnityEngine.Color.green);
            ExtraDuel.instance.SerializeArena(ExtraDuel.arenaPath);
        }
    }
}