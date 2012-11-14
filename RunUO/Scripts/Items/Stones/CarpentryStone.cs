using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class CarpentryStone : Item
    {
        public override string DefaultName
        {
            get { return "a Carpentry stone"; }
        }

        [Constructable]
        public CarpentryStone() : base(0xED4)
        {
            Movable = false;
            Hue = 1107;
        }

        public override void OnDoubleClick(Mobile from)
        {
            CarpentryBag carpentryBag = new CarpentryBag(5000);

            if (!from.AddToBackpack(carpentryBag))
                carpentryBag.Delete();
        }

        public CarpentryStone(Serial serial) : base(serial)
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a Carpentry stone"));
            }
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
    }
}