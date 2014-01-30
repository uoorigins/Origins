using System;
using Server.Items;
using Server.Network;
using Server.Engines.Harvest;

namespace Server.Items
{
	[FlipableAttribute( 0x13B0, 0x13AF )]
	public class WarAxe : BaseAxe
	{
        public override string AsciiName { get { return "war axe"; } }
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ArmorIgnore; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.BleedAttack; } }

        public override int AosStrengthReq { get { return 35; } }
        public override int AosMinDamage { get { return 14; } }
        public override int AosMaxDamage { get { return 15; } }
        public override int AosSpeed { get { return 33; } }

        public override int OldStrengthReq { get { return 35; } }
        public override int OldMinDamage { get { return 5; } }
        public override int OldMaxDamage { get { return 21; } }
        public override int OldSpeed { get { return 35; } }

        public override int DefHitSound { get { return 0x233; } }
        public override int DefMissSound { get { return 0x239; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 80; } }

        public override SkillName DefSkill { get { return SkillName.Macing; } }
        public override WeaponType DefType { get { return WeaponType.Bashing; } }
        public override WeaponAnimation DefAnimation { get { return WeaponAnimation.Bash1H; } }

		[Constructable]
		public WarAxe() : base( 0x13B0 )
		{
			Weight = 8.0;
		}

		public WarAxe( Serial serial ) : base( serial )
		{
		}

        public override void OnHit(Mobile attacker, Mobile defender, double damageBonus)
        {
            base.OnHit(attacker, defender, damageBonus);

            defender.Stam -= Utility.Random(3, 5); // 3-5 points of stamina loss
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