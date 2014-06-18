using System;
using Server;
using Server.Guilds;
using Server.Network;

namespace Server.Menus.Questions
{
    public class GuildRescindDeclarationMenu : GuildListMenu
    {
        public GuildRescindDeclarationMenu( Mobile from, Guild guild, int begin )
            : base( from, guild, begin, guild.WarDeclarations, "Select the guild to rescind our invitations:" )
        {
        }

        public override void OnResponse( NetState state, int index )
        {
            if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
                return;

            if ( index == m_StringList.IndexOf( "Next page" ) ) // next
            {
                m_Mobile.SendMenu( new GuildRescindDeclarationMenu( m_Mobile, m_Guild, m_Begin + ListSize ) );
            }
            else if ( index == m_StringList.IndexOf( "Previous page" ) ) // back
            {
                m_Mobile.SendMenu( new GuildRescindDeclarationMenu( m_Mobile, m_Guild, m_Begin - ListSize ) );
            }
            else
            {
                if ( index >= 0 && index < m_List.Count )
                {
                    Guild g = (Guild)m_List[index];

                    if ( g != null )
                    {
                        m_Guild.WarDeclarations.Remove( g );
                        g.WarInvitations.Remove( m_Guild );

                        if ( m_Guild.WarDeclarations.Count > 0 )
                            m_Mobile.SendMenu( new GuildRescindDeclarationMenu( m_Mobile, m_Guild, m_Begin ) );
                        else
                            m_Mobile.SendMenu( new GuildmasterMenu( m_Mobile, m_Guild ) );
                    }
                }
            }
        }
    }
}