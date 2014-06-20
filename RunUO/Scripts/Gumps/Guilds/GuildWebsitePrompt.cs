using System;
using Server;
using Server.Guilds;
using Server.Prompts;
using Server.Menus.Questions;

namespace Server.Gumps
{
	public class GuildWebsitePrompt : Prompt
	{
		private Mobile m_Mobile;
		private Guild m_Guild;

		public GuildWebsitePrompt( Mobile m, Guild g )
		{
			m_Mobile = m;
			m_Guild = g;
		}

		public override void OnCancel( Mobile from )
		{
			if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
				return;

			m_Mobile.SendMenu( new GuildmasterMenu( m_Mobile, m_Guild ) );
		}

		public override void OnResponse( Mobile from, string text )
		{
            if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
				return;

			text = text.Trim();

			if ( text.Length > 50 )
				text = text.Substring( 0, 50 );

			if ( text.Length > 0 )
				m_Guild.Website = text;

            m_Mobile.SendMenu( new GuildmasterMenu( m_Mobile, m_Guild ) );
		}
	}
}