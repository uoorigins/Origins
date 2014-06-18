using System;
using System.Collections;
using Server;
using Server.Guilds;
using Server.Network;
using System.Collections.Generic;

namespace Server.Menus.Questions
{
    public class GuildWarMenu : QuestionMenu
    {
        private Mobile m_Mobile;
        private Guild m_Guild;

        public GuildWarMenu( Mobile from, Guild guild )
            : base( "WARFARE STATUS", null )
        {
            m_Mobile = from;
            m_Guild = guild;

            List<String> list = new List<String>();

            list.Add( "Guilds we are at war with" );
            list.Add( "Guilds that we have declared war on" );
            list.Add( "Guilds that have declared war on us" );
            list.Add( "Return to the main menu." );

            Answers = list.ToArray();
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
                    m_Mobile.SendMenu( new InternalWarMenu( m_Mobile, m_Guild, 0 ) );
                    break;
                case 1:
                    m_Mobile.SendMenu( new InternalDeclarationsMenu( m_Mobile, m_Guild, 0 ) );
                    break;
                case 2:
                    m_Mobile.SendMenu( new InternalDeclaredMenu( m_Mobile, m_Guild, 0 ) );
                    break;
                case 3:
                default:
                    m_Mobile.SendMenu( new GuildMenu( m_Mobile, m_Guild ) );
                    break;
            }
        }

        private class InternalWarMenu : GuildListMenu
        {
            public InternalWarMenu( Mobile from, Guild guild, int begin )
                : base( from, guild, begin, guild.Enemies, "We are at war with:" )
            {
            }

            public override void OnCancel( NetState state )
            {
                if ( GuildMenu.BadMember( m_Mobile, m_Guild ) )
                    return;

                m_Mobile.SendMenu( new GuildWarMenu( m_Mobile, m_Guild ) );
            }

            public override void OnResponse( NetState state, int index )
            {
                if ( GuildMenu.BadMember( m_Mobile, m_Guild ) )
                    return;

                m_Mobile.SendMenu( new GuildMenu( m_Mobile, m_Guild ) );
            }
        }

        private class InternalDeclarationsMenu : GuildListMenu
        {
            public InternalDeclarationsMenu( Mobile from, Guild guild, int begin )
                : base( from, guild, begin, guild.WarDeclarations, "Guilds that we have declared war on: " )
            {
            }

            public override void OnCancel( NetState state )
            {
                if ( GuildMenu.BadMember( m_Mobile, m_Guild ) )
                    return;

                m_Mobile.SendMenu( new GuildWarMenu( m_Mobile, m_Guild ) );
            }

            public override void OnResponse( NetState state, int index )
            {
                if ( GuildMenu.BadMember( m_Mobile, m_Guild ) )
                    return;

                m_Mobile.SendMenu( new GuildMenu( m_Mobile, m_Guild ) );
            }
        }

        private class InternalDeclaredMenu : GuildListMenu
        {
            public InternalDeclaredMenu( Mobile from, Guild guild, int begin )
                : base( from, guild, begin, guild.WarInvitations, "Guilds that have declared war on us:" )
            {
            }

            public override void OnCancel( NetState state )
            {
                if ( GuildMenu.BadMember( m_Mobile, m_Guild ) )
                    return;

                m_Mobile.SendMenu( new GuildWarMenu( m_Mobile, m_Guild ) );
            }

            public override void OnResponse( NetState state, int index )
            {
                if ( GuildMenu.BadMember( m_Mobile, m_Guild ) )
                    return;

                m_Mobile.SendMenu( new GuildMenu( m_Mobile, m_Guild ) );
            }
        }
    }
}