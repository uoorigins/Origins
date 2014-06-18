using System;
using Server;
using Server.Guilds;
using Server.Prompts;
using Server.Menus.Questions;

namespace Server.Gumps
{
	public class GuildNamePrompt : Prompt
	{
		private Mobile m_Mobile;
		private Guild m_Guild;

		public GuildNamePrompt( Mobile m, Guild g )
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

			if ( text.Length > 40 )
				text = text.Substring( 0, 40 );

			if ( text.Length > 0 )
			{
				if ( Guild.FindByName( text ) != null )
				{
					m_Mobile.SendAsciiMessage( "{0} conflicts with the name of an existing guild.", text );
				}
				else
				{
					m_Guild.Name = text;
					m_Guild.GuildTextMessage( String.Format("The name of your guild has changed: {0}", text) ); // The name of your guild has changed:
				}
			}

            m_Mobile.SendMenu( new GuildmasterMenu( m_Mobile, m_Guild ) );
		}
	}
}