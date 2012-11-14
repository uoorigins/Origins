using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBMaceWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMaceWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( HammerPick ), 38, 9, 0x143D, 0 ) );
				Add( new GenericBuyInfo( typeof( Club ), 24, 9, 0x13B4, 0 ) );
				Add( new GenericBuyInfo( typeof( Mace ), 43, 9, 0xF5C, 0 ) );
				Add( new GenericBuyInfo( typeof( Maul ), 39, 9, 0x143B, 0 ) );
				Add( new GenericBuyInfo( typeof( WarHammer ), 37, 9, 0x1439, 0 ) );
				Add( new GenericBuyInfo( typeof( WarMace ), 39, 9, 0x1407, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			    Add( typeof( Club ), 12 );
				Add( typeof( HammerPick ), 19 );
				Add( typeof( Mace ), 22 );
				Add( typeof( Maul ), 20 );
				Add( typeof( WarHammer ), 19 );
				Add( typeof( WarMace ), 20 );
			}
		}
	}
}
