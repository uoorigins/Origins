using System;
using Server.Network;

namespace Server.Items
{
	public class Vines : Item
	{
		[Constructable]
		public Vines() : this( Utility.Random( 8 ) )
		{
		}

		[Constructable]
		public Vines( int v ) : base( 0xCEB )
		{
			if ( v < 0 || v > 7 )
				v = 0;

			ItemID += v;
			Weight = 1.0;
		}

		public Vines(Serial serial) : base(serial)
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "vines"));
            }
        }

		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}