using System;
using Server;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	[Flipable( 0x1051, 0x1052 )]
	public class AxleGears : Item
	{
		[Constructable]
		public AxleGears() : this( 1 )
		{
		}

		[Constructable]
		public AxleGears( int amount ) : base( 0x1051 )
		{
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
		}

		public AxleGears( Serial serial ) : base( serial )
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
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " axles with gears"));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "an axle with gears"));
                }
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!Movable)
                return;

            from.SendAsciiMessage("Use that on springs to make clock parts, or a hinge to make sextant parts.");
            from.Target = new InternalTarget(this);
        }

        private class InternalTarget : Target
        {
            private AxleGears m_Item;

            public InternalTarget(AxleGears item) : base(1, false, TargetFlags.None)
            {
                m_Item = item;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Item.Deleted) return;

                if (targeted is Springs)
                {
                    m_Item.Consume();

                    ((Springs)targeted).Consume();

                    from.AddToBackpack(new ClockParts());
                }
                else if (targeted is Hinge)
                {
                    m_Item.Consume();

                    ((Hinge)targeted).Consume();

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