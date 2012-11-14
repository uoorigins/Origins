using System;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class ReactiveArmorScroll : SpellScroll
	{
		[Constructable]
		public ReactiveArmorScroll() : this( 1 )
		{
		}

		[Constructable]
		public ReactiveArmorScroll( int amount ) : base( 6, 0x1F2D, amount )
		{
		}

		public ReactiveArmorScroll( Serial ser ) : base(ser)
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
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " Reactive Armor scrolls"));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a Reactive Armor scroll"));
                }
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