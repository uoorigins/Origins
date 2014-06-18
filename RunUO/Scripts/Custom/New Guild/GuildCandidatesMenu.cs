using System;
using Server;
using Server.Guilds;
using Server.Network;
using Server.Menus.Questions;

namespace Server.Menus.Questions
{
	public class GuildCandidatesMenu: GuildMobileListMenu
	{
        public GuildCandidatesMenu( Mobile from, Guild guild, int begin )
            : base( from, guild, begin, guild.Candidates, "Candidates" )
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
                m_Mobile.SendMenu( new GuildMenu( m_Mobile, m_Guild ) );
            }
		}
	}
}