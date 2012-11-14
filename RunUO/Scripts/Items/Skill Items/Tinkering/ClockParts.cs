using System;
using Server;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	[Flipable( 0x104F, 0x1050 )]
	public class ClockParts : Item
	{
		[Constructable]
		public ClockParts() : this( 1 )
		{
		}

		[Constructable]
		public ClockParts( int amount ) : base( 0x104F )
		{
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
		}

		public ClockParts( Serial serial ) : base( serial )
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
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " clock parts"));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "clock parts"));
                }
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            from.SendAsciiMessage("Use that on a clock frame to make a clock.");
            from.Target = new InternalTarget(this);
        }

        private class InternalTarget : Target
        {
            private ClockParts m_Item;

            public InternalTarget(ClockParts item) : base(1, false, TargetFlags.None)
            {
                m_Item = item;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Item.Deleted) return;

                if (targeted is ClockFrame)
                {
                    m_Item.Consume();

                    ((ClockFrame)targeted).Consume();

                    from.AddToBackpack(new Clock());
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