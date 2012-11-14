using System;
using System.Collections.Generic;
using System.Reflection;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Multis;
using Server.Menus.Questions;
using Server.Accounting;
using Server.Targeting;
using Server.Targets;
using Server.Gumps;
using Server.Engines.Help;

namespace Server.Commands
{
    public class ListNewPlayers
    {
        public static void Initialize()
        {
            CommandSystem.Register("ln", AccessLevel.Player, new CommandEventHandler(ListNP_Command));
            CommandSystem.Register("rn", AccessLevel.Player, new CommandEventHandler(LastLoc_Command));
            CommandSystem.Register("LeaveMessage", AccessLevel.Player, new CommandEventHandler(LeavMsg_Command));
            CommandSystem.Register("Nightsight", AccessLevel.GameMaster, new CommandEventHandler(Nightsight_Command));
        }

        public static void Nightsight_Command(CommandEventArgs e)
        {
            Mobile from = e.Mobile;
            if (from.BeginAction(typeof(LightCycle)))
            {
                new LightCycle.NightSightTimer(from).Start();
                from.LightLevel = LightCycle.DungeonLevel / 2;

                from.FixedParticles(0x376A, 9, 32, 5007, EffectLayer.Waist);
                from.PlaySound(0x1E3);

                BasePotion.PlayDrinkEffect(from);

            }
            else
            {
                from.SendAsciiMessage("You already have nightsight.");
            }
        }


        public static void LeavMsg_Command(CommandEventArgs e)
        {
            string toSend = e.ArgString.Trim();

            if (toSend.Length > 0)
                e.Mobile.Target = new SendMessageTarget(toSend);
        }

        private class SendMessageTarget : Target
        {
            private string m_toSend;

            public SendMessageTarget(string toSend) : base(-1, false, TargetFlags.None)
            {
                m_toSend = toSend;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Mobile)
                {
                    ((Mobile)targeted).SendGump(new MessageSentGump(from, from.Name, m_toSend));
                }
            }
        }

        public static void LastLoc_Command(CommandEventArgs e)
        {
            PlayerMobile pm = e.Mobile as PlayerMobile;

            if (pm != null && pm.Companion)
            {
                if (pm.CompanionLastLocation != Point3D.Zero)
                {
                    pm.Location = pm.CompanionLastLocation;
                    pm.CompanionLastLocation = Point3D.Zero;
                }
                else
                    pm.SendAsciiMessage("You must first teleport to a new player before returning to your last location.");
            }
        }

        public static void ListNP_Command(CommandEventArgs e)
        {
            PlayerMobile pm = e.Mobile as PlayerMobile;
            List<PlayerMobile> PlayerList = new List<PlayerMobile>();

            if (pm != null)
            {
                if (pm.Companion)
                {
                    foreach (NetState state in NetState.Instances)
                    {
                        Mobile m = state.Mobile;

                        if (m is PlayerMobile)
                        {
                            Account PlayerAccount = (Account)m.Account;
                            if (PlayerAccount.TotalGameTime <= TimeSpan.FromHours(20.0))
                                PlayerList.Add((PlayerMobile)m);
                        }
                    }
                    if (PlayerList.Count == 0)
                    {
                        pm.SendAsciiMessage("There are no new players online.");
                        return;
                    }

                    string[] Options = new string[PlayerList.Count];
                    for (int i = 0; i < PlayerList.Count; i++)
                    {
                        Options[i] = PlayerList[i].Name;
                    }

                    e.Mobile.SendMenu(new NewPlayerList(e.Mobile, PlayerList, Options));
                }
            }
        }

        private class NewPlayerList : QuestionMenu
        {
            private static List<PlayerMobile> m_PlayerList;
            private PlayerMobile m_Companion;

            public NewPlayerList(Mobile companion, List<PlayerMobile> PlayerList, string[] Options) : base("Who would you like to teleport to?", Options)
            {
                m_PlayerList = PlayerList;
                m_Companion = (PlayerMobile)companion;
            }

            public override void OnCancel(NetState state)
            {
                base.OnCancel(state);
            }

            public override void OnResponse(NetState state, int index)
            {
                Mobile m_Player = m_PlayerList[index] as Mobile;
                BaseHouse house = BaseHouse.FindHouseAt(m_Player);

                if (house != null && house.IsInside(m_Player))
                {
                    m_Companion.SendAsciiMessage("This player is inside a house and you cannot teleport to them.");
                }
                else
                {
                    m_Companion.CompanionLastLocation = m_Companion.Location;
                    m_Companion.Hidden = true;
                    m_Companion.Location = m_Player.Location;
                    m_Companion.CompanionTarget = (PlayerMobile)m_Player;
                }
            }
        }
    }
}