using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Rocket.Core.Logging;
using Rocket.Unturned;
using Rocket.Unturned.Events;

namespace ExtraConcentratedJuice.ExtraDuel
{
    public class ExtraPlayer : UnturnedPlayerComponent
    {
        static System.Random rand = new System.Random();
        public bool isInArena;
        public ArenaGame game;
        public Vector3 selectedPos1;
        public Vector3 selectedPos2;
        public Vector3 lastPosition;
        public Dictionary<string, Challenge> challenges;
        public bool acceptingChallengers;
        public DateTime lastUpdated;

        public enum TerminationReasons
        {
            DENIED,
            EXPIRED,
            LEAVE,
            TOGGLE_CHALLENGES,
            ENTERED_CHALLENGE
        };

        protected override void Load()
        {
            acceptingChallengers = true;
            lastUpdated = DateTime.Now;
            challenges = new Dictionary<string, Challenge>();
            isInArena = false;
            if (!Player.HasPermission("extraduel.can_spawn_in_arena"))
            {
                foreach (Arena a in ExtraDuel.instance.arenaList)
                {
                    if (a.IsInArena(Player.Position))
                    {

                        int rando = rand.Next(LevelPlayers.spawns.Count);
                        PlayerSpawnpoint spawn = LevelPlayers.spawns[rando];
                        Player.Teleport(spawn.point, Player.Rotation);
                        break;
                    }
                }
            }
            lastPosition = Player.Position;
        }

        public void OnDisconnect(UnturnedPlayer player)
        {
            if (challenges.ContainsKey(player.Id))
            {
                TryTerminateChallenge(player.Id, TerminationReasons.LEAVE);
            }
        }

        private void FixedUpdate()
        {
            List<string> keys = new List<string>(challenges.Keys);
            foreach (string key in keys)
            {
                if ((challenges[key].expiry - DateTime.Now).TotalSeconds < 0)
                {
                    TryTerminateChallenge(key, TerminationReasons.EXPIRED);
                }
            }
        }

        public void TryTerminateChallenge(string p, TerminationReasons r)
        {
            string reason;
            if (challenges.TryGetValue(p, out Challenge v))
            {
                switch (r)
                {
                    case TerminationReasons.DENIED:
                        reason = "Your challenge was denied.";
                        break;
                    case TerminationReasons.ENTERED_CHALLENGE:
                        reason = "The challenged player entered a duel.";
                        break;
                    case TerminationReasons.EXPIRED:
                        reason = "Your challenge with that player has expired.";
                        break;
                    case TerminationReasons.LEAVE:
                        reason = "That player has left.";
                        break;
                    case TerminationReasons.TOGGLE_CHALLENGES:
                        reason = "That player has turned their challenges off.";
                        break;
                    default:
                        reason = "No defined reason.";
                        break;
                }
                UnturnedChat.Say(v.challenger, Util.getTrans().Translate("extraduel_challenge_terminated", v.challenged.DisplayName, reason), UnityEngine.Color.red);
                challenges.Remove(p);
            }
        }

        public void CreateChallenge(UnturnedPlayer p, DateTime expiry, Arena arena)
        {
            ExtraPlayer ep = p.GetComponent<ExtraPlayer>();
            ep.challenges.Add(Player.Id, new Challenge(Player, p, arena, expiry));
            UnturnedChat.Say(p, Util.getTrans().Translate("extraduel_challenge", arena.name, Player.DisplayName), UnityEngine.Color.yellow);
        }
    }

}
