using System;
using Server;
using Server.Items;
using System.Collections;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "a poison elementals corpse" )]
	public class PoisonElemental : BaseCreature
	{
		[Constructable]
		public PoisonElemental () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a poison elemental";
			Body = 162;
			BaseSoundID = 263;

            Hue = Utility.RandomMinMax(61, 79);

            /*switch (Utility.Random(2))
            {
                case 0: Hue = 61; break;
                case 1: Hue = 79; break;
            }*/

			SetStr( 426, 515 );
			SetDex( 166, 185 );
			SetInt( 361, 435 );

			SetHits( 426, 515 );

			SetDamage( 12, 18 );

			SetDamageType( ResistanceType.Physical, 10 );
			SetDamageType( ResistanceType.Poison, 90 );

			SetResistance( ResistanceType.Physical, 60, 70 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.EvalInt, 80.1, 95.0 );
			SetSkill( SkillName.Magery, 80.1, 95.0 );
			SetSkill( SkillName.Meditation, 80.2, 120.0 );
			SetSkill( SkillName.Poisoning, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 85.2, 115.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 70.1, 90.0 );

			Fame = 12500;
			Karma = -12500;

			VirtualArmor = 70;

			//PackItem( new Nightshade( 4 ) );
			//PackItem( new LesserPoisonPotion() );
		}

		public override void GenerateLoot()
		{
            AddLoot(LootPack.HighScrolls);

            AddLootBackpack(LootPack.UltraRich);
		}

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            List<Item> list = new List<Item>();
            foreach (Item item in c.Items)
                list.Add(item);

            foreach (Item item in list)
                item.MoveToWorld(c.Location, c.Map);

            c.Delete();

        }

		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override double HitPoisonChance{ get{ return 0.75; } }

		public override int TreasureMapLevel{ get{ return 5; } }

		public PoisonElemental( Serial serial ) : base( serial )
		{
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