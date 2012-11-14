using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.Menus.Questions;

namespace Server.Gumps
{
	public class ReportMurdererGump : Gump
	{
		private int m_Idx;
		private List<Mobile> m_Killers;
		private Mobile m_Victum;
        private int balance;
        private int killerbalance;
        private Container bank;

        public void FindDeletePost(PlayerMobile from)
        {
            List<Item> list = new List<Item>();

            foreach (Item item in World.Items.Values)
            {
                if (item is BulletinBoardPost)
                {
                    BulletinBoardPost bm = item as BulletinBoardPost;

                    if (bm.Subject == String.Format("{0}: {1} gold pieces", from.Name, from.Bounty))
                        list.Add(bm);
                }
            }

            foreach (BulletinBoardPost post in list)
                post.Delete();

            list.Clear();
        }

        public static BulletinBoardPost FindPost(PlayerMobile from)
        {
            foreach (Item item in World.Items.Values)
            {
                if (item is BulletinBoardPost)
                {
                    BulletinBoardPost bm = item as BulletinBoardPost;

                    if (bm.Subject == String.Format("{0}: {1} gold pieces", from.Name, from.Bounty))
                        return bm;
                }
            }
            return null;
        }

        public static void FindEditPost(PlayerMobile from, BulletinBoardPost post)
        {
            string subtext1 = "";
            string subtext2 = "";

            switch (Utility.RandomMinMax(0, 17))
            {
                case 0: subtext1 = "hath murdered one too many!"; break;
                case 1: subtext1 = "shall not slay again!"; break;
                case 2: subtext1 = "hath slain too many!"; break;
                case 3: subtext1 = "cannot continue to kill!"; break;
                case 4: subtext1 = "must be stopped."; break;
                case 5: subtext1 = "is a bloodthirsty monster."; break;
                case 6: subtext1 = "is a killer of the worst sort."; break;
                case 7: subtext1 = "hath no conscience!"; break;
                case 8: subtext1 = "hath cowardly slain many."; break;
                case 9: subtext1 = "must die for all our sakes."; break;
                case 10: subtext1 = "sheds innocent blood!"; break;
                case 11: subtext1 = "must fall to preserve us."; break;
                case 12: subtext1 = "must be taken care of."; break;
                case 13: subtext1 = "is a thug and must die."; break;
                case 14: subtext1 = "cannot be redeemed."; break;
                case 15: subtext1 = "is a shameless butcher."; break;
                case 16: subtext1 = "is a callous monster."; break;
                case 17: subtext1 = "is a cruel, casual killer."; break;
            }
            switch (Utility.RandomMinMax(0, 6))
            {
                case 0: subtext2 = "A bounty is hereby offered"; break;
                case 1: subtext2 = "Lord British sets a price"; break;
                case 2: subtext2 = "Claim the reward! 'Tis"; break;
                case 3: subtext2 = "Lord Blackthorn set a price"; break;
                case 4: subtext2 = "The Paladins set a price"; break;
                case 5: subtext2 = "The Merchants set a price"; break;
                case 6: subtext2 = "Lord British's bounty"; break;
            }

            string text = String.Format("The foul scum known as {0} {1} For {2} is responsible for {3} murders. {4} of {5} gold pieces for {6} head!", from.Name, subtext1, (from.Body.IsFemale ? "she" : "he"), from.Kills, subtext2, (from.Bounty == 0 ? "alas, zero" : from.Bounty.ToString()), (from.Body.IsFemale ? "her" : "his"));
            int current = 0;
            int lineCount = 10;
            string[] lines = new string[lineCount];
            char space = ' ';

            // break up the text into single line length pieces
            while (text != null && current < text.Length)
            {
                // place the line on the page
                for (int i = 0; i < lineCount; i++)
                {
                    if (current < text.Length)
                    {
                        // make each line 25 chars long
                        int length = text.Length - current;

                        if (length > 30)
                        {
                            length = 30;

                            while (text[current + length] != space)
                                length--;

                            length++;
                            lines[i] = text.Substring(current, length);
                        }
                        else
                        {
                            lines[i] = String.Format("{0} ", text.Substring(current, length));
                        }

                        current += length;
                    }
                    else
                    {
                        // fill up the remaining lines
                        lines[i] = String.Empty;
                    }
                }
            }

            post.Subject = String.Format("{0}: {1} gold pieces", from.Name, from.Bounty);
            post.Message = lines;
        }

        public static void MakePost(PlayerMobile from)
        {
            string subject = String.Format("{0}: {1} gold pieces", from.Name, from.Bounty);
            string subtext1 = "";
            string subtext2 = "";

            switch (Utility.RandomMinMax(0, 17))
            {
                case 0: subtext1 = "hath murdered one too many!"; break;
                case 1: subtext1 = "shall not slay again!"; break;
                case 2: subtext1 = "hath slain too many!"; break;
                case 3: subtext1 = "cannot continue to kill!"; break;
                case 4: subtext1 = "must be stopped."; break;
                case 5: subtext1 = "is a bloodthirsty monster."; break;
                case 6: subtext1 = "is a killer of the worst sort."; break;
                case 7: subtext1 = "hath no conscience!"; break;
                case 8: subtext1 = "hath cowardly slain many."; break;
                case 9: subtext1 = "must die for all our sakes."; break;
                case 10: subtext1 = "sheds innocent blood!"; break;
                case 11: subtext1 = "must fall to preserve us."; break;
                case 12: subtext1 = "must be taken care of."; break;
                case 13: subtext1 = "is a thug and must die."; break;
                case 14: subtext1 = "cannot be redeemed."; break;
                case 15: subtext1 = "is a shameless butcher."; break;
                case 16: subtext1 = "is a callous monster."; break;
                case 17: subtext1 = "is a cruel, casual killer."; break;
            }
            switch (Utility.RandomMinMax(0, 6))
            {
                case 0: subtext2 = "A bounty is hereby offered"; break;
                case 1: subtext2 = "Lord British sets a price"; break;
                case 2: subtext2 = "Claim the reward! 'Tis"; break;
                case 3: subtext2 = "Lord Blackthorn set a price"; break;
                case 4: subtext2 = "The Paladins set a price"; break;
                case 5: subtext2 = "The Merchants set a price"; break;
                case 6: subtext2 = "Lord British's bounty"; break;
            }

            //string text = String.Format("The foul scum known as {0} hath murdered one too many! For he is responsible for {1} murders. Lord Blackthorn set a price of {2} gold pieces for his head!", from.Name, from.Kills, from.Bounty);
            string text = String.Format("The foul scum known as {0} {1} For {2} is responsible for {3} murders. {4} of {5} gold pieces for {6} head!", from.Name, subtext1, (from.Body.IsFemale ? "she" : "he"), from.Kills, subtext2, (from.Bounty == 0 ? "alas, zero" : from.Bounty.ToString()), (from.Body.IsFemale ? "her" : "his"));
            int current = 0;
            int lineCount = 10;
            string[] lines = new string[lineCount];
            char space = ' ';

            // break up the text into single line length pieces
            while (text != null && current < text.Length)
            {
                // place the line on the page
                for (int i = 0; i < lineCount; i++)
                {
                    if (current < text.Length)
                    {
                        // make each line 25 chars long
                        int length = text.Length - current;

                        if (length > 30)
                        {
                            length = 30;

                            while (text[current + length] != space)
                                length--;

                            length++;
                            lines[i] = text.Substring(current, length);
                        }
                        else
                        {
                            lines[i] = String.Format("{0} ", text.Substring(current, length));
                        }

                        current += length;
                    }
                    else
                    {
                        // fill up the remaining lines
                        lines[i] = String.Empty;
                    }
                }
            }

            BulletinBoardPost b = new BulletinBoardPost(subject, "", lines);
           // b.MoveToWorld(new Point3D(0, 0, 0), Map.Trammel);
        }

		public static void Initialize()
		{
			EventSink.PlayerDeath += new PlayerDeathEventHandler( EventSink_PlayerDeath );
		}
 
		public static void EventSink_PlayerDeath( PlayerDeathEventArgs e )
		{
			Mobile m = e.Mobile;

			List<Mobile> killers = new List<Mobile>();
			List<Mobile> toGive  = new List<Mobile>();

			foreach ( AggressorInfo ai in m.Aggressors )
			{
				if ( ai.Attacker.Player && ai.CanReportMurder && !ai.Reported )
				{
					killers.Add( ai.Attacker );
					ai.Reported = true;
				}

				if ( ai.Attacker.Player && (DateTime.Now - ai.LastCombatTime) < TimeSpan.FromSeconds( 30.0 ) && !toGive.Contains( ai.Attacker ) )
					toGive.Add( ai.Attacker );
			}

			foreach ( AggressorInfo ai in m.Aggressed )
			{
				if ( ai.Defender.Player && (DateTime.Now - ai.LastCombatTime) < TimeSpan.FromSeconds( 30.0 ) && !toGive.Contains( ai.Defender ) )
					toGive.Add( ai.Defender );
			}

			foreach ( Mobile g in toGive )
			{
				int n = Notoriety.Compute( g, m );

				int theirKarma = m.Karma, ourKarma = g.Karma;
				bool innocent = ( n == Notoriety.Innocent );
				bool criminal = ( n == Notoriety.Criminal || n == Notoriety.Murderer );

				int fameAward = m.Fame / 200;
				int karmaAward = 0;

                if (innocent)
                {
                    //Are they a virtue guard?
                    if (g.FindItemOnLayer(Layer.TwoHanded) is ChaosShield || g.FindItemOnLayer(Layer.TwoHanded) is OrderShield)
                    {
                        g.Kill();
                        g.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                        g.PlaySound(0x307);
                    }
                    karmaAward = 0;
                }
                else if (criminal)
                    karmaAward = 1;

				Titles.AwardKarma( g, karmaAward, true );
			}

			if ( m is PlayerMobile && ((PlayerMobile)m).NpcGuild == NpcGuild.ThievesGuild )
				return;

			if ( killers.Count > 0 )
				new GumpTimer( m, killers ).Start();
		}

		private class GumpTimer : Timer
		{
			private Mobile m_Victim;
			private List<Mobile> m_Killers;

			public GumpTimer( Mobile victim, List<Mobile> killers ) : base( TimeSpan.FromSeconds( 4.0 ) )
			{
				m_Victim = victim;
				m_Killers = killers;
			}

			protected override void OnTick()
			{
				//m_Victim.SendGump( new ReportMurdererGump( m_Victim, m_Killers ) );
                m_Victim.SendMenu(new ReportMenu(m_Victim, m_Killers));
			}
		}

		public ReportMurdererGump( Mobile victum, List<Mobile> killers ) : this( victum, killers, 0 )
		{
		}

		private ReportMurdererGump( Mobile victum, List<Mobile> killers, int idx ) : base( 0, 0 )
		{
			m_Killers = killers;
			m_Victum = victum;
			m_Idx = idx;
			BuildGump();
		}

		private void BuildGump() 
		{
            /*FIND GOLD*/
            balance = 0;
            Item[] gold;

            bank = ((Mobile)m_Victum).FindBankNoCreate();

            if (bank != null)
            {
                gold = bank.FindItemsByType(typeof(Gold));

                for (int i = 0; i < gold.Length; ++i)
                    balance += gold[i].Amount;
            }

            this.Closable = false;
            this.Disposable = false;
            this.Dragable = false;
            this.Resizable = false;

            AddPage(0);
            AddImage(200, 150, 1140);
            AddButton(408, 340, 1144, 1145, 2, GumpButtonType.Reply, 0);
            AddButton(317, 341, 1147, 1148, 1, GumpButtonType.Reply, 0);
            
            AddHtml(267, 196, 254, 37, String.Format("Would you like to report {0} as a murderer?", ((Mobile)m_Killers[m_Idx]).Name), (bool)false, (bool)false);
            /*if (m_Killers[m_Idx].Kills >= 5)
            {*/
                AddImage(260, 282, 1143);
                AddHtml(266, 258, 256, 21, String.Format("Optional bounty ({0}gp max)", balance), (bool)false, (bool)false);
                AddTextEntry(274, 285, 242, 16, 0, 0, "0", 25);
            //}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

            TextRelay entry0 = info.GetTextEntry(0);
            string text0 = (entry0 == null ? "0" : entry0.Text.Trim());
            BulletinBoardPost post = null;

			switch ( info.ButtonID )
			{
				case 1: 
				{            
					Mobile killer = m_Killers[m_Idx];
					if ( killer != null && !killer.Deleted )
					{
                        bool emptiedbank = false;

                        //add kills
                        killer.Kills++;
                        killer.ShortTermMurders++;

                        if (killer is PlayerMobile)
                        {
                            ((PlayerMobile)killer).ResetKillTime();

                            //do they have an existing post?
                            post = FindPost(((PlayerMobile)killer));

                            //is there a bounty being set?
                            if (balance >= Int32.Parse(text0) && Int32.Parse(text0) > 0)
                            {
                                //withdraw bounty from victims account
                                bank.ConsumeTotal(typeof(Gold), Int32.Parse(text0));

                                //set bounty
                                ((PlayerMobile)killer).Bounty = ((PlayerMobile)killer).Bounty + Int32.Parse(text0);

                                //make them dread lord
                                killer.Karma = -127;

                                //wipe bank
                                BankBox killerbank = killer.BankBox;
                                if (killerbank.Items.Count > 0)
                                {
                                    //add killers gold to his bounty
                                    killerbalance = 0;
                                    Item[] killergold;

                                    if (killerbank != null)
                                    {
                                        killergold = killerbank.FindItemsByType(typeof(Gold));

                                        for (int i = 0; i < killergold.Length; ++i)
                                            killerbalance += killergold[i].Amount;

                                        killerbank.ConsumeTotal(typeof(Gold), killerbalance);
                                        ((PlayerMobile)killer).Bounty += killerbalance;

                                        killer.SendAsciiMessage("A bounty hath been issued for thee, and thy worldly goods are hereby confiscated!");
                                        emptiedbank = true;

                                        //remove all items in the bank
                                        List<Item> list = new List<Item>();
                                        foreach (Item item in killerbank.Items)
                                            list.Add(item);

                                        foreach (Item i in list)
                                            i.Delete();
                                    }
                                }

                                //make new bounty post
                                if (post != null)
                                    FindEditPost(((PlayerMobile)killer), post);
                                else
                                    MakePost(((PlayerMobile)killer));
                            }

                            /*if (!emptiedbank)
                                killer.SendAsciiMessage("You have been reported for murder!");*/
                        }
					}
					break; 
				}
				case 2: 
				{
					break; 
				}
			}

			m_Idx++;
			if ( m_Idx < m_Killers.Count )
				from.SendGump( new ReportMurdererGump( from, m_Killers, m_Idx ) );
		}
	}
}
