using System;
using Server;
using Server.Guilds;
using Server.Network;
using Server.Menus.Questions;
using Server.Gumps;

namespace Server.Menus.Questions
{
    public class GrantGuildTitleMenu : GuildMobileListMenu
    {
        public GrantGuildTitleMenu( Mobile from, Guild guild, int begin )
            : base( from, guild, begin, guild.Members, "Grant a title to another member." )
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
                m_Mobile.SendMenu( new GrantGuildTitleMenu( m_Mobile, m_Guild, m_Begin + ListSize ) );
            }
            else if ( index == m_StringList.IndexOf( "Previous page" ) ) // back
            {
                m_Mobile.SendMenu( new GrantGuildTitleMenu( m_Mobile, m_Guild, m_Begin - ListSize ) );
            }
            else
            {

                if ( index >= 0 && index < m_List.Count )
                {
                    Mobile m = (Mobile)m_List[index];

                    if ( m != null && !m.Deleted )
                    {
                        m_Mobile.SendAsciiMessage( "New title (20 characters max):" ); // New title (20 characters max):
                        m_Mobile.Prompt = new GuildTitlePrompt( m_Mobile, m, m_Guild );
                    }
                }
            }
        }
    }
}