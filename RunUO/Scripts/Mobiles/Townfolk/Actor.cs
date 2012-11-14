using System; 
using Server.Items; 
using Server; 
using Server.Misc; 

namespace Server.Mobiles 
{ 
	public class Actor : BaseCreature 
	{ 
		[Constructable] 
		public Actor () : base( AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4 ) 
		{ 
			InitStats( 31, 41, 51 ); 

			SpeechHue = Utility.RandomDyedHue(); 

			Hue = Utility.RandomSkinHue(); 

			if ( this.Female = Utility.RandomBool() ) 
			{ 
				this.Body = 0x191; 
				this.Name = NameList.RandomName( "female" );
				AddItem( PlainShirt(Utility.RandomAllColors()) );
                AddItem( Skirt(Utility.RandomAllColors()) );

				Title = "the actress"; 
			} 
			else 
			{ 
				this.Body = 0x190; 
				this.Name = NameList.RandomName( "male" );
                AddItem(PlainShirt(Utility.RandomAllColors()));
                AddItem(PlainPants(Utility.RandomAllColors()));

				Title = "the actor";
			} 

			Utility.AssignRandomHair( this );

            AddItem(RandomShoes(Utility.RandomNeutralHue()));

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


		public override bool ClickTitle{ get{ return false; } }

		public Actor( Serial serial ) : base( serial ) 
		{ 
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
