using Server;
using System;
using System.Collections;
using System.Collections.Generic;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;
using Server.Items;

namespace Server.Menus.Questions
{

    public class ReportMenu : QuestionMenu
    {
        public ReportMenu(Mobile victum, List<Mobile> killers) : this(victum, killers, 0)
		{
		}

        private Mobile m_Victum;
        private List<Mobile> m_Killers;
        private int m_Idx;

        public ReportMenu( Mobile victum, List<Mobile> killers, int idx ) : base(String.Format("You have been murdered! Do you wish to report this crime to Lord British's guards?"),
                new string[] { "YES - You report them to Lord British's guards. This will increase the recorded murders under this person's name and may result in a bounty placed on their head.",
                    "NO - You forgive this person for killing you and do not report them to Lord British's guards." })
        {
            m_Killers = killers;
            m_Victum = victum;
            m_Idx = idx;
        }

        public override void OnCancel(NetState state)
        {
        }

        public override void OnResponse(NetState state, int index)
        {
            Mobile from = state.Mobile;

            if (index == 0)
            {
                foreach (Mobile killer in m_Killers)
                {
                    //add kills
                    killer.Kills++;
                    killer.ShortTermMurders++;

                    if (killer != null && !killer.Deleted)
                    {
                        if (killer is PlayerMobile)
                        {
                            ((PlayerMobile)killer).ResetKillTime();

                            //have they killed too many?
                            if (killer.Kills > 10)
                            {
                                BankBox killerbank = killer.BankBox;
                                int killerbalance = 0;
                                Item[] killergold;
                                ((PlayerMobile)killer).BountyMark = true;

                                //make them dread lord
                                killer.Karma = -127;

                                if (killerbank != null)
                                {
                                    killergold = killerbank.FindItemsByType(typeof(Gold));

                                    for (int i = 0; i < killergold.Length; ++i)
                                        killerbalance += killergold[i].Amount;

                                    killerbank.ConsumeTotal(typeof(Gold), killerbalance);
                                    ((PlayerMobile)killer).Bounty += killerbalance;

                                    killer.SendAsciiMessage("A bounty hath been issued for thee, and thy worldly goods are hereby confiscated!");

                                    //remove all items in the bank
                                    List<Item> list = new List<Item>();
                                    foreach (Item item in killerbank.Items)
                                        list.Add(item);

                                    foreach (Item i in list)
                                        i.Delete();
                                }

                                //make new bounty post
                                new BountyMessage(killer);
                            }
                        }
                    }
                }
            }
        }
    }
}