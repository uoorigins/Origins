using System;
using Server;
using Server.Items;
using EDI=Server.Mobiles.EscortDestinationInfo;

namespace Server.Mobiles
{
	public class Merchant : BaseCreature
	{
		[Constructable]
        public Merchant()
            : base(AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4) 
		{
			Title = "the merchant";
			SetSkill( SkillName.ItemID, 55.0, 78.0 );
			SetSkill( SkillName.ArmsLore, 55, 78 );
            InitBody();
            InitOutfit();
        }

        public virtual void InitBody()
        {
            SetStr(90, 100);
            SetDex(90, 100);
            SetInt(15, 25);

            Hue = Utility.RandomSkinHue();
            SpeechHue = Utility.RandomDyedHue();

            if (Female = Utility.RandomBool())
            {
                Body = 401;
                Name = NameList.RandomName("female");
            }
            else
            {
                Body = 400;
                Name = NameList.RandomName("male");
            }
        }

		public override bool CanTeach { get { return true; } }
		public override bool ClickTitle { get { return false; } } // Do not display 'the merchant' when single-clicking

		private static int GetRandomHue()
		{
			switch( Utility.Random( 6 ) )
			{
				default:
				case 0: return 0;
				case 1: return Utility.RandomBlueHue();
				case 2: return Utility.RandomGreenHue();
				case 3: return Utility.RandomRedHue();
				case 4: return Utility.RandomYellowHue();
				case 5: return Utility.RandomNeutralHue();
			}

		}

		public virtual void InitOutfit()
		{
			if( Female )
				AddItem( Skirt(Utility.RandomAllColors()) );
			else
                AddItem(new LongPants(Utility.RandomAllColors()));


			AddItem( new Shoes(Utility.RandomNeutralHue()) );
			AddItem( new FancyShirt( Utility.RandomAllColors() ) );

			Utility.AssignRandomHair( this );
		}

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
            AddLootPouch(LootPack.PoorPouch);
            AddLoot(LootPack.PoorPile);
        }

		public Merchant( Serial serial )
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

    public class EscortabelMerchant : BaseEscortable
    {
        [Constructable]
        public EscortabelMerchant()
        {
            Title = "the merchant";
            SetSkill(SkillName.ItemID, 55.0, 78.0);
            SetSkill(SkillName.ArmsLore, 55, 78);
            InitBody();
            InitOutfit();
        }

        public override void InitBody()
        {
            SetStr(90, 100);
            SetDex(90, 100);
            SetInt(15, 25);

            Hue = Utility.RandomSkinHue();
            SpeechHue = Utility.RandomDyedHue();

            if (Female = Utility.RandomBool())
            {
                Body = 401;
                Name = NameList.RandomName("female");
            }
            else
            {
                Body = 400;
                Name = NameList.RandomName("male");
            }
        }

        public override bool CanTeach { get { return true; } }
        public override bool ClickTitle { get { return false; } } // Do not display 'the merchant' when single-clicking

        private static int GetRandomHue()
        {
            switch (Utility.Random(6))
            {
                default:
                case 0: return 0;
                case 1: return Utility.RandomBlueHue();
                case 2: return Utility.RandomGreenHue();
                case 3: return Utility.RandomRedHue();
                case 4: return Utility.RandomYellowHue();
                case 5: return Utility.RandomNeutralHue();
            }

        }

        public override void InitOutfit()
        {
            if (Female)
                AddItem(Skirt(Utility.RandomAllColors()));
            else
                AddItem(new LongPants(Utility.RandomAllColors()));


            AddItem(new Shoes(Utility.RandomNeutralHue()));
            AddItem(new FancyShirt(Utility.RandomAllColors()));

            Utility.AssignRandomHair(this);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
            AddLootPouch(LootPack.PoorPouch);
            AddLoot(LootPack.PoorPile);
        }

        public EscortabelMerchant(Serial serial)
            : base(serial)
        {
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