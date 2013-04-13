using System;
using Server;
using Server.Items;
using EDI = Server.Mobiles.EscortDestinationInfo;

namespace Server.Mobiles
{
	public class Messenger : BaseCreature
	{
		[Constructable]
		public Messenger() : base(AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4) 
		{
			Title = "the messenger";
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

		public override bool CanTeach{ get{ return true; } }
		public override bool ClickTitle{ get{ return false; } } // Do not display 'the messenger' when single-clicking

		private static int GetRandomHue()
		{
			switch ( Utility.Random( 6 ) )
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

			AddItem( PlainShirt(Utility.RandomAllColors()) );

			if ( Female )
				AddItem( Skirt(Utility.RandomAllColors()) );
			else
				AddItem( PlainPants(Utility.RandomAllColors()) );

			switch ( Utility.Random( 4 ) )
			{
				case 0: AddItem( new ShortHair( Utility.RandomHairHue() ) ); break;
				case 1: AddItem( new TwoPigTails( Utility.RandomHairHue() ) ); break;
				case 2: AddItem( new ReceedingHair( Utility.RandomHairHue() ) ); break;
				case 3: AddItem( new KrisnaHair( Utility.RandomHairHue() ) ); break;
			}
		}

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
            AddLootPouch(LootPack.PoorPouch);
            AddLoot(LootPack.PoorPile);
        }

		public Messenger( Serial serial ) : base( serial )
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

    public class EscortableMessenger : BaseEscortable
    {
        [Constructable]
        public EscortableMessenger()
        {
            Title = "the messenger";
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
        public override bool ClickTitle { get { return false; } } // Do not display 'the messenger' when single-clicking

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

            AddItem(PlainShirt(Utility.RandomAllColors()));

            if (Female)
                AddItem(Skirt(Utility.RandomAllColors()));
            else
                AddItem(PlainPants(Utility.RandomAllColors()));

            switch (Utility.Random(4))
            {
                case 0: AddItem(new ShortHair(Utility.RandomHairHue())); break;
                case 1: AddItem(new TwoPigTails(Utility.RandomHairHue())); break;
                case 2: AddItem(new ReceedingHair(Utility.RandomHairHue())); break;
                case 3: AddItem(new KrisnaHair(Utility.RandomHairHue())); break;
            }
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
            AddLootPouch(LootPack.PoorPouch);
            AddLoot(LootPack.PoorPile);
        }

        public EscortableMessenger(Serial serial)
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