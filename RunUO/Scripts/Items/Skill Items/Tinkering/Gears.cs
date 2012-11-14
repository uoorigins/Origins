using System;
using Server;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	[Flipable( 0x1053, 0x1054 )]
	public class Gears : Item
	{
		[Constructable]
		public Gears() : this( 1 )
		{
		}

		[Constructable]
		public Gears( int amount ) : base( 0x1053 )
		{
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
		}

		public Gears( Serial serial ) : base( serial )
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
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " gears"));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "gears"));
                }
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            from.SendAsciiMessage("Use that on an axle to make an axle with gears.");
            from.Target = new InternalTarget(this);
        }

        private class InternalTarget : Target
        {
            private Gears m_Item;

            public InternalTarget(Gears item): base(1, false, TargetFlags.None)
            {
                m_Item = item;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Item.Deleted) return;

                if (targeted is Axle)
                {
                    m_Item.Consume();

                    ((Axle)targeted).Consume();

                    from.AddToBackpack(new AxleGears());
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