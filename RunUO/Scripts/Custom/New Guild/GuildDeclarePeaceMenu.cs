using System;
using Server;
using Server.Guilds;
using Server.Network;
using Server.Menus.Questions;

namespace Server.Menus.Questions
{
    public class GuildDeclarePeaceMenu : GuildListMenu
    {
        public GuildDeclarePeaceMenu( Mobile from, Guild guild, int begin )
            : base( from, guild, begin, guild.Enemies, "Select the guild you wish to declare peace with." )
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
                m_Mobile.SendMenu( new GuildDeclarePeaceMenu( m_Mobile, m_Guild, m_Begin + ListSize ) );
            }
            else if ( index == m_StringList.IndexOf( "Previous page" ) ) // back
            {
                m_Mobile.SendMenu( new GuildDeclarePeaceMenu( m_Mobile, m_Guild, m_Begin - ListSize ) );
            }
            else
            {
                if ( index >= 0 && index < m_List.Count )
                {
                    Guild g = (Guild)m_List[index];

                    if ( g != null )
                    {
                        m_Guild.RemoveEnemy( g );
                        m_Guild.GuildTextMessage( String.Format( "Guild Message: You are now at peace with this guild: {0} ({1})", g.Name, g.Abbreviation ) ); // Guild Message: You are now at peace with this guild:

                        if ( m_Guild.Enemies.Count > 0 )
                            m_Mobile.SendMenu( new GuildDeclarePeaceMenu( m_Mobile, m_Guild, m_Begin ) );
                        else
                            m_Mobile.SendMenu( new GuildmasterMenu( m_Mobile, m_Guild ) );
                    }
                }
            }
        }
    }
}