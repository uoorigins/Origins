using System;
using Server;
using Server.Network;

namespace Server.Items
{
	[Flipable( 0x1059, 0x105A )]
	public class SextantParts : Item
	{
		[Constructable]
		public SextantParts() : this( 1 )
		{
		}

		[Constructable]
		public SextantParts( int amount ) : base( 0x1059 )
		{
			Stackable = true;
			Amount = amount;
			Weight = 2.0;
		}

		public SextantParts( Serial serial ) : base( serial )
		{
		}

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                if (Amount >= 2)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " " + this.Name));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
                }
            }
            else
            {
                if (Amount >= 2)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " sextant parts"));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "sextant parts"));
                }
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            this.Consume();
            from.AddToBackpack(new Sextant());
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