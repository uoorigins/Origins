using System;
using Server;
using Server.Guilds;
using Server.Prompts;
using Server.Menus.Questions;

namespace Server.Gumps
{
	public class GuildTitlePrompt : Prompt
	{
		private Mobile m_Leader, m_Target;
		private Guild m_Guild;

		public GuildTitlePrompt( Mobile leader, Mobile target, Guild g )
		{
			m_Leader = leader;
			m_Target = target;
			m_Guild = g;
		}

		public override void OnCancel( Mobile from )
		{
			if ( GuildMenu.BadLeader( m_Leader, m_Guild ) )
				return;
			else if ( m_Target.Deleted || !m_Guild.IsMember( m_Target ) )
				return;

			m_Leader.SendMenu( new GuildmasterMenu( m_Leader, m_Guild ) );
		}

		public override void OnResponse( Mobile from, string text )
		{
			if ( GuildMenu.BadLeader( m_Leader, m_Guild ) )
				return;
			else if ( m_Target.Deleted || !m_Guild.IsMember( m_Target ) )
				return;

			text = text.Trim();

			if ( text.Length > 20 )
				text = text.Substring( 0, 20  );

			if ( text.Length > 0 )
				m_Target.GuildTitle = text;

			m_Leader.SendMenu( new GuildmasterMenu( m_Leader, m_Guild ) );
		}
	}
}