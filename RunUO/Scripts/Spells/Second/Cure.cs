using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.Second
{
	public class CureSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Cure", "An Nox",
				212,
				9061,
				Reagent.Garlic,
				Reagent.Ginseng
			);

		public override SpellCircle Circle { get { return SpellCircle.Second; } }

		public CureSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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

				Poison p = m.Poison;

				if ( p != null )
				{
					int chanceToCure = 10000 + (int)(Caster.Skills[SkillName.Magery].Value * 75) - ((p.Level + 1) * (Core.AOS ? (p.Level < 4 ? 3300 : 3100) : 1750));
					chanceToCure /= 100;

                    if (((10000 + ((Caster.Skills[SkillName.Magery].Value * 75) - (p.Level+1 * 1750))) / 100) > Utility.Random(1, 100)) //if ( chanceToCure > Utility.Random( 100 ) )
					{
						if ( m.CurePoison( Caster ) )
						{
							if ( Caster != m )
								Caster.SendAsciiMessage( String.Format("You have cured {0} of all poisons!", m.Name) ); // You have cured the target of all poisons!

                            m.SendAsciiMessage(String.Format("{0} has cured you of all poisons!",Caster.Name)); // You have been cured of all poisons.
						}
					}
					else
					{
						m.SendAsciiMessage( String.Format("You have failed to cure {0}!",m.Name )); // You have failed to cure your target!
					}
				}

				m.FixedParticles( 0x373A, 10, 15, 5012, EffectLayer.Waist );
				m.PlaySound( 0x1E0 );
			}

			FinishSequence();
		}

		public class InternalTarget : Target
		{
			private CureSpell m_Owner;

			public InternalTarget( CureSpell owner ) : base( 12, false, TargetFlags.Beneficial )
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