using Server;
using Server.Items;
using Server.Mobiles;
using Server.Multis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scripts.Multis.Camps
{
    class WarlordsKeep : BaseCamp
    {
        public virtual Mobile Mages { get { return new EvilMage(); } }

        public override CampType Camp { get { return CampType.EvilMage; } }

        [Constructable]
        public WarlordsKeep()
            : base(1558)
        {
        }

        public override void AddComponents()
        {
            BaseCreature bc;

            DecayDelay = TimeSpan.FromMinutes(5.0);

            //AddItem(new Static(100054 0 0 -1 AT 3 2 7);
            AddItem(new Static(5484),  1, -1,  6);
            AddItem(new Static(5472),  2, -1,  6);
            AddItem(new Static(5472),  4, -1,  6);
            AddItem(new Static(5484),  6, -1,  6);
            AddItem(new Static(5472), -2,  2,  6);
            AddItem(new Static(5472), -6,  2,  6);
            AddItem(new Static(5482), -1, -4,  6);
            AddItem(new Static(5483), -1, -5,  6);
            AddItem(new Static(5480),  0, -6,  6);
            AddItem(new Static(5481),  1, -6,  6);
            AddItem(new Static(5468),  2, -6,  6);
            AddItem(new Static(5469),  3, -6,  6);
            AddItem(new Static(5468),  4, -6,  6);
            AddItem(new Static(5480),  5, -6,  6);
            AddItem(new Static(5481),  6, -6,  6);
            //AddItem(new Static(100054), 6 -6  6);
            AddItem(new Static(5645),  2,  5, 26);
            AddItem(new Static(5645),  2,  3, 26);
            AddItem(new Static(5645),  2,  2, 26);
            AddItem(new Static(5645),  2,  1, 26);
            AddItem(new Static(5645),  2,  0, 26);
            AddItem(new Static(5645),  2, -1, 26);
            AddItem(new Static(5645),  2, -2, 26);
            AddItem(new Static(5645),  2, -3, 26);
            AddItem(new Static(5645),  2, -4, 26);
            AddItem(new Static(5645),  2, -5, 26);
            AddItem(new Static(5645),  2, -6, 26);
            AddItem(new Static(5646),  2, -6, 26);
            AddItem(new Static(5646),  3, -6, 26);
            AddItem(new Static(5646),  4, -6, 26);
            AddItem(new Static(5646),  5, -6, 26);
            AddItem(new Static(5646),  6, -6, 26);
            AddItem(new Static(5646),  7, -6, 26);
            //AddItem(new Static(100054), 3 2 27);
            AddItem(new Static(4609),  5,  0, 46);
            AddItem(new Static(4611),  5, -1, 46);
            AddItem(new Static(4611),  5, -2, 46);
            AddItem(new Static(4610),  5, -3, 46);
            AddItem(new Static(100054), 3, 2, 47);
            //AddItem(new Static({ 101586 1 101587 1 0 3 }), 2 0 66);
            AddItem(new Static(100054), 2, 3, 67);
            AddItem(new Static(100054), 3, 2, 67);

        }

        public WarlordsKeep(Serial serial)
            : base(serial)
        {
        }

        // Don't refresh decay timer
        public override void OnEnter(Mobile m)
        {
            if (m.Player && Prisoner != null && Prisoner.CantWalk)
            {
                string number;

                switch (Utility.Random(8))
                {
                    case 0: number = "HELP!"; break; // HELP!
                    case 1: number = "Help me!"; break; // Help me!
                    case 2: number = "Canst thou aid me?!"; break; // Canst thou aid me?!
                    case 3: number = "Help a poor prisoner!"; break; // Help a poor prisoner!
                    case 4: number = "Help! Please!"; break; // Help! Please!
                    case 5: number = "Aaah! Help me!"; break; // Aaah! Help me!
                    case 6: number = "Go and get some help!"; break; // Go and get some help!
                    default: number = "Quickly, I beg thee! Unlock my chains! If thou dost look at me close thou canst see them.	"; break; // Quickly, I beg thee! Unlock my chains! If thou dost look at me close thou canst see them.	
                }

                Prisoner.Yell(true, number);
            }
        }

        // Don't refresh decay timer
        public override void OnExit(Mobile m)
        {
        }

        public override void AddItem(Item item, int xOffset, int yOffset, int zOffset)
        {
            if (item != null)
                item.Movable = false;

            base.AddItem(item, xOffset, yOffset, zOffset);
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
