using System;
using Server;
using Server.Guilds;
using Server.Network;
using Server.Menus.Questions;
using System.Collections.Generic;

namespace Server.Menus.Questions
{
    public class DeclareFealtyMenu : GuildMobileListMenu
    {
        public DeclareFealtyMenu( Mobile from, Guild guild, int begin )
            : base( from, guild, begin, guild.Members, "Declare your fealty" )
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

            if ( index == m_StringList.IndexOf( "Next page" ) ) // next
            {
                m_Mobile.SendMenu( new DeclareFealtyMenu( m_Mobile, m_Guild, m_Begin + ListSize ) );
            }
            else if ( index == m_StringList.IndexOf( "Previous page" ) ) // back
            {
                m_Mobile.SendMenu( new DeclareFealtyMenu( m_Mobile, m_Guild, m_Begin - ListSize ) );
            }
            else
            {
                if ( index >= 0 && index < m_List.Count )
                {
                    Mobile m = (Mobile)m_List[index];

                    if ( m != null && !m.Deleted )
                    {
                        state.Mobile.GuildFealty = m;
                    }
                }

                m_Mobile.SendMenu( new GuildMenu( m_Mobile, m_Guild ) );
            }
        }
    }
}
