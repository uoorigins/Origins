using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class RightArm : Item, ICarvable
	{
        private String m_Owner;

        [Constructable]
        public RightArm() : this(null)
        {
        }

		[Constructable]
        public RightArm(String owner)
            : base(0x1DA2)
		{
            m_Owner = owner;
		}

		public RightArm( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", (m_Owner == null ? "right arm" : "right arm of " + m_Owner)));
            }
        }

        public void Carve(Mobile from, Item item)
        {
            from.AddToBackpack(new HumanJerky(m_Owner));
            Delete();
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2); // version

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