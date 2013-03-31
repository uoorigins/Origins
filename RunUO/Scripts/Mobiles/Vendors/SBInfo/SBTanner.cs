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
				Add( new GenericBuyInfo( typeof( LeatherGorget ), 75, 9, 0x13C7, 0 ) );
				Add( new GenericBuyInfo( typeof( Bonnet ), 26, 9, 0x1DB9, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherArms ), 80, 9, 0x13CD, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherChest ), 103, 9, 0x13CC, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherLegs ), 80, 9, 0x13CB, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherGloves ), 60, 9, 0x13C6, 0 ) );

				Add( new GenericBuyInfo( typeof( StuddedGorget ), 74, 9, 0x13D6, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedArms ), 90, 9, 0x13DC, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedChest ), 128, 9, 0x13DB, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedLegs ), 103, 9, 0x13DA, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedGloves ), 80, 9, 0x13D5, 0 ) );

				Add( new GenericBuyInfo( typeof( FemaleStuddedChest ), 144, 9, 0x1C02, 0 ) );
				Add( new GenericBuyInfo( typeof( FemalePlateChest ), 194, 9, 0x1C04, 0 ) );
				Add( new GenericBuyInfo( typeof( FemaleLeatherChest ), 115, 9, 0x1C06, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherShorts ), 90, 9, 0x1C00, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherSkirt ), 90, 9, 0x1C08, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherBustierArms ), 111, 9, 0x1C0A, 0 ) );
				Add( new GenericBuyInfo( typeof( LeatherBustierArms ), 134, 9, 0x1C0B, 0 ) );
				Add( new GenericBuyInfo( typeof( StuddedBustierArms ), 134, 9, 0x1C0C, 0 ) );

				Add( new GenericBuyInfo( typeof( Bag ), 7, 9, 0xE76, 0 ) );
				Add( new GenericBuyInfo( typeof( Pouch ), 7, 9, 0xE79, 0 ) );
				Add( new GenericBuyInfo( typeof( Backpack ), 18, 9, 0x9B2, 0 ) );
				Add( new GenericBuyInfo( typeof( Leather ), 7, 9, 0x1081, 0 ) );

				Add( new GenericBuyInfo( typeof( SkinningKnife ), 27, 9, 0xEC4, 0 ) );

				//Add( new GenericBuyInfo( "1041279", typeof( TaxidermyKit ), 100000, 9, 0x1EBA, 0 ) );

			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Bag ), 3 );
				Add( typeof( Pouch ), 3 );
				Add( typeof( Backpack ), 9 );

				Add( typeof( Leather ), 3 );

				Add( typeof( SkinningKnife ), 14 );
				
				Add( typeof( LeatherArms ), 40 );
				Add( typeof( LeatherChest ), 52 );
				Add( typeof( LeatherGloves ), 30 );
				Add( typeof( LeatherGorget ), 37 );
				Add( typeof( LeatherLegs ), 40 );
				Add( typeof( Bonnet ), 14 );

				Add( typeof( StuddedArms ), 45 );
				Add( typeof( StuddedChest ), 64 );
				Add( typeof( StuddedGloves ), 40 );
				Add( typeof( StuddedGorget ), 37 );
				Add( typeof( StuddedLegs ), 51 );

				Add( typeof( FemaleStuddedChest ), 72 );
				Add( typeof( StuddedBustierArms ), 67 );
                Add(typeof(FemalePlateChest), 97);
				Add( typeof( FemaleLeatherChest ), 63 );
				Add( typeof( LeatherBustierArms ), 55 );
				Add( typeof( LeatherShorts ), 43 );
				Add( typeof( LeatherSkirt ), 43 );
			}
		}
	}
}