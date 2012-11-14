using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBMapmaker : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMapmaker()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( BlankMap ), 5, 40, 0x14EC, 0 ) );
				//Add( new GenericBuyInfo( typeof( MapmakersPen ), 8, 9, 0x0FBF, 0 ) );
				//Add( new GenericBuyInfo( typeof( BlankScroll ), 12, 40, 0xEF3, 0 ) );
				
				for ( int i = 0; i < PresetMapEntry.Table.Length; ++i )
					Add( new PresetMapBuyInfo( PresetMapEntry.Table[i], Utility.RandomMinMax( 12, 24 ), 9 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				//Add( typeof( BlankScroll ), 6 );
				Add( typeof( MapmakersPen ), 4 );
				Add( typeof( BlankMap ), 2 );
				Add( typeof( CityMap ), 7 );
                Add( typeof( SeaChart ), 15 );
				Add( typeof( LocalMap ), 16 );
				Add( typeof( WorldMap ), 36 );
				Add( typeof( PresetMapEntry ), 3 );
				//TODO: Buy back maps that the mapmaker sells!!!
			}
		}
	}
}