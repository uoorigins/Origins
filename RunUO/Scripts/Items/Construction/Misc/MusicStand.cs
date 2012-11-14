using System;
using Server.Network;

namespace Server.Items
{
	[Furniture]
	[Flipable(0xEBB, 0xEBC)]
	public class TallMusicStand : Item
	{
		[Constructable]
		public TallMusicStand() : base(0xEBB)
		{
			Weight = 10.0;
		}

		public TallMusicStand(Serial serial) : base(serial)
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a music stand"));
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

			if ( Weight == 8.0 )
				Weight = 10.0;
		}
	}

	[Furniture]
	[Flipable(0xEB6,0xEB8)]
	public class ShortMusicStand : Item
	{
		[Constructable]
		public ShortMusicStand() : base(0xEB6)
		{
			Weight = 10.0;
		}

		public ShortMusicStand(Serial serial) : base(serial)
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a music stand"));
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

			if ( Weight == 6.0 )
				Weight = 10.0;
		}
	}
}
