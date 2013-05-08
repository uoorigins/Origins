using Server;
using Server.Items;
using Server.Mobiles;
using Server.Multis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scripts.Multis.Camps
{
    class EvilMageTower : BaseCamp
    {
        public virtual Mobile Mages { get { return new EvilMage(); } }

        public override CampType Camp { get { return CampType.EvilMage; } }

        [Constructable]
        public EvilMageTower() : base(1558)
        {
        }

        public override void AddComponents()
        {
            BaseCreature bc;

            DecayDelay = TimeSpan.FromMinutes(5.0);

            AddItem(new Static(4628),   2,  3,  0);
            AddItem(new Static(4629),   2,  2,  0);
            AddItem(new Static(4622),   2,  1,  0);
            AddItem(new Static(4627),   3,  3,  0);
            AddItem(new Static(4630),   3,  2,  0);
            AddItem(new Static(4623),   3,  1,  0);
            AddItem(new Static(4626),   4,  3,  0);
            AddItem(new Static(4625),   4,  2,  0);
            AddItem(new Static(4624),   4,  1,  0);
            AddMobile(Mages, 6, 3, 2, 0);
            AddItem(new Static(6665),  -1, -4,  0);
            AddItem(new Static(6661),  -1, -6,  0);
            AddItem(new Static(6658),   3, -6,  1);
            AddItem(new Static(6659),   6, -6,  1);
            AddItem(new Static(5645),   2,  5, 0);
            AddItem(new Static(5645),   2,  3, 0);
            AddItem(new Static(5645),   2,  2, 0);
            AddItem(new Static(5645),   2 , 1, 0);
            AddItem(new Static(5645),   2,  0, 0);
            AddItem(new Static(5645),   2, -1, 0);
            AddItem(new Static(5645),   2, -2, 0);
            AddItem(new Static(5645),   2, -3, 0);
            AddItem(new Static(5645),   2, -4, 0);
            AddItem(new Static(5645),   2, -5, 0);
            AddItem(new Static(5645),   2, -6, 0);
            AddItem(new Static(5646), 2, -6, 0);
            AddItem(new Static(5646), 3, -6, 0);
            AddItem(new Static(5646), 4, -6, 0);
            AddItem(new Static(5646), 5, -6, 0);
            AddItem(new Static(5646), 6, -6, 0);
            AddItem(new Static(5646), 7, -6, 0);
            AddItem(new Static(7576),   4, -1, 0);
            AddItem(new Static(7575),   4,  0, 0);
            AddItem(new Static(7420),   5, -1, 0);
            AddItem(new Static(7418),   5,  0, 0);
            AddMobile(Mages, 6, 3, 2, 0);
            AddItem(new Static(4609),   5,  0, 0);
            AddItem(new Static(4611),   5, -1, 0);
            AddItem(new Static(4611),   5, -2, 0);
            AddItem(new Static(4610),   5, -3, 0);
            AddItem(new Static(7400),   5, -2, 6);
            AddItem(new Static(7399),   5, -1, 6);
            AddMobile(Mages, 6, 3, 2, 0);
            AddItem(new Static(4073),   1,  1, 0);
            AddItem(new Static(4070),   1,  0, 0);
            AddItem(new Static(4071),   1, -1, 0);
            AddItem(new Static(4076),   2,  1, 0);
            AddItem(new Static(4074),   2,  0, 0);
            AddItem(new Static(4072),   2, -1, 0);
            AddItem(new Static(4077),   3,  1, 0);
            AddItem(new Static(4078),   3,  0, 0);
            AddItem(new Static(4075),   3, -1, 0);

            if (Utility.RandomBool())
                Prisoner = new EscortableNoble(this);
            else
                Prisoner = new SeekerOfAdventure(this);

            bc = (BaseCreature)Prisoner;
            bc.IsPrisoner = true;
            bc.CantWalk = true;

            AddMobile(Prisoner, 0, 2, 0, 0);

            AddMobile(Mages, 6, 2, 3, 0);
            AddMobile(Mages, 6, 3, 2, 0);
        }

        public EvilMageTower(Serial serial)
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
		
		public override void AddItem( Item item, int xOffset, int yOffset, int zOffset )
		{
			if ( item != null )
				item.Movable = false;
				
			base.AddItem( item, xOffset, yOffset, zOffset );
		}
			
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
    }


}
