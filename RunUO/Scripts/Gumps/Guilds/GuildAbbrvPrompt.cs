using System;
using Server;
using Server.Guilds;
using Server.Prompts;
using Server.Menus.Questions;

namespace Server.Gumps
{
	public class GuildAbbrvPrompt : Prompt
	{
		private Mobile m_Mobile;
		private Guild m_Guild;

		public GuildAbbrvPrompt( Mobile m, Guild g )
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
			if ( GuildGump.BadLeader( m_Mobile, m_Guild ) )
				return;

			text = text.Trim();

			if ( text.Length > 3 )
				text = text.Substring( 0, 3 );

			if ( text.Length > 0 )
			{
				if ( Guild.FindByAbbrev( text ) != null )
				{
					m_Mobile.SendAsciiMessage( "{0} conflicts with the abbreviation of an existing guild.", text );
				}
				else
				{
					m_Guild.Abbreviation = text;
					m_Guild.GuildTextMessage( String.Format("Your guild abbreviation has changed: {0}", text )); // Your guild abbreviation has changed:
				}
			}

			m_Mobile.SendMenu( new GuildmasterMenu( m_Mobile, m_Guild ) );
		}
	}
}