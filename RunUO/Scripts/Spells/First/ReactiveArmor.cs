using System;
using System.Collections;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.First
{
	public class ReactiveArmorSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Reactive Armor", "Flam Sanct",
				236,
				9011,
				Reagent.Garlic,
				Reagent.SpidersSilk,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.First; } }

		public ReactiveArmorSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast()
		{
			return true;
		}

		private static Hashtable m_Table = new Hashtable();

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(Mobile m)
        {
            if (CheckBSequence(m))
            {
                Timer t = m_Table[m] as Timer;
                if (t != null && t.Running)
                {
                    Caster.SendAsciiMessage("This target already has reactive armor.");
                }
                else
                {
                    m.MeleeDamageAbsorb = (int)(10 + Caster.Skills[SkillName.Magery].Value / 4);
                    m.FixedParticles(0x376A, 9, 32, 5008, EffectLayer.Waist);
                    m.PlaySound(0x1F2);

                    t = new ExpireTimer(m, TimeSpan.FromSeconds(25 + (Caster.Skills[SkillName.Magery].Value / 2.0)));
                    t.Start();

                    m_Table[m] = t;
                }
            }

            FinishSequence();
        }

        private class ExpireTimer : Timer
        {
            private Mobile m_Mob;

            public ExpireTimer(Mobile m, TimeSpan delay)
                : base(delay)
            {
                m_Mob = m;
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                m_Mob.MeleeDamageAbsorb = 0;
                m_Table.Remove(m_Mob);
                m_Mob.PlaySound(92);
                //m_Mob.SendLocalizedMessage( 1005556 ); // Your reactive armor spell has been nullified.
            }
        }

        public class InternalTarget : Target
        {
            private ReactiveArmorSpell m_Owner;

            public InternalTarget(ReactiveArmorSpell owner)
                : base(12, false, TargetFlags.Beneficial)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is Mobile)
                {
                    m_Owner.Target((Mobile)o);
                }
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
	}
}