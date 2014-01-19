using System;
using Server;
using Server.Gumps;
using Server.Targeting;

namespace Server.Items
{
	public interface IGameFlag
	{
		void ReturnToHome();
	}

	public class CTFFlag : Item, IGameFlag
	{
		private CTFTeam m_Team;
		private int m_TeamID;
		private CTFGame m_Game;
		private Timer m_Timer;
		private bool m_Home;

		[Constructable()]
		public CTFFlag() : base( 0x1627 )
		{
			this.Movable = false;
			this.Weight = 1.0;

			m_TeamID = -1;
			m_Home = true;
		}
		
		public CTFFlag( Serial serial ) : base( serial )
		{
		}

		[CommandProperty( AccessLevel.Seer )]
		public CTFGame Game 
		{ 
			get{ return m_Game; } 
			set
			{ 
				m_Game = value; 
				UpdateTeam();
			} 
		}

		public bool Home{ get { return m_Home; } }

		[CommandProperty( AccessLevel.Seer )]
		public int TeamID
		{ 
			get
			{ 
				UpdateTeam();
				if ( m_Team != null )
					return m_Team.UId; 
				else
					return m_TeamID;
			} 
			set
			{ 
				m_TeamID = value;
				UpdateTeam();
			} 
		}

		public CTFTeam Team { get { return m_Team; } }

		public void UpdateTeam()
		{
			if ( m_Game != null && m_TeamID != -1 )
			{
				m_Team = m_Game.GetTeam( m_TeamID );
				if ( m_Team != null )
				{
					this.Hue = m_Team.Hue;
					m_Team.Flag = this;
				}
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );

			writer.Write( m_TeamID );
			writer.Write( m_Game );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_TeamID = reader.ReadInt();
					m_Game = reader.ReadItem() as CTFGame;

					break;
				}
			}

			Timer.DelayCall( TimeSpan.Zero, new TimerCallback( ReturnToHome ) );
		}

		public override void OnSingleClick( Mobile from )
		{
			UpdateTeam();
			if ( m_Team == null )
				LabelTo( from, "Flag (Unowned)" );
			else
				LabelTo( from, "{0} Flag", m_Team.Name );
		}

		public void ReturnToHome()
		{
			UpdateTeam();

			if ( !m_Home && m_Team != null )
			{
				MoveToWorld( m_Team.FlagHome, m_Team.Map );
				m_Home = true;
			}

			if ( m_Timer != null )
				m_Timer.Stop();
		}
		
		public void BeginCapture()
		{
			if ( m_Timer != null && m_Timer.Running )
				m_Timer.Stop();
			m_Timer = new ReturnTimer( this );
			m_Timer.Start();
			m_Home = false;
		}

		public override void OnAdded( object parent )
		{
			Mobile m = this.RootParent as Mobile;
			if ( m != null )
				m.SolidHueOverride = 0x496; // BRIGHT orange (brighter than blaze)
		}

		public override void OnRemoved( object oldParent )
		{
			Mobile m = null;
			if ( oldParent is Item )
				m = ((Item)oldParent).RootParent as Mobile;
			else 
				m = oldParent as Mobile;
			
			if ( m != null )
				m.SolidHueOverride = -1;
		}

		public override void OnParentDeleted( object Parent )
		{
			UpdateTeam();
			if ( m_Team != null && m_Game != null )
				ReturnToHome();
			else
				base.OnParentDeleted( Parent );
		}

		public override DeathMoveResult OnInventoryDeath( Mobile parent )
		{
			parent.SolidHueOverride = -1;
			Timer.DelayCall( TimeSpan.Zero, new TimerCallback( MoveToGround ) );
			return DeathMoveResult.MoveToCorpse;
		}

		private void MoveToGround()
		{
			if ( !(RootParent is Mobile ) && !Home )
				MoveToWorld( GetWorldLocation(), Map );
		}

		public override bool Decays{ get{ return false; } }

		public override void OnDoubleClick( Mobile from )
		{
			UpdateTeam();
			if ( from.AccessLevel >= AccessLevel.Seer )
			{
				if ( m_Game == null || m_Team == null )
					from.SendGump( new PropertiesGump( from, this ) );
				else
					from.SendGump( new PropertiesGump( from, m_Team ) );
			}
			else if ( m_Team != null && m_Game != null )
			{
				if ( !m_Game.Running )
				{
					from.SendMessage( "The game is currently closed." );
					return;
				}

				CTFTeam team = m_Game.GetTeam( from );
				if ( team != null )
				{
					if ( !from.InLOS( this.GetWorldLocation() ) )
					{
						from.SendLocalizedMessage( 502800 ); // You can't see that.
						return;
					}
					else if ( from.GetDistanceToSqrt( this.GetWorldLocation() ) > 3 )
					{
						from.SendLocalizedMessage( 500446 ); // That is too far away.
						return;
					}

					if ( RootParent is Mobile ) 
					{
						if ( RootParent == from )
						{
							from.Target = new CaptureTarget( this );
							from.SendMessage( "Target your flag to capture, or target a team-mate to pass the flag." );//"What do you wish to do with the flag?" );
						}
					}
					else
					{
						if ( team != m_Team )
						{
							if ( from.Backpack != null )
							{
								from.RevealingAction();
								from.Backpack.DropItem( this );
								from.SendMessage( "You got the enemy flag!" );
								BeginCapture();
								m_Game.PlayerMessage( "{0} ({1}) got the {2} flag!", from.Name, team.Name, m_Team.Name );
							}
							else
							{
								from.SendMessage( "You have no backpack to carry that flag!" );
							}
						}
						else
						{
							if ( !m_Home )
							{
								m_Game.PlayerMessage( "{0} has returned the {1} flag!", from.Name, m_Team.Name );
								ReturnToHome();
							}
						}
					}
				}
				else
				{
					from.SendMessage( "You are not part of the game." );
				}
			}
		}

		private class CaptureTarget : Target
		{
			private CTFFlag m_Flag;

			public CaptureTarget( CTFFlag flag ) : base( 3, false, TargetFlags.None )
			{
				m_Flag = flag;
			}

			protected override void OnTarget( Mobile from, object target )
			{
				CTFTeam fteam = m_Flag.Game.GetTeam( from );
				if ( target is Mobile )
				{
					Mobile targ = (Mobile)target;
					CTFTeam tteam = m_Flag.Game.GetTeam( targ );
					if ( tteam == fteam && from != targ )
					{
						if ( targ.Backpack != null )
						{
							targ.Backpack.DropItem( m_Flag );
							targ.SendMessage( "{0} gave you the {1} flag!", from.Name, m_Flag.Team.Name );
							m_Flag.Game.PlayerMessage( "{0} passed the {1} flag to {2}!", from.Name, m_Flag.Team.Name, targ.Name );
						}
					}
					else
					{
						from.SendMessage( "You cannot give the flag to them!" );
					}
				}
				else if ( target is CTFFlag )
				{
					CTFFlag flag = target as CTFFlag;
					if ( flag.Team == fteam )
					{
						if ( flag.Home )
						{
							from.SendMessage( "You captured the {0} flag!", m_Flag.Team.Name );
							flag.Game.PlayerMessage( "{0} ({1}) captured the {2} flag!", from.Name, fteam.Name, m_Flag.Team.Name );
							m_Flag.ReturnToHome();
							fteam.Points += 15;
						}
						else
						{
							from.SendMessage( "Your flag must be at home to capture!" );
						}
					}
					else
					{
						from.SendMessage( "You can only capture for your own team!" );
					}
				}
			}
		}

		private class ReturnTimer : Timer
		{
			public static readonly TimeSpan MaxFlagHoldTime = TimeSpan.FromMinutes( 3.0 );

			private CTFFlag m_Flag;
			private DateTime m_Start;

			public ReturnTimer( CTFFlag flag ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 30.0 ) )
			{
				m_Flag = flag;
				m_Start = DateTime.Now;
				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				Mobile owner = m_Flag.RootParent as Mobile;
				CTFGame game = m_Flag.Game;

				TimeSpan left = MaxFlagHoldTime - (DateTime.Now - m_Start);

				if ( left >= TimeSpan.FromSeconds( 1.0 ) )
				{
					if ( left > TimeSpan.FromMinutes( 1.0 ) )
						Interval = TimeSpan.FromSeconds( 30.0 );
					else if ( left > TimeSpan.FromSeconds( 30.0 ) )
						Interval = TimeSpan.FromSeconds( 15.0 );
					else if ( left >= TimeSpan.FromSeconds( 10.0 ) )
						Interval = TimeSpan.FromSeconds( 5.0 );
					else 
						Interval = TimeSpan.FromSeconds( 1.0 );

					if ( owner != null )
						owner.SendMessage( "You must take the {0} flag to your flag in {1} seconds or be killed!", m_Flag.Team.Name, (int)left.TotalSeconds );
				}
				else
				{
					if ( owner != null )
					{
						owner.BoltEffect( 0 );
						owner.PlaySound( 0xDC );//snake hiss
						owner.Kill();
					}

					m_Flag.Game.PlayerMessage( "The {0} flag has been returned to base!", m_Flag.Team.Name );
					m_Flag.ReturnToHome();
					Stop();
				}
			}
		}
	}
}

