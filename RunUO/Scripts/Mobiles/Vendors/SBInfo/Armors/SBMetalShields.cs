using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBMetalShields : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMetalShields()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( BronzeShield ), 91, 9, 0x1B72, 0 ) );
				Add( new GenericBuyInfo( typeof( Buckler ), 67, 9, 0x1B73, 0 ) );
				Add( new GenericBuyInfo( typeof( MetalKiteShield ), 134, 9, 0x1B74, 0 ) );
				Add( new GenericBuyInfo( typeof( HeaterShield ), 174, 9, 0x1B76, 0 ) );
				Add( new GenericBuyInfo( typeof( WoodenKiteShield ), 122, 9, 0x1B78, 0 ) );
				Add( new GenericBuyInfo( typeof( MetalShield ), 98, 9, 0x1B7B, 0 ) );

			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Buckler ), 34 );
				Add( typeof( BronzeShield ), 46 );
				Add( typeof( MetalShield ), 54 );
				Add( typeof( MetalKiteShield ), 67 );
				Add( typeof( HeaterShield ), 87 );
				Add( typeof( WoodenKiteShield ), 61 );
			}
		}
	}
}