using System; 
using System.Collections.Generic; 
using Server.Items; 

namespace Server.Mobiles 
{ 
	public class SBWeaponSmith: SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBWeaponSmith() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
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

				Add( new GenericBuyInfo( typeof( Hatchet ), 25, 9, 0xF44, 0 ) );
				Add( new GenericBuyInfo( typeof( Hatchet ), 27, 9, 0xF43, 0 ) );
				Add( new GenericBuyInfo( typeof( WarFork ), 32, 9, 0x1405, 0 ) );

            			switch ( Utility.Random( 3 )) 
            			{ 
					case 0:
                        {
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

                        Add(new GenericBuyInfo(typeof(Katana), 50, 9, 0x13FF, 0));
                        Add(new GenericBuyInfo(typeof(Kryss), 55, 9, 0x1401, 0));
                        Add(new GenericBuyInfo(typeof(Broadsword), 56, 9, 0xF5E, 0));
                        Add(new GenericBuyInfo(typeof(Longsword), 68, 9, 0xF61, 0));
                        Add(new GenericBuyInfo(typeof(ThinLongsword), 37, 9, 0x13B8, 0));
                        Add(new GenericBuyInfo(typeof(VikingSword), 81, 9, 0x13B9, 0));

				        Add( new GenericBuyInfo( typeof( Cleaver ), 25, 9, 0xEC3, 0 ) );
				        Add( new GenericBuyInfo( typeof( Axe ), 48, 9, 0xF49, 0 ) );
				        Add( new GenericBuyInfo( typeof( DoubleAxe ), 75, 9, 0xF4B, 0 ) );
				        Add( new GenericBuyInfo( typeof( Pickaxe ), 36, 9, 0xE86, 0 ) );
				        Add( new GenericBuyInfo( typeof( Pitchfork ), 30, 9, 0xE87, 0 ) );
				        Add( new GenericBuyInfo( typeof( Scimitar ), 48, 9, 0x13B6, 0 ) );
				        Add( new GenericBuyInfo( typeof( SkinningKnife ), 26, 9, 0xEC4, 0 ) );
				        Add( new GenericBuyInfo( typeof( LargeBattleAxe ), 50, 9, 0x13FB, 0 ) );
				        Add( new GenericBuyInfo( typeof( WarAxe ), 45, 9, 0x13B0, 0 ) );

						break;
					}

				}
	
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

				Add( typeof( Hatchet ), 13 );
				Add( typeof( WarFork ), 16 );
			} 
		} 
	} 
}
