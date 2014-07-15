using System;
using System.Collections.Generic;

using Server;
using Server.Gumps;
using Server.Network;
using Server.Menus.Questions;

namespace Server.Menus
{
	public interface IRewardOption
	{
		void GetOptions( RewardOptionList list );
		void OnOptionSelected( Mobile from, int choice );
	}

	public class RewardOptionMenu : QuestionMenu
	{
		private RewardOptionList m_Options = new RewardOptionList();
		private IRewardOption m_Option;

		public RewardOptionMenu( IRewardOption option ) : this( option, null )
		{
		}

        public RewardOptionMenu( IRewardOption option, string title )
            : base( title != null && title.Length > 0 ? title : "Select your choice from the menu below.", null )
		{
			m_Option = option;

			if ( m_Option != null )
				m_Option.GetOptions( m_Options );

            List<String> answer = new List<String>();

			for ( int i = 0; i < m_Options.Count; i++ )
			{
				answer.Add(m_Options[ i ].Text); 
			}

            Answers = answer.ToArray();
		}

		public override void OnResponse( NetState sender, int index )
		{
            index = index + 1;
			if ( m_Option != null && Contains( index ) )
				m_Option.OnOptionSelected( sender.Mobile, index );			
		}

		private bool Contains( int chosen )
		{
			if ( m_Options == null )
				return false;

			foreach ( RewardOption option in m_Options )
			{
				if ( option.ID == chosen )
					return true;
			}

			return false;
		}
	}

	public class RewardOption
	{
		private int m_ID;
		private string m_Text;

		public int ID{ get{ return m_ID; } }
		public string Text{ get{ return m_Text; } }

		public RewardOption( int id, string text )
		{
			m_ID = id;
            m_Text = text;
		}
	}

	public class RewardOptionList : List<RewardOption>
	{
		public RewardOptionList() : base()
		{
		}

		public void Add( int id, string text )
		{
			Add( new RewardOption( id, text ) );
		}
	}
}
