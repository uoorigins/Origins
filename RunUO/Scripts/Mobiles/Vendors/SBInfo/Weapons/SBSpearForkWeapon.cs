using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBSpearForkWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSpearForkWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Pitchfork ), 30, 9, 0xE87, 0 ) );
				Add( new GenericBuyInfo( typeof( ShortSpear ), 39, 9, 0x1403, 0 ) );
				Add( new GenericBuyInfo( typeof( Spear ), 43, 9, 0xF62, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Spear ), 22 );
				Add( typeof( Pitchfork ), 15 );
				Add( typeof( ShortSpear ), 20 );
			}
		}
	}
}
