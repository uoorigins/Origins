using System;
using Server;
using Server.Menus.Questions;
using System.Collections.Generic;

namespace Server.Gumps
{
	public delegate void WarningGumpCallback( Mobile from, bool okay, object state );

	public class WarningGump : QuestionMenu
	{
		private WarningGumpCallback m_Callback;
		private object m_State;
		private bool m_CancelButton;

		public WarningGump( int header, int headerColor, object content, int contentColor, int width, int height, WarningGumpCallback callback, object state )
			: this( header, headerColor, content, contentColor, width, height, callback, state, true )
		{
		}

		public WarningGump( int header, int headerColor, object content, int contentColor, int width, int height, WarningGumpCallback callback, object state, bool cancelButton ) : base( String.Format("{1}", header, content is int ? "" : (string)content), null )
		{
			m_Callback = callback;
			m_State = state;
			m_CancelButton = cancelButton;

            List<String> mAnswers = new List<String>();

            mAnswers.Add("Okay");
            mAnswers.Add("Cancel");

            Answers = mAnswers.ToArray();
		}

		public override void OnResponse( Server.Network.NetState sender, int index )
		{
			if ( index == 0 && m_Callback != null )
				m_Callback( sender.Mobile, true, m_State );
			else if ( m_Callback != null )
				m_Callback( sender.Mobile, false, m_State );
		}
	}
}