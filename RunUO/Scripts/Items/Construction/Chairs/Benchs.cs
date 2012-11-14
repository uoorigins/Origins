using System;
using Server.Network;

namespace Server.Items
{
	[Furniture]
	[Flipable( 0xB2D, 0xB2C )]
	public class WoodenBench : Item
	{
		[Constructable]
		public WoodenBench() : base( 0xB2D )
		{
			Weight = 6;
		}

		public WoodenBench(Serial serial) : base(serial)
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a wooden bench"));
            }
        }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}