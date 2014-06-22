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
    public class GuildMenu : QuestionMenu
    {
        private Mobile m_Mobile;
		private Guild m_Guild;

        public GuildMenu(Mobile beholder, Guild guild)
            : base(String.Format("{0} (Guild {1} {2})", guild.Name, guild.Leader.Female ? "Mistress" : "Master", guild.Leader.Name), null)
        {
            m_Mobile = beholder;
			m_Guild = guild;

            Mobile fealty = m_Mobile.GuildFealty;

            if ( fealty == null )
                fealty = beholder;

            string loyalty = fealty.Name;

            Answers = new string[] { "Recruit someone into the guild.",
                "View the current roster.",
                "View the guild's charter.",
                String.Format("Declare your fealty. You are currently loyal to {0}.", loyalty),
                String.Format("Toggle showing the guild's abbreviation in your name to unguilded people. Currently {0}.", beholder.DisplayGuildTitle ? "on" : "off"),
                "Resign from the guild.",
                "View list of candidates who have been sponsored to the guild.",
                String.Format("Access Guild {0} functions.", guild.Leader.Female ? "Mistress" : "Master"),
                String.Format("View list of guilds that {0} has declared war on.", guild.Name) };
        }

        public static bool BadLeader(Mobile m, Guild g)
        {
            if (m.Deleted || g.Disbanded || (m.AccessLevel < AccessLevel.GameMaster && g.Leader != m))
                return true;

            Item stone = g.Guildstone;

            return (stone == null || stone.Deleted || !m.InRange(stone.GetWorldLocation(), 2));
        }

        public static bool BadMember(Mobile m, Guild g)
        {
            if (m.Deleted || g.Disbanded || (m.AccessLevel < AccessLevel.GameMaster && !g.IsMember(m)))
                return true;

            Item stone = g.Guildstone;

            return (stone == null || stone.Deleted || !m.InRange(stone.GetWorldLocation(), 2));
        }

        public override void OnCancel(NetState state)
        {
        }

        public override void OnResponse(NetState state, int index)
        {
            if (BadMember(m_Mobile, m_Guild))
                return;

            if (index == 0) // recruit
            {
                m_Mobile.Target = new GuildRecruitTarget(m_Mobile, m_Guild);
            }
            else if (index == 1) // roster
            {
                m_Mobile.SendMenu(new GuildRosterMenu(m_Mobile, m_Guild, 0));
            }
            else if (index == 2) // charter
            {
                m_Mobile.SendMenu(new GuildCharterMenu(m_Mobile, m_Guild));
            }
            else if (index == 3) // fealty
            {
                m_Mobile.SendMenu(new DeclareFealtyMenu(m_Mobile, m_Guild, 0));
            }
            else if (index == 4) //abbreviation
            {
                m_Mobile.DisplayGuildTitle = !m_Mobile.DisplayGuildTitle;

                m_Mobile.SendMenu(new GuildMenu(m_Mobile, m_Guild));
            }
            else if (index == 5) // resign
            {
                m_Guild.RemoveMember(m_Mobile);
            }
            else if (index == 6) // candidates
            {
                m_Mobile.SendMenu(new GuildCandidatesMenu(m_Mobile, m_Guild, 0));
            }
            else if (index == 7) // guildmaster functions
            {
                if (m_Mobile.AccessLevel >= AccessLevel.GameMaster || m_Guild.Leader == m_Mobile)
                    m_Mobile.SendMenu(new GuildmasterMenu(m_Mobile, m_Guild));
            }
            else if (index == 8) // wars
            {
                m_Mobile.SendMenu(new GuildWarMenu(m_Mobile, m_Guild));
            }
        }
    }
}