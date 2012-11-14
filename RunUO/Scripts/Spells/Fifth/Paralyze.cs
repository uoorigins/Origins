using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.Fifth
{
	public class ParalyzeSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Paralyze", "An Ex Por",
				218,
				9012,
				Reagent.Garlic,
				Reagent.MandrakeRoot,
				Reagent.SpidersSilk
			);

		public override SpellCircle Circle { get { return SpellCircle.Fifth; } }

		public ParalyzeSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

        public void Target(Mobile m)
        {
            if (!Caster.CanSee(m))
            {
                Caster.SendAsciiMessage("Target can not be seen."); // Target can not be seen.
            }
            else if (Core.AOS && (m.Frozen || m.Paralyzed || (m.Spell != null && m.Spell.IsCasting)))
            {
                Caster.SendAsciiMessage("The target is already frozen."); // The target is already frozen.
            }
            else if (CheckHSequence(m))
            {
                SpellHelper.Turn(Caster, m);

                SpellHelper.CheckReflect((int)this.Circle, Caster, ref m);

                double duration;
                duration = (((Caster.Skills[SkillName.Magery].Value / 10) + 1) * 2) + 5;

                if (CheckResisted(m, 5))
                    duration /= 2;

                m.Paralyze(TimeSpan.FromSeconds(duration));

                if (m.Spell != null && m.Spell is Spell)
                    ((Spell)m.Spell).Disturb(DisturbType.Hurt);

                m.PlaySound(0x204);
                m.FixedEffect(0x376A, 6, 1);
            }

            FinishSequence();
        }

		public class InternalTarget : Target
		{
			private ParalyzeSpell m_Owner;

			public InternalTarget( ParalyzeSpell owner ) : base( 12, false, TargetFlags.Harmful )
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