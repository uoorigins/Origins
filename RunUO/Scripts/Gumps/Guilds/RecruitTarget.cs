using System;
using Server;
using Server.Guilds;
using Server.Targeting;
using Server.Factions;

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

				PlayerState guildState = PlayerState.Find( m_Guild.Leader );
				PlayerState targetState = PlayerState.Find( m );

				Faction guildFaction = ( guildState == null ? null : guildState.Faction );
				Faction targetFaction = ( targetState == null ? null : targetState.Faction );

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
				#region Factions
				else if ( guildFaction != targetFaction )
				{
					if ( guildFaction == null )
						m_Mobile.SendLocalizedMessage( 1013027 ); // That player cannot join a non-faction guild.
					else if ( targetFaction == null )
						m_Mobile.SendLocalizedMessage( 1013026 ); // That player must be in a faction before joining this guild.
					else
						m_Mobile.SendLocalizedMessage( 1013028 ); // That person has a different faction affiliation.
				}
				else if ( targetState != null && targetState.IsLeaving )
				{
					// OSI does this quite strangely, so we'll just do it this way
					m_Mobile.SendMessage( "That person is quitting their faction and so you may not recruit them." );
				}
				#endregion
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
			if ( GuildGump.BadMember( m_Mobile, m_Guild ) )
				return;

			GuildGump.EnsureClosed( m_Mobile );
			m_Mobile.SendGump( new GuildGump( m_Mobile, m_Guild ) );
		}
	}
}