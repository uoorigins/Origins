using System;
using Server;

namespace Server.Items
{
    public class ScoreBook : BlueBook
    {
        private IngotChest m_Chest;
        [CommandProperty(AccessLevel.Owner)]
        public IngotChest TargetChest
        {
            get
            {
                return m_Chest;
            }
            set
            {
                m_Chest = value;
            }
        }

        public override BookContent DefaultContent { get { return null; } }

        [Constructable]
        public ScoreBook() : base(false)
        {
            Hue = 0x89B;
        }

        public override void OnDoubleClick(Mobile from)
        {
            //PagesCount = m_Chest.Size;
            if (m_Chest.ScorePages != null)
                Pages = m_Chest.ScorePages;

            if (m_Chest != null)
            {
                Title = m_Chest.Title;
                Author = m_Chest.Author;
            }

            base.OnDoubleClick(from);
        }

        public ScoreBook(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt((int)0); // version

            writer.Write((Item)m_Chest);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
            m_Chest = reader.ReadItem() as IngotChest;
        }
    }
}