using System; 
using System.Collections.Generic; 
using Server.Items; 

namespace Server.Mobiles 
{ 
	public class SBBlacksmith : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBBlacksmith() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 	
				//Add( new GenericBuyInfo( typeof( IronIngot ), 9, 9, 0x1BF2, 0 ) );
				Add( new GenericBuyInfo( typeof( Tongs ), 15, 9, 0xFBB, 0 ) ); 
 
				Add( new GenericBuyInfo( typeof( BronzeShield ), 91, 9, 0x1B72, 0 ) );
				Add( new GenericBuyInfo( typeof( Buckler ), 67, 9, 0x1B73, 0 ) );
				Add( new GenericBuyInfo( typeof( MetalKiteShield ), 134, 9, 0x1B74, 0 ) );
				Add( new GenericBuyInfo( typeof( HeaterShield ), 174, 9, 0x1B76, 0 ) );
				Add( new GenericBuyInfo( typeof( WoodenKiteShield ), 122, 9, 0x1B78, 0 ) );
				Add( new GenericBuyInfo( typeof( MetalShield ), 98, 9, 0x1B7B, 0 ) );

				Add( new GenericBuyInfo( typeof( WoodenShield ), 62, 9, 0x1B7A, 0 ) );

				Add( new GenericBuyInfo( typeof( PlateGorget ), 141, 9, 0x1413, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateChest ), 272, 9, 0x1415, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateLegs ), 217, 9, 0x1411, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateArms ), 182, 9, 0x1410, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateGloves ), 144, 9, 0x1414, 0 ) );

                switch (Utility.Random(6))
                {
                    case 0: Add(new GenericBuyInfo(typeof(PlateGorget), 141 * 2, 1, 0x1413, Utility.RandomMetalHue())); break;
                    case 1: Add(new GenericBuyInfo(typeof(PlateChest), 272 * 2, 1, 0x1415, Utility.RandomMetalHue())); break;
                    case 2: Add(new GenericBuyInfo(typeof(PlateLegs), 217 * 2, 1, 0x1411, Utility.RandomMetalHue())); break;
                    case 3: Add(new GenericBuyInfo(typeof(PlateArms), 182 * 2, 1, 0x1410, Utility.RandomMetalHue())); break;
                    case 4: Add(new GenericBuyInfo(typeof(PlateGloves), 144 * 2, 1, 0x1414, Utility.RandomMetalHue())); break;
                    case 5: Add(new GenericBuyInfo(typeof(PlateHelm), 170 * 2, 1, 0x1412, Utility.RandomMetalHue())); break;
                }

				Add( new GenericBuyInfo( typeof( PlateHelm ), 171, 9, 0x1412, 0 ) );
				Add( new GenericBuyInfo( typeof( CloseHelm ), 165, 9, 0x1408, 0 ) );
				Add( new GenericBuyInfo( typeof( CloseHelm ), 165, 9, 0x1409, 0 ) );
				Add( new GenericBuyInfo( typeof( Helmet ), 182, 9, 0x140A, 0 ) );
				Add( new GenericBuyInfo( typeof( Helmet ), 168, 9, 0x140B, 0 ) );
				Add( new GenericBuyInfo( typeof( NorseHelm ), 165, 9, 0x140E, 0 ) );
				Add( new GenericBuyInfo( typeof( NorseHelm ), 169, 9, 0x140F, 0 ) );
				Add( new GenericBuyInfo( typeof( Bascinet ), 128, 9, 0x140C, 0 ) );
				Add( new GenericBuyInfo( typeof( PlateHelm ), 170, 9, 0x1419, 0 ) );

				Add( new GenericBuyInfo( typeof( ChainCoif ), 132, 9, 0x13BB, 0 ) );
				Add( new GenericBuyInfo( typeof( ChainChest ), 205, 9, 0x13BF, 0 ) );
				Add( new GenericBuyInfo( typeof( ChainLegs ), 168, 9, 0x13BE, 0 ) );

				Add( new GenericBuyInfo( typeof( RingmailChest ), 218, 9, 0x13ec, 0 ) );
				Add( new GenericBuyInfo( typeof( RingmailLegs ), 150, 9, 0x13F0, 0 ) );
				Add( new GenericBuyInfo( typeof( RingmailArms ), 127, 9, 0x13EE, 0 ) );
				Add( new GenericBuyInfo( typeof( RingmailGloves ), 122, 9, 0x13eb, 0 ) );

				Add( new GenericBuyInfo( typeof( ExecutionersAxe ), 48, 9, 0xF45, 0 ) );
				Add( new GenericBuyInfo( typeof( Bardiche ), 61, 9, 0xF4D, 0 ) );
				Add( new GenericBuyInfo( typeof( BattleAxe ), 43, 9, 0xF47, 0 ) );
				Add( new GenericBuyInfo( typeof( TwoHandedAxe ), 42, 9, 0x1443, 0 ) );
				Add( new GenericBuyInfo( typeof( Bow ), 63, 9, 0x13B2, 0 ) );
				Add( new GenericBuyInfo( typeof( ButcherKnife ), 26, 9, 0x13F6, 0 ) );
				Add( new GenericBuyInfo( typeof( Crossbow ), 63, 9, 0xF50, 0 ) );
				Add( new GenericBuyInfo( typeof( HeavyCrossbow ), 60, 9, 0x13FD, 0 ) );
				Add( new GenericBuyInfo( typeof( Cutlass ), 39, 9, 0x1441, 0 ) );
				Add( new GenericBuyInfo( typeof( Dagger ), 33, 9, 0xF52, 0 ) );
				Add( new GenericBuyInfo( typeof( Halberd ), 54, 9, 0x143E, 0 ) );
				Add( new GenericBuyInfo( typeof( HammerPick ), 38, 9, 0x143D, 0 ) );
				Add( new GenericBuyInfo( typeof( Katana ), 50, 9, 0x13FF, 0 ) );
				Add( new GenericBuyInfo( typeof( Kryss ), 55, 9, 0x1401, 0 ) );
				Add( new GenericBuyInfo( typeof( Broadsword ), 56, 9, 0xF5E, 0 ) );
				Add( new GenericBuyInfo( typeof( Longsword ), 68, 9, 0xF61, 0 ) );
				Add( new GenericBuyInfo( typeof( ThinLongsword ), 37, 9, 0x13B8, 0 ) );
				Add( new GenericBuyInfo( typeof( VikingSword ), 81, 9, 0x13B9, 0 ) );
				Add( new GenericBuyInfo( typeof( Cleaver ), 25, 9, 0xEC3, 0 ) );
				Add( new GenericBuyInfo( typeof( Axe ), 48, 9, 0xF49, 0 ) );
				Add( new GenericBuyInfo( typeof( DoubleAxe ), 75, 9, 0xF4B, 0 ) );
				Add( new GenericBuyInfo( typeof( Pickaxe ), 36, 9, 0xE86, 0 ) );
				Add( new GenericBuyInfo( typeof( Pitchfork ), 30, 9, 0xE87, 0 ) );
				Add( new GenericBuyInfo( typeof( Scimitar ), 48, 9, 0x13B6, 0 ) );
				Add( new GenericBuyInfo( typeof( SkinningKnife ), 26, 9, 0xEC4, 0 ) );
				Add( new GenericBuyInfo( typeof( LargeBattleAxe ), 50, 9, 0x13FB, 0 ) );
				Add( new GenericBuyInfo( typeof( WarAxe ), 45, 9, 0x13B0, 0 ) );

				Add( new GenericBuyInfo( typeof( BlackStaff ), 32, 9, 0xDF1, 0 ) );
				Add( new GenericBuyInfo( typeof( Club ), 24, 9, 0x13B4, 0 ) );
				Add( new GenericBuyInfo( typeof( GnarledStaff ), 25, 9, 0x13F8, 0 ) );
				Add( new GenericBuyInfo( typeof( Mace ), 43, 9, 0xF5C, 0 ) );
				Add( new GenericBuyInfo( typeof( Maul ), 39, 9, 0x143B, 0 ) );
				Add( new GenericBuyInfo( typeof( QuarterStaff ), 36, 9, 0xE89, 0 ) );
				Add( new GenericBuyInfo( typeof( ShepherdsCrook ), 27, 9, 0xE81, 0 ) );
				Add( new GenericBuyInfo( typeof( SmithHammer ), 32, 9, 0x13E3, 0 ) );
				Add( new GenericBuyInfo( typeof( ShortSpear ), 39, 9, 0x1403, 0 ) );
				Add( new GenericBuyInfo( typeof( Spear ), 43, 9, 0xF62, 0 ) );
				Add( new GenericBuyInfo( typeof( WarHammer ), 37, 9, 0x1439, 0 ) );
				Add( new GenericBuyInfo( typeof( WarMace ), 39, 9, 0x1407, 0 ) );
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				Add( typeof( Tongs ), 8 ); 
				//Add( typeof( IronIngot ), 5 ); 

				Add( typeof( Buckler ), 34 );
				Add( typeof( BronzeShield ), 46 );
				Add( typeof( MetalShield ), 54 );
				Add( typeof( MetalKiteShield ), 67 );
				Add( typeof( HeaterShield ), 87 );
				Add( typeof( WoodenKiteShield ), 61 );

				Add( typeof( WoodenShield ), 31 );

				Add( typeof( PlateArms ), 91 );
				Add( typeof( PlateChest ), 136 );
				Add( typeof( PlateGloves ), 72 );
				Add( typeof( PlateGorget ), 71 );
				Add( typeof( PlateLegs ), 109 );

				Add( typeof( FemalePlateChest ), 136 );
				Add( typeof( FemaleLeatherChest ), 18 );
				Add( typeof( FemaleStuddedChest ), 25 );
				Add( typeof( LeatherShorts ), 14 );
				Add( typeof( LeatherSkirt ), 11 );
				Add( typeof( LeatherBustierArms ), 11 );
				Add( typeof( StuddedBustierArms ), 27 );

				Add( typeof( Bascinet ), 62 );
				Add( typeof( CloseHelm ), 83 );
				Add( typeof( Helmet ), 84 );
				Add( typeof( NorseHelm ), 83 );
				Add( typeof( PlateHelm ), 85 );

				Add( typeof( ChainCoif ), 66 );
				Add( typeof( ChainChest ), 103 );
				Add( typeof( ChainLegs ), 84 );

				Add( typeof( RingmailArms ), 42 );
				Add( typeof( RingmailChest ), 60 );
				Add( typeof( RingmailGloves ), 26 );
				Add( typeof( RingmailLegs ), 45 );

				Add( typeof( BattleAxe ), 22 );
				Add( typeof( DoubleAxe ), 38 );
				Add( typeof( ExecutionersAxe ), 24 );
				Add( typeof( LargeBattleAxe ), 25 );
				Add( typeof( Pickaxe ), 18 );
				Add( typeof( TwoHandedAxe ), 21 );
				Add( typeof( WarAxe ), 23 );
				Add( typeof( Axe ), 24 );

				Add( typeof( Bardiche ), 31 );
				Add( typeof( Halberd ), 27 );

				Add( typeof( ButcherKnife ), 13 );
				Add( typeof( Cleaver ), 13 );
				Add( typeof( Dagger ), 17 );
				Add( typeof( SkinningKnife ), 13 );

				Add( typeof( Club ), 12 );
				Add( typeof( HammerPick ), 19 );
				Add( typeof( Mace ), 22 );
				Add( typeof( Maul ), 20 );
				Add( typeof( WarHammer ), 19 );
				Add( typeof( WarMace ), 20 );

				Add( typeof( HeavyCrossbow ), 30 );
				Add( typeof( Bow ), 32 );
				Add( typeof( Crossbow ), 32 ); 

				Add( typeof( Spear ), 22 );
				Add( typeof( Pitchfork ), 15 );
				Add( typeof( ShortSpear ), 20 );

				Add( typeof( BlackStaff ), 16 );
				Add( typeof( GnarledStaff ), 13 );
				Add( typeof( QuarterStaff ), 18 );
				Add( typeof( ShepherdsCrook ), 14 );

				Add( typeof( SmithHammer ), 16 );

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