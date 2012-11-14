using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a hare corpse" )]
	public class Bunny : BaseCreature
	{
		[Constructable]
		public Bunny() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a bunny";
			Body = 205;

		    Hue = Utility.RandomAnimalHue();

			SetStr( 6, 10 );
			SetDex( 26, 38 );
			SetInt( 6, 14 );

			SetHits( 4, 6 );
			SetMana( 0 );

			SetDamage( 1 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 5, 10 );

			SetSkill( SkillName.MagicResist, 5.0 );
			SetSkill( SkillName.Tactics, 5.0 );
			SetSkill( SkillName.Wrestling, 5.0 );

			Fame = 150;
			Karma = 0;

			VirtualArmor = 6;

			Tamable = false;
			ControlSlots = 1;
            MinTameSkill = 120.0;

            /*if (Utility.Random(1000) == 500)
            {
                Timer.DelayCall(TimeSpan.Zero, new TimerCallback(CreateBunny));
                Timer.DelayCall(TimeSpan.Zero, new TimerCallback(Delete));
            }*/
		}

		public override int Meat{ get{ return 2; } }
		public override int Fur{ get{ return 4; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies; } }
        public override bool CanBeDamaged() { return false; }
        public override bool Unprovokable { get { return true; } }

		public Bunny(Serial serial) : base(serial)
		{
		}

        public void CreateBunny()
        {
            new EasterBunny().MoveToWorld(this.Location, this.Map);
            return;
        }

        private static int Convert(string value)
        {
            try
            {
                int number = Int32.Parse(value);
                return number;
            }
            catch (FormatException)
            {
                return 0;
            }
            catch (ArgumentNullException)
            {
                return 0;
            }
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            if (Convert(from.Tag) >= 10)
            {
                from.SendAsciiMessage("You must bring your bunnies to the rabbit herder before taking more.");
                return false;
            }

            if (dropped is Carrot)
            {
                from.Tag = (Convert(from.Tag) + 1).ToString();
                this.Controlled = true;
                this.ControlMaster = from;
                this.ControlOrder = OrderType.Follow;
                this.ControlTarget = from;
            }

            return base.OnDragDrop(from, dropped);
        }

		public override int GetAttackSound() 
		{ 
			return 0xC9; 
		} 

		public override int GetHurtSound() 
		{ 
			return 0xCA; 
		} 

		public override int GetDeathSound() 
		{ 
			return 0xCB; 
		}

        public override bool HandlesOnSpeech(Mobile from)
        {
            return true;
        }

        public override void OnSpeech(SpeechEventArgs e)
        {
            if (!e.Handled && e.HasKeyword(0x16D))
            {
                return;
            }
            else
            {
                base.OnSpeech(e);
            }
        }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}