using System;
using System.Collections;
using Server;
using Server.Guilds;
using Server.Network;
using Server.Menus.Questions;
using System.Collections.Generic;
using Server.Gumps;

namespace Server.Menus.Questions
{
    public class GuildWarAdminMenu : QuestionMenu
    {
        private Mobile m_Mobile;
        private Guild m_Guild;

        public GuildWarAdminMenu( Mobile from, Guild guild )
            : base( "WAR FUNCTIONS", null )
        {
            m_Mobile = from;
            m_Guild = guild;

            List<String> list = new List<String>();

            list.Add( "Declare war through guild name search." );

            list.Add( "Declare peace." );

            list.Add( "Accept war invitations." );
            list.Add( "Reject war invitations." );

            list.Add( "Rescind your war declarations." );

            list.Add( "Return to the previous menu." );

            Answers = list.ToArray();
        }

        public override void OnCancel( NetState state )
        {
            if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
                return;

            m_Mobile.SendMenu( new GuildmasterMenu( m_Mobile, m_Guild ) );
        }

        public override void OnResponse( NetState state, int index )
        {
            if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
                return;

            switch ( index + 1 )
            {
                case 1: // Declare war
                    {
                        m_Mobile.SendAsciiMessage( "Declare war through search - Enter Guild Name:" ); // Declare war through search - Enter Guild Name:  
                        m_Mobile.Prompt = new GuildDeclareWarPrompt( m_Mobile, m_Guild );

                        break;
                    }
                case 2: // Declare peace
                    {
                        if ( m_Guild.Enemies.Count > 0 )
                        {
                            m_Mobile.SendMenu( new GuildDeclarePeaceMenu( m_Mobile, m_Guild, 0 ) );
                        }
                        else
                        {
                            m_Mobile.SendMenu( new GuildWarAdminMenu( m_Mobile, m_Guild ) );
                            m_Mobile.SendAsciiMessage( "No current wars" );
                        }

                        break;
                    }
                case 3: // Accept war
                    {
                        if ( m_Guild.WarInvitations.Count > 0 )
                        {
                            m_Mobile.SendMenu( new GuildAcceptWarMenu( m_Mobile, m_Guild, 0 ) );
                        }
                        else
                        {
                            m_Mobile.SendMenu( new GuildWarAdminMenu( m_Mobile, m_Guild ) );
                            m_Mobile.SendAsciiMessage( "No current invitations received for war." );
                        }

                        break;
                    }
                case 4: // Reject war
                    {
                        if ( m_Guild.WarInvitations.Count > 0 )
                        {
                            m_Mobile.SendMenu( new GuildRejectWarMenu( m_Mobile, m_Guild, 0 ) );
                        }
                        else
                        {
                            m_Mobile.SendMenu( new GuildWarAdminMenu( m_Mobile, m_Guild ) );
                            m_Mobile.SendAsciiMessage( "No current invitations received for war." );
                        }

                        break;
                    }
                case 5: // Rescind declarations
                    {
                        if ( m_Guild.WarDeclarations.Count > 0 )
                        {
                            m_Mobile.SendMenu( new GuildRescindDeclarationMenu( m_Mobile, m_Guild, 0 ) );
                        }
                        else
                        {
                            m_Mobile.SendMenu( new GuildWarAdminMenu( m_Mobile, m_Guild ) );
                            m_Mobile.SendAsciiMessage( "No current war declarations" );
                        }

                        break;
                    }
                case 6: // Return
                    {
                        m_Mobile.SendMenu( new GuildmasterMenu( m_Mobile, m_Guild ) );

                        break;
                    }
            }
        }
    }
}