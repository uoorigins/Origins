using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBBowyer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBBowyer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
                /*Add(new GenericBuyInfo(typeof(Crossbow), 46, 9, 0xF50, 0));
                Add(new GenericBuyInfo(typeof(HeavyCrossbow), 55, 9, 0x13FD, 0));             
                Add(new GenericBuyInfo(typeof(Bolt), 35, 9, 0x1BFB, 0));
                Add(new GenericBuyInfo(typeof(Bow), 35, 9, 0x13B2, 0));
                Add(new GenericBuyInfo(typeof(Arrow), 35, 9, 0xF3F, 0));
                Add(new GenericBuyInfo(typeof(Shaft), 35, 9, 0x1BD4, 0));
                Add(new GenericBuyInfo(typeof(Feather), 35, 9, 0x1BD1, 0));*/

				//Add( new GenericBuyInfo( typeof( FletcherTools ), 2, 9, 0x1022, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				//Add( typeof( FletcherTools ), 1 );
			}
		}
	}
}