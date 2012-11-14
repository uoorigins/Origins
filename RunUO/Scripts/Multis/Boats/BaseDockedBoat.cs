using System;
using Server;
using Server.Regions;
using Server.Targeting;
using Server.Engines.CannedEvil;
using Server.Network;
using Server.Mobiles;

namespace Server.Multis
{
	public abstract class BaseDockedBoat : Item
	{
		private int m_MultiID;
		private Point3D m_Offset;
		private string m_ShipName;
        private Point3D m_Location;

		[CommandProperty( AccessLevel.GameMaster )]
		public int MultiID{ get{ return m_MultiID; } set{ m_MultiID = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Offset{ get{ return m_Offset; } set{ m_Offset = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public string ShipName{ get{ return m_ShipName; } set{ m_ShipName = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D DockLocation { get { return m_Location; } set { m_Location = value; } }

        public BaseDockedBoat(int id, Point3D offset, BaseBoat boat) : base( /*0x14F4*/0x14F2)
		{
			Weight = 1.0;
			//LootType = LootType.Blessed;

			m_MultiID = id;
			m_Offset = offset;
            m_Location = boat.Location;

			m_ShipName = boat.ShipName;
		}

		public BaseDockedBoat( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( m_MultiID );
			writer.Write( m_Offset );
			writer.Write( m_ShipName );
            writer.Write( m_Location );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				case 0:
				{
					m_MultiID = reader.ReadInt();
					m_Offset = reader.ReadPoint3D();
					m_ShipName = reader.ReadString();
                    m_Location = reader.ReadPoint3D();

					if ( version == 0 )
						reader.ReadUInt();

					break;
				}
			}

			if ( Weight == 0.0 )
				Weight = 1.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendAsciiMessage( "That must be in your pack for you to use it." ); // That must be in your pack for you to use it.
			}
			else
			{
				from.SendAsciiMessage( "Where do you wish to place the ship?" ); // Where do you wish to place the ship?

				from.Target = new InternalTarget( this );
			}
		}

		public abstract BaseBoat Boat{ get; }

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( m_ShipName != null )
				list.Add( m_ShipName );
			else
				base.AddNameProperty( list );
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( m_ShipName != null )
				LabelTo( from, m_ShipName );
			else
				base.OnSingleClick( from );
		}

        public override bool OnDroppedToMobile(Mobile from, Mobile target)
        {
            if (target is HarborMaster)
            {
                target.Say(true, "If you already gave me a ship, just use your claim ticket as you would any other deed.");
                return false;
            }
            else
                return base.OnDroppedToMobile(from, target);
        }

		public void OnPlacement( Mobile from, Point3D p )
		{
			if ( Deleted )
			{
				return;
			}
			else if ( !IsChildOf( from.Backpack ) )
			{
				from.SendAsciiMessage( "That must be in your pack for you to use it." ); // That must be in your pack for you to use it.
			}
			else
			{
				Map map = from.Map;

				if ( map == null )
					return;

				BaseBoat boat = Boat;

				if ( boat == null )
					return;

                if (from.InRange(this.DockLocation, 20))
                {
                }
                else
                {
                    from.SendAsciiMessage("You are too far away from the location at which the ship was docked.");
                    return;
                }

				p = new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z );

				if ( BaseBoat.IsValidLocation( p, map ) && boat.CanFit( p, map, boat.ItemID ) && map != Map.Ilshenar && map != Map.Malas )
				{
					Delete();

					boat.Owner = from;
					boat.Anchored = true;
					boat.ShipName = m_ShipName;

					uint keyValue = boat.CreateKeys( from );

					if ( boat.PPlank != null )
						boat.PPlank.KeyValue = keyValue;

					if ( boat.SPlank != null )
						boat.SPlank.KeyValue = keyValue;

					boat.MoveToWorld( p, map );
				}
				else
				{
					boat.Delete();
					from.SendAsciiMessage( "A ship can not be created here." ); // A ship can not be created here.
				}
			}
		}

		private class InternalTarget : MultiTarget
		{
			private BaseDockedBoat m_Model;

			public InternalTarget( BaseDockedBoat model ) : base( model.MultiID, model.Offset )
			{
				m_Model = model;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				IPoint3D ip = o as IPoint3D;

				if ( ip != null )
				{
					if ( ip is Item )
						ip = ((Item)ip).GetWorldTop();

					Point3D p = new Point3D( ip );

					Region region = Region.Find( p, from.Map );

					if ( region.IsPartOf( typeof( DungeonRegion ) ) )
						from.SendAsciiMessage( "You can not place a ship inside a dungeon." ); // You can not place a ship inside a dungeon.
					else if ( region.IsPartOf( typeof( HouseRegion ) ) || region.IsPartOf( typeof( ChampionSpawnRegion ) ) )
						from.SendAsciiMessage( "A boat may not be placed in this area." ); // A boat may not be placed in this area.
					else
						m_Model.OnPlacement( from, p );
				}
			}
		}
	}
}