using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a sea serpents corpse" )]
	[TypeAlias( "Server.Mobiles.Seaserpant" )]
	public class SeaSerpent : BaseCreature
	{
		[Constructable]
		public SeaSerpent() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a sea serpent";
			Body = 150;
			BaseSoundID = 447;

			Hue = Utility.Random( 0x530, 9 );

			SetStr( 168, 225 );
			SetDex( 58, 85 );
			SetInt( 53, 95 );

			SetHits( 166, 185 );

			SetDamage( 7, 13 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 15, 20 );

			SetSkill( SkillName.MagicResist, 60.1, 75.0 );
			SetSkill( SkillName.Tactics, 60.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 70.0 );

			Fame = 6000;
			Karma = -6000;

			VirtualArmor = 30;
			CanSwim = true;
			CantWalk = true;
		}

        public override void GenerateLoot()
        {
            if ( Utility.Random( 100 ) > 75 )
                AddLoot( LootPack.MedScrolls );

            AddLootBackpack( LootPack.Rich );
        }

		public override bool HasBreath{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 2; } }

		public override int Hides{ get{ return 10; } }
		//public override HideType HideType{ get{ return HideType.Horned; } }
		//public override int Scales{ get{ return 8; } }
		//public override ScaleType ScaleType{ get{ return ScaleType.Blue; } }
        public override bool AlwaysMurderer { get { return true; } }

		public SeaSerpent( Serial serial ) : base( serial )
		{
		}

        public override int GetAttackSound()
        {
            return 448;
        }

        public override int GetAngerSound()
        {
            return 447;
        }

        public override int GetDeathSound()
        {
            return 450; //Other Death sound is 1258... One for Yamadon, one for Serado?
        }

        public override int GetHurtSound()
        {
            return 449;
        }

        public override int GetIdleSound()
        {
            return 447;
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}