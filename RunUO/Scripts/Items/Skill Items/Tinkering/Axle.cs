using System;
using Server;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	[Flipable( 0x105B, 0x105C )]
	public class Axle : Item
	{
		[Constructable]
		public Axle() : this( 1 )
		{
		}

		[Constructable]
		public Axle( int amount ) : base( 0x105B )
		{
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
		}

		public Axle( Serial serial ) : base( serial )
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
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " axles"));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "an axle"));
                }
            }
        }

        public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

            from.SendAsciiMessage("Use that on gears to make an axle with gears.");
			from.Target = new InternalTarget( this );
		}

        private class InternalTarget : Target
        {
            private Axle m_Item;

            public InternalTarget(Axle item) : base(1, false, TargetFlags.None)
            {
                m_Item = item;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Item.Deleted) return;

                if (targeted is Gears)
                {
                    m_Item.Consume();

                    ((Gears)targeted).Consume();

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