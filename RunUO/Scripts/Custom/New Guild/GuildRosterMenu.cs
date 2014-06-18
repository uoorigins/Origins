using System;
using System.Collections;
using Server;
using Server.Guilds;
using Server.Network;
using Server.Prompts;
using Server.Targeting;
using Server.Gumps;
using System.Collections.Generic;

namespace Server.Menus.Questions
{
    public class GuildRosterMenu : GuildMobileListMenu
    {

        public GuildRosterMenu( Mobile from, Guild guild, int begin )
            : base( from, guild, begin, guild.Members, "Guild Roster" )
        {
        }

        public override void OnCancel( NetState state )
        {
            if ( GuildMenu.BadMember( m_Mobile, m_Guild ) )
                return;

            m_Mobile.SendMenu( new GuildMenu( m_Mobile, m_Guild ) );
        }

        public override void OnResponse( NetState state, int index )
        {
            if ( GuildMenu.BadMember( m_Mobile, m_Guild ) )
                return;

            Mobile from = state.Mobile;
            
            if (index == m_StringList.IndexOf("Next page")) // next
            {
                m_Mobile.SendMenu( new GuildRosterMenu( m_Mobile, m_Guild, m_Begin + 11 ) );
            }
            else if ( index == m_StringList.IndexOf( "Previous page" ) ) // back
            {
                m_Mobile.SendMenu( new GuildRosterMenu( m_Mobile, m_Guild, m_Begin - 11 ) );
            }
            else
            {
                m_Mobile.SendMenu( new GuildMenu( m_Mobile, m_Guild ) );
            }
        }
    }
}