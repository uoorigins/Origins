using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBRanger : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBRanger()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new AnimalBuyInfo( 1, typeof( Cat ), 138, 9, 201, 0 ) );
				Add( new AnimalBuyInfo( 1, typeof( Dog ), 181, 9, 217, 0 ) );
				Add( new AnimalBuyInfo( 1, typeof( PackLlama ), 491, 9, 292, 0 ) );
				Add( new AnimalBuyInfo( 1, typeof( PackHorse ), 606, 9, 291, 0 ) );
				Add( new GenericBuyInfo( typeof( Bandage ), 5, 9, 0xE21, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
}