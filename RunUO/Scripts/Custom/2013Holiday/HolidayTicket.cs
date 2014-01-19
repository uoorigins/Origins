using Server.Mobiles;
using System;
using System.Collections.Generic;

namespace Server.Misc
{
    class HolidayTicket : Item
    {

        private PlayerMobile m_Player;

        [CommandProperty(AccessLevel.Administrator)]
        public PlayerMobile Player
        {
            get
            {
                return m_Player;
            }
            set
            {
                m_Player = value;
            }
        }
        /*[Constructable]
        public HolidayTicket() : this(null)
        {
        }*/

        [Constructable]
        public HolidayTicket(PlayerMobile mobile) : base(0x14F0)
        {
            m_Player = mobile;
            Name = "This is a prize ticket! Bring this ticket to the nearest Santa at a city bank. This ticket will only work for YOU, so don't give it away!";
            Weight = 1.0;
            LootType = LootType.Newbied;
        }

        public HolidayTicket(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.WriteMobile(m_Player);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_Player = reader.ReadMobile() as PlayerMobile;
        }
    }
}
