using System;
using Server.Items;
using Server;
using Server.Misc;

namespace Server.Mobiles
{
	public class Hunter : BaseCreature
	{
		[Constructable]
		public Hunter() : base(AIType.AI_Archer, FightMode.Aggressor, 10, 1, 0.2, 0.4)
		{
			InitStats( 300, 300, 100 );
			Title = "the hunter";

			SpeechHue = Utility.RandomDyedHue();

			Hue = Utility.RandomSkinHue();

			if ( Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
			}

            AddItem( new StuddedChest() );
			AddItem( new StuddedArms() );
			AddItem( new StuddedGloves() );
			AddItem( new StuddedGorget() );
			AddItem( new StuddedLegs() );
			AddItem( new ThighBoots() );
            AddItem( new BodySash(2118));
            AddItem(Utility.RandomBool() ? (Item)new BearMask() : (Item)new DeerMask());
            AddItem(Utility.RandomBool() ? (Item)new Bow() : (Item)new Crossbow());

			Container pack = new Backpack();

			pack.Movable = false;

			Arrow arrows = new Arrow( 250 );

			arrows.LootType = LootType.Newbied;

			pack.DropItem( arrows );
			pack.DropItem( new Gold( 10, 25 ) );

			AddItem( pack );

			Skills[SkillName.Anatomy].Base = 120.0;
			Skills[SkillName.Tactics].Base = 120.0;
			Skills[SkillName.Archery].Base = 120.0;
			Skills[SkillName.MagicResist].Base = 120.0;
			Skills[SkillName.DetectHidden].Base = 100.0;
		}

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
            AddLootPouch(LootPack.PoorPouch);
            AddLoot(LootPack.PoorPile);
        }


		public override bool CanTeach { get { return false; } }
		public override bool ClickTitle { get { return false; } }

        public Hunter(Serial serial)
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version 
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
