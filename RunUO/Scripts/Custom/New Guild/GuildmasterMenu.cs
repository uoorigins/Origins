using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Guilds;
using Server.Network;
using Server.Menus.Questions;
using System.Collections.Generic;
using Server.Gumps;

namespace Server.Menus.Questions
{
    public class GuildmasterMenu : QuestionMenu
    {
        private Mobile m_Mobile;
        private Guild m_Guild;

        public GuildmasterMenu( Mobile from, Guild guild )
            : base( "GUILDMASTER FUNCTIONS", null )
        {
            m_Mobile = from;
            m_Guild = guild;

            List<String> list = new List<String>();

            list.Add( "Set the guild name." );
            list.Add( "Set the guild's abbreviation." );
            list.Add( "Set the guild's charter." );
            list.Add( "Dismiss a member." );
            list.Add( "Go to the WAR menu." );

            if ( m_Guild.Candidates.Count > 0 )
            {
                list.Add( "Administer the list of candidates" );
            }
            else
            {
                list.Add( "There are currently no candidates for membership." );
            }

            list.Add( "Set the guildmaster's title." );
            list.Add( "Grant a title to another member." );
            list.Add( "Move this guildstone." );
            list.Add( "Access Guild Protection menu." );
            list.Add( "Return to the main menu." );

            Answers = list.ToArray();
        }

        public override void OnResponse( NetState state, int index )
        {
            if ( GuildMenu.BadLeader( m_Mobile, m_Guild ) )
                return;

            switch ( index )
            {
                case 9: //guild protection
                    {
                        m_Mobile.SendMenu( new GuildProtectionMenu( m_Mobile, m_Guild ) );
                        break;
                    }
                case 10: // Main menu
                    {
                        m_Mobile.SendMenu( new GuildMenu( m_Mobile, m_Guild ) );

                        break;
                    }
                case 0: // Set guild name
                    {
                        m_Mobile.SendAsciiMessage( "Enter new guild name (40 characters max):" ); // Enter new guild name (40 characters max):
                        m_Mobile.Prompt = new GuildNamePrompt( m_Mobile, m_Guild );

                        break;
                    }
                case 1: // Set guild abbreviation
                    {
                        m_Mobile.SendAsciiMessage( "Enter new guild abbreviation (3 characters max):" ); // Enter new guild abbreviation (3 characters max):
                        m_Mobile.Prompt = new GuildAbbrvPrompt( m_Mobile, m_Guild );

                        break;
                    }
                case 2: // Set charter
                    {
                        m_Mobile.SendAsciiMessage( "Enter the new guild charter (50 characters max):" ); // Enter the new guild charter (50 characters max):
                        m_Mobile.Prompt = new GuildCharterPrompt( m_Mobile, m_Guild );

                        break;
                    }
                case 3: // Dismiss member
                    {
                        m_Mobile.SendMenu( new GuildDismissMenu( m_Mobile, m_Guild, 0 ) );

                        break;
                    }
                case 4: // War menu
                    {
                        m_Mobile.SendMenu( new GuildWarAdminMenu( m_Mobile, m_Guild ) );

                        break;
                    }
                case 5: // Administer candidates
                    {
                        m_Mobile.SendMenu( new GuildAdminCandidatesMenu( m_Mobile, m_Guild, 0 ) );

                        break;
                    }
                case 6: // Set guildmaster's title
                    {
                        m_Mobile.SendAsciiMessage( "Enter new guildmaster title (20 characters max):" ); // Enter new guildmaster title (20 characters max):
                        m_Mobile.Prompt = new GuildTitlePrompt( m_Mobile, m_Mobile, m_Guild );

                        break;
                    }
                case 7: // Grant title
                    {
                        m_Mobile.SendMenu( new GrantGuildTitleMenu( m_Mobile, m_Guild, 0 ) );

                        break;
                    }
                case 8: // Move guildstone
                    {
                        if ( m_Guild.Guildstone != null )
                        {
                            GuildTeleporter item = new GuildTeleporter( m_Guild.Guildstone );

                            if ( m_Guild.Teleporter != null )
                                m_Guild.Teleporter.Delete();

                            m_Mobile.SendAsciiMessage( "Use the teleporting object placed in your backpack to move this guildstone." ); // Use the teleporting object placed in your backpack to move this guildstone.

                            m_Mobile.AddToBackpack( item );
                            m_Guild.Teleporter = item;
                        }

                        m_Mobile.SendMenu( new GuildmasterMenu( m_Mobile, m_Guild ) );

                        break;
                    }
            }
        }
    }
}