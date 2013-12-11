using System;
using System.Collections.Generic;

namespace Server.Misc
{
    class HolidayTicket : Item
    {

        [Constructable]
        public HolidayTicket(Mobile mobile) : base(0x14F0)
        {
            Name = "This is prize ticket! Bring this ticket to the nearest bank with a holiday tree. This ticket will only work for YOU, so don’t give it away!";
            Weight = 1.0;
            LootType = LootType.Blessed;
        }

        public HolidayTicket(Serial serial)
            : base(serial)
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
