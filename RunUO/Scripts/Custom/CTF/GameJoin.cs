using System;
using System.Collections;
using Server.Items;
using Server.Gumps;

namespace Server.Items
{
	[FlipableAttribute( 0xEDC, 0xEDB )]
	public class GameJoinStone : Item
	{
		private CTFGame m_Game;
		private string m_GameName;

		[Constructable]
		public GameJoinStone( string gameName ) : base( 0xEDC )
		{
			m_GameName = gameName;

			Name = m_GameName + " Signup Stone";
			Movable = false;
		}

		public GameJoinStone( Serial serial ) : base( serial )
		{
		}

		[CommandProperty( AccessLevel.Seer )]
		public CTFGame Game{ get{ return m_Game; } set{ m_Game = value; } }

		[CommandProperty( AccessLevel.Seer )]
		public string GameName{ get{ return m_GameName; } set{ m_GameName = value; } }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
			writer.Write( m_GameName );
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
					m_GameName = reader.ReadString();
					m_Game = reader.ReadItem() as CTFGame;
					break;
				}
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( m_Game != null )
			{
				if ( m_Game.OpenJoin )
				{
					if ( m_Game.IsInGame( from ) )
					{
						from.SendGump( new GameTeamSelector( m_Game ) );
					}
					else
					{
						if ( from.AccessLevel == AccessLevel.Player )
							from.SendGump( new GameJoinGump( m_Game, m_GameName ) );
						else
							from.SendMessage( "It might not be wise for staff to be playing..." );
					}
				}
				else
				{
					from.SendMessage( "{0} join is closed.", m_GameName );
				}
			}
			else
			{
				from.SendMessage( "This stone must be linked to a game stone.  Please contact a game master." );
			}
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );
			LabelTo( from, "[{0} Signup {1}]", m_GameName, m_Game == null ? "UNLINKED" : (m_Game.OpenJoin ? "Open" : "Closed") );
		}
	}
}
