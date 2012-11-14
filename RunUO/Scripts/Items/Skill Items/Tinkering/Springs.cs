using System;
using Server;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	[Flipable( 0x105D, 0x105E )]
	public class Springs : Item
	{
		[Constructable]
		public Springs() : this( 1 )
		{
		}

		[Constructable]
		public Springs( int amount ) : base( 0x105D )
		{
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
		}

		public Springs( Serial serial ) : base( serial )
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
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " springs"));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "springs"));
                }
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            from.SendAsciiMessage("Use that on an axle with gears to make clock parts.");
            from.Target = new InternalTarget(this);
        }

        private class InternalTarget : Target
        {
            private Springs m_Item;

            public InternalTarget(Springs item) : base(1, false, TargetFlags.None)
            {
                m_Item = item;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Item.Deleted) return;

                if (targeted is AxleGears)
                {
                    m_Item.Consume();

                    ((AxleGears)targeted).Consume();

                    from.AddToBackpack(new ClockParts());
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