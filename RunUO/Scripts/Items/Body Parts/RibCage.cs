using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class RibCage : Item
	{
        private String m_Owner;

        [Constructable]
        public RibCage() : this(null)
        {
        }

		[Constructable]
		public RibCage(String owner) : base( 0x1B17 )
		{
			Stackable = false;
			Weight = 5.0;
            m_Owner = owner;
		}

		public RibCage( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", (m_Owner == null ? "ribcage" : "ribcage of " + m_Owner)));
            }
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version

            writer.Write(m_Owner);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

            if (version == 1)
                reader.ReadMobile();
            else if (version == 2)
                m_Owner = reader.ReadString();
		}
	}
}