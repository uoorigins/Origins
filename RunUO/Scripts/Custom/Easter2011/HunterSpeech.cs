using System;
using Server;
using Server.Network;
using Server.Mobiles;
using System.Collections;

namespace Server.Items
{
    public class HunterSpeech : BaseEquipableLight
    {
        public override int LitItemID { get { return 0xA0F; } }
        public override int UnlitItemID { get { return 0xA28; } }

        [Constructable]
        public HunterSpeech()
            : base(0xA28)
        {
            Duration = TimeSpan.Zero;

            Burning = false;
            Light = LightType.Circle150;
            Weight = 1.0;
            Hue = 2101;
            LootType = LootType.Blessed;

            Timer.DelayCall(TimeSpan.Zero, new TimerCallback(NewTimer));

        }

        public HunterSpeech(Serial serial) : base(serial)
        {
        }

        public void NewTimer()
        {
            new InternalTimer(this, TimeSpan.Zero, TimeSpan.FromMinutes(Utility.RandomMinMax(5, 10))).Start();
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "I lit the way, Origins Beta Tester"));
            }
        }

        private class InternalTimer : Timer
        {
            private Item m_Item;

            public InternalTimer(Item myitem, TimeSpan duration,TimeSpan interval) : base(duration, interval)
            {
                m_Item = myitem;
            }

            protected override void OnTick()
            {
                foreach (Mobile m in m_Item.GetMobilesInRange(5))
                {
                    if (Utility.RandomBool())
                        continue;

                    if (m is Hunter)
                    {
                        switch (Utility.Random(3))
                        {
                            case 2: m.Say(true, "The rabbits must go!"); break;
                            case 1: m.Say(true, "Who does Burian think he is?! Hunting is the only way to solve this."); break;
                            case 0: m.Say(true, "I've seen the easter bunny once. Almost got him in my trap!"); break;
                        }
                    }
                    break;
                }
                //new InternalTimer(m_Item, TimeSpan.Zero, TimeSpan.FromSeconds(Utility.RandomMinMax(5, 10))).Start();
                //Stop();
            }
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

            if (LootType != LootType.Blessed)
                LootType = LootType.Blessed;
        }
    }
}