using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.Fifth
{
	public class MindBlastSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Mind Blast", "Por Corp Wis",
				218,
				Core.AOS ? 9002 : 9032,
				Reagent.BlackPearl,
				Reagent.MandrakeRoot,
				Reagent.Nightshade,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Fifth; } }

		public MindBlastSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
			if ( Core.AOS )
				m_Info.LeftHandEffect = m_Info.RightHandEffect = 9002;
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		private void AosDelay_Callback( object state )
		{
			object[] states = (object[])state;
			Mobile caster = (Mobile)states[0];
			Mobile target = (Mobile)states[1];
			Mobile defender = (Mobile)states[2];
			int damage = (int)states[3];

			if ( caster.HarmfulCheck( defender ) )
			{
				SpellHelper.Damage( this, target, Utility.RandomMinMax( damage, damage + 4 ), 0, 0, 100, 0, 0 );

				target.FixedParticles( 0x374A, 10, 15, 5038, 1181, 2, EffectLayer.Head );
				target.PlaySound( 0x213 );
			}
		}

		public override bool DelayedDamage{ get{ return true; } }

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendAsciiMessage( "Target can not be seen." ); // Target can not be seen.
			}
			else if ( CheckHSequence( m ) )
			{
				Mobile from = Caster, target = m;

				SpellHelper.Turn( from, target );

				SpellHelper.CheckReflect( (int)this.Circle, ref from, ref target );

                //this is what the demo says, is it right? Seems overpowered
                int casterInt = Caster.Int;
                int targetInt = target.Int;

                if ( casterInt > 100 )
                {
                    casterInt = 100;
                }

                if ( targetInt > 100 )
                {
                    targetInt = 100;
                }

                int stat = ( casterInt - targetInt );
                if (stat <= 0)
                    stat = ( targetInt - casterInt );

                stat /= 4;

                double damage = stat * ((50 + Caster.Skills[SkillName.Magery].Value)) / 100;

				if ( CheckResisted( target, damage ) )
				{
                    damage = (damage + 1) / 2;
					target.SendAsciiMessage( "You feel yourself resisting magical energy." ); // You feel yourself resisting magical energy.
				}

				from.FixedParticles( 0x374A, 10, 15, 2038, EffectLayer.Head );

				target.FixedParticles( 0x374A, 10, 15, 5038, EffectLayer.Head );
				target.PlaySound( 0x213 );

				SpellHelper.Damage( this, target, damage, 0, 0, 100, 0, 0 );
			}

			FinishSequence();
		}

		public override double GetSlayerDamageScalar( Mobile target )
		{
			return 1.0; //This spell isn't affected by slayer spellbooks
		}

		private class InternalTarget : Target
		{
			private MindBlastSpell m_Owner;

			public InternalTarget( MindBlastSpell owner ) : base( 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile)o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}