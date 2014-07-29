using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using System.Collections.Generic;

namespace Server
{
    public class LootPack
    {
        public enum Intensity
        {
            Normal = 0,
            Level1Min = 1,
            Level1Max = 50,
            Level2Min = 51,
            Level2Max = 75,
            Level3Min = 76,
            Level3Max = 88,
            Level4Min = 89,
            Level4Max = 96,
            Level5Min = 97,
            Level5Max = 100
        }

        public static int GetLuckChance( Mobile killer, Mobile victim )
        {
            if ( !Core.AOS )
                return 0;

            int luck = killer.Luck;

            PlayerMobile pmKiller = killer as PlayerMobile;
            if ( pmKiller != null && pmKiller.SentHonorContext != null && pmKiller.SentHonorContext.Target == victim )
                luck += pmKiller.SentHonorContext.PerfectionLuckBonus;

            if ( luck < 0 )
                return 0;

            if ( !Core.SE && luck > 1200 )
                luck = 1200;

            return (int)( Math.Pow( luck, 1 / 1.8 ) * 100 );
        }

        public static int GetLuckChanceForKiller( Mobile dead )
        {
            List<DamageStore> list = BaseCreature.GetLootingRights( dead.DamageEntries, dead.HitsMax );

            DamageStore highest = null;

            for ( int i = 0; i < list.Count; ++i )
            {
                DamageStore ds = list[i];

                if ( ds.m_HasRight && ( highest == null || ds.m_Damage > highest.m_Damage ) )
                    highest = ds;
            }

            if ( highest == null )
                return 0;

            return GetLuckChance( highest.m_Mobile, dead );
        }

        public static bool CheckLuck( int chance )
        {
            return ( chance > Utility.Random( 10000 ) );
        }

        private LootPackEntry[] m_Entries;

        public LootPack( LootPackEntry[] entries )
        {
            m_Entries = entries;
        }

        public void Generate( Mobile from, Container cont, bool spawning, int luckChance )
        {
            if ( cont == null )
                return;

            bool checkLuck = Core.AOS;

            for ( int i = 0; i < m_Entries.Length; ++i )
            {
                LootPackEntry entry = m_Entries[i];

                bool shouldAdd = ( entry.Chance > Utility.Random( 10000 ) );

                if ( !shouldAdd && checkLuck )
                {
                    checkLuck = false;

                    if ( LootPack.CheckLuck( luckChance ) )
                        shouldAdd = ( entry.Chance > Utility.Random( 10000 ) );
                }

                if ( !shouldAdd )
                    continue;

                Item item = entry.Construct( from, luckChance, spawning );

                if ( item != null )
                {
                    if ( !item.Stackable || !cont.TryDropItem( from, item, false ) )
                    {
                        cont.DropItem( item );
                    }
                }
            }
        }

        public static readonly LootPackItem[] Gold = new LootPackItem[]
			{
				new LootPackItem( typeof( Gold ), 1 )
			};

        public static readonly LootPackItem[] Platinum = new LootPackItem[]
			{
				new LootPackItem( typeof( Platinum), 1 )
			};

        public static readonly LootPackItem[] Instruments = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseInstrument ), 1 )
			};


        public static readonly LootPackItem[] LowScrollItems = new LootPackItem[]
			{
				new LootPackItem( typeof( ClumsyScroll ), 1 )
			};

        public static readonly LootPackItem[] MedScrollItems = new LootPackItem[]
			{
				new LootPackItem( typeof( ArchCureScroll ), 1 )
			};

        public static readonly LootPackItem[] HighScrollItems = new LootPackItem[]
			{
				new LootPackItem( typeof( SummonAirElementalScroll ), 1 )
			};

        public static readonly LootPackItem[] GemItems = new LootPackItem[]
			{
				new LootPackItem( typeof( Amber ), 1 )
			};

        public static readonly LootPackItem[] PotionItems = new LootPackItem[]
			{
				new LootPackItem( typeof( AgilityPotion ), 1 ),
				new LootPackItem( typeof( StrengthPotion ), 1 ),
				new LootPackItem( typeof( RefreshPotion ), 1 ),
				new LootPackItem( typeof( LesserCurePotion ), 1 ),
				new LootPackItem( typeof( LesserHealPotion ), 1 ),
				new LootPackItem( typeof( LesserPoisonPotion ), 1 )
			};

        public static readonly LootPackItem[] Food = new LootPackItem[]
			{
				new LootPackItem( typeof(BreadLoaf), 1 ),
                new LootPackItem( typeof(CheeseWheel), 1)

			};
        public static readonly LootPackItem[] LightSource = new LootPackItem[]
			{
				new LootPackItem( typeof( Torch ), 1 ),
                new LootPackItem( typeof( Candle ), 1 )
			};
        private static readonly LootPackItem[] Missle = new LootPackItem[]
			{
				new LootPackItem( typeof( Arrow ), 1 ),
				new LootPackItem( typeof( Bolt ), 1 ),
			};

        public static readonly LootPackItem[] Beverage = new LootPackItem[]
			{
				new LootPackItem( typeof(BaseBeverage), 1 )
			};
        public static readonly LootPackItem[] PotionReagent = new LootPackItem[]
			{
				new LootPackItem( typeof(BasePotion), 1 ),
                new LootPackItem( typeof(BaseReagent), 1 )
			};

        public static readonly LootPackItem[] Clothing = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseClothing ), 1 )
			};

        public static readonly LootPackItem[] OldItems = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseArmor ), 3 ),
				new LootPackItem( typeof( BaseWeapon ), 3 ),
				new LootPackItem( typeof( BaseShield ), 3 ),
                new LootPackItem( typeof( BaseJewel ), 2 )
			};

        #region Old Magic Items
        public static readonly LootPackItem[] OldMagicItems = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseClothing ), 1 ),
				new LootPackItem( typeof( BaseArmor ), 3 ),
				new LootPackItem( typeof( BaseWeapon ), 3 ),
				new LootPackItem( typeof( BaseShield ), 1 ),
                new LootPackItem( typeof( BaseWand ), 2),
                new LootPackItem( typeof( BaseJewel ), 2 )
			};

        public static readonly LootPackItem[] OldMagicJewel = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseJewel ), 1 )
			};

        public static readonly LootPackItem[] OldMagicWand = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWand ), 1 )
			};

        #endregion

        #region AOS Magic Items
        public static readonly LootPackItem[] AosMagicItemsPoor = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWeapon ), 3 ),
				new LootPackItem( typeof( BaseRanged ), 1 ),
				new LootPackItem( typeof( BaseArmor ), 4 ),
				new LootPackItem( typeof( BaseShield ), 1 ),
				new LootPackItem( typeof( BaseJewel ), 2 )
			};

        public static readonly LootPackItem[] AosMagicItemsMeagerType1 = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWeapon ), 56 ),
				new LootPackItem( typeof( BaseRanged ), 14 ),
				new LootPackItem( typeof( BaseArmor ), 81 ),
				new LootPackItem( typeof( BaseShield ), 11 ),
				new LootPackItem( typeof( BaseJewel ), 42 )
			};

        public static readonly LootPackItem[] AosMagicItemsMeagerType2 = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWeapon ), 28 ),
				new LootPackItem( typeof( BaseRanged ), 7 ),
				new LootPackItem( typeof( BaseArmor ), 40 ),
				new LootPackItem( typeof( BaseShield ), 5 ),
				new LootPackItem( typeof( BaseJewel ), 21 )
			};

        public static readonly LootPackItem[] AosMagicItemsAverageType1 = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWeapon ), 90 ),
				new LootPackItem( typeof( BaseRanged ), 23 ),
				new LootPackItem( typeof( BaseArmor ), 130 ),
				new LootPackItem( typeof( BaseShield ), 17 ),
				new LootPackItem( typeof( BaseJewel ), 68 )
			};

        public static readonly LootPackItem[] AosMagicItemsAverageType2 = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWeapon ), 54 ),
				new LootPackItem( typeof( BaseRanged ), 13 ),
				new LootPackItem( typeof( BaseArmor ), 77 ),
				new LootPackItem( typeof( BaseShield ), 10 ),
				new LootPackItem( typeof( BaseJewel ), 40 )
			};

        public static readonly LootPackItem[] AosMagicItemsRichType1 = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWeapon ), 211 ),
				new LootPackItem( typeof( BaseRanged ), 53 ),
				new LootPackItem( typeof( BaseArmor ), 303 ),
				new LootPackItem( typeof( BaseShield ), 39 ),
				new LootPackItem( typeof( BaseJewel ), 158 )
			};

        public static readonly LootPackItem[] AosMagicItemsRichType2 = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWeapon ), 170 ),
				new LootPackItem( typeof( BaseRanged ), 43 ),
				new LootPackItem( typeof( BaseArmor ), 245 ),
				new LootPackItem( typeof( BaseShield ), 32 ),
				new LootPackItem( typeof( BaseJewel ), 128 )
			};

        public static readonly LootPackItem[] AosMagicItemsFilthyRichType1 = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWeapon ), 219 ),
				new LootPackItem( typeof( BaseRanged ), 55 ),
				new LootPackItem( typeof( BaseArmor ), 315 ),
				new LootPackItem( typeof( BaseShield ), 41 ),
				new LootPackItem( typeof( BaseJewel ), 164 )
			};

        public static readonly LootPackItem[] AosMagicItemsFilthyRichType2 = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWeapon ), 239 ),
				new LootPackItem( typeof( BaseRanged ), 60 ),
				new LootPackItem( typeof( BaseArmor ), 343 ),
				new LootPackItem( typeof( BaseShield ), 90 ),
				new LootPackItem( typeof( BaseJewel ), 45 )
			};

        public static readonly LootPackItem[] AosMagicItemsUltraRich = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWeapon ), 276 ),
				new LootPackItem( typeof( BaseRanged ), 69 ),
				new LootPackItem( typeof( BaseArmor ), 397 ),
				new LootPackItem( typeof( BaseShield ), 52 ),
				new LootPackItem( typeof( BaseJewel ), 207 )
			};
        #endregion

        #region SE definitions
        public static readonly LootPack SePoor = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,						100.00, "2d10+20" ),
				new LootPackEntry( false, AosMagicItemsPoor,		  1.00, 1, 5, 0, 100 ),
				new LootPackEntry( false, Instruments,				  0.02, 1 )
			} );

        public static readonly LootPack SeMeager = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,						100.00, "4d10+40" ),
				new LootPackEntry( false, AosMagicItemsMeagerType1,	 20.40, 1, 2, 0, 50 ),
				new LootPackEntry( false, AosMagicItemsMeagerType2,	 10.20, 1, 5, 0, 100 ),
				new LootPackEntry( false, Instruments,				  0.10, 1 )
			} );

        public static readonly LootPack SeAverage = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,						100.00, "8d10+100" ),
				new LootPackEntry( false, AosMagicItemsAverageType1, 32.80, 1, 3, 0, 50 ),
				new LootPackEntry( false, AosMagicItemsAverageType1, 32.80, 1, 4, 0, 75 ),
				new LootPackEntry( false, AosMagicItemsAverageType2, 19.50, 1, 5, 0, 100 ),
				new LootPackEntry( false, Instruments,				  0.40, 1 )
			} );

        public static readonly LootPack SeRich = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,						100.00, "15d10+225" ),
				new LootPackEntry( false, AosMagicItemsRichType1,	 76.30, 1, 4, 0, 75 ),
				new LootPackEntry( false, AosMagicItemsRichType1,	 76.30, 1, 4, 0, 75 ),
				new LootPackEntry( false, AosMagicItemsRichType2,	 61.70, 1, 5, 0, 100 ),
				new LootPackEntry( false, Instruments,				  1.00, 1 )
			} );

        public static readonly LootPack SeFilthyRich = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,						   100.00, "3d100+400" ),
				new LootPackEntry( false, AosMagicItemsFilthyRichType1,	79.50, 1, 5, 0, 100 ),
				new LootPackEntry( false, AosMagicItemsFilthyRichType1,	79.50, 1, 5, 0, 100 ),
				new LootPackEntry( false, AosMagicItemsFilthyRichType2,	77.60, 1, 5, 25, 100 ),
				new LootPackEntry( false, Instruments,					 2.00, 1 )
			} );

        public static readonly LootPack SeUltraRich = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,						100.00, "6d100+600" ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 33, 100 ),
				new LootPackEntry( false, Instruments,				  2.00, 1 )
			} );

        public static readonly LootPack SeSuperBoss = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,						100.00, "10d100+800" ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 33, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 33, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 33, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 33, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 50, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 50, 100 ),
				new LootPackEntry( false, Instruments,				  2.00, 1 )
			} );
        #endregion

        #region AOS definitions
        public static readonly LootPack AosPoor = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,			100.00, "1d1+14" ),
				new LootPackEntry( false, AosMagicItemsPoor,	  0.02, 1, 5, 0, 90 ),
				new LootPackEntry( false, Instruments,	  0.02, 1 )
			} );

        public static readonly LootPack AosMeager = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,			100.00, "3d10+20" ),
				new LootPackEntry( false, AosMagicItemsMeagerType1,	  1.00, 1, 2, 0, 10 ),
				new LootPackEntry( false, AosMagicItemsMeagerType2,	  0.20, 1, 5, 0, 90 ),
				new LootPackEntry( false, Instruments,	  0.10, 1 )
			} );

        public static readonly LootPack AosAverage = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,			100.00, "5d10+50" ),
				new LootPackEntry( false, AosMagicItemsAverageType1,  5.00, 1, 4, 0, 20 ),
				new LootPackEntry( false, AosMagicItemsAverageType1,  2.00, 1, 3, 0, 50 ),
				new LootPackEntry( false, AosMagicItemsAverageType2,  0.50, 1, 5, 0, 90 ),
				new LootPackEntry( false, Instruments,	  0.40, 1 )
			} );

        public static readonly LootPack AosRich = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,			100.00, "10d10+150" ),
				new LootPackEntry( false, AosMagicItemsRichType1,	 20.00, 1, 4, 0, 40 ),
				new LootPackEntry( false, AosMagicItemsRichType1,	 10.00, 1, 5, 0, 60 ),
				new LootPackEntry( false, AosMagicItemsRichType2,	  1.00, 1, 5, 0, 90 ),
				new LootPackEntry( false, Instruments,	  1.00, 1 )
			} );

        public static readonly LootPack AosFilthyRich = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,			100.00, "2d100+200" ),
				new LootPackEntry( false, AosMagicItemsFilthyRichType1,	 33.00, 1, 4, 0, 50 ),
				new LootPackEntry( false, AosMagicItemsFilthyRichType1,	 33.00, 1, 4, 0, 60 ),
				new LootPackEntry( false, AosMagicItemsFilthyRichType2,	 20.00, 1, 5, 0, 75 ),
				new LootPackEntry( false, AosMagicItemsFilthyRichType2,	  5.00, 1, 5, 0, 100 ),
				new LootPackEntry( false, Instruments,	  2.00, 1 )
			} );

        public static readonly LootPack AosUltraRich = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,			100.00, "5d100+500" ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 35, 100 ),
				new LootPackEntry( false, Instruments,	  2.00, 1 )
			} );

        public static readonly LootPack AosSuperBoss = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,			100.00, "5d100+500" ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 33, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 33, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 33, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 33, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 50, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 50, 100 ),
				new LootPackEntry( false, Instruments,	  2.00, 1 )
			} );
        public static readonly LootPack AosSpecial = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,			100.00, "5d100+500" ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 25, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 33, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 33, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 33, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 33, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 50, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	100.00, 1, 5, 50, 100 ),
				new LootPackEntry( false, Instruments,	  2.00, 1 )
			} );
        #endregion

        #region Gold Piles
        public static readonly LootPack PoorPile = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry(  true, Gold,			100.00, "1d25+25" )
			} );

        public static readonly LootPack MeagerPile = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry(  true, Gold,			100.00, "1d100+75" )
			} );

        public static readonly LootPack AveragePile = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry(  true, Gold,			100.00, "1d150+150" ),
                new LootPackEntry(  true, Platinum,			0.25, 1 )
			} );

        public static readonly LootPack RichPile = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry(  true, Gold,			100.00, "1d50+250" ),
                new LootPackEntry(  true, Platinum,			0.50, 1 )
			} );

        public static readonly LootPack FilthyRichPile = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry(  true, Gold,			100.00, "1d100+400" ),
                new LootPackEntry(  true, Platinum,			1.50, 1 )

			} );

        public static readonly LootPack SpecialPile = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry(  true, Gold,			100.00, "1d400+1600" ),
                new LootPackEntry(  true, Platinum,			1.50, 1 )
			} );

        public static readonly LootPack UltraRichPile = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry(  true, Gold,			100.00, "1d500+500" ),
                new LootPackEntry(  true, Platinum,			1.50, 1 )
			} );
        public static readonly LootPack SuperBossPile = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry(  true, Gold,			100.00, "1d200+1100" ),
                new LootPackEntry(  true, Platinum,			1.50, 1 )
			} );
        #endregion

        #region Pouches
        public static readonly LootPack PoorPouch = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry(  true, Gold,			100.00, "1d25+25" )
			} );

        public static readonly LootPack MeagerPouch = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry(  true, Gold,			100.00, "1d100+75" )
			} );

        public static readonly LootPack AveragePouch = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry(  true, Gold,			100.00, "1d125+175" )
			} );

        public static readonly LootPack RichPouch = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry(  true, Gold,			100.00, "1d100+300" ),
                new LootPackEntry(  true, GemItems,			100.00, 1 )
			} );

        public static readonly LootPack FilthyRichPouch = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry(  true, Gold,			100.00, "1d100+400" ),
                new LootPackEntry(  true, GemItems,			100.00, 1 ),
                new LootPackEntry(  true, GemItems,			100.00, 1 )
			} );

        public static readonly LootPack SpecialPouch = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry(  true, Gold,			100.00, "1d200+1200" )
			} );

        public static readonly LootPack UltraRichPouch = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry(  true, Gold,			100.00, "1d500+500" )
			} );
        public static readonly LootPack SuperBossPouch = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry(  true, Gold,			100.00, "1d200+1100" )
			} );

        #endregion

        #region Pre-AOS definitions
        /*
        5: 97 - 100
        4: 89 - 96
        3: 75 - 88
        2: 50 - 74
        1: 0  - 49
        */
        public static readonly LootPack OldPoor = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry( true,  Beverage,     100.00, 1),
                new LootPackEntry( true,  LightSource,  100.00, 1),
                new LootPackEntry( true,  Food,         100.00, 1),
                new LootPackEntry( true,  Beverage,     33.00, 1),
                new LootPackEntry( true,  LightSource,  33.00, 1),
                new LootPackEntry( true,  Food,         33.00, 1)
			} );

        public static readonly LootPack OldMeager = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry( true, OldItems,       45.00, 1 ),
                new LootPackEntry( true, PotionReagent,	 25.00, Utility.RandomMinMax( 3, 6 ) ),
				new LootPackEntry( true, Missle,         40.00, Utility.RandomMinMax( 5, 15 ) ),
                new LootPackEntry( true,  Clothing,     100.00, 1),
                new LootPackEntry( true,  Beverage,     100.00, 1),
                new LootPackEntry( true,  LightSource,  100.00, 1),
                new LootPackEntry( true,  Food,         100.00, 1)
			} );

        public static readonly LootPack OldAverage = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry( true, OldMagicItems,  15.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level2Max ),
                new LootPackEntry( true, OldMagicItems,  15.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level2Max ),
                new LootPackEntry( true, OldMagicItems,  15.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level2Max ),

                new LootPackEntry( true, OldMagicItems,  30.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level2Max ),
                new LootPackEntry( true, PotionReagent,	 50.00, Utility.RandomMinMax( 3, 6 ) ),
				new LootPackEntry( true, Missle,         50.00, Utility.RandomMinMax( 5, 15 ) ),
                new LootPackEntry( true,  Clothing,     100.00, 1),
                new LootPackEntry( true,  Beverage,     100.00, 1),
                new LootPackEntry( true,  LightSource,  100.00, 1),
                new LootPackEntry( true,  Food,         100.00, 1)
			} );

        public static readonly LootPack OldRich = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry( true, OldMagicItems,  20.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  20.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  20.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level5Max ),

				new LootPackEntry( true, OldMagicItems,  40.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, PotionReagent,	 50.00, Utility.RandomMinMax( 3, 6 ) ),
				new LootPackEntry( true, Missle,         50.00, Utility.RandomMinMax( 5, 15 ) ),
                new LootPackEntry( true,  Beverage,     100.00, 1),
                new LootPackEntry( true,  LightSource,  100.00, 1),
                new LootPackEntry( true,  Food,         100.00, 1)
			} );

        public static readonly LootPack OldFilthyRich = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level5Max ),

                new LootPackEntry( true, OldMagicItems,  55.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, PotionReagent,	 50.00, Utility.RandomMinMax( 3, 6 ) ),
				new LootPackEntry( true, Missle,         50.00, Utility.RandomMinMax( 5, 15 ) ),
                new LootPackEntry( true,  Beverage,     100.00, 1),
                new LootPackEntry( true,  LightSource,  100.00, 1),
                new LootPackEntry( true,  Food,         100.00, 1)
			} );

        //Blood
        public static readonly LootPack OldUltraRich = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),

                new LootPackEntry( true, OldMagicItems,  40.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, PotionReagent,	 50.00, Utility.RandomMinMax( 3, 6 ) ),
				new LootPackEntry( true, Missle,         50.00, Utility.RandomMinMax( 5, 15 ) ),
                new LootPackEntry( true,  Beverage,     100.00, 1),
                new LootPackEntry( true,  LightSource,  100.00, 1),
                new LootPackEntry( true,  Food,         100.00, 1)
			} );

        //Dragon 
        public static readonly LootPack OldSuperBoss = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  45.00, 10, 1, (int)Intensity.Level4Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, PotionReagent,	 50.00, Utility.RandomMinMax( 3, 6 ) ),
				new LootPackEntry( true, Missle,         50.00, Utility.RandomMinMax( 5, 15 ) ),
                new LootPackEntry( true,  Beverage,     100.00, 1),
                new LootPackEntry( true,  LightSource,  100.00, 1),
                new LootPackEntry( true,  Food,         100.00, 1)
			} );

        //AW
        public static readonly LootPack OldSpecial = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  80.00, 10, 1, (int)Intensity.Level4Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, PotionReagent,	 50.00, Utility.RandomMinMax( 3, 6 ) ),
				new LootPackEntry( true, Missle,         50.00, Utility.RandomMinMax( 5, 15 ) ),
                new LootPackEntry( true, Beverage,      100.00, 1),
                new LootPackEntry( true, LightSource,   100.00, 1),
                new LootPackEntry( true, Food,          100.00, 1)
			} );
        #endregion

        #region Pre-AOS Magic definitions
        /*
        5: 97 - 100
        4: 89 - 96
        3: 75 - 88
        2: 50 - 74
        1: 0  - 49
        */

        public static readonly LootPack OldAverageMagicItems = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry( true, OldMagicItems,  15.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level2Max ),
                new LootPackEntry( true, OldMagicItems,  15.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level2Max ),
                new LootPackEntry( true, OldMagicItems,  15.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level2Max ),
                new LootPackEntry( true, OldMagicItems,  30.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level2Max )
			} );

        public static readonly LootPack OldRichMagicItems = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry( true, OldMagicItems,  20.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  20.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  20.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level5Max ),
				new LootPackEntry( true, OldMagicItems,  40.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level5Max )
			} );

        public static readonly LootPack OldFilthyRichMagicItems = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  55.00, 10, 1, (int)Intensity.Level1Min, (int)Intensity.Level5Max )
			} );

        //Blood
        public static readonly LootPack OldUltraRichMagicItems = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  40.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max )
			} );

        //Dragon 
        public static readonly LootPack OldSuperBossMagicItems = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  45.00, 10, 1, (int)Intensity.Level4Min, (int)Intensity.Level5Max )
			} );

        //AW
        public static readonly LootPack OldSpecialMagicItems = new LootPack( new LootPackEntry[]
			{
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  25.00, 10, 1, (int)Intensity.Level3Min, (int)Intensity.Level5Max ),
                new LootPackEntry( true, OldMagicItems,  80.00, 10, 1, (int)Intensity.Level4Min, (int)Intensity.Level5Max )
			} );
        #endregion

        #region Misc
        public static readonly LootPack RichNone = new LootPack( new LootPackEntry[]
			{
				/*new LootPackEntry( true, OldMagicItems, 25.00, 1, 1, 50, 100 ),
                new LootPackEntry( true, OldMagicItems, 15.00, 1, 1, 50, 100 ),
				new LootPackEntry( true, OldMagicItems, 10.00, 1, 1, 75, 100 )*/
			} );

        public static readonly LootPack Undead = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry( true, OldMagicItems, 10.00, 1, 1, 0, 90 )
			} );

        public static readonly LootPack FilthyRichNone = new LootPack( new LootPackEntry[]
			{
				//new LootPackEntry(  true, Gold,			100.00, "1d1+99" ),
				/*new LootPackEntry( true, OldMagicItems, 60.00, 1, 1, 70, 90 ),
				new LootPackEntry( true, OldMagicItems, 60.00, 1, 1, 70, 90 ),
				new LootPackEntry( true, OldMagicItems, 40.00, 1, 1, 70, 90 )*/
			} );

        public static readonly LootPack UltraRichNone = new LootPack( new LootPackEntry[]
			{
				//new LootPackEntry(  true, Gold,			100.00, "1d1+99" ),
				/*new LootPackEntry( true, OldMagicItems, 20.00, 1, 1, 80, 100 ),
				new LootPackEntry( true, OldMagicItems, 20.00, 1, 1, 80, 100 )*/
			} );
        #endregion

        #region Generic accessors
        public static LootPack Poor { get { return Core.AOS ? AosPoor : OldPoor; } }
        public static LootPack Meager { get { return Core.AOS ? AosMeager : OldMeager; } }
        public static LootPack Average { get { return Core.AOS ? AosAverage : OldAverage; } }
        public static LootPack Rich { get { return Core.AOS ? AosRich : OldRich; } }
        public static LootPack FilthyRich { get { return Core.AOS ? AosFilthyRich : OldFilthyRich; } }
        public static LootPack UltraRich { get { return Core.AOS ? AosUltraRich : OldUltraRich; } }
        public static LootPack SuperBoss { get { return Core.AOS ? AosSuperBoss : OldSuperBoss; } }
        public static LootPack Special { get { return Core.AOS ? AosSpecial : OldSpecial; } }
        #endregion
      
        public static readonly LootPack LowScrolls = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry( true, LowScrollItems,	100.00, 1 )
			} );

        public static readonly LootPack MedScrolls = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry( true, MedScrollItems,	100.00, 1 )
			} );

        public static readonly LootPack HighScrolls = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry( true, HighScrollItems,	100.00, 1 )
			} );

        public static readonly LootPack Gems = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry( true, GemItems,			100.00, 1 )
			} );

        public static readonly LootPack Potions = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry( true, PotionItems,		100.00, 1 )
			} );

        public static readonly LootPack Targetables = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry( true, OldMagicWand,		100.00, 1, 1, 100, 200 )
			} );
    }

    public class LootPackEntry
    {
        private int m_Chance;
        private LootPackDice m_Quantity;

        private int m_MaxProps, m_MinIntensity, m_MaxIntensity;

        private bool m_AtSpawnTime;

        private LootPackItem[] m_Items;

        public int Chance
        {
            get { return m_Chance; }
            set { m_Chance = value; }
        }

        public LootPackDice Quantity
        {
            get { return m_Quantity; }
            set { m_Quantity = value; }
        }

        public int MaxProps
        {
            get { return m_MaxProps; }
            set { m_MaxProps = value; }
        }

        public int MinIntensity
        {
            get { return m_MinIntensity; }
            set { m_MinIntensity = value; }
        }

        public int MaxIntensity
        {
            get { return m_MaxIntensity; }
            set { m_MaxIntensity = value; }
        }

        public LootPackItem[] Items
        {
            get { return m_Items; }
            set { m_Items = value; }
        }

        private static bool IsInTokuno( Mobile m )
        {
            if ( m == null )
                return false;

            if ( m.Region.IsPartOf( "Fan Dancer's Dojo" ) )
                return true;

            if ( m.Region.IsPartOf( "Yomotsu Mines" ) )
                return true;

            return ( m.Map == Map.Tokuno );
        }

        public Item Construct( Mobile from, int luckChance, bool spawning )
        {
            if ( m_AtSpawnTime != spawning )
                return null;

            int totalChance = 0;

            for ( int i = 0; i < m_Items.Length; ++i )
                totalChance += m_Items[i].Chance;

            int rnd = Utility.Random( totalChance );

            for ( int i = 0; i < m_Items.Length; ++i )
            {
                LootPackItem item = m_Items[i];

                if ( rnd < item.Chance )
                    return Mutate( from, luckChance, item.Construct( IsInTokuno( from ) ) );

                rnd -= item.Chance;
            }

            return null;
        }

        private int GetRandomOldBonus()
        {
            int rnd = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );

            if ( rnd == 0 )
                return 0;

            if ( 50 > rnd )
                return 1;
            else
                rnd -= 50;

            if ( 25 > rnd )
                return 2;
            else
                rnd -= 25;

            if ( 14 > rnd )
                return 3;
            else
                rnd -= 14;

            if ( 8 > rnd )
                return 4;

            return 5;
        }

        public Item Mutate( Mobile from, int luckChance, Item item )
        {
            if ( item != null )
            {
                bool isMagic = GetRandomOldBonus() > 0;

                if ( item is GnarledStaff && isMagic )
                {
                    GnarledStaff staff = (GnarledStaff)item;
                    int rand = Utility.Random( 300 );

                    #region Random Magic Effect
                    if ( 40 > rand )
                    {
                        staff.Charges = Utility.RandomMinMax( 50, 200 );
                        staff.StaffEffect = WandEffect.Identification;
                    }
                    else if ( 80 > rand )
                    {
                        staff.Charges = Utility.RandomMinMax( 1, 60 );
                        staff.StaffEffect = WandEffect.Healing;
                    }
                    else if ( 100 > rand )
                    {
                        staff.Charges = Utility.RandomMinMax( 1, 10 );
                        staff.StaffEffect = WandEffect.GreaterHealing;
                    }
                    else if ( 133 > rand )
                    {
                        staff.Charges = Utility.RandomMinMax( 1, 60 );
                        staff.StaffEffect = WandEffect.Clumsiness;
                    }
                    else if ( 166 > rand )
                    {
                        staff.Charges = Utility.RandomMinMax( 1, 60 );
                        staff.StaffEffect = WandEffect.Feeblemindedness;
                    }
                    else if ( 200 > rand )
                    {
                        staff.Charges = Utility.RandomMinMax( 1, 60 );
                        staff.StaffEffect = WandEffect.Weakness;
                    }
                    else if ( 230 > rand )
                    {
                        staff.Charges = Utility.RandomMinMax( 1, 25 );
                        staff.StaffEffect = WandEffect.MagicArrow;
                    }
                    else if ( 255 > rand )
                    {
                        staff.Charges = Utility.RandomMinMax( 1, 10 );
                        staff.StaffEffect = WandEffect.Harming;
                    }
                    else if ( 275 > rand )
                    {
                        staff.Charges = Utility.RandomMinMax( 1, 25 );
                        staff.StaffEffect = WandEffect.Fireball;
                    }
                    else if ( 290 > rand )
                    {
                        staff.Charges = Utility.RandomMinMax( 1, 10 );
                        staff.StaffEffect = WandEffect.Lightning;
                    }
                    else
                    {
                        staff.Charges = Utility.RandomMinMax( 1, 200 );
                        staff.StaffEffect = WandEffect.ManaDraining;
                    }
                    #endregion Random Magic Effect
                }
                else if ( item is BaseWeapon )
                {
                    if ( item is BaseWand || item is GnarledStaff )
                        return item;

                    BaseWeapon weapon = (BaseWeapon)item;
                    int rand = 0;

                    while ( isMagic && weapon.AccuracyLevel == 0 && weapon.DamageLevel == 0 && weapon.DurabilityLevel == 0 && weapon.Effect == WeaponEffect.None )
                    {
                        rand = Utility.Random( 100 );

                        if ( 35 > Utility.Random( 100 ) )
                            weapon.AccuracyLevel = (WeaponAccuracyLevel)GetRandomOldBonus();

                        if ( 30 > Utility.Random( 100 ) )
                            weapon.DamageLevel = (WeaponDamageLevel)GetRandomOldBonus();

                        if ( 25 > Utility.Random( 100 ) )
                            weapon.DurabilityLevel = (WeaponDurabilityLevel)GetRandomOldBonus();

                        if ( 10 > Utility.Random( 100 ) )
                        {
                            #region Random Magic Effect
                            if ( 25 > rand )
                            {
                                weapon.Charges = Utility.RandomMinMax( 10, 30 );
                                weapon.Effect = WeaponEffect.Clumsy;
                            }
                            else if ( 35 > rand )
                            {
                                weapon.Charges = Utility.RandomMinMax( 10, 30 );
                                weapon.Effect = WeaponEffect.Feeblemind;
                            }
                            else if ( 45 > rand )
                            {
                                weapon.Charges = Utility.RandomMinMax( 10, 30 );
                                weapon.Effect = WeaponEffect.MagicArrow;
                            }
                            else if ( 55 > rand )
                            {
                                weapon.Charges = Utility.RandomMinMax( 10, 30 );
                                weapon.Effect = WeaponEffect.Weakness;
                            }
                            else if ( 65 > rand )
                            {
                                weapon.Charges = Utility.RandomMinMax( 10, 30 );
                                weapon.Effect = WeaponEffect.Harm;
                            }
                            else if ( 75 > rand )
                            {
                                weapon.Charges = Utility.RandomMinMax( 10, 30 );
                                weapon.Effect = WeaponEffect.Paralyze;
                            }
                            else if ( 80 > rand )
                            {
                                weapon.Charges = Utility.RandomMinMax( 10, 30 );
                                weapon.Effect = WeaponEffect.Fireball;
                            }
                            else if ( 85 > rand )
                            {
                                weapon.Charges = Utility.RandomMinMax( 10, 30 );
                                weapon.Effect = WeaponEffect.Curse;
                            }
                            else if ( 90 > rand )
                            {
                                weapon.Charges = Utility.RandomMinMax( 10, 30 );
                                weapon.Effect = WeaponEffect.ManaDrain;
                            }
                            else
                            {
                                weapon.Charges = Utility.RandomMinMax( 10, 30 );
                                weapon.Effect = WeaponEffect.Lightning;
                            }
                            #endregion
                        }

                        if ( 5 > Utility.Random( 100 ) )
                            weapon.Slayer = SlayerName.Silver;
                    }
                }
                else if ( item is BaseArmor )
                {
                    BaseArmor armor = (BaseArmor)item;

                    while ( isMagic && armor.ProtectionLevel == 0 && armor.Durability == 0 )
                    {
                        if ( 75 > Utility.Random( 100 ) )
                            armor.ProtectionLevel = (ArmorProtectionLevel)GetRandomOldBonus();

                        if ( 25 > Utility.Random( 100 ) )
                            armor.Durability = (ArmorDurabilityLevel)GetRandomOldBonus();
                    }
                }
                else if ( item is BaseShield )
                {
                    BaseShield shield = (BaseShield)item;

                    while ( isMagic && shield.ProtectionLevel == 0 && shield.Durability == 0 )
                    {
                        if ( 75 > Utility.Random( 100 ) )
                            shield.ProtectionLevel = (ArmorProtectionLevel)GetRandomOldBonus();

                        if ( 25 > Utility.Random( 100 ) )
                            shield.Durability = (ArmorDurabilityLevel)GetRandomOldBonus();
                    }
                }
                else if ( item is BaseClothing && isMagic )
                {
                    BaseClothing clothing = (BaseClothing)item;

                    int random = Utility.Random( 100 );
                    #region Random
                    if ( 33 >= random ) //nightsight
                    {
                        clothing.Effect = ClothEffect.NightSight;
                        clothing.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else if ( 46 >= random )
                    {
                        clothing.Effect = ClothEffect.Protection;
                        clothing.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else if ( 54 >= random )
                    {
                        clothing.Effect = ClothEffect.Agility;
                        clothing.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else if ( 62 >= random )
                    {
                        clothing.Effect = ClothEffect.Cunning;
                        clothing.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else if ( 70 >= random )
                    {
                        clothing.Effect = ClothEffect.Strength;
                        clothing.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else if ( 78 >= random )
                    {
                        clothing.Effect = ClothEffect.Invisibility;
                        clothing.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else if ( 83 >= random )
                    {
                        clothing.Effect = ClothEffect.MagicReflection;
                        clothing.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else if ( 87 >= random )
                    {
                        clothing.Effect = ClothEffect.Feeblemind;
                        clothing.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else if ( 91 >= random )
                    {
                        clothing.Effect = ClothEffect.Clumsy;
                        clothing.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else if ( 95 >= random )
                    {
                        clothing.Effect = ClothEffect.Weaken;
                        clothing.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else if ( 98 >= random )
                    {
                        clothing.Effect = ClothEffect.Bless;
                        clothing.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else
                    {
                        clothing.Effect = ClothEffect.Curse;
                        clothing.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    #endregion
                }
                else if ( item is BaseJewel && isMagic )
                {
                    BaseJewel jewel = (BaseJewel)item;

                    int random = Utility.Random( 100 );
                    #region Random
                    if ( 33 >= random ) //nightsight
                    {
                        jewel.Effect = JewelEffect.NightSight;
                        jewel.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else if ( 46 >= random )
                    {
                        jewel.Effect = JewelEffect.Protection;
                        jewel.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else if ( 54 >= random )
                    {
                        jewel.Effect = JewelEffect.Agility;
                        jewel.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else if ( 62 >= random )
                    {
                        jewel.Effect = JewelEffect.Cunning;
                        jewel.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else if ( 70 >= random )
                    {
                        jewel.Effect = JewelEffect.Strength;
                        jewel.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else if ( 78 >= random )
                    {
                        if ( Utility.RandomBool() && jewel is BaseRing )
                        {
                            jewel.Effect = JewelEffect.Teleportation;
                            jewel.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                        }
                        else
                        {
                            jewel.Effect = JewelEffect.Invisibility;
                            jewel.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                        }
                    }
                    else if ( 83 >= random )
                    {
                        jewel.Effect = JewelEffect.MagicReflection;
                        jewel.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else if ( 87 >= random )
                    {
                        jewel.Effect = JewelEffect.Feeblemind;
                        jewel.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else if ( 91 >= random )
                    {
                        jewel.Effect = JewelEffect.Clumsy;
                        jewel.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else if ( 95 >= random )
                    {
                        jewel.Effect = JewelEffect.Weaken;
                        jewel.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else if ( 98 >= random )
                    {
                        jewel.Effect = JewelEffect.Bless;
                        jewel.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    else
                    {
                        jewel.Effect = JewelEffect.Curse;
                        jewel.Charges = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );
                    }
                    #endregion
                }
            }

            if ( item.Stackable )
                item.Amount = m_Quantity.Roll();

            return item;
        }

        public LootPackEntry( bool atSpawnTime, LootPackItem[] items, double chance, string quantity )
            : this( atSpawnTime, items, chance, new LootPackDice( quantity ), 0, 0, 0 )
        {
        }

        public LootPackEntry( bool atSpawnTime, LootPackItem[] items, double chance, int quantity )
            : this( atSpawnTime, items, chance, new LootPackDice( 0, 0, quantity ), 0, 0, 0 )
        {
        }

        public LootPackEntry( bool atSpawnTime, LootPackItem[] items, double chance, string quantity, int maxProps, int minIntensity, int maxIntensity )
            : this( atSpawnTime, items, chance, new LootPackDice( quantity ), maxProps, minIntensity, maxIntensity )
        {
        }

        public LootPackEntry( bool atSpawnTime, LootPackItem[] items, double chance, int quantity, int maxProps, int minIntensity, int maxIntensity )
            : this( atSpawnTime, items, chance, new LootPackDice( 0, 0, quantity ), maxProps, minIntensity, maxIntensity )
        {
        }

        public LootPackEntry( bool atSpawnTime, LootPackItem[] items, double chance, LootPackDice quantity, int maxProps, int minIntensity, int maxIntensity )
        {
            m_AtSpawnTime = atSpawnTime;
            m_Items = items;
            m_Chance = (int)( 100 * chance );
            m_Quantity = quantity;
            m_MaxProps = maxProps;
            m_MinIntensity = minIntensity;
            m_MaxIntensity = maxIntensity;
        }

        public int GetBonusProperties()
        {
            int p0 = 0, p1 = 0, p2 = 0, p3 = 0, p4 = 0, p5 = 0;

            switch ( m_MaxProps )
            {
                case 1: p0 = 3; p1 = 1; break;
                case 2: p0 = 6; p1 = 3; p2 = 1; break;
                case 3: p0 = 10; p1 = 6; p2 = 3; p3 = 1; break;
                case 4: p0 = 16; p1 = 12; p2 = 6; p3 = 5; p4 = 1; break;
                case 5: p0 = 30; p1 = 25; p2 = 20; p3 = 15; p4 = 9; p5 = 1; break;
            }

            int pc = p0 + p1 + p2 + p3 + p4 + p5;

            int rnd = Utility.Random( pc );

            if ( rnd < p5 )
                return 5;
            else
                rnd -= p5;

            if ( rnd < p4 )
                return 4;
            else
                rnd -= p4;

            if ( rnd < p3 )
                return 3;
            else
                rnd -= p3;

            if ( rnd < p2 )
                return 2;
            else
                rnd -= p2;

            if ( rnd < p1 )
                return 1;

            return 0;
        }
    }

    public class LootPackItem
    {
        private Type m_Type;
        private int m_Chance;

        public Type Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        public int Chance
        {
            get { return m_Chance; }
            set { m_Chance = value; }
        }

        private static Type[] m_BlankTypes = new Type[] { typeof( BlankScroll ) };
        private static Type[][] m_NecroTypes = new Type[][]
			{
				new Type[] // low
				{
					typeof( AnimateDeadScroll ),		typeof( BloodOathScroll ),		typeof( CorpseSkinScroll ),	typeof( CurseWeaponScroll ),
					typeof( EvilOmenScroll ),			typeof( HorrificBeastScroll ),	typeof( MindRotScroll ),	typeof( PainSpikeScroll ),
					typeof( SummonFamiliarScroll ),		typeof( WraithFormScroll )
				},
				new Type[] // med
				{
					typeof( LichFormScroll ),			typeof( PoisonStrikeScroll ),	typeof( StrangleScroll ),	typeof( WitherScroll )
				},

				((Core.SE) ?
				new Type[] // high
				{
					typeof( VengefulSpiritScroll ),		typeof( VampiricEmbraceScroll ), typeof( ExorcismScroll )
				} : 
				new Type[] // high
				{
					typeof( VengefulSpiritScroll ),		typeof( VampiricEmbraceScroll )
				})
			};

        public static Item RandomScroll( int index, int minCircle, int maxCircle )
        {
            --minCircle;
            --maxCircle;

            int scrollCount = ( ( maxCircle - minCircle ) + 1 ) * 8;

            if ( index == 0 )
                scrollCount += m_BlankTypes.Length;

            if ( Core.AOS )
                scrollCount += m_NecroTypes[index].Length;

            int rnd = Utility.Random( scrollCount );

            if ( index == 0 && rnd < m_BlankTypes.Length )
                return Loot.Construct( m_BlankTypes );
            else if ( index == 0 )
                rnd -= m_BlankTypes.Length;

            if ( Core.AOS && rnd < m_NecroTypes.Length )
                return Loot.Construct( m_NecroTypes[index] );
            else if ( Core.AOS )
                rnd -= m_NecroTypes[index].Length;

            return Loot.RandomScroll( minCircle * 8, ( maxCircle * 8 ) + 7, SpellbookType.Regular );
        }

        public Item Construct( bool inTokuno )
        {
            try
            {
                Item item;

                if ( m_Type == typeof( BaseRanged ) )
                    item = Loot.RandomRangedWeapon( inTokuno );
                else if ( m_Type == typeof( BaseWeapon ) )
                    item = Loot.RandomWeapon( inTokuno );
                else if ( m_Type == typeof( BaseArmor ) )
                    item = Loot.RandomArmor( inTokuno );
                else if ( m_Type == typeof( BaseShield ) )
                    item = Loot.RandomShield();
                else if ( m_Type == typeof( BaseJewel ) )
                    item = Loot.RandomJewelry();
                else if ( m_Type == typeof( BaseWand ) )
                    item = Loot.RandomWand();
                else if ( m_Type == typeof( BaseClothing ) )
                    item = Loot.RandomClothing();
                else if ( m_Type == typeof( BaseInstrument ) )
                    item = Loot.RandomInstrument();
                else if ( m_Type == typeof( Amber ) ) // gem
                    item = Loot.RandomGem();
                else if ( m_Type == typeof( ClumsyScroll ) ) // low scroll
                    item = RandomScroll( 0, 1, 3 );
                else if ( m_Type == typeof( ArchCureScroll ) ) // med scroll
                    item = RandomScroll( 1, 4, 7 );
                else if ( m_Type == typeof( SummonAirElementalScroll ) ) // high scroll
                    item = RandomScroll( 2, 8, 8 );
                else if ( m_Type == typeof( BaseBeverage ) )
                    item = Loot.RandomBeverage();
                else if ( m_Type == typeof( BasePotion ) )
                    item = Loot.RandomPotion();
                else if ( m_Type == typeof( BaseReagent ) )
                    item = Loot.RandomPossibleReagent();
                else
                    item = Activator.CreateInstance( m_Type ) as Item;

                return item;
            }
            catch
            {
            }

            return null;
        }

        public LootPackItem( Type type, int chance )
        {
            m_Type = type;
            m_Chance = chance;
        }
    }

    public class LootPackDice
    {
        private int m_Count, m_Sides, m_Bonus;

        public int Count
        {
            get { return m_Count; }
            set { m_Count = value; }
        }

        public int Sides
        {
            get { return m_Sides; }
            set { m_Sides = value; }
        }

        public int Bonus
        {
            get { return m_Bonus; }
            set { m_Bonus = value; }
        }

        public int Roll()
        {
            int v = m_Bonus;

            for ( int i = 0; i < m_Count; ++i )
                v += Utility.Random( 1, m_Sides );

            return v;
        }

        public LootPackDice( string str )
        {
            int start = 0;
            int index = str.IndexOf( 'd', start );

            if ( index < start )
                return;

            m_Count = Utility.ToInt32( str.Substring( start, index - start ) );

            bool negative;

            start = index + 1;
            index = str.IndexOf( '+', start );

            if ( negative = ( index < start ) )
                index = str.IndexOf( '-', start );

            if ( index < start )
                index = str.Length;

            m_Sides = Utility.ToInt32( str.Substring( start, index - start ) );

            if ( index == str.Length )
                return;

            start = index + 1;
            index = str.Length;

            m_Bonus = Utility.ToInt32( str.Substring( start, index - start ) );

            if ( negative )
                m_Bonus *= -1;
        }

        public LootPackDice( int count, int sides, int bonus )
        {
            m_Count = count;
            m_Sides = sides;
            m_Bonus = bonus;
        }
    }
}