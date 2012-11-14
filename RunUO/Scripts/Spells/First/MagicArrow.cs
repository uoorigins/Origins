using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.First
{
	public class MagicArrowSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Magic Arrow", "In Por Ylem",
				212,
				9041,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.First; } }

		public MagicArrowSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

        public override bool DelayedDamageStacking { get { return !Core.AOS; } }

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return true; } }

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
                Caster.SendAsciiMessage("Target can not be seen."); // Target can not be seen.
			}
            else if (CheckHSequence(m))
            {
                Mobile source = Caster;

                SpellHelper.Turn(source, m);

                SpellHelper.CheckReflect((int)this.Circle, ref source, ref m);

                double damage;
                damage = (((1 * Utility.Random(1, 3)) + 3) * (50 + Caster.Skills[SkillName.Magery].Value)) / 100;

                if (CheckResisted(m, damage))
                {
                    damage = (damage + 1) / 2;
                    m.SendAsciiMessage("You feel yourself resisting magical energy."); // You feel yourself resisting magical energy.
                }

                damage *= GetDamageScalar(m);

                source.MovingParticles(m, 0x36E4, 5, 0, false, true, 3006, 4006, 0);
                source.PlaySound(0x1E5);

                SpellHelper.Damage(this, m, damage, 0, 100, 0, 0, 0);
            }

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private MagicArrowSpell m_Owner;

			public InternalTarget( MagicArrowSpell owner ) : base( 12, false, TargetFlags.Harmful )
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