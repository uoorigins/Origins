using Server;
using System;
using Server.Network;
using Server.Mobiles;

namespace Server.Menus.Questions
{
    public enum ResurrectMessage
    {
        ChaosShrine = 0,
        VirtueShrine = 1,
        Healer = 2,
        Generic = 3,
    }

    public class ResurrectGump : QuestionMenu
    {
        private double m_HitsScalar;
        private Mobile m_Mobile;
        private Mobile m_Healer;

        public ResurrectGump(Mobile owner)
            : this(owner, owner, ResurrectMessage.Generic, false)
        {
        }
        public ResurrectGump(Mobile owner, double hitsScalar)
            : this(owner, owner, ResurrectMessage.Generic, false, hitsScalar)
        {
        }
        public ResurrectGump(Mobile owner, bool fromSacrifice)
            : this(owner, owner, ResurrectMessage.Generic, fromSacrifice)
        {
        }
        public ResurrectGump(Mobile owner, Mobile healer)
            : this(owner, healer, ResurrectMessage.Generic, false)
        {
        }
        public ResurrectGump(Mobile owner, ResurrectMessage msg)
            : this(owner, owner, msg, false)
        {
        }
        public ResurrectGump(Mobile owner, Mobile healer, ResurrectMessage msg, bool fromSacrifice)
            : this(owner, healer, msg, fromSacrifice, 0.0)
        {
        }

        public ResurrectGump(Mobile m, Mobile healer, ResurrectMessage msg, bool fromSacrifice, double hitsScalar) : base(String.Format("It is possible for you to be resurrected now. Do you wish to try?"),
            new string[] { "YES - You choose to try to come back to life now.", "NO - You prefer to remain a ghost for now."})
        {
            m_Healer = healer;
            m_Mobile = m;
            m_HitsScalar = hitsScalar;
        }

        public override void OnCancel(NetState state)
        {
            m_Mobile.CantWalk = false;

            if (m_Mobile is PlayerMobile)
                ((PlayerMobile)m_Mobile).HasMenu = false;
        }

        public override void OnResponse(NetState state, int index)
        {
            Mobile from = state.Mobile;

            if (index == 0)
            {
                if (from.Map == null || !from.Map.CanFit(from.Location, 16, false, false))
                {
                    from.SendAsciiMessage("Thou can not be resurrected there!");
                    m_Mobile.CantWalk = false;

                    if (m_Mobile is PlayerMobile)
                        ((PlayerMobile)m_Mobile).HasMenu = false;

                    return;
                }

                /*if (((PlayerMobile)from).DeathCount >= 5)
                {
                    from.SendAsciiMessage("Your spirit was too weak to return to corporeal form.");
                    m_Mobile.CantWalk = false;

                    if (m_Mobile is PlayerMobile)
                        ((PlayerMobile)m_Mobile).HasMenu = false;

                    return;
                }
                else if (((PlayerMobile)from).DeathCount == 4)
                from.SendAsciiMessage("Your spirit returns to corporeal form, but is too weak to do so a gain for a while.");
                else if (((PlayerMobile)from).DeathCount == 3)
                    from.SendAsciiMessage("Your spirit barely manages to return to corporeal form.");
                else if (((PlayerMobile)from).DeathCount == 2)
                    from.SendAsciiMessage("With some effort your spirit returns to corporeal form.");
                else if (((PlayerMobile)from).DeathCount == 1)
                    from.SendAsciiMessage("Your spirit easily returns to corporeal form.");
                else if (((PlayerMobile)from).DeathCount == 0)
                    from.SendAsciiMessage("Your spirit easily returns to corporeal form.");*/

                from.PlaySound(0x214);
                from.FixedEffect(0x376A, 10, 16);

                from.Resurrect();

                if (from.Karma > 0)
                {
                    if (from.Karma > m_Healer.Karma)
                    {
                        Misc.Titles.AwardKarma(m_Healer, 1, true);
                    }
                }

                //statloss
                //NO STATLOSS HERE > DONE IN PLAYERMOBILE ONDEATH()
                /*if (!Core.AOS && from.ShortTermMurders >= 5)
                {
                    double loss = (100.0 - (4.0 + (from.ShortTermMurders / 5.0))) / 100.0; // 5 to 15% loss

                    if (loss < 0.85)
                        loss = 0.85;
                    else if (loss > 0.95)
                        loss = 0.95;

                    if (from.RawStr * loss > 10)
                        from.RawStr = (int)(from.RawStr * loss);
                    if (from.RawInt * loss > 10)
                        from.RawInt = (int)(from.RawInt * loss);
                    if (from.RawDex * loss > 10)
                        from.RawDex = (int)(from.RawDex * loss);

                    for (int s = 0; s < from.Skills.Length; s++)
                    {
                        if (from.Skills[s].Base * loss > 35)
                            from.Skills[s].Base *= loss;
                    }
                }*/

                if (from.Alive && m_HitsScalar > 0)
                    from.Hits = (int)(from.HitsMax * m_HitsScalar);

            }
            else if (index == 1)
            {
            }

            if (m_Mobile is PlayerMobile)
                ((PlayerMobile)m_Mobile).HasMenu = false;

            from.CantWalk = false;
        }
    }
}