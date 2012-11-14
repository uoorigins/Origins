using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class BagOfFullPlatemail : Bag
    {
        [Constructable]
        public BagOfFullPlatemail(): this(1)
        {
        }

        [Constructable]
        public BagOfFullPlatemail(int amount)
        {
            DropItem(new PlateChest());
            DropItem(new PlateLegs());
            DropItem(new PlateGorget());
            DropItem(new PlateHelm());
            DropItem(new PlateArms());
            DropItem(new PlateGloves());
        }

        public BagOfFullPlatemail(Serial serial) : base(serial)
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