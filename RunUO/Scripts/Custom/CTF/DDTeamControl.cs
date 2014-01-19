using System;
using Server;
using Server.Gumps;

namespace Server.Items
{
	public class DDTeamControl : Item
	{
		private CTFGame m_Game;
		private CTFTeam m_Team;
		private int m_TeamID;

		[Constructable]
		public DDTeamControl() : base( 0xED4 )
		{
			m_TeamID = -1;
			this.Movable = false;
			this.Name = "DD Team Control";
		}

		public DDTeamControl( Serial serial ) : base(serial)
		{
		}

		public CTFTeam Team { get { return m_Team; } }

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

		public void UpdateTeam()
		{
			if ( m_Game != null && m_TeamID != -1 )
			{
				m_Team = m_Game.GetTeam( m_TeamID );
				if ( m_Team != null )
				{
					this.Hue = m_Team.Hue;
					this.Name = m_Team.Name + " Team Control";
				}
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.AccessLevel >= AccessLevel.Seer )
			{
				UpdateTeam();
				if ( m_Team != null )
					from.SendGump( new PropertiesGump( from, m_Team ) );
				else
					from.SendMessage( "Set game and team on props first!" );
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
		}
	}
}
