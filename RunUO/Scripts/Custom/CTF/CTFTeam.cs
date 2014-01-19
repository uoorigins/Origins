using System;
using System.Collections;

namespace Server.Items
{
	public class CTFTeam
	{
		private ArrayList m_Members;
		private Point3D m_Home;
		private Point3D m_FlagHome;
		private Map m_Map;
		private int m_Points;
		private string m_Name;
		private int m_Hue;
		private IGameFlag m_Flag;

		private int m_UId;
		private CTFGame m_Game;

		public CTFTeam( CTFGame game, int uid )
		{
			m_UId = uid;
			m_Game = game;

			m_Points = 0;
			m_Home = Point3D.Zero;
			m_Map = Map.Felucca;
			m_Members = new ArrayList();
		}

		public CTFTeam( GenericReader reader )
		{
			Deserialize( reader );
		}

		public void Serialize( GenericWriter writer )
		{
			writer.Write( (int)1 );//version

			writer.Write( (Item)m_Flag );

			writer.WriteMobileList( m_Members );
			writer.Write( m_Home );
			writer.Write( m_FlagHome );
			writer.Write( m_Map );
			writer.Write( m_Points );
			writer.Write( m_Name );
			writer.Write( m_Hue );
			writer.Write( m_UId );
			writer.Write( m_Game );
		}

		public void Deserialize( GenericReader reader )
		{
			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_Flag = reader.ReadItem() as IGameFlag;
					goto case 0;
				}
				case 0:
				{
					m_Members = reader.ReadMobileList();
					m_Home = reader.ReadPoint3D();
					m_FlagHome = reader.ReadPoint3D();
					m_Map = reader.ReadMap();
					m_Points = reader.ReadInt();
					m_Name = reader.ReadString();
					m_Hue = reader.ReadInt();
					m_UId = reader.ReadInt();
					m_Game = reader.ReadItem() as CTFGame;
					break;
				}
			}
		}

		public IGameFlag Flag{ get{ return m_Flag; } set {m_Flag = value; } }
		
		[CommandProperty( AccessLevel.Seer )]
		public Point3D Home{ get{ return m_Home; } set{ m_Home = value; } }

		[CommandProperty( AccessLevel.Seer )]
		public Point3D FlagHome{ get{ return m_FlagHome; } set{ m_FlagHome = value; } }

		[CommandProperty( AccessLevel.Seer )]
		public Map Map{ get{ return m_Map; } set{ m_Map = value; } }

		[CommandProperty( AccessLevel.Seer )]
		public int Points
		{ 
			get{ return m_Points; } 
			set
			{ 
				m_Points = value; 
				if ( m_Points >= m_Game.MaxScore )
					m_Game.EndGame();
			} 
		}

		[CommandProperty( AccessLevel.Seer )]
		public string Name
		{ 
			get
			{ 
				if ( m_Name == null || m_Name == "" )
					return "-unnamed-";
				else
					return m_Name; 
			} 
			set { m_Name = value; } 
		}

		[CommandProperty( AccessLevel.Seer )]
		public int Hue
		{
			get{ return m_Hue; } 
			set
			{ 
				m_Hue = value; 
				if ( m_Flag is CTFFlag )
					((CTFFlag)m_Flag).Hue = m_Hue;
			} 
		}

		[CommandProperty( AccessLevel.Counselor )]
		public int ActiveMemberCount
		{
			get
			{
				int count = 0;
				for (int i=0;i<m_Members.Count;i++)
				{
					if ( ((Mobile)m_Members[i]).NetState != null )
						++count;
				}

				return count;
			}
		}

		public ArrayList Members { get{ return m_Members; } }
		public int UId { get{ return m_UId; } }
		public CTFGame Game { get{ return m_Game; } }

		public bool IsMember( Mobile m )
		{
			return m_Members.Contains( m );
		}

		public void AddMember( Mobile m )
		{
			m_Members.Add( m );
		}

		public void RemoveMember( Mobile m )
		{
			m_Members.Remove( m );
		}
	}
}
