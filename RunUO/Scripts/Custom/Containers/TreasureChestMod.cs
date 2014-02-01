// Treasure Chest Pack - Version 0.99H
// By Nerun

using Server;
using Server.Items;
using Server.Multis;
using Server.Network;
using System;

namespace Server.Items
{

// ---------- [Level 1] ----------
// Large, Medium and Small Crate
	[FlipableAttribute( 0xe3e, 0xe3f )] 
	public class TreasureLevel1 : BaseTreasureChestMod 
	{ 
		[Constructable] 
		public TreasureLevel1() : base( Utility.RandomList( 0xE3C, 0xE3E, 0x9a9 ) )
		{ 
			RequiredSkill = 52;
			LockLevel = this.RequiredSkill - Utility.Random( 1, 10 );
			MaxLockLevel = this.RequiredSkill;
			TrapType = TrapType.MagicTrap;
			TrapPower = 1 * Utility.Random( 1, 25 );

            //Base
			DropItem( Loot.RandomBeverage() );
			DropItem( Loot.RandomFood() );
			DropItem( Loot.RandomLightSource() );

            //Broke template
            if (Utility.RandomBool())
                DropItem(Loot.RandomClothing());
            else 
                DropItem(Loot.RandomArmorOrShield());

            if (Utility.RandomBool())
                DropItem(new Arrow(10));
            else
                DropItem(new Bolt(10));

            if (Utility.RandomBool())
                DropItem(Loot.RandomArmor());
            else
                DropItem(Loot.RandomReagent());

            if (Utility.RandomBool())
                DropItem(Loot.RandomPotion());
            else
                DropItem(Loot.RandomWeapon());

            DropItem(new Gold(100, 175));
		}

		public TreasureLevel1( Serial serial ) : base( serial ) 
		{ 
		}

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt();
		} 
	}

// ---------- [Level 1h] ----------
// Large, Medium and Small Crate
	[FlipableAttribute( 0xe3e, 0xe3f )] 
	public class TreasureLevel1h : BaseTreasureChestMod 
	{ 
		[Constructable] 
		public TreasureLevel1h() : base( Utility.RandomList( 0xE3C, 0xE3E, 0x9a9 ) ) 
		{ 
			RequiredSkill = 56;
			LockLevel = this.RequiredSkill - Utility.Random( 1, 10 );
			MaxLockLevel = this.RequiredSkill;
			TrapType = TrapType.MagicTrap;
			TrapPower = 1 * Utility.Random( 1, 25 );

            //Base
            DropItem(Loot.RandomBeverage());
            DropItem(Loot.RandomFood());
            DropItem(Loot.RandomLightSource());

            //Broke template
            if (Utility.RandomBool())
                DropItem(Loot.RandomClothing());
            else
                DropItem(Loot.RandomArmorOrShield());

            if (Utility.RandomBool())
                DropItem(new Arrow(10));
            else
                DropItem(new Bolt(10));

            if (Utility.RandomBool())
                DropItem(Loot.RandomArmor());
            else
                DropItem(Loot.RandomReagent());

            if (Utility.RandomBool())
                DropItem(Loot.RandomPotion());
            else
                DropItem(Loot.RandomWeapon());

            DropItem(new Gold(100, 175));

		} 

		public TreasureLevel1h( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 
		} 
	}

// ---------- [Level 2] ----------
// Large, Medium and Small Crate
// Wooden, Metal and Metal Golden Chest
// Keg and Barrel
	[FlipableAttribute( 0xe43, 0xe42 )] 
	public class TreasureLevel2 : BaseTreasureChestMod 
	{ 
		[Constructable] 
		public TreasureLevel2() : base( Utility.RandomList( 0xe3c, 0xE3E, 0x9a9, 0xe42, 0x9ab, 0xe40 ) ) 
		{ 
			RequiredSkill = 72;
			LockLevel = this.RequiredSkill - Utility.Random( 1, 10 );
			MaxLockLevel = this.RequiredSkill;
			TrapType = TrapType.MagicTrap;
			TrapPower = 2 * Utility.Random( 1, 25 );

            LootPack.OldAverage.Generate(null, this, true, 100);
            LootPack.AveragePile.Generate(null, this, true, 100);
		} 

		public TreasureLevel2( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 
		} 
	} 

// ---------- [Level 3] ----------
// Wooden, Metal and Metal Golden Chest
	[FlipableAttribute( 0x9ab, 0xe7c )] 
	public class TreasureLevel3 : BaseTreasureChestMod 
	{ 
		public override int DefaultGumpID{ get{ return 0x4A; } }

		[Constructable] 
		public TreasureLevel3() : base( Utility.RandomList( 0x9ab, 0xe40, 0xe42 ) ) 
		{ 
			RequiredSkill = 84;
			LockLevel = this.RequiredSkill - Utility.Random( 1, 10 );
			MaxLockLevel = this.RequiredSkill;
			TrapType = TrapType.MagicTrap;
			TrapPower = 3 * Utility.Random( 1, 25 );

            LootPack.OldFilthyRich.Generate( null, this, true, 100 );
            LootPack.UltraRichPile.Generate(null, this, true, 100);
		} 

		public TreasureLevel3( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 
		} 
	} 

// ---------- [Level 4] ----------
// Wooden, Metal and Metal Golden Chest
	[FlipableAttribute( 0xe41, 0xe40 )] 
	public class TreasureLevel4 : BaseTreasureChestMod 
	{ 
		[Constructable] 
		public TreasureLevel4() : base( Utility.RandomList( 0xe40, 0xe42, 0x9ab ) )
		{ 
			RequiredSkill = 92;
			LockLevel = this.RequiredSkill - Utility.Random( 1, 10 );
			MaxLockLevel = this.RequiredSkill;
			TrapType = TrapType.MagicTrap;
			TrapPower = 4 * Utility.Random( 1, 25 );

            LootPack.OldSuperBoss.Generate( null, this, true, 100 );
            LootPack.SpecialPile.Generate(null, this, true, 100);
		} 

		public TreasureLevel4( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 
		} 
	} 

}