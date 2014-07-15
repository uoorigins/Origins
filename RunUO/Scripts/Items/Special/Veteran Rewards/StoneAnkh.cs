using System;
using Server;
using Server.Multis;
using Server.Gumps;
using Server.Network;
using Server.Engines.VeteranRewards;
using Server.Menus.Questions;

namespace Server.Items
{	
	public class StoneAnkhComponent : AddonComponent
	{
		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }

		public StoneAnkhComponent( int itemID ) : base( itemID )
		{
			Weight = 1.0;
		}

		public StoneAnkhComponent( Serial serial ) : base( serial )
		{
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			
			if ( Addon is StoneAnkh && ((StoneAnkh) Addon).IsRewardItem )
				list.Add( 1076221 ); // 5th Year Veteran Reward
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}
			
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class StoneAnkh : BaseAddon, IRewardItem
	{
		public override BaseAddonDeed Deed
		{ 
			get
			{ 
				StoneAnkhDeed deed = new StoneAnkhDeed();
				deed.IsRewardItem = m_IsRewardItem;

				return deed; 
			} 
		}

		private bool m_IsRewardItem;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsRewardItem
		{
			get{ return m_IsRewardItem; }
			set{ m_IsRewardItem = value; InvalidateProperties(); }
		}
		
		 
		public StoneAnkh() : this( true )
		{
		}
		
		 
		public StoneAnkh( bool east ) : base()
		{			
			if ( east )
			{
				AddComponent( new StoneAnkhComponent( 0x2 ), 0, 0, 0 );
				AddComponent( new StoneAnkhComponent( 0x3 ), 0, -1, 0 );
			}
			else
			{
				AddComponent( new StoneAnkhComponent( 0x5 ), 0, 0, 0 );
				AddComponent( new StoneAnkhComponent( 0x4 ), -1, 0, 0 );
			}
		}

		public StoneAnkh( Serial serial ) : base( serial )
		{
		}
		
		public override void OnChop( Mobile from )
		{
            from.SendLocalizedMessage( "You can't use an axe on that." ); // You can't use an axe on that.
			return;
		}

			public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			
			if ( Core.ML && m_IsRewardItem )
				list.Add( 1076221 ); // 5th Year Veteran Reward
		}

		public override void OnComponentUsed( AddonComponent c, Mobile from )
			{
				if ( from.InRange( Location, 2 ) )
				{
				BaseHouse house = BaseHouse.FindHouseAt( this );  
				
						if ( house != null && house.IsOwner( from ) )
						{
						from.CloseGump( typeof( RewardDemolitionGump ) );
						from.SendGump( new RewardDemolitionGump( this, 1049783 ) ); // Do you wish to re-deed this decoration?
					}
					else
                            from.SendLocalizedMessage( "You can only re-deed this decoration if you are the house owner or originally placed the decoration." ); // You can only re-deed this decoration if you are the house owner or originally placed the decoration.
				}
				else
                    from.Say( true, "I can't reach that." ); // I can't reach that.
			}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
			
			writer.Write( (bool) m_IsRewardItem );
		}
			
			public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
			
			m_IsRewardItem = reader.ReadBool();
		}
	}	
	
	public class StoneAnkhDeed : BaseAddonDeed, IRewardItem
	{
		public override string AsciiName{ get{ return "a deed for a stone ankh"; } } // deed for a stone ankh
		
		private bool m_East;
		private bool m_IsRewardItem;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsRewardItem
		{
			get{ return m_IsRewardItem; }
			set{ m_IsRewardItem = value; InvalidateProperties(); }
		}

		public override BaseAddon Addon
		{ 
			get
			{ 
				StoneAnkh addon = new StoneAnkh( m_East );
				addon.IsRewardItem = m_IsRewardItem;

				return addon; 
			} 
		}

		[Constructable] 
		public StoneAnkhDeed() : base()
		{
			LootType = LootType.Blessed;
		}

		public StoneAnkhDeed( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( m_IsRewardItem && !RewardSystem.CheckIsUsableBy( from, this, null ) )
				return;
			
			if ( IsChildOf( from.Backpack ) )
			{
				from.SendMenu( new InternalMenu( this ) );
			}
			else
				from.SendLocalizedMessage( "You must have the object in your backpack to use it." ); // You must have the object in your backpack to use it.    
		}
		
		private void SendTarget( Mobile m )
		{
			base.OnDoubleClick( m );
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			
			if ( m_IsRewardItem )
				list.Add( 1076221 ); // 5th Year Veteran Reward
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version

			writer.Write( (bool) m_IsRewardItem );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
			
			m_IsRewardItem = reader.ReadBool();
		}
		
		private class InternalMenu : QuestionMenu
		{
			private StoneAnkhDeed m_Deed;

            public InternalMenu( StoneAnkhDeed deed )
                : base( "Select your choice from the menu below", new string[] { "South", "East" } )
			{
				m_Deed = deed;				
			}

			public override void OnResponse( NetState sender, int index )
			{
				if ( m_Deed == null || m_Deed.Deleted )
					return;

					m_Deed.m_East = ( index != 0 );
					m_Deed.SendTarget( sender.Mobile );
			}
		}
	}
}
