using System;
using System.Collections;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.Second
{
    public class ProtectionSpell : MagerySpell
    {
        private static Hashtable m_Registry = new Hashtable();
        public static Hashtable Registry { get { return m_Registry; } }
        private Item m_Scroll;

        private static SpellInfo m_Info = new SpellInfo(
                "Protection", "Uus Sanct",
                236,
                9011,
                Reagent.Garlic,
                Reagent.Ginseng,
                Reagent.SulfurousAsh
            );

        public override SpellCircle Circle { get { return SpellCircle.Second; } }

        public ProtectionSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
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

        public static void Toggle(Mobile caster, Mobile target)
        {
            /* Players under the protection spell effect can no longer have their spells "disrupted" when hit.
             * Players under the protection spell have decreased physical resistance stat value,
             * a decreased "resisting spells" skill value by -35,
             * and a slower casting speed modifier (technically, a negative "faster cast speed") of 2 points.
             * The protection spell has an indefinite duration, becoming active when cast, and deactivated when re-cast.
             * Reactive Armor, Protection, and Magic Reflection will stay on—even after logging out,
             * even after dying—until you “turn them off” by casting them again.
             */

            object[] mods = (object[])m_Table[target];

            if (mods == null)
            {
                target.PlaySound(0x1E9);
                target.FixedParticles(0x375A, 9, 20, 5016, EffectLayer.Waist);

                mods = new object[2]
					{
						new ResistanceMod( ResistanceType.Physical, -15 + (int)(caster.Skills[SkillName.Inscribe].Value / 20) ),
						new DefaultSkillMod( SkillName.MagicResist, true, -35 + (int)(caster.Skills[SkillName.Inscribe].Value / 20) )
					};

                m_Table[target] = mods;
                Registry[target] = 100.0;

                target.AddResistanceMod((ResistanceMod)mods[0]);
                target.AddSkillMod((SkillMod)mods[1]);
            }
            else
            {
                target.PlaySound(0x1ED);
                target.FixedParticles(0x375A, 9, 20, 5016, EffectLayer.Waist);

                m_Table.Remove(target);
                Registry.Remove(target);

                target.RemoveResistanceMod((ResistanceMod)mods[0]);
                target.RemoveSkillMod((SkillMod)mods[1]);
            }
        }

        public void Target(Mobile m)
        {
            if (m.VirtualArmorMod != 0)
            {
                Caster.SendAsciiMessage("This spell is already in effect."); // This spell is already in effect.
            }
            else if (CheckBSequence(m))
            {
                SpellHelper.Turn(Caster, m);

                SpellHelper.CheckReflect((int)this.Circle, Caster, ref m);

                new InternalTimer(m, Caster).Start();
                m.FixedParticles(0x375A, 9, 20, 5016, EffectLayer.Waist);
                m.PlaySound(0x1ED);
            }

            FinishSequence();
        }

        public class InternalTarget : Target
        {
            private ProtectionSpell m_Owner;

            public InternalTarget(ProtectionSpell owner) : base(12, false, TargetFlags.Beneficial)
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

        private class InternalTimer : Timer
        {
            private Mobile m_Targ;
            int oldarmor;

            public InternalTimer(Mobile targ, Mobile caster) : base(TimeSpan.FromSeconds(0))
            {
                m_Targ = targ;
                oldarmor = targ.VirtualArmorMod;
                int armor = (int)(caster.Skills[SkillName.Magery].Value / 10 + 1);

                if (armor < 0)
                    armor = 0;
                else if (armor > 10)
                    armor = 10;

                targ.VirtualArmorMod = targ.VirtualArmorMod + armor;
                Delay = TimeSpan.FromSeconds(6 * caster.Skills[SkillName.Magery].Value / 5);
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                m_Targ.VirtualArmorMod = oldarmor;
            }
        }
    }
}