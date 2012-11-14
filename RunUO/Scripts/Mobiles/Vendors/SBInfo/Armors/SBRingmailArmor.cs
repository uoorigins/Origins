using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBRingmailArmor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBRingmailArmor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( RingmailChest ), 218, 9, 0x13ec, 0 ) );
				Add( new GenericBuyInfo( typeof( RingmailLegs ), 150, 9, 0x13F0, 0 ) );
				Add( new GenericBuyInfo( typeof( RingmailArms ), 127, 9, 0x13EE, 0 ) );
				Add( new GenericBuyInfo( typeof( RingmailGloves ), 122, 9, 0x13eb, 0 ) );

			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( RingmailArms ), 42 );
				Add( typeof( RingmailChest ), 60 );
				Add( typeof( RingmailGloves ), 26 );
				Add( typeof( RingmailLegs ), 45 );
			}
		}
	}
}
