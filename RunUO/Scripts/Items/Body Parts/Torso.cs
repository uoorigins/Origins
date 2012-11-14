using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class Torso : Item, ICarvable
	{
        private String m_Owner;

        [Constructable]
        public Torso() : this(null)
        {
        }

		[Constructable]
        public Torso(String owner)
            : base(0x1D9F)
		{
			Weight = 2.0;
            m_Owner = owner;
		}

		public Torso( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", (m_Owner == null ? "a torso" : "torso of " + m_Owner)));
            }
        }

        public void Carve(Mobile from, Item item)
        {
            from.AddToBackpack(new Heart(m_Owner));
            from.AddToBackpack(new Entrails(m_Owner));
            from.AddToBackpack(new RibCage(m_Owner));
            Delete();
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

    public class Heart : Item
    {
        private String m_Owner;

        [Constructable]
        public Heart() : this(null)
        {
        }

		[Constructable]
        public Heart(String owner)
            : base(0x1CED)
		{
			Weight = 1.0;
            m_Owner = owner;
		}

        public Heart(Serial serial) : base(serial)
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", (m_Owner == null? "heart" : "heart of " + m_Owner)));
            }
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

            writer.Write(m_Owner);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

            if (version == 0)
                reader.ReadMobile();
            else if (version == 1)
                m_Owner = reader.ReadString();
		}
    }

    public class Entrails : Item
    {
        private String m_Owner;

        [Constructable]
        public Entrails()
            : this(null)
        {
        }

        [Constructable]
        public Entrails(String owner)
            : base(0x1CEC)
        {
            Weight = 1.0;
            m_Owner = owner;
        }

        public Entrails(Serial serial)
            : base(serial)
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", (m_Owner == null ? "entrails" : "entrails of " + m_Owner)));
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 1); // version

            writer.Write(m_Owner);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version == 0)
                reader.ReadMobile();
            else if (version == 1)
                m_Owner = reader.ReadString();
        }
    }
}