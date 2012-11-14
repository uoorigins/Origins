using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Regions;

namespace Server.Multis
{
	public class LargeDragonBoat : BaseBoat
	{
		public override int NorthID{ get{ return 0x14; } }
		public override int  EastID{ get{ return 0x15; } }
		public override int SouthID{ get{ return 0x16; } }
		public override int  WestID{ get{ return 0x17; } }

		public override int HoldDistance{ get{ return 5; } }
		public override int TillerManDistance{ get{ return -5; } }

		public override Point2D StarboardOffset{ get{ return new Point2D(  2, -1 ); } }
		public override Point2D      PortOffset{ get{ return new Point2D( -2, -1 ); } }

		public override Point3D MarkOffset{ get{ return new Point3D( 0, 0, 3 ); } }

        public override BaseDockedBoat DockedBoat { get { return new LargeDockedDragonBoat(this); } }
        public static Rectangle2D[] AreaArrayNorth = new Rectangle2D[] { new Rectangle2D(-1, -5, 3, 11) };
        public static Rectangle2D[] AreaArrayEast = new Rectangle2D[] { new Rectangle2D(-5, -1, 11, 3) };
        public override Rectangle2D[] AreaNorth { get { return AreaArrayNorth; } }
        public override Rectangle2D[] AreaEast { get { return AreaArrayEast; } }

		[Constructable]
		public LargeDragonBoat()
		{
		}

		public LargeDragonBoat( Serial serial ) : base( serial )
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

	public class LargeDragonBoatDeed : BaseBoatDeed
	{
		public override int LabelNumber{ get{ return 1041210; } }// large dragon ship deed
		public override BaseBoat Boat{ get{ return new LargeDragonBoat(); } }

		[Constructable]
		public LargeDragonBoatDeed() : base( 0x14, new Point3D( 0, -1, 0 ) )
		{
		}

		public LargeDragonBoatDeed( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a large dragon ship deed"));
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

	public class LargeDockedDragonBoat : BaseDockedBoat
	{
		public override BaseBoat Boat{ get{ return new LargeDragonBoat(); } }

		public LargeDockedDragonBoat( BaseBoat boat ) : base( 0x14, new Point3D( 0, -1, 0 ), boat )
		{
		}

		public LargeDockedDragonBoat( Serial serial ) : base( serial )
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