using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;

namespace ExtraConcentratedJuice.ExtraDuel
{
    public class CommandChallenge : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "challenge";

        public string Help => "Challenge a player to a duel.";

        public string Syntax => "/challenge <player> <arena>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "extraduel.challenge" };

        public void Execute(IRocketPlayer caller, string[] args)
        {
            if (args.Length != 2)
            {
                UnturnedChat.Say(caller, Syntax, UnityEngine.Color.red);
                return;
            }
            UnturnedPlayer challenged = UnturnedPlayer.FromName(args[0]);

            if (challenged == null)
            {
                UnturnedChat.Say(caller, Util.getTrans().Translate("extraduel_invalid_player"), UnityEngine.Color.red);
                return;
            }
            UnturnedPlayer p = (UnturnedPlayer)caller;

            if (challenged.Equals(p))
            {
                UnturnedChat.Say(caller, Util.getTrans().Translate("extraduel_self_invoke"), UnityEngine.Color.red);
                return;
            }

            ExtraPlayer ep = challenged.GetComponent<ExtraPlayer>();
            if (!ep.acceptingChallengers)
            {
                UnturnedChat.Say(caller, Util.getTrans().Translate("extraduel_challenges_off"), UnityEngine.Color.red);
                return;
            }
            if (!ExtraDuel.ArenaExists(args[1]))
            {
                UnturnedChat.Say(caller, Util.getTrans().Translate("extraduel_removearena_fail_not_found"), UnityEngine.Color.red);
                return;
            }

            if (ep.challenges.ContainsKey(p.Id))
            {
                UnturnedChat.Say(caller, Util.getTrans().Translate("extraduel_already_challenged"), UnityEngine.Color.red);
                return;
            }

            p.GetComponent<ExtraPlayer>().CreateChallenge(challenged, DateTime.Now.AddSeconds(20), ExtraDuel.ArenaFromName(args[1]));
            UnturnedChat.Say(caller, Util.getTrans().Translate("extraduel_challenge_success", challenged.DisplayName), UnityEngine.Color.green);
        }
    }
}