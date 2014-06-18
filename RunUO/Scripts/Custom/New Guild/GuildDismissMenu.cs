using System;
using Server;
using Server.Guilds;
using Server.Network;
using Server.Menus.Questions;

namespace Server.Menus.Questions
{
    public class GuildDismissMenu : GuildMobileListMenu
    {
        public GuildDismissMenu( Mobile from, Guild guild, int begin )
            : base( from, guild, begin, guild.Members, "Whom do you wish to dismiss?" )
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
                m_Mobile.SendMenu( new GuildDismissMenu( m_Mobile, m_Guild, m_Begin + ListSize ) );
            }
            else if ( index == m_StringList.IndexOf( "Previous page" ) ) // back
            {
                m_Mobile.SendMenu( new GuildDismissMenu( m_Mobile, m_Guild, m_Begin - ListSize ) );
            }
            else
            {
                if ( index >= 0 && index < m_List.Count )
                {
                    Mobile m = (Mobile)m_List[index];

                    if ( m != null && !m.Deleted )
                    {
                        m_Guild.RemoveMember( m );

                        if ( m_Mobile.AccessLevel >= AccessLevel.GameMaster || m_Mobile == m_Guild.Leader )
                        {
                            m_Mobile.SendMenu( new GuildmasterMenu( m_Mobile, m_Guild ) );
                        }
                    }
                }
            }
        }
    }
}