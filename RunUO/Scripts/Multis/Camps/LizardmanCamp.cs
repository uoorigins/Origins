using System;
using System.Collections.Generic;
using System.Text;

namespace Scripts.Multis.Camps
{
    using Server;
    using Server.Items;
    using Server.Mobiles;
    using Server.Multis;
    using System;
    using System.Collections.Generic;
    using System.Text;

    namespace Scripts.Multis.Camps
    {
        class LizardmanCamp : BaseCamp
        {
            public virtual Mobile Lizardmen { get { return new Lizardman(); } }

            public override CampType Camp { get { return CampType.Lizardman; } }

            [Constructable]
            public LizardmanCamp()
                : base(4334)
            {
            }

            public override void AddComponents()
            {
                BaseCreature bc;

                DecayDelay = TimeSpan.FromMinutes(5.0);

                AddItem(new Static(4012), 0, 7, 0);
                AddItem(new Static(3555), 0, 7, 0);
                AddItem(new Static(2420), 0, 7, 0);

                AddMobile(Lizardmen, 6, 4, 4, 0);
                AddMobile(Lizardmen, 6, 4, -4, 0);
                AddMobile(Lizardmen, 6, -4, 4, 0);
                AddMobile(Lizardmen, 6, -4, -4, 0);

                if (Utility.RandomBool())
                    Prisoner = new EscortableNoble(this);
                else
                    Prisoner = new SeekerOfAdventure(this);

                bc = (BaseCreature)Prisoner;
                bc.IsPrisoner = true;
                bc.CantWalk = true;

                AddMobile(Prisoner, 0, 1, 1, 0);

                AddItem(new Static(1055), 5, 5, 0);
                AddItem(new Static(1065), 5, -4, 0);
                AddItem(new Static(1056), -4, 5, 0);
                AddItem(new Static(1064), -4, -4, 0);

                AddItem(new TreasureLevel1(), 2, 2, 0);
                AddItem(new TreasureLevel2(), -2, -2, 0);
            }

            public LizardmanCamp(Serial serial)
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
}
