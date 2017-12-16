using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using Steamworks;
using System;
using System.Collections.Generic;

namespace ExtraConcentratedJuice.ExtraDuel
{
    public class CommandChallenges : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "challenges";

        public string Help => "List all your challenges.";

        public string Syntax => "/challenges";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "extraduel.challenges" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
            UnturnedPlayer p = (UnturnedPlayer)caller;
            ExtraPlayer ep = p.GetComponent<ExtraPlayer>();
            if (ep.challenges.Count <= 0)
            {
                UnturnedChat.Say(caller, Util.getTrans().Translate("extraduel_challenges_none"), UnityEngine.Color.yellow);
                return;
            }
            foreach (string c in ep.challenges.Keys)
            {
                UnturnedPlayer player = UnturnedPlayer.FromCSteamID(new CSteamID(ulong.Parse(c)));
                UnturnedChat.Say(caller, Util.getTrans().Translate("extraduel_challenges_message", player), UnityEngine.Color.green);
            }
        }
    }
}