using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBAxeWeapon: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBAxeWeapon()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( ExecutionersAxe ), 48, 9, 0xF45, 0 ) );
				Add( new GenericBuyInfo( typeof( BattleAxe ), 43, 9, 0xF47, 0 ) );
				Add( new GenericBuyInfo( typeof( TwoHandedAxe ), 42, 9, 0x1443, 0 ) );
				Add( new GenericBuyInfo( typeof( Axe ), 48, 9, 0xF49, 0 ) );
				Add( new GenericBuyInfo( typeof( DoubleAxe ), 75, 9, 0xF4B, 0 ) );
				Add( new GenericBuyInfo( typeof( Pickaxe ), 36, 9, 0xE86, 0 ) );
				Add( new GenericBuyInfo( typeof( LargeBattleAxe ), 50, 9, 0x13FB, 0 ) );
				Add( new GenericBuyInfo( typeof( WarAxe ), 45, 9, 0x13B0, 0 ) );

			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( BattleAxe ), 22 );
				Add( typeof( DoubleAxe ), 38 );
				Add( typeof( ExecutionersAxe ), 24 );
				Add( typeof( LargeBattleAxe ), 25 );
				Add( typeof( Pickaxe ), 18 );
				Add( typeof( TwoHandedAxe ), 21 );
				Add( typeof( WarAxe ), 23 );
				Add( typeof( Axe ), 24 );
			}
		}
	}
}
