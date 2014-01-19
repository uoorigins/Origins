using System;
using System.Collections;
using Server;

namespace Server.Items
{
	public class DDWayPoint : BaseAddon, IGameFlag
	{
		private DDTeamControl m_TeamControl;

		private CTFTeam m_TeamOwner;
		private CTFGame m_Game;
		private DDWayPoint m_OtherPoint;
		private Timer m_Timer;
		private bool m_CanDom;

		[Constructable]
		public DDWayPoint()
		{
			this.ItemID = 0x519;
			this.Name = "Waypoint";

			AddComponent( new DDWayStep( 0x7A8 ), -1, -1, -5 );
			AddComponent( new DDWayStep( 0x7A6 ),  0, -1, -5 );
			AddComponent( new DDWayStep( 0x7AA ),  1, -1, -5 );

			AddComponent( new DDWayStep( 0x7A5 ),  1,  0, -5 );

			AddComponent( new DDWayStep( 0x7A9 ),  1,  1, -5 );
			AddComponent( new DDWayStep( 0x7A4 ),  0,  1, -5 );
			AddComponent( new DDWayStep( 0x7AB ), -1,  1, -5 );

			AddComponent( new DDWayStep( 0x7A7 ), -1,  0, -5 );

			ReturnToHome();
		}

		public DDWayPoint( Serial serial ) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)1 );

			writer.Write( m_TeamControl );
			writer.Write( m_Game );
			writer.Write( m_OtherPoint );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_TeamControl = reader.ReadItem() as DDTeamControl;
					goto case 0;
				}
				case 0:
				{
					m_Game = reader.ReadItem() as CTFGame;
					m_OtherPoint = reader.ReadItem() as DDWayPoint;
					break;
				}
			}

			new InitTimer( this ).Start();
		}

		public void ReturnToHome()
		{
			CanDom = true;
			TeamOwner = null;
			if ( m_OtherPoint != null )
				m_OtherPoint.TeamOwner = null;
			if ( m_Timer != null && m_Timer.Running )
				m_Timer.Stop();
			m_Timer = null;
		}

		private class InitTimer : Timer
		{
			private DDWayPoint m_Point;

			public InitTimer( DDWayPoint pt ) : base( TimeSpan.Zero )
			{
				m_Point = pt;
			}

			protected override void OnTick()
			{
				m_Point.TeamOwner = null;
				m_Point.CanDom = true;
			}
		}

		public override bool ShareHue{ get{ return false; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool CanDom
		{ 
			get { return m_CanDom && m_TeamControl != null && m_OtherPoint != null; } 
			set
			{ 
				m_CanDom = value; 
				this.Visible = m_CanDom;
				for (int i=0;i<Components.Count;i++)
					((Item)Components[i]).Visible = m_CanDom;
				if ( m_OtherPoint != null )
				{
					m_OtherPoint.m_CanDom = value;
					m_OtherPoint.Visible = m_CanDom;
					for (int i=0;i<m_OtherPoint.Components.Count;i++)
						((Item)m_OtherPoint.Components[i]).Visible = m_CanDom;
				}
			} 
		}

		[CommandProperty( AccessLevel.Seer )]
		public DDTeamControl TeamControl
		{
			get { return m_TeamControl; }
			set
			{
				if ( m_TeamControl == value )
					return;
				if ( m_TeamControl != null && m_TeamControl.Team != null )
					m_TeamControl.Team.Flag = null;
				m_TeamControl = value;
				if ( m_TeamControl != null && m_TeamControl.Team != null )
					m_TeamControl.Team.Flag = this;
			}
		}

		[CommandProperty( AccessLevel.Seer )]
		public CTFGame Game{ get{ return m_Game; } set{ m_Game = value; ReturnToHome(); } }

		[CommandProperty( AccessLevel.Seer )]
		public DDWayPoint OtherPoint
		{ 
			get{ return m_OtherPoint; } 
			set
			{ 
				if ( m_OtherPoint != this )
					m_OtherPoint = value; // recursion is bad.
			} 
		}

		public CTFTeam TeamOwner
		{ 
			get{ return m_TeamOwner; } 
			set
			{ 
				m_TeamOwner = value; 

				UpdateHues();
			} 
		}

		public void UpdateHues()
		{
			for (int i=0;i<Components.Count;i++)
				((Item)Components[i]).Hue = 0x38a;

			if ( m_TeamOwner != null )
				this.Hue = m_TeamOwner.Hue;
			else
				this.Hue = 0x38a;
		}

		public override bool OnMoveOver( Mobile from )
		{
			if ( m_Game == null || !CanDom || !from.Alive || from.AccessLevel != AccessLevel.Player )
				return true;

			if ( !m_Game.Running )
			{
				from.SendMessage( "The game is currently closed." );
				return false;
			}

			CTFTeam team = m_Game.GetTeam( from );

			if ( team == null )
				return false;

			if ( team != m_TeamOwner )
			{
				TeamOwner = team;
				m_Game.PlayerMessage( "{0} has taken point {1}!", team.Name, this.Name );
				DominationChange();
			}

			return true;
		}

		public void DominationChange()
		{
			if ( m_OtherPoint == null || m_Game == null || !m_CanDom )
				return;

			if ( m_Timer != null && m_Timer.Running )
			{
				m_Game.PlayerMessage( "Domination averted." );
				m_OtherPoint.UpdateHues();//force colors update
				m_Timer.Stop();
				m_Timer = null;
			}

			if ( m_TeamOwner == m_OtherPoint.TeamOwner && m_TeamOwner != null )
			{
				m_Timer = m_OtherPoint.m_Timer = new DominationTimer( this );
				m_Timer.Start();
			}
		}

		private class DominationTimer : Timer
		{
			private DDWayPoint m_Point;
			private int m_Stage;

			public DominationTimer( DDWayPoint point ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 1.0 ) )
			{
				m_Point = point;
				m_Stage = 10;
				Priority = TimerPriority.FiftyMS;
			}

			protected override void OnTick()
			{
				--m_Stage;
				if ( m_Stage > 0 )
				{
					m_Point.Game.PlayerMessage( "{0} is dominating... {1}", m_Point.TeamOwner.Name, m_Stage );

					if ( m_Stage <= m_Point.Components.Count )
						((Item)m_Point.Components[m_Stage-1]).Hue = m_Point.TeamOwner.Hue;

					if ( m_Stage <= m_Point.m_OtherPoint.Components.Count )
						((Item)m_Point.m_OtherPoint.Components[m_Stage-1]).Hue = m_Point.TeamOwner.Hue;
				}
				else
				{
					m_Point.Game.PlayerMessage( "{0} scores!", m_Point.TeamOwner.Name );
					m_Point.TeamOwner.Points++;
					Stop();
					m_Point.TeamOwner = null;
					m_Point.OtherPoint.TeamOwner = null;
					m_Point.CanDom = false;
					m_Point.m_Timer = m_Point.OtherPoint.m_Timer = new WaitTimer( m_Point );
					m_Point.m_Timer.Start();
				}
			}
		}

		private class WaitTimer : Timer
		{
			private DDWayPoint m_Point;

			public WaitTimer( DDWayPoint pt ) : base( TimeSpan.FromSeconds( 20.0 ) )
			{
				m_Point = pt;
				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				m_Point.CanDom = true;
				m_Point.m_Timer = m_Point.OtherPoint.m_Timer = null;
			}
		}
	}

	public class DDWayStep : AddonComponent
	{
		public DDWayStep( int itemID ) : base( itemID )
		{
		}

		public DDWayStep( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
		}

		public override bool OnMoveOver( Mobile from )
		{
			return Addon.OnMoveOver( from );
		}
	}
}
