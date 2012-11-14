using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.Second
{
	public class AgilitySpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Agility", "Ex Uus",
				212,
				9061,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot
			);

		public override SpellCircle Circle { get { return SpellCircle.Second; } }

		public AgilitySpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
            Caster.Target = new InternalTarget(this);
		}

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendAsciiMessage( "Target can not be seen." ); // Target can not be seen.
			}
			else if ( CheckBSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

                SpellHelper.CheckReflect((int)this.Circle, Caster, ref m);

				SpellHelper.AddStatBonus( Caster, m, StatType.Dex );

				m.FixedParticles( 0x375A, 10, 15, 5010, EffectLayer.Waist );
				m.PlaySound( 487 );

				//int percentage = (int)(SpellHelper.GetOffsetScalar( Caster, m, false )*100);
				//TimeSpan length = SpellHelper.GetDuration( Caster, m );
                int percentage = (int)((Caster.Skills[SkillName.Magery].Value / 10) + 1);
                TimeSpan length = TimeSpan.FromSeconds((6 * Caster.Skills[SkillName.Magery].Value / 5) + 1);

				BuffInfo.AddBuff( m, new BuffInfo( BuffIcon.Agility, 1075841, length, m, percentage.ToString() ) );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private AgilitySpell m_Owner;

			public InternalTarget( AgilitySpell owner ) : base( 12, false, TargetFlags.Beneficial )
			{
				m_Owner = owner;
			} 

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}