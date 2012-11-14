using System;
using Server;
using Server.Network;

namespace Server.Items
{
    public class NPCBlocker : Item
    {
        private String m_NPC;
        [CommandProperty(AccessLevel.GameMaster)]
        public String TargetNPC { get { return m_NPC; } set { m_NPC = value; } }

        [Constructable]
        public NPCBlocker() : this(null)
        {
        }

        [Constructable]
        public NPCBlocker(String npc) : base(0x1BC3)
        {
            Movable = false;
            Name = "NPC Blocker";
            Visible = false;
            m_NPC = npc;
        }

        public NPCBlocker(Serial serial) : base(serial)
        {
        }

        public override bool OnMoveOver(Mobile m)
        {
            if (m != null && m.Tag != null && m.Tag == m_NPC)
                return false;

            return base.OnMoveOver(m);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }


    }
}