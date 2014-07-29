using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "an orcish corpse" )]
	public class Orc : BaseCreature
	{
		public override InhumanSpeech SpeechType{ get{ return InhumanSpeech.Orc; } }

		[Constructable]
		public Orc() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "orc" );
			Body = (Utility.RandomBool() ? 17 : 41);
            BaseSoundID = 432;//0x45A;

			SetStr( 96, 120 );
			SetDex( 81, 105 );
			SetInt( 36, 60 );

			SetHits( 96, 120 );

			SetDamage( 5, 7 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 30 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.MagicResist, 50.1, 75.0 );
			SetSkill( SkillName.Tactics, 55.1, 80.0 );
			SetSkill( SkillName.Wrestling, 50.1, 70.0 );

			Fame = 1500;
			Karma = -1500;

			VirtualArmor = 28;

            if (BodyValue == 41)
                PackItem(new Club());

			PackItem( new ThighBoots() );
		}

        public override void GenerateLoot()
        {
            AddLootBackpack(LootPack.Poor);
            AddLoot(LootPack.PoorPile);
        }

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 1; } }
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

		public Orc( Serial serial ) : base( serial )
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
