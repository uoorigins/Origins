using System;
using System.Collections.Generic;
using Server.Items;

namespace Server.Mobiles
{
	public class SBJewel: SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBJewel()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( GoldRing ), 27, 9, 0x108A, 0 ) );
				Add( new GenericBuyInfo( typeof( Necklace ), 26, 9, 0x1085, 0 ) );
				Add( new GenericBuyInfo( typeof( GoldNecklace ), 27, 9, 0x1088, 0 ) );
				Add( new GenericBuyInfo( typeof( GoldBeadNecklace ), 27, 9, 0x1089, 0 ) );
				Add( new GenericBuyInfo( typeof( Beads ), 27, 9, 0x108B, 0 ) );
				Add( new GenericBuyInfo( typeof( GoldBracelet ), 27, 9, 0x1086, 0 ) );
				Add( new GenericBuyInfo( typeof( GoldEarrings ), 27, 9, 0x1087, 0 ) );

				Add( new GenericBuyInfo( "1060740", typeof( BroadcastCrystal ),  68, 9, 0x1ED0, 0, new object[] {  500 } ) ); // 500 charges
				Add( new GenericBuyInfo( "1060740", typeof( BroadcastCrystal ), 131, 9, 0x1ED0, 0, new object[] { 1000 } ) ); // 1000 charges
				Add( new GenericBuyInfo( "1060740", typeof( BroadcastCrystal ), 256, 9, 0x1ED0, 0, new object[] { 2000 } ) ); // 2000 charges

				Add( new GenericBuyInfo( "1060740", typeof( ReceiverCrystal ), 6, 9, 0x1ED0, 0 ) );

				Add( new GenericBuyInfo( typeof( StarSapphire ), 125, 9, 0xF21, 0 ) );
				Add( new GenericBuyInfo( typeof( Emerald ), 100, 9, 0xF10, 0 ) );
				Add( new GenericBuyInfo( typeof( Sapphire ), 100, 9, 0xF19, 0 ) );
				Add( new GenericBuyInfo( typeof( Ruby ), 75, 9, 0xF13, 0 ) );
				Add( new GenericBuyInfo( typeof( Citrine ), 50, 9, 0xF15, 0 ) );
				Add( new GenericBuyInfo( typeof( Amethyst ), 100, 9, 0xF16, 0 ) );
				Add( new GenericBuyInfo( typeof( Tourmaline ), 75, 9, 0xF2D, 0 ) );
				Add( new GenericBuyInfo( typeof( Amber ), 50, 9, 0xF25, 0 ) );
				Add( new GenericBuyInfo( typeof( Diamond ), 200, 9, 0xF26, 0 ) );

			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Amber ), 25 );
				Add( typeof( Amethyst ), 50 );
				Add( typeof( Citrine ), 25 );
				Add( typeof( Diamond ), 100 );
				Add( typeof( Emerald ), 50 );
				Add( typeof( Ruby ), 37 );
				Add( typeof( Sapphire ), 50 );
				Add( typeof( StarSapphire ), 62 );
				Add( typeof( Tourmaline ), 47 );
				Add( typeof( GoldRing ), 13 );
				Add( typeof( SilverRing ), 10 );
				Add( typeof( Necklace ), 13 );
				Add( typeof( GoldNecklace ), 13 );
				Add( typeof( GoldBeadNecklace ), 13 );
				Add( typeof( SilverNecklace ), 10 );
				Add( typeof( SilverBeadNecklace ), 10 );
				Add( typeof( Beads ), 13 );
				Add( typeof( GoldBracelet ), 13 );
				Add( typeof( SilverBracelet ), 10 );
				Add( typeof( GoldEarrings ), 13 );
				Add( typeof( SilverEarrings ), 10 );
			}
		}
	}
}
