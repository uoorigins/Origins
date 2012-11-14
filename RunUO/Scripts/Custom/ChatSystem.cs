using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Misc;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Gumps
{
    public class ChatSystem : Item
    {
        public Dictionary<Mobile,bool> m_Players;
        public List<string> m_Chat;

        [Constructable]
        public ChatSystem(Mobile from)
        {
            if (m_Players == null)
                m_Players = new Dictionary<Mobile,bool>();
            
            if (m_Chat == null)
                m_Chat = new List<string>();

            AddPlayer(from);
        }

        public ChatSystem(Serial serial) : base(serial)
        {
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
            
            m_Chat = new List<string>();
            m_Players = new Dictionary<Mobile, bool>();
        }

        public void AddPlayer(Mobile from)
        {
            if (!m_Players.ContainsKey(from))
            {
                m_Players.Add(from,from.AccessLevel == AccessLevel.Player);
            }
            UpdateGump();
        }

        public void ToggleVisible(Mobile from)
        {
            bool visible = true;
            m_Players.TryGetValue(from, out visible);
            
            m_Players.Remove(from);

            if (visible)
                m_Players.Add(from, false);
            else
                m_Players.Add(from, true);
        }

        public void RemovePlayer(Mobile from)
        {
            m_Players.Remove(from);
            UpdateGump();
        }

        public void Say(Mobile from, string msg)
        {
            m_Chat.Add(String.Format("{0}: {1}", (from.AccessLevel > AccessLevel.Player ? "@"+from.Name : from.Name), msg));

            if (m_Chat.Count > 20)
                m_Chat.RemoveAt(0);

            UpdateGump();
        }

        public void UpdateGump()
        {
            foreach (Mobile m in m_Players.Keys)
            {
                if (m.HasGump(typeof(ChatGump)))
                    m.CloseGump(typeof(ChatGump));

                m.SendGump(new ChatGump(m, this));
            }
        }
    }
}
