using Server;
using System;
using Server.Mobiles;
using Server.Menus.Questions;
using Server.Network;

namespace Server.Gumps
{
    public class ResNowOption : QuestionMenu
    {
        public class ResNowOptionTimer : Timer
        {
            private Mobile m;

            public ResNowOptionTimer(Mobile mobile) : base(TimeSpan.FromSeconds(3.0))
            {
                m = mobile;
            }

            protected override void OnTick()
            {
                m.SendMenu(new ResNowOption(m));
            }
        }

        private static string[] m_Options = new string[]
		{
			"Resurrect Now - You will be resurrected instantly, but will lose some of your stats and skills.",
			"Play as a Ghost - You will maintain your stats and skills, but must seek out a healer or mage to resurrect you.",
			"Always Play as Ghost - Never show this menu again."
		};

        private DateTime m_Start;

        public ResNowOption(Mobile m) : base("Do you wish to resurrect now?", m_Options)
        {
            m_Start = DateTime.Now;
            m.CantWalk = true;
        }

        public override void OnCancel(NetState state)
        {
            base.OnCancel(state);

            state.Mobile.CantWalk = false;
        }

        public override void OnResponse(NetState state, int index)
        {
            PlayerMobile pm = state.Mobile as PlayerMobile;

            if (pm == null)
                return;

            if (m_Start + TimeSpan.FromMinutes(10.0) < DateTime.Now)
            {
                pm.CantWalk = false;

                if (index == 0)
                    pm.SendAsciiMessage("It has been too long since you died.");

                return;
            }

            switch (index)
            {
                case 0:
                    {
                        /*if (pm.DeathCount >= 5)
                        {
                            pm.SendAsciiMessage("Your spirit was too weak to return to corporeal form.");
                            pm.CantWalk = false;
                            return;
                        }
                        else if (pm.DeathCount == 4)
                            pm.SendAsciiMessage("Your spirit returns to corporeal form, but is too weak to do so a gain for a while.");
                        else if (pm.DeathCount == 3)
                            pm.SendAsciiMessage("Your spirit barely manages to return to corporeal form.");
                        else if (pm.DeathCount == 2)
                            pm.SendAsciiMessage("With some effort your spirit returns to corporeal form.");
                        else if (pm.DeathCount == 1)
                            pm.SendAsciiMessage("Your spirit easily returns to corporeal form.");
                        else if (pm.DeathCount == 0)
                            pm.SendAsciiMessage("Your spirit easily returns to corporeal form.");*/

                        for (int i = 0; i < pm.Skills.Length; i++)
                        {
                            if (pm.Skills[i].Base > 25.0)
                                pm.Skills[i].Base -= Utility.Random(5) + 5;
                        }

                        pm.PlaySound(0x214);
                        pm.FixedEffect(0x376A, 10, 16);
                        pm.Resurrect();
                        pm.CantWalk = false;

                        if (pm.RawDex > 15)
                            pm.RawDex -= pm.RawDex / 10;
                        if (pm.RawStr > 15)
                            pm.RawStr -= pm.RawStr / 10;
                        if (pm.RawInt > 15)
                            pm.RawInt -= pm.RawInt / 10;

                        pm.Hits = pm.HitsMax / 2;
                        pm.Mana = pm.ManaMax / 5;
                        break;
                    }
                case 1:
                    {
                        pm.CantWalk = false;
                        break;
                    }
                case 2:
                    {
                        pm.CantWalk = false;
                        pm.AssumePlayAsGhost = true;
                        break;
                    }
            }
        }
    }
}