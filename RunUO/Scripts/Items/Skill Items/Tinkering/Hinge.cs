using System;
using Server;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	[Flipable( 0x1055, 0x1056 )]
	public class Hinge : Item
	{
		[Constructable]
		public Hinge() : this( 1 )
		{
		}

		[Constructable]
		public Hinge( int amount ) : base( 0x1055 )
		{
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
		}

		public Hinge( Serial serial ) : base( serial )
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
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " hinges"));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a hinge"));
                }
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            from.SendAsciiMessage("Use that on an axle with gears to make sextant parts.");
            from.Target = new InternalTarget(this);
        }

        private class InternalTarget : Target
        {
            private Hinge m_Item;

            public InternalTarget(Hinge item) : base(1, false, TargetFlags.None)
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

                    from.AddToBackpack(new SextantParts());
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