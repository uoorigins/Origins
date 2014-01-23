using System;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Menus.Questions;
using System.Collections.Generic;

namespace Server.Gumps
{
    public class GameTeamSelector : QuestionMenu
	{
		private CTFGame m_Game;
		private int m_TeamSize;

		public GameTeamSelector( CTFGame game ) : this( game, game.TeamSize )
		{
		}

		public GameTeamSelector( CTFGame game, int teamSize ) : base( "Select a Team", null )
		{
            List<String> mTeams = new List<String>();

			m_Game = game;
			m_TeamSize = teamSize;

			for (int i=0;i<m_Game.Teams.Count;i++)
			{
				CTFTeam team = (CTFTeam)m_Game.Teams[i];
				if ( team.ActiveMemberCount < m_TeamSize )
				{
					mTeams.Add( "Join Team " + team.Name );	
				}
			}

            Answers = mTeams.ToArray();
		}

        public override void OnResponse(NetState state, int index)
		{
			Mobile from = state.Mobile;
			
			if ( m_Game.Deleted )
				return;
			
			CTFTeam team = m_Game.GetTeam( index );
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

                from.SendAsciiMessage("You have joined team {0}!", team.Name);
			}
			else
			{
                from.SendAsciiMessage("That team is full, please try again.");
				from.SendMenu( new GameTeamSelector( m_Game ) );
			}
		}
	}

	public class GameJoinGump : QuestionMenu
	{
		private CTFGame m_Game;
        public GameJoinGump(CTFGame game, string gameName)
            : base(String.Format("Welcome to {0}! Enter the game with caution! All non-bless/non-newbied items will be lost! All pets will be lost! Bank your items before joining, supplies will be provided.  Enjoy!", gameName), null)
		{
			m_Game = game;

            List<String> mAnswers = new List<String>();

			mAnswers.Add( "Cancel" );
            mAnswers.Add( "Okay, Join!" );

            Answers = mAnswers.ToArray();
		}

        public override void OnResponse(NetState state, int index)
		{
			Mobile from = state.Mobile;

			if ( index == 1 )
				from.SendMenu( new GameTeamSelector( m_Game ) );
		}
	}
}
