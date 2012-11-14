using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBRangedWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBRangedWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Crossbow ), 63, 9, 0xF50, 0 ) );
				Add( new GenericBuyInfo( typeof( HeavyCrossbow ), 60, 9, 0x13FD, 0 ) );

				Add( new GenericBuyInfo( typeof( Bolt ), 6, 9, 0x1BFB, 0 ) );
				Add( new GenericBuyInfo( typeof( Bow ), 63, 9, 0x13B2, 0 ) );
				Add( new GenericBuyInfo( typeof( Arrow ), 3, 9, 0xF3F, 0 ) );
				Add( new GenericBuyInfo( typeof( Feather ), 2, 9, 0x1BD1, 0 ) );
				Add( new GenericBuyInfo( typeof( Shaft ), 3, 9, 0x1BD4, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Bolt ), 3 );
				Add( typeof( Arrow ), 1 );
				Add( typeof( Shaft ), 1 );
				Add( typeof( Feather ), 1 );			

				Add( typeof( HeavyCrossbow ), 30 );
				Add( typeof( Bow ), 32 );
				Add( typeof( Crossbow ), 32 ); 
			}
		}
	}
}
