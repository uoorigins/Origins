using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class CarpentryBag : Bag
    {
        public override string DefaultName
        {
            get { return "a Carpentry bag"; }
        }

        [Constructable]
        public CarpentryBag() : this(1)
        {
            Movable = true;
            Hue = 0x105;
        }

        [Constructable]
        public CarpentryBag(int amount)
        {
            DropItem(new Log(amount));
            DropItem(new Saw());
        }

        public CarpentryBag(Serial serial) : base(serial)
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
    }
}