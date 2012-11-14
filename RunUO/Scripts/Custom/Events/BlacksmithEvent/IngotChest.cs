using System;
using System.Collections.Generic;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.ContextMenus;

namespace Server.Items
{
    [DynamicFliping]
    [Flipable(0xE41, 0xE40)]
    public class IngotChest : MetalGoldenChest
    {
        private Dictionary<PlayerMobile, int> DonationList;
        private int MaxAmount = 15000;

        public string Title = "Recorded Donations";
        public string Author = "James";
        public BookPageInfo[] ScorePages;
        public int Size;

        private Mobile m_James;
        [CommandProperty(AccessLevel.Owner)]
        public Mobile James
        {
            get
            {
                return m_James;
            }
            set
            {
                m_James = value;
            }
        }
        [Constructable]
        public IngotChest()
        {
            DonationList = new Dictionary<PlayerMobile, int>();
            Locked = true;
            Movable = false;
        }

        public IngotChest(Serial serial): base(serial)
        {
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
                if (this.Locked == false)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "(" + this.TotalItems + " items, " + this.TotalWeight + " stones)"));
                }
            }
            else
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a donation chest"));
                if (this.Locked == false)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "(" + this.TotalItems + " items, " + this.TotalWeight + " stones)"));
                }
            }
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            int newAmount = 0;
            if (m_James == null)
                return false;

            if (DonationList.ContainsValue(MaxAmount))
            {
                m_James.SayTo(from, true, "Thank you but we have already reached our donation amount!");
                return false;
            }

            if (dropped is IronIngot && dropped.Tag == "mining")
            {
                if (!DonationList.ContainsKey((PlayerMobile)from))
                {
                    newAmount = dropped.Amount;
                    if (newAmount > MaxAmount)
                    {
                        from.AddToBackpack(new IronIngot(newAmount - MaxAmount));
                        newAmount = MaxAmount;
                    }

                    DonationList.Add((PlayerMobile)from, dropped.Amount);
                }
                else
                {
                    DonationList.TryGetValue((PlayerMobile)from, out newAmount);

                    newAmount += dropped.Amount;
                    if (newAmount > MaxAmount)
                    {
                        from.AddToBackpack(new IronIngot(newAmount - MaxAmount));
                        newAmount = MaxAmount;
                    }

                    DonationList.Remove((PlayerMobile)from);
                    DonationList.Add((PlayerMobile)from, newAmount);
                }

                if (DonationList.ContainsValue(MaxAmount))
                {
                    m_James.Say(true, String.Format("{0}! You are the first to reach our donation goal! Thank you for your help and here is your reward!", from.Name));
                    World.Broadcast(m_James.SpeechHue, true, String.Format("James: {0} was the first to donate a total of {1} iron ingots. Congratulations!", from.Name, MaxAmount));
                    Backpack prize = new Backpack();
                    prize.LootType = LootType.Newbied;
                    prize.AddItem(new Forge() { Movable = false, Weight = 255 });

                    from.AddToBackpack(prize);
                }
                else
                {
                    DonationList.TryGetValue((PlayerMobile)from, out newAmount);
                    m_James.Say(true, String.Format("Thank you {0}! You have donated {1} iron ingots in total.", from.Name,newAmount));
                }

                UpdateBook();

                dropped.Delete();
                Tag = "mining";
                return true;
            }
            else if (dropped.Tag != "mining")
            {
                m_James.SayTo(from, true, "You can only donate ingots that have been mined after the event began!");
                Tag = "mining";
                return false;
            }
            else
            {
                m_James.SayTo(from, true, "You can only donate iron ingots!");
                Tag = "mining";
                return false;
            }
        }

        public void UpdateBook()
        {
            int line = 0;
            int page = 0;
            Size = (int)Math.Ceiling((double)DonationList.Count / 8);
            string[] lines = new string[8];
            ScorePages = new BookPageInfo[Size];

            foreach (KeyValuePair<PlayerMobile, int> kvp in DonationList)
            {
                if (line == 8)
                {
                    ScorePages[page] = new BookPageInfo(lines);
                    line = 0;
                    page++;
                }

                lines[line] = String.Format("{0}: {1} ingot{2}", kvp.Key.Name, kvp.Value, (kvp.Value > 1 ? "s" : ""));
                line++;
            }

            while (line < 8)
            {
                lines[line] = "";
                line++;
            }
            ScorePages[page] = new BookPageInfo(lines);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version

            writer.Write((int)DonationList.Count);
            foreach (KeyValuePair<PlayerMobile,int> kvp in DonationList)
            {
                writer.Write((Mobile)kvp.Key);
                writer.Write((int)kvp.Value);
            }

            writer.Write((Mobile)m_James);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            int size = reader.ReadInt();

            DonationList = new Dictionary<PlayerMobile, int>();
            for (int i = 0; i < size; i++)
            {
                PlayerMobile m = reader.ReadMobile() as PlayerMobile;
                int value = reader.ReadInt();
                DonationList.Add(m, value);
            }
            UpdateBook();

            m_James = reader.ReadMobile();

            if (version == 0 && Weight == 25)
                Weight = -1;
        }
    }
}