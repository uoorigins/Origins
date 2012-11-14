using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class GreatLordStone : Item
	{
		/*public override string DefaultName
		{
			get { return "a Blacksmith Supply Stone"; }
		}*/

		[Constructable]
		public GreatLordStone() : base( 0xED4 )
		{
			Movable = false;
			Hue = 1312;
		}

		public override void OnDoubleClick( Mobile from )
		{
            from.Karma = 127;
            from.SendAsciiMessage("You are now a Great Lord.");
		}

        public GreatLordStone(Serial serial) : base(serial)
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a Great Lord stone"));
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
	}
}