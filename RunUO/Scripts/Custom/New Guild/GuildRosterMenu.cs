using System;
using System.Collections;
using Server;
using Server.Guilds;
using Server.Network;
using Server.Prompts;
using Server.Targeting;
using Server.Gumps;

namespace Server.Menus.Questions
{
    public class GuildRosterMenu : QuestionMenu
    {
        public GuildRosterMenu(Mobile beholder, Guild guild)
            : base("GUILD OF                    AMZAONIG",
                new string[] { "lol lol"})
        {

        }

        public override void OnCancel(NetState state)
        {
        }

        public override void OnResponse(NetState state, int index)
        {
            //Mobile from = state.Mobile;

            /*if (index == 0) // recruit
            {
                m_Mobile.Target = new GuildRecruitTarget(m_Mobile, m_Guild);
            }
            else if (index == 1) // roster
            {
                m_Mobile.SendMenu(new GuildRosterMenu(m_Mobile, m_Guild));
            }*/
        }
    }
}