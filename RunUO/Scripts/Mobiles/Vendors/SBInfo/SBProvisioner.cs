using System;
using System.Collections.Generic;
using Server.Items;
using Server.Guilds;
using Server.Multis.Deeds;

namespace Server.Mobiles
{
	public class SBProvisioner : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBProvisioner()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				//Add( new GenericBuyInfo( "1060834", typeof( Engines.Plants.PlantBowl ), 2, 9, 0x15FD, 0 ) );

				Add( new GenericBuyInfo( typeof( Arrow ), 3, 9, 0xF3F, 0 ) );
				Add( new GenericBuyInfo( typeof( Bolt ), 6, 9, 0x1BFB, 0 ) );

				Add( new GenericBuyInfo( typeof( Backpack ), 18, 9, 0x9B2, 0 ) );
				Add( new GenericBuyInfo( typeof( Pouch ), 6, 9, 0xE79, 0 ) );
				Add( new GenericBuyInfo( typeof( Bag ), 6, 9, 0xE76, 0 ) );
				
				Add( new GenericBuyInfo( typeof( Candle ), 6, 9, 0xA28, 0 ) );
				Add( new GenericBuyInfo( typeof( Torch ), 8, 9, 0xF6B, 0 ) );
				Add( new GenericBuyInfo( typeof( Lantern ), 2, 9, 0xA25, 0 ) );
					
				//TODO: Oil Flask @ 8GP

				Add( new GenericBuyInfo( typeof( Lockpick ), 12, 9, 0x14FC, 0 ) );

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

				Add( new GenericBuyInfo( typeof( BreadLoaf ), 6, 9, 0x103B, 0 ) );
				Add( new GenericBuyInfo( typeof( LambLeg ), 8, 9, 0x160A, 0 ) );
				Add( new GenericBuyInfo( typeof( ChickenLeg ), 5, 9, 0x1608, 0 ) );
				Add( new GenericBuyInfo( typeof( CookedBird ), 17, 9, 0x9B7, 0 ) );

				Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Ale, 7, 9, 0x99F, 0 ) );
				Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Wine, 7, 9, 0x9C7, 0 ) );
				Add( new BeverageBuyInfo( typeof( BeverageBottle ), BeverageType.Liquor, 7, 9, 0x99B, 0 ) );
				Add( new BeverageBuyInfo( typeof( Jug ), BeverageType.Cider, 13, 9, 0x9C8, 0 ) );

				Add( new GenericBuyInfo( typeof( Pear ), 3, 9, 0x994, 0 ) );
				Add( new GenericBuyInfo( typeof( Apple ), 3, 9, 0x9D0, 0 ) );

				Add( new GenericBuyInfo( typeof( Beeswax ), 1, 9, 0x1422, 0 ) );

				Add( new GenericBuyInfo( typeof( Garlic ), 3, 9, 0xF84, 0 ) );
				Add( new GenericBuyInfo( typeof( Ginseng ), 3, 9, 0xF85, 0 ) );

				Add( new GenericBuyInfo( typeof( Bottle ), 5, 9, 0xF0E, 0 ) );

				Add( new GenericBuyInfo( typeof( RedBook ), 15, 9, 0xFF1, 0 ) );
				Add( new GenericBuyInfo( typeof( BlueBook ), 15, 9, 0xFF2, 0 ) );
				Add( new GenericBuyInfo( typeof( TanBook ), 15, 9, 0xFF0, 0 ) );

				Add( new GenericBuyInfo( typeof( WoodenBox ), 14, 9, 0xE7D, 0 ) );
				Add( new GenericBuyInfo( typeof( Key ), 2, 9, 0x100E, 0 ) );

				Add( new GenericBuyInfo( typeof( Bedroll ), 5, 9, 0xA59, 0 ) );
				Add( new GenericBuyInfo( typeof( Kindling ), 2, 9, 0xDE1, 0 ) );

				Add( new GenericBuyInfo( "1041205", typeof( Multis.SmallBoatDeed ), 10177, 9, 0x14F2, 0 ) );
                Add(new GenericBuyInfo("deed to a blue tent", typeof(BlueTentDeed), 22800, 9, 0x14F0, 0));
                Add(new GenericBuyInfo("deed to a green tent", typeof(GreenTentDeed), 22800, 9, 0x14F0, 0));

                Add(new GenericBuyInfo("1041060", typeof(HairDye), 60, 9, 0xEFF, HairDye.GetHue()));

				Add( new GenericBuyInfo( "1016450", typeof( Chessboard ), 2, 9, 0xFA6, 0 ) );
				Add( new GenericBuyInfo( "1016449", typeof( CheckerBoard ), 2, 9, 0xFA6, 0 ) );
				Add( new GenericBuyInfo( typeof( Backgammon ), 2, 9, 0xE1C, 0 ) );
				if ( Core.AOS )
					Add( new GenericBuyInfo( typeof( Engines.Mahjong.MahjongGame ), 6, 9, 0xFAA, 0 ) );
				Add( new GenericBuyInfo( typeof( Dices ), 2, 9, 0xFA7, 0 ) );

				if ( Core.AOS )
				{
					Add( new GenericBuyInfo( typeof( SmallBagBall ), 3, 9, 0x2256, 0 ) );
					Add( new GenericBuyInfo( typeof( LargeBagBall ), 3, 9, 0x2257, 0 ) );
				}

				if( !Guild.NewGuildSystem )
					Add( new GenericBuyInfo( "1041055", typeof( GuildDeed ), 15002, 9, 0x14F0, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Arrow ), 1 );
				Add( typeof( Bolt ), 2 );
				Add( typeof( Backpack ), 9 );
				Add( typeof( Pouch ), 3 );
				Add( typeof( Bag ), 3 );
				Add( typeof( Candle ), 3 );
				Add( typeof( Torch ), 4 );
				Add( typeof( Lantern ), 1 );
				Add( typeof( Lockpick ), 6 );
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
				Add( typeof( Bottle ), 3 );
				Add( typeof( RedBook ), 7 );
				Add( typeof( BlueBook ), 7 );
				Add( typeof( TanBook ), 7 );
				Add( typeof( WoodenBox ), 7 );
				Add( typeof( Kindling ), 1 );
				Add( typeof( HairDye ), 30 );
				Add( typeof( Chessboard ), 1 );
				Add( typeof( CheckerBoard ), 1 );
				Add( typeof( Backgammon ), 1 );
				Add( typeof( Dices ), 1 );

				Add( typeof( Beeswax ), 1 );

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

				if( !Guild.NewGuildSystem )
					Add( typeof( GuildDeed ), 6225 );
			}
		}
	}
}
