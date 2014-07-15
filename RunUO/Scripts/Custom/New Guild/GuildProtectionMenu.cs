using Server.Guilds;
using Server.Items;
using Server.Menus.ItemLists;
using Server.Mobiles;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Menus.Questions
{
    class GuildProtectionMenu : QuestionMenu
    {
        private Mobile m_Mobile;
        private Guild m_Guild;

        public GuildProtectionMenu( Mobile from, Guild guild )
            : base( "Guild Protection", null)
        {
            m_Mobile = from;
            m_Guild = guild;

            List<String> list = new List<String>();

            if ( m_Guild.IsProtected( m_Guild.Leader ) )
            {
                int total = 0;
                foreach ( Mobile m in m_Guild.Protected )
                {
                    if ( m == m_Guild.Leader )
                        total += Guild.ProtectionRateLeader;
                    else
                        total -= Guild.ProtectionRateMember;

                    if ( total < 5000 )
                        total = 5000;
                }
                TimeSpan days = m_Guild.LastProtectionPayment.AddDays(m_Guild.ProtectionPeriod.TotalDays) - DateTime.Now;

                list.Add( String.Format( "Toggle guild protection and lockdowns. Currently on. The next payment of {0:n0} gold pieces will be charged in {1} days.", total, Math.Ceiling( days.TotalDays ) ) );
            }
            else
            {
                list.Add( "Toggle guild protection and lockdowns. Currently off." );
            }


            list.Add( "Set guild house sign." );
            list.Add( "Grant protection to a guild member." );
            list.Add( "Revoke protection from a guild member." );
            list.Add( "Return to main menu." );

            Answers = list.ToArray();
        }

        public override void OnCancel( Network.NetState state )
        {
            if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
                return;

            m_Mobile.SendMenu( new GuildmasterMenu( m_Mobile, m_Guild ) );
        }

        public override void OnResponse( Network.NetState state, int index )
        {
            if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
                return;

            switch ( index )
            {
                case 0: //toggle protection
                {
                    m_Mobile.SendMenu( new ConfirmToggle( m_Mobile, m_Guild, !m_Guild.IsProtected( m_Guild.Leader ) ) );
                    break;
                }
                case 1: //set house sign
                    m_Mobile.SendMenu( new HouseSignMenu( m_Mobile, m_Guild ) );
                    break;
                case 2: //grant protection
                    if ( m_Guild.IsProtected( m_Guild.Leader ) )
                    {
                        List<Mobile> list = new List<Mobile>( m_Guild.Members );
                        list.Remove( m_Guild.Leader );
                        m_Mobile.SendMenu( new GrantProtectionMenu( m_Mobile, m_Guild, list, 0 ) );
                    }
                    else
                    {
                        m_Mobile.SendAsciiMessage( "You must enable guild protection first." );
                        m_Mobile.SendMenu( new GuildProtectionMenu( m_Mobile, m_Guild ) );
                    }
                    break;
                case 3: //revoke protection
                    if ( m_Guild.IsProtected( m_Guild.Leader ) )
                    {
                        List<Mobile> list = new List<Mobile>( m_Guild.Protected );
                        list.Remove( m_Guild.Leader );
                        m_Mobile.SendMenu( new RevokeProtectionMenu( m_Mobile, m_Guild, list, 0 ) );
                    }
                    else
                    {
                        m_Mobile.SendAsciiMessage( "You must enable guild protection first." );
                        m_Mobile.SendMenu( new GuildProtectionMenu( m_Mobile, m_Guild ) );
                    }
                    break;
                case 4: //return to main menu
                default:
                    m_Mobile.SendMenu( new GuildmasterMenu( m_Mobile, m_Guild ) );
                    break;
            }

        }

        private class ConfirmToggle : QuestionMenu
        {
            private Mobile m_Mobile;
            private Guild m_Guild;
            private bool TurnOn;

            public ConfirmToggle( Mobile from, Guild guild, bool on ) : base (null, new string[] { "Yes.", "No." })
            {
                m_Mobile = from;
                m_Guild = guild;
                TurnOn = on;
                String title;

                if ( TurnOn )
                {
                    title = String.Format( "Guild protection costs a one-time fee of {0:n0} gold pieces and a weekly payment of {1:n0} gold pieces which will be withdrawn from your bank account. Are you sure you wish to enable lockdowns?", Guild.ProtectionFeeInital, Guild.ProtectionRateLeader );
                }
                else
                {
                    title = "Are you sure you wish to disable guild protection? All lockdown items will be released from all houses, and any paid fees will not be refunded.";
                }

                Question = title;
            }

            public override void OnCancel( Network.NetState state )
            {
                if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
                    return;

                m_Mobile.SendMenu( new GuildProtectionMenu( m_Mobile, m_Guild ) );
            }

            public override void OnResponse( Network.NetState state, int index )
            {
                if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
                    return;

                if ( index == 0 ) //yes
                {
                    if ( TurnOn )
                    {
                        Container bank = m_Mobile.FindBankNoCreate();

                        // Add guild leader as protected
                        if ( !m_Guild.AddProtection( m_Mobile ) )
                        {
                            m_Mobile.SendAsciiMessage("You already have guild protection on another character.");
                            return;
                        }
                        
                        if ( bank != null && bank.ConsumeTotal( typeof( Gold ), 10000 ) )
                        {
                            m_Guild.LastProtectionPayment = DateTime.Now;
                        }
                        else
                        {
                            m_Guild.RemoveProtection( m_Mobile );
                            m_Mobile.SendAsciiMessage( "You lack the required funds in your bank account to enable protection." );
                        }
                    }
                    else
                    {
                       m_Guild.ClearAllProtection();
                    }

                    m_Mobile.SendMenu( new GuildProtectionMenu( m_Mobile, m_Guild ) );
                }
                else //no
                {
                    m_Mobile.SendMenu( new GuildProtectionMenu( m_Mobile, m_Guild ) );
                }

            }
        }

        private class HouseSignMenu : ItemListMenu
        {
            private Mobile m_Mobile;
            private Guild m_Guild;

            public const int InitialSignId = 3028;
            public const int SignAmount = 54;

            public HouseSignMenu(Mobile from, Guild guild) : base("Choose your guild's house sign" , null )
            {
                m_Mobile = from;
                m_Guild = guild;

                List<ItemListEntry> list = new List<ItemListEntry>();

                for ( int i = InitialSignId; i < InitialSignId + SignAmount; i += 2 )
                {
                    list.Add( new ItemListEntry( "House Sign", i ) );
                }

                Entries = list.ToArray();
            }

            public override void OnCancel( Network.NetState state )
            {
                if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
                    return;

                m_Mobile.SendMenu( new GuildProtectionMenu( m_Mobile, m_Guild ) );
            }

            public override void OnResponse( NetState state, int index )
            {
                if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
                    return;

                m_Guild.GuildHouseSignItemID = InitialSignId + (index*2);

                PlayerMobile pm;
                foreach ( Mobile m in m_Guild.Protected )
                {
                    pm = m as PlayerMobile;
                    if ( pm.ProtectedHouse != null )
                    {
                        pm.ProtectedHouse.ChangeSignType( m_Guild.GuildHouseSignItemID );
                    }
                }

            }
        }

        private class GrantProtectionMenu : GuildMobileListMenu
        {
            public GrantProtectionMenu( Mobile from, Guild guild, List<Mobile> list, int begin )
                : base( from, guild, begin, list, String.Format("Guild protection for each member costs an additional {0:n0} gold pieces per week. Whom do you wish to grant protection to?", Guild.ProtectionRateMember) )
            {
            }

            public override void OnCancel( NetState state )
            {
                if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
                    return;

                m_Mobile.SendMenu( new GuildProtectionMenu( m_Mobile, m_Guild ) );
            }

            public override void OnResponse( NetState state, int index )
            {
                if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
                    return;

                if ( index == m_StringList.IndexOf( "Next page" ) ) // next
                {
                    m_Mobile.SendMenu( new GrantProtectionMenu( m_Mobile, m_Guild, m_List, m_Begin + 11 ) );
                }
                else if ( index == m_StringList.IndexOf( "Previous page" ) ) // back
                {
                    m_Mobile.SendMenu( new GrantProtectionMenu( m_Mobile, m_Guild, m_List, m_Begin - 11 ) );
                }
                else
                {
                    if ( index >= 0 && index < m_List.Count )
                    {
                        Mobile m = (Mobile)m_List[index];

                        if ( m != null && !m.Deleted )
                        {
                            if ( !m_Guild.AddProtection( m ) )
                            {
                                m_Mobile.SendAsciiMessage( "They already have guild protection on another character." );
                            }
                        }
                    }

                    m_Mobile.SendMenu( new GuildProtectionMenu( m_Mobile, m_Guild ) );
                }
            }
        }

        private class RevokeProtectionMenu : GuildMobileListMenu
        {
            public RevokeProtectionMenu( Mobile from, Guild guild, List<Mobile> list, int begin )
                : base( from, guild, begin, list, "Whom do you wish to revoke protection from?" )
            {

            }

            public override void OnCancel( NetState state )
            {
                if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
                    return;

                m_Mobile.SendMenu( new GuildProtectionMenu( m_Mobile, m_Guild ) );
            }

            public override void OnResponse( NetState state, int index )
            {
                if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
                    return;

                if ( index == m_StringList.IndexOf( "Next page" ) ) // next
                {
                    m_Mobile.SendMenu( new RevokeProtectionMenu( m_Mobile, m_Guild, m_List, m_Begin + 11 ) );
                }
                else if ( index == m_StringList.IndexOf( "Previous page" ) ) // back
                {
                    m_Mobile.SendMenu( new RevokeProtectionMenu( m_Mobile, m_Guild, m_List, m_Begin - 11 ) );
                }
                else
                {
                    if ( index >= 0 && index < m_List.Count )
                    {
                        Mobile m = (Mobile)m_List[index];

                        if ( m != null && !m.Deleted )
                        {
                            m_Guild.RemoveProtection( m );
                        }
                    }

                    m_Mobile.SendMenu( new GuildProtectionMenu( m_Mobile, m_Guild ) );
                }
            }
        }
    }
}
