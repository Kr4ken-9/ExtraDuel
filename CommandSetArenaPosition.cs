using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;

namespace ExtraConcentratedJuice.ExtraDuel
{
    public class CommandSetArenaPosition : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "setarenaposition";

        public string Help => "Sets your positions before defining an arena.";

        public string Syntax => "/setarenaposition <1 or 2>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "extraduel.setposition" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
            if (args.Length != 1)
            {
                UnturnedChat.Say(caller, Syntax, UnityEngine.Color.red);
                return;
            }

            UnturnedPlayer uPlayer = (UnturnedPlayer)caller;
            if (Int32.TryParse(args[0], out int posNumber))
            {
                if (posNumber == 1)
                {
                    UnityEngine.Vector3 position = uPlayer.Position;
                    ExtraPlayer player = uPlayer.GetComponent<ExtraPlayer>();
                    player.selectedPos1 = position;
                    UnturnedChat.Say(Util.getTrans().Translate("extraduel_setposition_success", 1, position.ToString()), UnityEngine.Color.green);
                }
                else if (posNumber == 2)
                {
                    UnityEngine.Vector3 position = uPlayer.Position;
                    ExtraPlayer player = uPlayer.GetComponent<ExtraPlayer>();
                    player.selectedPos2 = position;
                    UnturnedChat.Say(Util.getTrans().Translate("extraduel_setposition_success", 2, position.ToString()), UnityEngine.Color.green);
                }
                else
                {
                    UnturnedChat.Say(caller, Syntax, UnityEngine.Color.red);
                }
            }
            else
            {
                UnturnedChat.Say(caller, Syntax, UnityEngine.Color.red);
            }
        }
    }
}