using System;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0xE41, 0xE40 )]
	public class TrashChest : Container
	{
		public override int DefaultMaxWeight{ get{ return 0; } } // A value of 0 signals unlimited weight

		public override bool IsDecoContainer
		{
			get{ return false; }
		}

		[Constructable]
		public TrashChest() : base( 0xE41 )
		{
			Movable = false;
		}

		public TrashChest( Serial serial ) : base( serial )
		{
		}

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "(" + this.TotalItems + " items, " + this.TotalWeight + " stones)"));
            }
            else
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a trash chest"));
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "(" + this.TotalItems + " items, " + this.TotalWeight + " stones)"));
            }
        }


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( !base.OnDragDrop( from, dropped ) )
				return false;

			PublicOverheadMessage( Network.MessageType.Regular, 0x3B2, Utility.Random( 1042891, 8 ) );
			dropped.Delete();

			return true;
		}

		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{
			if ( !base.OnDragDropInto( from, item, p ) )
				return false;

			PublicOverheadMessage( Network.MessageType.Regular, 0x3B2, Utility.Random( 1042891, 8 ) );
			item.Delete();

			return true;
		}
	}
}