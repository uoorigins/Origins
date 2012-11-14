using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBKnifeWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBKnifeWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( ButcherKnife ), 26, 9, 0x13F6, 0 ) );
				Add( new GenericBuyInfo( typeof( Dagger ), 33, 9, 0xF52, 0 ) );
				Add( new GenericBuyInfo( typeof( Cleaver ), 25, 9, 0xEC3, 0 ) );
				Add( new GenericBuyInfo( typeof( SkinningKnife ), 26, 9, 0xEC4, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( ButcherKnife ), 13 );
				Add( typeof( Cleaver ), 13 );
				Add( typeof( Dagger ), 17 );
				Add( typeof( SkinningKnife ), 13 );
			}
		}
	}
}
