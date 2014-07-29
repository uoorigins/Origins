using System;
using System.Collections;
using Server.Misc;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "an orcish corpse" )]
	public class OrcCaptain : BaseCreature
	{
		public override InhumanSpeech SpeechType{ get{ return InhumanSpeech.Orc; } }

		[Constructable]
		public OrcCaptain() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "orc" );
			Body = 7;
            BaseSoundID = 432;// 0x45A;

			SetStr( 111, 145 );
			SetDex( 101, 135 );
			SetInt( 86, 110 );

			SetHits( 111, 145 );

			SetDamage( 5, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 70.1, 85.0 );
			SetSkill( SkillName.Swords, 70.1, 95.0 );
			SetSkill( SkillName.Tactics, 85.1, 100.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 34;

            PackItem( new ThighBoots() );
            PackItem(new RingmailChest());
            PackItem(new TwoHandedAxe());

            if (Utility.Random(10) == 1)
                PackItem(new OrcHelm());
		}

        public override void GenerateLoot()
        {
            AddLootBackpack(LootPack.Meager);

            if (Utility.RandomBool())
                AddLoot(LootPack.Gems);
        }

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int Meat{ get{ return 1; } }

        public override int GetIdleSound()
        {
            return 432;
        }

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.SavagesAndOrcs; }
		}

        public override bool IsEnemy( Mobile m )
        {
            bool isFightingOrc = false;
            isFightingOrc = m != null && m.Combatant != null && ( m.Combatant is OrcishMage || m.Combatant is Orc || m.Combatant is OrcCaptain || m.Combatant is OrcishLord );

            if ( m.Player && m.FindItemOnLayer( Layer.Helm ) is OrcishKinMask || ( m.Guild != null && m.Guild.Id == 34 ) )
            {
                if ( Combatant != null && Combatant.Guild != null && Combatant.Guild.Id == 34 )
                {
                    return true;
                }

                if ( m.Guild != null && m.Guild.Id == 34 && isFightingOrc )
                {
                    return true;
                }

                return false;
            }

            return base.IsEnemy( m );
        }

		public override void AggressiveAction( Mobile aggressor, bool criminal )
		{
			base.AggressiveAction( aggressor, criminal );

			Item item = aggressor.FindItemOnLayer( Layer.Helm );

			if ( item is OrcishKinMask )
			{
				AOS.Damage( aggressor, 50, 0, 100, 0, 0, 0 );
				item.Delete();
				aggressor.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
				aggressor.PlaySound( 0x307 );
			}
		}

		public OrcCaptain( Serial serial ) : base( serial )
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