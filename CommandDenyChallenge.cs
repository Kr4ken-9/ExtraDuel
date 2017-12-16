using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;

namespace ExtraConcentratedJuice.ExtraDuel
{
    public class CommandDenyChallenge : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "denychallenge";

        public string Help => "Deny a challenge from a player.";

        public string Syntax => "/denychallenge <player>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "extraduel.denychallenge" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
            if (args.Length != 1)
            {
                UnturnedChat.Say(caller, Syntax, UnityEngine.Color.red);
                return;
            }
            UnturnedPlayer challenger = UnturnedPlayer.FromName(args[0]);
            if (challenger == null)
            {
                UnturnedChat.Say(caller, Util.getTrans().Translate("extraduel_invalid_player"), UnityEngine.Color.red);
                return;
            }
            UnturnedPlayer p = (UnturnedPlayer)caller;
            ExtraPlayer ep = p.GetComponent<ExtraPlayer>();
            if (!ep.challenges.ContainsKey(challenger.Id))
            {
                UnturnedChat.Say(caller, Util.getTrans().Translate("extraduel_no_challenge"), UnityEngine.Color.red);
                return;
            }
            ep.TryTerminateChallenge(challenger.Id, ExtraPlayer.TerminationReasons.DENIED);
            UnturnedChat.Say(caller, Util.getTrans().Translate("extraduel_challenge_deny_success"), UnityEngine.Color.green);
        }
    }
}