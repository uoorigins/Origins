using System;
using Server;
using Server.Guilds;
using Server.Network;
using Server.Menus.Questions;

namespace Server.Menus.Questions
{
    public class GuildAcceptWarMenu : GuildListMenu
    {
        public GuildAcceptWarMenu( Mobile from, Guild guild, int begin )
            : base( from, guild, begin, guild.WarInvitations, "Select the guild to accept the invitations:" )
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
                m_Mobile.SendMenu( new GuildAcceptWarMenu( m_Mobile, m_Guild, m_Begin + ListSize ) );
            }
            else if ( index == m_StringList.IndexOf( "Previous page" ) ) // back
            {
                m_Mobile.SendMenu( new GuildAcceptWarMenu( m_Mobile, m_Guild, m_Begin - ListSize ) );
            }
            else
            {
                if ( index >= 0 && index < m_List.Count )
                {
                    Guild g = (Guild)m_List[index];

                    if ( g != null )
                    {
                        m_Guild.WarInvitations.Remove( g );
                        g.WarDeclarations.Remove( m_Guild );

                        m_Guild.AddEnemy( g );
                        m_Guild.GuildTextMessage( String.Format( "Guild Message: Your guild is now at war: {0} ({1})", g.Name, g.Abbreviation ) );

                        if ( m_Guild.WarInvitations.Count > 0 )
                            m_Mobile.SendMenu( new GuildAcceptWarMenu( m_Mobile, m_Guild, m_Begin ) );
                        else
                            m_Mobile.SendMenu( new GuildmasterMenu( m_Mobile, m_Guild ) );
                    }
                }
            }
        }
    }
}