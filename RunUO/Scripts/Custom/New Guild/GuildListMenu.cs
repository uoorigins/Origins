using System;
using System.Collections;
using Server;
using Server.Guilds;
using System.Collections.Generic;
using Server.Menus.Questions;

namespace Server.Menus.Questions
{
    public abstract class GuildListMenu : QuestionMenu
    {
        protected Mobile m_Mobile;
        protected Guild m_Guild;
        protected List<Guild> m_List;
        protected int m_Begin;

        public static List<String> m_StringList;

        public const int ListSize = 11;

        public GuildListMenu( Mobile from, Guild guild, int begin, List<Guild> list, string title )
            : base( title, null )
        {
            m_Mobile = from;
            m_Guild = guild;
            m_Begin = begin;

            m_List = new List<Guild>( list );
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

                Guild g = m_List[i];

                string name;

                if ( ( name = g.Name ) != null && ( name = name.Trim() ).Length <= 0 )
                    name = "(empty)";

                m_StringList.Add( name );

                if ( i + 1 == m_List.Count && i != begin)
                {
                    m_StringList.Add( "Previous page" );
                }
            }

            Answers = m_StringList.ToArray();
        }
    }
}