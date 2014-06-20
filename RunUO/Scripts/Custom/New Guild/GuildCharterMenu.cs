using System;
using System.Collections;
using Server;
using Server.Guilds;
using Server.Network;
using Server.Menus.Questions;
using System.Collections.Generic;

namespace Server.Menus.Questions
{
    public class GuildCharterMenu : QuestionMenu
    {
        private Mobile m_Mobile;
        private Guild m_Guild;
        private List<String> m_StringList;

        private const string DefaultWebsite = "http://www.uoorigins.com/";

        public GuildCharterMenu( Mobile from, Guild guild )
            : base( "", null )
        {
            m_Mobile = from;
            m_Guild = guild;
            m_StringList = new List<String>();
            
            string charter;

            if ( ( charter = guild.Charter ) == null || ( charter = charter.Trim() ).Length <= 0 )
                Question = "No charter has been defined.";
            else
                Question = charter;

            string website;

            if ( ( website = guild.Website ) == null || ( website = website.Trim() ).Length <= 0 )
                website = DefaultWebsite;

            m_StringList.Add( "Visit the guild website : " + website );
            m_StringList.Add( "Return to the main menu." );


            Answers = m_StringList.ToArray();
        }

        public override void OnCancel( NetState state )
        {
            if ( GuildMenu.BadMember( m_Mobile, m_Guild ) )
                return;

            m_Mobile.SendMenu( new GuildMenu( m_Mobile, m_Guild ) );
        }

        public override void OnResponse( NetState state, int index )
        {
            if ( GuildMenu.BadMember( m_Mobile, m_Guild ) )
                return;

            switch ( index )
            {
                case 0:
                    {
                        string website;

                        if ( ( website = m_Guild.Website ) == null || ( website = website.Trim() ).Length <= 0 )
                            website = DefaultWebsite;

                        m_Mobile.LaunchBrowser( website );
                        break;
                    }
                case 1:
                    {
                        m_Mobile.SendMenu(new GuildMenu(m_Mobile, m_Guild));
                        break;
                    }
            }
        }
    }
}