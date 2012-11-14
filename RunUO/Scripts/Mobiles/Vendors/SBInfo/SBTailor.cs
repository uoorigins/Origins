using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBTailor: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBTailor()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{

				Add( new GenericBuyInfo( typeof( SewingKit ), 3, 9, 0xF9D, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Scissors ), 13, 9, 0xF9F, 0 ) );
				Add( new GenericBuyInfo( typeof( DyeTub ), 9, 9, 0xFAB, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Dyes ), 8, 9, 0xFA9, 0 ) ); 

				Add( new GenericBuyInfo( typeof( ShortPants ), 25, 9, 0x152E, 0 ) );
				Add( new GenericBuyInfo( typeof( FancyShirt ), 55, 9, 0x1EFD, 0 ) );
				Add( new GenericBuyInfo( typeof( LongPants ), 33, 9, 0x1539, 0 ) );
				Add( new GenericBuyInfo( typeof( FancyDress ), 25, 9, 0x1EFF, 0 ) );
				Add( new GenericBuyInfo( typeof( PlainDress ), 56, 9, 0x1F01, 0 ) );
				Add( new GenericBuyInfo( typeof( Skirt ), 33, 9, 0x1537, 0 ) );
				Add( new GenericBuyInfo( typeof( Kilt ), 31, 9, 0x1537, Utility.RandomNeutralHue() ) );
				Add( new GenericBuyInfo( typeof( HalfApron ), 27, 9, 0x153b, 0 ) );
				Add( new GenericBuyInfo( typeof( Robe ), 72, 9, 0x1F03, 0 ) );
				Add( new GenericBuyInfo( typeof( Cloak ), 49, 9, 0x1515, 0 ) );
				Add( new GenericBuyInfo( typeof( Cloak ), 44, 9, 0x1515, 0 ) );
				Add( new GenericBuyInfo( typeof( Doublet ), 32, 9, 0x1F7B, 0 ) );
				Add( new GenericBuyInfo( typeof( Tunic ), 33, 33, 0x1FA1, 0 ) );
				Add( new GenericBuyInfo( typeof( JesterSuit ), 60, 9, 0x1F9F, 0 ) );

				Add( new GenericBuyInfo( typeof( JesterHat ), 31, 9, 0x171C, 0 ) );
				Add( new GenericBuyInfo( typeof( FloppyHat ), 25, 9, 0x1713, Utility.RandomNeutralHue() ) );
				Add( new GenericBuyInfo( typeof( WideBrimHat ), 26, 9, 0x1714, Utility.RandomNeutralHue() ) );
				Add( new GenericBuyInfo( typeof( Cap ), 27, 9, 0x1715, Utility.RandomNeutralHue() ) );
				Add( new GenericBuyInfo( typeof( TallStrawHat ), 26, 9, 0x1716, Utility.RandomNeutralHue() ) );
				Add( new GenericBuyInfo( typeof( StrawHat ), 25, 9, 0x1717, Utility.RandomNeutralHue() ) );
				Add( new GenericBuyInfo( typeof( WizardsHat ), 30, 9, 0x1718, Utility.RandomNeutralHue() ) );
				Add( new GenericBuyInfo( typeof( Bonnet ), 26, 9, 0x1719, Utility.RandomNeutralHue() ) );
				Add( new GenericBuyInfo( typeof( FeatheredHat ), 27, 9, 0x171A, Utility.RandomNeutralHue() ) );
				Add( new GenericBuyInfo( typeof( TricorneHat ), 26, 9, 0x171B, Utility.RandomNeutralHue() ) );
				Add( new GenericBuyInfo( typeof( Bandana ), 14, 9, 0x1540, Utility.RandomNeutralHue() ) );
				Add( new GenericBuyInfo( typeof( SkullCap ), 13, 9, 0x1544, Utility.RandomNeutralHue() ) );

                Add(new GenericBuyInfo(typeof(BoltOfCloth1), 75, 9, 0xF96, Utility.RandomNeutralHue()));
                Add(new GenericBuyInfo(typeof(BoltOfCloth2), 75, 9, 0xF97, Utility.RandomNeutralHue()));
                Add(new GenericBuyInfo(typeof(BoltOfCloth3), 75, 9, 0xF9B, Utility.RandomNeutralHue()));
                Add(new GenericBuyInfo(typeof(BoltOfCloth4), 75, 9, 0xF9C, Utility.RandomNeutralHue())); 

				Add( new GenericBuyInfo( typeof( UncutCloth ), 2, 9, 0x1767, Utility.RandomNeutralHue() ) ); 

				Add( new GenericBuyInfo( typeof( Cotton ), 91, 9, 0xDF9, 0 ) );
				Add( new GenericBuyInfo( typeof( Wool ), 45, 9, 0xDF8, 0 ) );
				Add( new GenericBuyInfo( typeof( Flax ), 97, 9, 0x1A9C, 0 ) );
				Add( new GenericBuyInfo( typeof( SpoolOfThread ), 3, 9, 0xFA0, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Scissors ), 7 );
				Add( typeof( SewingKit ), 1 );
				Add( typeof( Dyes ), 4 );
				Add( typeof( DyeTub ), 5 );

				Add( typeof( BoltOfCloth ), 38 );

				Add( typeof( FancyShirt ), 28 );
				Add( typeof( Shirt ), 18 );

				Add( typeof( ShortPants ), 13 );
				Add( typeof( LongPants ), 17 );

				Add( typeof( Cloak ), 25 );
				Add( typeof( FancyDress ), 13 );
				Add( typeof( Robe ), 36 );
				Add( typeof( PlainDress ), 28 );

				Add( typeof( Skirt ), 17 );
				Add( typeof( Kilt ), 16 );

				Add( typeof( Doublet ), 16 );
				Add( typeof( Tunic ), 17 );
				Add( typeof( JesterSuit ), 30 );

				Add( typeof( HalfApron ), 13 );

				Add( typeof( JesterHat ), 16 );
				Add( typeof( FloppyHat ), 13 );
				Add( typeof( WideBrimHat ), 13 );
				Add( typeof( Cap ), 14 );
				Add( typeof( SkullCap ), 13 );
				Add( typeof( Bandana ), 7 );
				Add( typeof( TallStrawHat ), 13 );
				Add( typeof( StrawHat ), 13 );
				Add( typeof( WizardsHat ), 15 );
				Add( typeof( Bonnet ), 13 );
				Add( typeof( FeatheredHat ), 14 );
				Add( typeof( TricorneHat ), 13 );

				Add( typeof( SpoolOfThread ), 1 );

				Add( typeof( Flax ), 49 );
				Add( typeof( Cotton ), 46 );
				Add( typeof( Wool ), 23 );
			}
		}
	}
}
