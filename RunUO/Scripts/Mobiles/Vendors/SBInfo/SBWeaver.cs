using System; 
using System.Collections.Generic; 
using Server.Items; 

namespace Server.Mobiles 
{ 
	public class SBWeaver: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBWeaver() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				Add( new GenericBuyInfo( typeof( Dyes ), 8, 9, 0xFA9, 0 ) ); 
				Add( new GenericBuyInfo( typeof( DyeTub ), 9, 9, 0xFAB, 0 ) ); 

				Add( new GenericBuyInfo( typeof( UncutCloth ), 2, 9, 0x1761, 0 ) ); 
				Add( new GenericBuyInfo( typeof( UncutCloth ), 2, 9, 0x1762, 0 ) ); 
				Add( new GenericBuyInfo( typeof( UncutCloth ), 2, 9, 0x1763, 0 ) ); 
				Add( new GenericBuyInfo( typeof( UncutCloth ), 2, 9, 0x1764, 0 ) ); 

				Add( new GenericBuyInfo( typeof( BoltOfCloth1 ), 75, 9, 0xf96, 0 ) ); 
				Add( new GenericBuyInfo( typeof( BoltOfCloth2 ), 75, 9, 0xf97, 0 ) ); 
				Add( new GenericBuyInfo( typeof( BoltOfCloth3 ), 75, 9, 0xf9b, 0 ) ); 
				Add( new GenericBuyInfo( typeof( BoltOfCloth4 ), 75, 9, 0xf9c, 0 ) ); 

				Add( new GenericBuyInfo( typeof( DarkYarn ), 15, 9, 0xE1D, 0 ) );
				Add( new GenericBuyInfo( typeof( LightYarn ), 15, 9, 0xE1E, 0 ) );
				Add( new GenericBuyInfo( typeof( LightYarnUnraveled ), 15, 9, 0xE1F, 0 ) );

				Add( new GenericBuyInfo( typeof( Scissors ), 13, 9, 0xF9F, 0 ) );

			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				Add( typeof( Scissors ), 7 ); 
				Add( typeof( Dyes ), 4 ); 
				Add( typeof( DyeTub ), 5 ); 
				Add( typeof( UncutCloth ), 1 );
				Add( typeof( BoltOfCloth ), 38 ); 
				Add( typeof( LightYarnUnraveled ), 8 );
				Add( typeof( LightYarn ), 8 );
				Add( typeof( DarkYarn ), 8 );
			} 
		} 
	} 
}