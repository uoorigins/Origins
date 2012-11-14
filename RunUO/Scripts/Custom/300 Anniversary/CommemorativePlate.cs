using System;
using System.Collections.Generic;
using System.Text;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class CommemorativePlate : Item
    {
        [Constructable]
        public CommemorativePlate() : base(2519)
        {
            LootType = LootType.Blessed;
            Weight = 1.0;
        }

        public CommemorativePlate(Serial serial) : base(serial)
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "A plate decorated with a beautiful painting of Mondain's defeat as the Gem of Immortality shatters."));
            }
        }
    }
}
