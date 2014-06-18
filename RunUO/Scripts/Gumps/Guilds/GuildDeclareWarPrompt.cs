using System;
using System.Collections;
using Server;
using Server.Guilds;
using Server.Prompts;
using System.Collections.Generic;
using Server.Menus.Questions;

namespace Server.Gumps
{
	public class GuildDeclareWarPrompt : Prompt
	{
		private Mobile m_Mobile;
		private Guild m_Guild;

		public GuildDeclareWarPrompt( Mobile m, Guild g )
		{
			m_Mobile = m;
			m_Guild = g;
		}

		public override void OnCancel( Mobile from )
		{
			if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
				return;

			m_Mobile.SendMenu( new GuildWarAdminMenu( m_Mobile, m_Guild ) );
		}

		public override void OnResponse( Mobile from, string text )
		{
			if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
				return;

			text = text.Trim();

			if ( text.Length >= 3 )
			{
				List<Guild> guilds = Utility.CastConvertList<BaseGuild, Guild>( Guild.Search( text ) );

				if ( guilds.Count > 0 )
				{
					m_Mobile.SendMenu( new GuildDeclareWarMenu( m_Mobile, m_Guild, 0, guilds ) );
				}
				else
				{
					m_Mobile.SendMenu( new GuildWarAdminMenu( m_Mobile, m_Guild ) );
					m_Mobile.SendAsciiMessage( "No guilds found matching - try another name in the search" ); // No guilds found matching - try another name in the search
				}
			}
			else
			{
				m_Mobile.SendAsciiMessage( "Search string must be at least three letters in length." );
			}
		}
	}
}