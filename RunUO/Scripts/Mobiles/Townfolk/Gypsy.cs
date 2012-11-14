using System;
using Server.Items;
using Server;
using Server.Misc;

namespace Server.Mobiles
{
	public class Gypsy : BaseCreature
	{


		[Constructable]
		public Gypsy()
			: base( AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4 )
		{
			InitStats( 31, 41, 51 );

			SpeechHue = Utility.RandomDyedHue();

			SetSkill( SkillName.Cooking, 65, 88 );
			SetSkill( SkillName.Snooping, 65, 88 );
			SetSkill( SkillName.Stealing, 65, 88 );

			Hue = Utility.RandomSkinHue();

			if( this.Female = Utility.RandomBool() )
			{
				this.Body = 0x191;
				this.Name = NameList.RandomName( "female" );
				AddItem( Skirt(Utility.RandomAllColors()));
				AddItem( new Shirt( Utility.RandomAllColors() ) );
				AddItem( RandomFootware() );
                AddItem( new Bandana(Utility.RandomBlueHue()));
				Title = "the gypsy";
			}
			else
			{
				this.Body = 0x190;
				this.Name = NameList.RandomName( "male" );
                AddItem( new ShortPants(Utility.RandomAllColors()));
                AddItem(new Shirt(Utility.RandomAllColors()));
                AddItem(RandomFootware());
                AddItem(new Bandana(Utility.RandomBlueHue()));
				Title = "the gypsy";
			}

			Utility.AssignRandomHair( this );

			Container pack = new Backpack();

			pack.Movable = false;

			AddItem( pack );
		}

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
            AddLootPouch(LootPack.PoorPouch);
            AddLoot(LootPack.PoorPile);
        }


		public override bool CanTeach { get { return true; } }
		public override bool ClickTitle { get { return false; } }

		public Gypsy( Serial serial )
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
