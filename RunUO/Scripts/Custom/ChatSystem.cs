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
        public List<Mobile> m_Squelched;
        public List<string> m_Chat;

        [Constructable]
        public ChatSystem()
        {
            if (m_Squelched == null)
                m_Squelched = new List<Mobile>();

            if (m_Players == null)
                m_Players = new Dictionary<Mobile,bool>();
            
            if (m_Chat == null)
                m_Chat = new List<string>();
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
            m_Squelched = new List<Mobile>();
        }

        public void AddPlayer(Mobile from)
        {
            if (m_Squelched.Contains(from))
            {
                from.SendAsciiMessage("You have been squelched from the chat system and cannot join.");
                return;
            }

            if (!m_Players.ContainsKey(from))
            {
                m_Players.Add(from,from.AccessLevel == AccessLevel.Player);
            }

            from.SendAsciiMessage(0x49, "You have joined the Chat System! Type [chat <message> to talk.");
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

        public void SquelchPlayer(Mobile from)
        {
            if (m_Squelched.Contains(from))
            {
                m_Squelched.Remove(from);
            }
            else
            {
                m_Squelched.Add(from);
                RemovePlayer(from);
            }
        }

        public void RemovePlayer(Mobile from)
        {
            m_Players.Remove(from);
            UpdateGump();

            if (from.HasGump(typeof(ChatGump)))
                from.CloseGump(typeof(ChatGump));
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
