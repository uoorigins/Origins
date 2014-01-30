using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class Fists : BaseMeleeWeapon
	{
        public override string AsciiName { get { return "fist"; } }

		public static void Initialize()
		{
			Mobile.DefaultWeapon = new Fists();
		}

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.Disarm; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }

		public override int AosStrengthReq{ get{ return 0; } }
		public override int AosMinDamage{ get{ return 1; } }
		public override int AosMaxDamage{ get{ return 4; } }
		public override int AosSpeed{ get{ return 50; } }

		public override int OldStrengthReq{ get{ return 0; } }
		public override int OldMinDamage{ get{ return 1; } }
		public override int OldMaxDamage{ get{ return 8; } }
		public override int OldSpeed{ get{ return 50; } }

		public override int DefHitSound{ get{ return -1; } }
		public override int DefMissSound{ get{ return -1; } }

		public override SkillName DefSkill{ get{ return SkillName.Wrestling; } }
		public override WeaponType DefType{ get{ return WeaponType.Fists; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Wrestle; } }

		public Fists() : base( 0 )
		{
			Visible = false;
			Movable = false;
			Quality = WeaponQuality.Regular;
		}

		public Fists( Serial serial ) : base( serial )
		{
		}

		public override double GetDefendSkillValue( Mobile attacker, Mobile defender )
		{
			double wresValue = defender.Skills[SkillName.Wrestling].Value;

			return wresValue;
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

			Delete();
		}

		/* Wrestling moves */

		private static bool CheckMove( Mobile m, SkillName other )
		{
			double wresValue = m.Skills[SkillName.Wrestling].Value;
			double scndValue = m.Skills[other].Value;

			/* 40% chance at 80, 80
			 * 50% chance at 100, 100
			 * 60% chance at 120, 120
			 */

			double chance = (wresValue + scndValue) / 400.0;

			return ( chance >= Utility.RandomDouble() );
		}

		private static bool HasFreeHands( Mobile m )
		{
			Item item = m.FindItemOnLayer( Layer.OneHanded );

			if ( item != null && !(item is Spellbook) )
				return false;

			return m.FindItemOnLayer( Layer.TwoHanded ) == null;
		}

		private class MoveDelayTimer : Timer
		{
			private Mobile m_Mobile;

			public MoveDelayTimer( Mobile m ) : base( TimeSpan.FromSeconds( 10.0 ) )
			{
				m_Mobile = m;

				Priority = TimerPriority.TwoFiftyMS;

				m_Mobile.BeginAction( typeof( Fists ) );
			}

			protected override void OnTick()
			{
				m_Mobile.EndAction( typeof( Fists ) );
			}
		}

		private static void StartMoveDelay( Mobile m )
		{
			new MoveDelayTimer( m ).Start();
		}
	}
}