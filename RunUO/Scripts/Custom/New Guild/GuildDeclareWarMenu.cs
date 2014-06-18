using System;
using System.Collections;
using Server;
using Server.Guilds;
using Server.Network;
using Server.Factions;
using System.Collections.Generic;
using Server.Menus.Questions;

namespace Server.Menus.Questions
{
    public class GuildDeclareWarMenu : GuildListMenu
    {
        public GuildDeclareWarMenu( Mobile from, Guild guild, int begin, List<Guild> list )
            : base( from, guild, begin, list, "Select the guild you wish to declare war on." )
        {
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

            if ( index == m_StringList.IndexOf( "Next page" ) ) // next
            {
                m_Mobile.SendMenu( new GuildDeclareWarMenu( m_Mobile, m_Guild, m_Begin + ListSize, m_List ) );
            }
            else if ( index == m_StringList.IndexOf( "Previous page" ) ) // back
            {
                m_Mobile.SendMenu( new GuildDeclareWarMenu( m_Mobile, m_Guild, m_Begin - ListSize, m_List ) );
            }
            else
            {
                if ( index >= 0 && index < m_List.Count )
                {
                    Guild g = m_List[index];

                    if ( g != null )
                    {
                        if ( g == m_Guild )
                        {
                            m_Mobile.SendAsciiMessage( "You cannot declare war against yourself!" ); // You cannot declare war against yourself!
                        }
                        else if ( ( g.WarInvitations.Contains( m_Guild ) && m_Guild.WarDeclarations.Contains( g ) ) || m_Guild.IsWar( g ) )
                        {
                            m_Mobile.SendAsciiMessage( "You are already at war with that guild." ); // You are already at war with that guild.
                        }
                        else if ( Faction.Find( m_Guild.Leader ) != null )
                        {
                            m_Mobile.SendLocalizedMessage( 1005288 ); // You cannot declare war while you are in a faction
                        }
                        else
                        {
                            if ( !m_Guild.WarDeclarations.Contains( g ) )
                            {
                                m_Guild.WarDeclarations.Add( g );
                                m_Guild.GuildTextMessage( String.Format( "Guild Message: Your guild has sent an invitation for war: {0} ({1})", g.Name, g.Abbreviation ) ); // Guild Message: Your guild has sent an invitation for war:
                            }

                            if ( !g.WarInvitations.Contains( m_Guild ) )
                            {
                                g.WarInvitations.Add( m_Guild );
                                g.GuildTextMessage( String.Format( "Guild Message: Your guild has received an invitation to war: {0} ({1})", m_Guild.Name, m_Guild.Abbreviation ) ); // Guild Message: Your guild has received an invitation to war:
                            }
                        }

                        m_Mobile.SendMenu( new GuildWarAdminMenu( m_Mobile, m_Guild ) );
                    }
                }
            }
        }
    }
}