using System;
using Server;
using Server.Guilds;
using Server.Targeting;
using Server.Factions;
using Server.Menus.Questions;

namespace Server.Gumps
{
	public class GuildRecruitTarget : Target
	{
		private Mobile m_Mobile;
		private Guild m_Guild;

		public GuildRecruitTarget( Mobile m, Guild guild ) : base( 10, false, TargetFlags.None )
		{
			m_Mobile = m;
			m_Guild = guild;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( GuildGump.BadMember( m_Mobile, m_Guild ) )
				return;

			if ( targeted is Mobile )
			{
				Mobile m = (Mobile)targeted;

				if ( !m.Player )
				{
					m_Mobile.SendAsciiMessage( "You may only recruit players into the guild." ); // You may only recruit players into the guild.
				}
				else if ( !m.Alive )
				{
					m_Mobile.SendAsciiMessage( "Only the living may be recruited." ); // Only the living may be recruited.
				}
				else if ( m_Guild.IsMember( m ) )
				{
					m_Mobile.SendAsciiMessage( "They are already a guildmember!" ); // They are already a guildmember!
				}
				else if ( m_Guild.Candidates.Contains( m ) )
				{
					m_Mobile.SendAsciiMessage( "They are already a candidate." ); // They are already a candidate.
				}
				else if ( m_Guild.Accepted.Contains( m ) )
				{
					m_Mobile.SendAsciiMessage( "They have already been accepted for membership, and merely need to use the Guildstone to gain full membership." ); // They have already been accepted for membership, and merely need to use the Guildstone to gain full membership.
				}
				else if ( m.Guild != null )
				{
					m_Mobile.SendAsciiMessage( "You can only recruit candidates who are not already in a guild." ); // You can only recruit candidates who are not already in a guild.
				}

				else if ( m_Mobile.AccessLevel >= AccessLevel.GameMaster || m_Guild.Leader == m_Mobile )
				{
					m_Guild.Accepted.Add( m );
				}
				else
				{
					m_Guild.Candidates.Add( m );
				}
			}
		}

		protected override void OnTargetFinish( Mobile from )
		{
			if ( GuildMenu.BadMember( m_Mobile, m_Guild ) )
				return;

			m_Mobile.SendMenu( new GuildMenu( m_Mobile, m_Guild ) );
		}
	}
}