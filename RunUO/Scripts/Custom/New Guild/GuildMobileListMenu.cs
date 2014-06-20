using System;
using System.Collections;
using Server;
using Server.Guilds;
using System.Collections.Generic;
using Server.Menus.Questions;
using Server.Network;

namespace Server.Menus.Questions
{
    public class GuildMobileListMenu : QuestionMenu
    {
        protected Mobile m_Mobile;
        protected Guild m_Guild;
        protected List<Mobile> m_List;
        protected static int m_Begin;

        public static List<String> m_StringList;

        public const int ListSize = 11;

        public GuildMobileListMenu( Mobile from, Guild guild, int begin, List<Mobile> list, string title )
            : base( title, null )
        {
            m_Mobile = from;
            m_Guild = guild;
            m_Begin = begin;

            m_List = new List<Mobile>( list );
            m_StringList = new List<String>();

            for ( int i = begin; i < m_List.Count; ++i )
            {
                if ( ( i % 11 ) == 0 )
                {
                    if ( i != begin )
                    {
                        m_StringList.Add( "Next page" );

                        if ( begin != 0 )
                        {
                            m_StringList.Add( "Previous page" );
                        }
                        break;
                    }
                }

                Mobile m = m_List[i];

                string name;

                if ( ( name = m.Name ) != null && ( name = name.Trim() ).Length <= 0 )
                    name = "(empty)";

                m_StringList.Add( name );

                if ( i+1 == m_List.Count && begin != 0 )
                {
                    m_StringList.Add( "Previous page" );
                }
            }

            Answers = m_StringList.ToArray();
        }
    }
}