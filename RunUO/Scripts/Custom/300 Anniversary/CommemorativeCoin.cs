using System;
using System.Collections.Generic;
using System.Text;
using Server.Network;

namespace Server.Items
{
    public class CommemorativeCoin : Item
    {
        [Constructable]
        public CommemorativeCoin() : base(6255)
        {
            LootType = LootType.Blessed;
            Hue = 2401;
            Weight = 1.0;
        }

        public CommemorativeCoin(Serial serial) : base(serial)
		{
		}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "In Commemoration of the 300th anniversary of Mondain's defeat"));
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (Utility.RandomBool())
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Ankhs"));
            else
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Serpents"));
        }
    }
}
