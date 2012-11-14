using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Regions;

namespace Server.Multis
{
    /*case Direction.North: if ( !CanFit( Location, Map, NorthID ) ) return false; break;
					case Direction.East:  if ( !CanFit( Location, Map,  EastID ) ) return false; break;
					case Direction.South: if ( !CanFit( Location, Map, SouthID ) ) return false; break;
					case Direction.West:  if ( !CanFit( Location, Map,  WestID ) ) return false; break;*/

	public class SmallBoat : BaseBoat
	{
		public override int NorthID{ get{ return 0x0; } }
		public override int  EastID{ get{ return 0x1; } }
		public override int SouthID{ get{ return 0x2; } }
		public override int  WestID{ get{ return 0x3; } }

		public override int HoldDistance{ get{ return 4; } }
		public override int TillerManDistance{ get{ return -4; } }

		public override Point2D StarboardOffset{ get{ return new Point2D(  2, 0 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -2, 0 ); } }

		public override Point3D MarkOffset{ get{ return new Point3D( 0, 1, 3 ); } }

		public override BaseDockedBoat DockedBoat{ get{ return new SmallDockedBoat( this ); } }

        public static Rectangle2D[] AreaArrayNorth = new Rectangle2D[] { new Rectangle2D(-1, -4, 3, 9) };
        public static Rectangle2D[] AreaArrayEast = new Rectangle2D[] { new Rectangle2D(-4, -1, 9, 3) };
        public override Rectangle2D[] AreaNorth { get { return AreaArrayNorth; } }
        public override Rectangle2D[] AreaEast { get { return AreaArrayEast; } }

		[Constructable]
		public SmallBoat()
		{
		}

		public SmallBoat( Serial serial ) : base( serial )
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

			writer.Write( (int)0 );
		}
	}

	public class SmallBoatDeed : BaseBoatDeed
	{
		public override int LabelNumber{ get{ return 1041205; } } // small ship deed
		public override BaseBoat Boat{ get{ return new SmallBoat(); } }

		[Constructable]
		public SmallBoatDeed() : base( 0x0, Point3D.Zero )
		{
		}

		public SmallBoatDeed( Serial serial ) : base( serial )
		{
		}

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a small ship deed"));
            }
        }

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}
	}

	public class SmallDockedBoat : BaseDockedBoat
	{
		public override BaseBoat Boat{ get{ return new SmallBoat(); } }

		public SmallDockedBoat( BaseBoat boat ) : base( 0x0, Point3D.Zero, boat )
		{
		}

		public SmallDockedBoat( Serial serial ) : base( serial )
		{
		}

        public override void OnSingleClick(Mobile from)
        {
            if (this.ShipName != null)
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a ship claim ticket from {0} for the {1}", BaseRegion.GetRuneNameFor(Region.Find(DockLocation, Map.Felucca)), this.ShipName)));
            else
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a ship claim ticket from {0}", BaseRegion.GetRuneNameFor(Region.Find(DockLocation, Map.Felucca)))));
        }

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}
	}
}