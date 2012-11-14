using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.Second
{
	public class CunningSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Cunning", "Uus Wis",
				212,
				9061,
				Reagent.MandrakeRoot,
				Reagent.Nightshade
			);

		public override SpellCircle Circle { get { return SpellCircle.Second; } }

		public CunningSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
		    Caster.Target = new InternalTarget( this );
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

				SpellHelper.AddStatBonus( Caster, m, StatType.Int );

				m.FixedParticles( 0x375A, 10, 15, 5011, EffectLayer.Head );
				m.PlaySound( 0x1EB );

				//int percentage = (int)(SpellHelper.GetOffsetScalar( Caster, m, false )*100);
				//TimeSpan length = SpellHelper.GetDuration( Caster, m );
                int percentage = (int)((Caster.Skills[SkillName.Magery].Value / 10) + 1);
                TimeSpan length = TimeSpan.FromSeconds((6 * Caster.Skills[SkillName.Magery].Value / 5) + 1);

				BuffInfo.AddBuff( m, new BuffInfo( BuffIcon.Cunning, 1075843, length, m, percentage.ToString() ) );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private CunningSpell m_Owner;

			public InternalTarget( CunningSpell owner ) : base( 12, false, TargetFlags.Beneficial )
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