using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a hare corpse" )]
	public class EasterBunny : BaseCreature
	{
		[Constructable]
		public EasterBunny() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "an easter bunny";
			Body = 205;

            Hue = Utility.RandomList(198,78,18);

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

            Blessed = true;
		}

		public override int Meat{ get{ return 2; } }
		public override int Fur{ get{ return 4; } }
        public override bool Unprovokable { get { return true; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies; } }
        public override bool CanBeDamaged() { return false; }

		public EasterBunny(Serial serial) : base(serial)
		{
		}

        public class BunnyHole : Item
        {
            public BunnyHole() : base(0x913)
            {
                Movable = false;
                Hue = 1;
                Name = "a rabbit hole";

                Timer.DelayCall(TimeSpan.FromSeconds(40.0), new TimerCallback(Delete));
            }

            public BunnyHole(Serial serial)
                : base(serial)
            {
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

                Delete();
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (this.Tag == "taken")
                return;

            DelayBeginTunnel();
            this.Tag = "taken";
            Item eggs = new BrightlyColoredEggs();
            eggs.Hue = this.Hue;
            from.AddToBackpack(eggs);
            from.SendAsciiMessage("You have recieved some brightly colored eggs!");
        }

        public virtual void DelayBeginTunnel()
        {
            Timer.DelayCall(TimeSpan.FromSeconds(2.0), new TimerCallback(BeginTunnel));
        }

        private Item hole;
        private Point3D location;

        public virtual void BeginTunnel()
        {
            if (Deleted)
                return;

            hole = new BunnyHole();
            hole.MoveToWorld(Location, Map);
            location = this.Location;
            Frozen = true;
            Say(true,"* The bunny begins to dig a tunnel back to its underground lair *");
            PlaySound(0x247);

            Timer.DelayCall(TimeSpan.FromSeconds(2.0), new TimerCallback(Delete));
            Timer.DelayCall(TimeSpan.FromSeconds(4.0), new TimerCallback(Vorpal));
        }

        public void Vorpal()
        {
            new VorpalBunny().MoveToWorld(location, Map.Felucca);
            hole.PublicOverheadMessage(Network.MessageType.Regular, 0x0, true, "*A vorpal bunny appears*");
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