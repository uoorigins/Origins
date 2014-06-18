using System;
using Server;
using Server.Guilds;
using Server.Network;
using Server.Factions;
using Server.Menus.Questions;
using System.Collections.Generic;

namespace Server.Menus.Questions
{
    public class GuildAdminCandidatesMenu : GuildMobileListMenu
    {
        public GuildAdminCandidatesMenu( Mobile from, Guild guild, int begin )
            : base( from, guild, begin, guild.Candidates, "Accept or Refuse candidates for membership" )
        {
        }

        public override void OnCancel( NetState state )
        {
            if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
                return;

            m_Mobile.SendMenu( new GuildmasterMenu( m_Mobile, m_Guild ) );
        }

        public override void OnResponse( NetState state, int index )
        {
            if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
                return;

            if ( index == m_StringList.IndexOf( "Next page" ) ) // next
            {
                m_Mobile.SendMenu( new GuildAdminCandidatesMenu( m_Mobile, m_Guild, m_Begin + ListSize ) );
            }
            else if ( index == m_StringList.IndexOf( "Previous page" ) ) // back
            {
                m_Mobile.SendMenu( new GuildAdminCandidatesMenu( m_Mobile, m_Guild, m_Begin - ListSize ) );
            }
            else
            {
                if ( index >= 0 && index < m_List.Count )
                {
                    Mobile m = (Mobile)m_List[index];

                    if ( m != null && !m.Deleted )
                    {
                        m_Mobile.SendMenu( new GuildCandidateMenu( m_Mobile, m_Guild, m ) );
                    }
                }
            }
        }

        private class GuildCandidateMenu : QuestionMenu
        {
            private Mobile m_Mobile;
            private Guild m_Guild;
            private Mobile m_Candidate;

            public GuildCandidateMenu( Mobile from, Guild guild, Mobile candidate )
                : base( candidate.Name, null )
            {
                m_Mobile = from;
                m_Guild = guild;
                m_Candidate = candidate;

                List<String> list = new List<String>();

                list.Add( "Accept" );
                list.Add( "Reject" );

                Answers = list.ToArray();
            }

            public override void OnCancel( NetState state )
            {
                if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
                    return;

                m_Mobile.SendMenu( new GuildAdminCandidatesMenu( m_Mobile, m_Guild, 0 ) );
            }

            public override void OnResponse( NetState state, int index )
            {
                if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
                    return;

                if ( index == 0 ) //accept
                {
                    m_Guild.Candidates.Remove( m_Candidate );
                    m_Guild.Accepted.Add( m_Candidate );

                    if ( m_Guild.Candidates.Count > 0 )
                        m_Mobile.SendMenu( new GuildAdminCandidatesMenu( m_Mobile, m_Guild, 0 ) );
                    else
                        m_Mobile.SendMenu( new GuildmasterMenu( m_Mobile, m_Guild ) );
                }
                else // reject
                {
                    m_Guild.Candidates.Remove( m_Candidate );

                    if ( m_Guild.Candidates.Count > 0 )
                        m_Mobile.SendMenu( new GuildAdminCandidatesMenu( m_Mobile, m_Guild, 0 ) );
                    else
                        m_Mobile.SendMenu( new GuildmasterMenu( m_Mobile, m_Guild ) );
                }
            }
        }
    }
}