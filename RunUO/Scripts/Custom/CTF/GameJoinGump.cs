using System;
using Server.Gumps;
using Server.Items;
using Server.Network;

namespace Server.Gumps
{
	public class GameTeamSelector : Gump
	{
		private CTFGame m_Game;
		private int m_TeamSize;

		public GameTeamSelector( CTFGame game ) : this( game, game.TeamSize )
		{
		}

		public GameTeamSelector( CTFGame game, int teamSize ) : base( 50, 50 )
		{
			m_Game = game;
			m_TeamSize = teamSize;

			Closable = false;
			Dragable = false;

			AddPage( 0 );
			AddBackground( 0, 0, 250, 220, 5054 );
			AddBackground( 10, 10, 230, 200, 3000 );

			AddPage( 1 );
			AddLabel( 20, 20, 0, "Select a team:" );
			for (int i=0;i<m_Game.Teams.Count;i++)
			{
				CTFTeam team = (CTFTeam)m_Game.Teams[i];
				if ( team.ActiveMemberCount < m_TeamSize )
				{
					AddButton( 20, 60 + i*20, 4005, 4006, i+1, GumpButtonType.Reply, 0 );
					AddLabel( 55, 60 + i*20, 0, "Join Team " + team.Name );	
				}
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			from.CloseGump( typeof( GameTeamSelector ) );
			
			if ( m_Game.Deleted )
				return;
			
			CTFTeam team = m_Game.GetTeam( info.ButtonID - 1 );
			if ( team != null && team.ActiveMemberCount < m_TeamSize )
			{
				bool freeze = from.Frozen;

				from.Kill();
				if ( from.Corpse != null && !from.Corpse.Deleted )
					from.Corpse.Delete();
				from.Location = team.Home;
				from.Map = team.Map;
				from.Resurrect();

				from.Frozen = freeze;
				
				m_Game.SwitchTeams( from, team );

				from.SendMessage( "You have joined team {0}!", team.Name );
			}
			else
			{
				from.SendMessage( "That team is full, please try again." );
				from.SendGump( new GameTeamSelector( m_Game ) );
			}
		}
	}

	public class GameJoinGump : Gump
	{
		private CTFGame m_Game;
		public GameJoinGump( CTFGame game, string gameName ) : base( 20, 30 )
		{
			m_Game = game;

			AddPage( 0 );
			AddBackground( 0, 0, 550, 220, 5054 );
			AddBackground( 10, 10, 530, 200, 3000 );
			
			AddPage( 1 );
			AddLabel( 20, 20, 0, String.Format( "Welcome to {0}!", gameName ) );
			//AddLabel( 20, 60, 0, "Let it be known to all who join the the melee that lays within, you will not" );
			AddLabel( 20, 60, 0, "Enter the game with caution! ll non-bless/non-newbied items will be lost!" );
			AddLabel( 20, 80, 0, "All pets will be lost! Bank your items before joining, supplies" );
			AddLabel( 20, 100, 0, "will be provided.  Enjoy!" );

			AddLabel( 55, 180, 0, "Cancel" );
			AddButton( 20, 180, 4005, 4006, 0, GumpButtonType.Reply, 0 );
			AddLabel( 165, 180, 0, "Okay, Join!" );
			AddButton( 130, 180, 4005, 4006, 1, GumpButtonType.Reply, 0 );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			from.CloseGump( typeof( GameJoinGump ) );

			if ( info.ButtonID == 1 )
				from.SendGump( new GameTeamSelector( m_Game ) );
		}
	}
}
