using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Accounting;
using System.Linq;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Items;

namespace Server.Gumps
{
    public class LoginGump : Gump
    {
        public enum PageType
        {
            News = 1,
            Wallet,
            Account
        }

        Mobile m_Mobile;

        public static void Initialize()
        {
            CommandSystem.Register( "news", AccessLevel.Player, new CommandEventHandler( NewsGump_OnCommand ) );
            CommandSystem.Register( "wallet", AccessLevel.Player, new CommandEventHandler( WalletGump_OnCommand ) );
            CommandSystem.Register( "account", AccessLevel.Player, new CommandEventHandler( AccountGump_OnCommand ) );
        }

        [Usage( "news" )]
        [Description( "Makes a call to the news gump." )]
        public static void NewsGump_OnCommand( CommandEventArgs e )
        {
            Mobile m = e.Mobile;

            if ( m.HasGump( typeof( LoginGump ) ) )
                m.CloseGump( typeof( LoginGump ) );
            m.SendGump( new LoginGump( m ) );
        }

        [Usage("wallet")]
        [Description("Makes a call to the wallet gump.")]
        public static void WalletGump_OnCommand( CommandEventArgs e )
        {
            Mobile m = e.Mobile;

            if (m.HasGump(typeof(LoginGump)))
                m.CloseGump(typeof(LoginGump));
            m.SendGump(new LoginGump(m, PageType.Wallet));
        }

        [Usage( "account" )]
        [Description( "Makes a call to the account gump." )]
        public static void AccountGump_OnCommand( CommandEventArgs e )
        {
            Mobile m = e.Mobile;

            if ( m.HasGump( typeof( LoginGump ) ) )
                m.CloseGump( typeof( LoginGump ) );
            m.SendGump( new LoginGump( m, PageType.Account ) );
        }

        private string content = System.IO.File.ReadAllText( @"Data\news.txt" );

        public LoginGump( Mobile from ) : this( from, PageType.News )
        {
        }

        public LoginGump(Mobile from, PageType pagetype) : base( 0, 0 )
        {
            content = content.Replace( "\r", "" ).Trim();
            content = content.Replace( "\n", "" ).Trim();

            int updatePage = 1;
            int walletPage = 2;
            int accountPage = 3;

            switch ( pagetype )
            {
                case PageType.News:
                default:
                    break;
                case PageType.Wallet:
                    updatePage = 2;
                    walletPage = 1;
                    accountPage = 3;
                    break;
                case PageType.Account:
                    updatePage = 2;
                    walletPage = 3;
                    accountPage = 1;
                    break;
            }

            m_Mobile = from;
            PlayerMobile pm = from as PlayerMobile;

            Account account = (Account)from.Account;

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddBackground(184, 56, 450, 400, 9380);
			AddImage(201, 90, 5536);
            AddHtml( 224, 242, 64, 17, @"<basefont color=black size=5>News</basefont>", false, false );
            AddHtml( 224, 267, 64, 17, @"<basefont color=black size=5>Wallet</basefont>", false, false );
            AddHtml( 224, 292, 64, 17, @"<basefont color=black size=5>Account</basefont>", false, false );
            AddButton( 205, 245, 1209, 1210, 0, GumpButtonType.Page, updatePage ); // updates
            AddButton( 205, 270, 1209, 1210, 0, GumpButtonType.Page, walletPage ); // wallet
            AddButton( 205, 295, 1209, 1210, 0, GumpButtonType.Page, accountPage ); // account
            AddImage( 293, 90, 92 );
            AddImage( 344, 90, 93 );
            AddImage( 569, 89, 94 );
            AddImage( 454, 90, 93 );

            AddPage( updatePage );
            AddBackground( 301-5, 138-5, 302+10, 271+10, 9350 );
            AddHtml( 301, 138, 302, 271, content, (bool)false, (bool)true );

            AddHtml( 345, 95, 113, 18, @"<basefont color=black size=5>Shard News</basefont>", false, false );
            AddHtml( 241, 431, 126, 18, @"<basefont color=black size=5>Receive on Login</basefont>", false, false );
			AddCheck(208, 427, 2152, 2154, (pm != null ? pm.ShowNews : true), 0);

            AddPage( walletPage );
            AddHtml( 345, 95, 153, 18, @"<basefont color=black size=5>My Wallet</basefont>", false, false );
            AddHtml( 345, 257, 160, 18, @"<basefont color=black size=5>Activity This Month</basefont>", false, false );
            AddHtml( 334, 122, 200, 18, @"<basefont color=black size=5>Account Gametime:</basefont>", false, false );
            AddHtml( 334, 142, 200, 45, String.Format(@"<basefont color=black size=5>{0}'s Gametime:</basefont>", from.Name), false, false );

            TimeSpan age = TimeSpan.FromDays( ( DateTime.Now - account.Created ).Days );
            TimeSpan accountGametime = account.TotalGameTime;
            TimeSpan playerGametime = pm.GameTime;

            AddLabel( 515, 122, 2057, Math.Round( accountGametime.TotalHours / 24, 1 ).ToString() + @" days" );
            AddLabel( 515, 142, 2057, Math.Round( playerGametime.TotalHours / 24, 1 ).ToString() + @" days" );
            AddLabel( 395, 175, 2057, account.WalletBalance.ToString() + @" coins" );

            AddItem( 515, 174, 3826, 1154 );
            AddHtml( 334, 175, 64, 18, @"<basefont color=black size=5>Balance:</basefont>", false, false );
			AddButton(338, 200, 4005, 4007, 1, GumpButtonType.Reply, 0);
            AddHtml( 376, 200, 179, 18, @"<basefont color=black size=5>Deposit to Bank Account</basefont>", false, false );
            AddButton( 338, 223, 4005, 4007, 2, GumpButtonType.Reply, 0 );
            AddHtml( 376, 223, 220, 18, @"<basefont color=black size=5>Learn About Platinum</basefont>", false, false );

            AddImage( 293, 253, 92 );
            AddImage( 344, 253, 93 );
            AddImage( 454, 253, 93 );
            AddImage( 569, 252, 94 );

            AddHtml( 334, 285, 76, 18, @"<basefont color=black size=5>Character</basefont>", false, false );
            AddHtml( 500, 285, 49, 18, @"<basefont color=black size=5>Hours</basefont>", false, false );

            List<Mobile> list = account.Mobiles.ToList();
            double total = 0;

            int i = 0;
            int count = 0;
            for ( i = 0; i < list.Count; i++ )
            {
                pm = (PlayerMobile)list[i];
                if ( pm != null )
                {
                    AddLabel( 334, 305+(count*15), 2057, pm.Name );
                    AddLabel( 500, 305+(count*15), 2057, String.Format("{0:0.0}", pm.MonthlyGameTime.TotalHours));
                    total += pm.MonthlyGameTime.TotalHours;
                    count++;
                }
            }

            AddImage( 319, 379, 95 );
            AddImage( 327, 388, 96 );
            AddImage( 375, 388, 96 );
            AddImage( 553, 379, 97 );

            AddHtml( 399, 396, 98, 18, @"<basefont color=black size=5>Total Hours:</basefont>", false, false );
            AddLabel( 500, 396, 2057, String.Format("{0:0.0}", total));

            AddPage( accountPage );
            AddHtml( 345, 95, 153, 18, @"<basefont color=black size=5>Account Management</basefont>", false, false );
            AddHtml( 334, 122, 96, 18, @"<basefont color=black size=5>Coming Soon</basefont>", false, false );
        }

        

        public override void OnResponse(NetState sender, RelayInfo info)
        { 
            Mobile from = sender.Mobile;
            PlayerMobile pm = from as PlayerMobile;

            if ( pm != null )
            {
                pm.ShowNews = info.IsSwitched( 0 );
            }

            switch(info.ButtonID)
            {
                //deposit
                case 1:
				{
                    if ( from.Account != null )
                    {
                        Account account = from.Account as Account;

                        if ( account.WalletBalance > 0 )
                        {
                            from.BankBox.DropItem( new Platinum(account.WalletBalance) );
                            from.SendAsciiMessage(String.Format("You have deposited {0} platinum coins in to your bank box.", account.WalletBalance));
                            account.WalletBalance = 0;
                        }
                        else
                        {
                            from.SendAsciiMessage( "You have no platinum coins to deposit." );
                        }

                        from.CloseGump( typeof( LoginGump ) );
                    }
					break;
				}
                case 2: //learn
                {
                    from.LaunchBrowser( "http://www.uoorigins.com/threads/platinum-and-veteran-rewards.1863/" );
                    break;
                }

            }
        }
    }
}