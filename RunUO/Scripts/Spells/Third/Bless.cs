using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.Third
{
	public class BlessSpell : MagerySpell
	{
 		private static SpellInfo m_Info = new SpellInfo(
				"Bless", "Rel Sanct",
				203,
				9061,
				Reagent.Garlic,
				Reagent.MandrakeRoot
			);

		public override SpellCircle Circle { get { return SpellCircle.Third; } }

		public BlessSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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

                SpellHelper.AddStatBonus(Caster, m, StatType.Str, (int)((Caster.Skills[SkillName.Magery].Value / 10) + 1), TimeSpan.FromSeconds((6 * Caster.Skills[SkillName.Magery].Value / 5) + 1));
                SpellHelper.AddStatBonus(Caster, m, StatType.Dex, (int)((Caster.Skills[SkillName.Magery].Value / 10) + 1), TimeSpan.FromSeconds((6 * Caster.Skills[SkillName.Magery].Value / 5) + 1));
                SpellHelper.AddStatBonus(Caster, m, StatType.Int, (int)((Caster.Skills[SkillName.Magery].Value / 10) + 1), TimeSpan.FromSeconds((6 * Caster.Skills[SkillName.Magery].Value / 5) + 1));

				//SpellHelper.AddStatBonus( Caster, m, StatType.Str ); SpellHelper.DisableSkillCheck = true;
				//SpellHelper.AddStatBonus( Caster, m, StatType.Dex );
				//SpellHelper.AddStatBonus( Caster, m, StatType.Int ); SpellHelper.DisableSkillCheck = false;

				m.FixedParticles( 0x373A, 10, 15, 5018, EffectLayer.Waist );
				m.PlaySound( 0x1EA );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private BlessSpell m_Owner;

			public InternalTarget( BlessSpell owner ) : base( 12, false, TargetFlags.Beneficial )
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