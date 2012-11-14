using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBTanner : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBTanner()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( LeatherGorget ), 31, 9, 0x13C7, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherCap ), 10, 9, 0x1DB9, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherArms ), 37, 9, 0x13CD, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherChest ), 47, 9, 0x13CC, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherLegs ), 36, 9, 0x13CB, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherGloves ), 31, 9, 0x13C6, 0 ) );

				Add( new GenericBuyInfo( typeof( StuddedGorget ), 50, 9, 0x13D6, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedArms ), 57, 9, 0x13DC, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedChest ), 75, 9, 0x13DB, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedLegs ), 67, 9, 0x13DA, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedGloves ), 45, 9, 0x13D5, 0 ) );

				Add( new GenericBuyInfo( typeof( FemaleStuddedChest ), 62, 9, 0x1C02, 0 ) );
				Add( new GenericBuyInfo( typeof( FemalePlateChest ), 207, 9, 0x1C04, 0 ) );
				Add( new GenericBuyInfo( typeof( FemaleLeatherChest ), 36, 9, 0x1C06, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherShorts ), 28, 9, 0x1C00, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherSkirt ), 25, 9, 0x1C08, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherBustierArms ), 25, 9, 0x1C0A, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherBustierArms ), 30, 9, 0x1C0B, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedBustierArms ), 50, 9, 0x1C0C, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedBustierArms ), 47, 9, 0x1C0D, 0 ) );

				Add( new GenericBuyInfo( typeof( Bag ), 6, 9, 0xE76, 0 ) );
				Add( new GenericBuyInfo( typeof( Pouch ), 6, 9, 0xE79, 0 ) );
				Add( new GenericBuyInfo( typeof( Backpack ), 15, 9, 0x9B2, 0 ) );
				Add( new GenericBuyInfo( typeof( Leather ), 6, 9, 0x1081, 0 ) );

				Add( new GenericBuyInfo( typeof( SkinningKnife ), 15, 9, 0xEC4, 0 ) );

				//Add( new GenericBuyInfo( "1041279", typeof( TaxidermyKit ), 100000, 9, 0x1EBA, 0 ) );

			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Bag ), 3 );
				Add( typeof( Pouch ), 3 );
				Add( typeof( Backpack ), 7 );

				Add( typeof( Leather ), 5 );

				Add( typeof( SkinningKnife ), 7 );
				
				Add( typeof( LeatherArms ), 18 );
				Add( typeof( LeatherChest ), 23 );
				Add( typeof( LeatherGloves ), 15 );
				Add( typeof( LeatherGorget ), 15 );
				Add( typeof( LeatherLegs ), 18 );
				Add( typeof( LeatherCap ), 5 );

				Add( typeof( StuddedArms ), 43 );
				Add( typeof( StuddedChest ), 37 );
				Add( typeof( StuddedGloves ), 39 );
				Add( typeof( StuddedGorget ), 22 );
				Add( typeof( StuddedLegs ), 33 );

				Add( typeof( FemaleStuddedChest ), 31 );
				Add( typeof( StuddedBustierArms ), 23 );
				Add( typeof( FemalePlateChest), 103 );
				Add( typeof( FemaleLeatherChest ), 18 );
				Add( typeof( LeatherBustierArms ), 12 );
				Add( typeof( LeatherShorts ), 14 );
				Add( typeof( LeatherSkirt ), 12 );
			}
		}
	}
}