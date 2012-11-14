using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBSwordWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSwordWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Cutlass ), 39, 9, 0x1441, 0 ) );
				Add( new GenericBuyInfo( typeof( Katana ), 50, 9, 0x13FF, 0 ) );
				Add( new GenericBuyInfo( typeof( Kryss ), 55, 9, 0x1401, 0 ) );
				Add( new GenericBuyInfo( typeof( Broadsword ), 56, 9, 0xF5E, 0 ) );
				Add( new GenericBuyInfo( typeof( Longsword ), 68, 9, 0xF61, 0 ) );
				Add( new GenericBuyInfo( typeof( ThinLongsword ), 37, 9, 0x13B8, 0 ) );
				Add( new GenericBuyInfo( typeof( VikingSword ), 81, 9, 0x13B9, 0 ) );
				Add( new GenericBuyInfo( typeof( Scimitar ), 48, 9, 0x13B6, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Broadsword ), 28 );
				Add( typeof( Cutlass ), 20 );
				Add( typeof( Katana ), 25 );
				Add( typeof( Kryss ), 28 );
				Add( typeof( Longsword ), 34 );
				Add( typeof( Scimitar ), 24 );
				Add( typeof( ThinLongsword ), 19 );
				Add( typeof( VikingSword ), 41 );
			}
		}
	}
}
