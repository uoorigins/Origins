using System;

namespace Server.Items
{
	public class Blight : Item
	{
		 
		public Blight()
			: this( 1 )
		{
		}

		 
		public Blight( int amount )
			: base( 0x3183 )
		{
			Stackable = true;
			Amount = amount;
		}

		public Blight( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class LuminescentFungi : Item
	{
		 
		public LuminescentFungi()
			: this( 1 )
		{
		}

		 
		public LuminescentFungi( int amount )
			: base( 0x3191 )
		{
			Stackable = true;
			Amount = amount;
		}

		public LuminescentFungi( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}


	public class CapturedEssence : Item
	{
		 
		public CapturedEssence()
			: this( 1 )
		{
		}

		 
		public CapturedEssence( int amount )
			: base( 0x318E )
		{
			Stackable = true;
			Amount = amount;
		}

		public CapturedEssence( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}


	public class EyeOfTheTravesty : Item
	{
		 
		public EyeOfTheTravesty()
			: this( 1 )
		{
		}

		 
		public EyeOfTheTravesty( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public EyeOfTheTravesty( int amount )
			: base( 0x318D )
		{
			Stackable = true;
			Amount = amount;
		}

		public EyeOfTheTravesty( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}


	public class Corruption : Item
	{
		 
		public Corruption()
			: this( 1 )
		{
		}

		 
		public Corruption( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public Corruption( int amount )
			: base( 0x3184 )
		{
			Stackable = true;
			Amount = amount;
		}

		public Corruption( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}


	public class DreadHornMane : Item
	{
		 
		public DreadHornMane()
			: this( 1 )
		{
		}

		 
		public DreadHornMane( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public DreadHornMane( int amount )
			: base( 0x318A )
		{
			Stackable = true;
			Amount = amount;
		}

		public DreadHornMane( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}


	public class ParasiticPlant : Item
	{
		 
		public ParasiticPlant()
			: this( 1 )
		{
		}

		 
		public ParasiticPlant( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public ParasiticPlant( int amount )
			: base( 0x3190 )
		{
			Stackable = true;
			Amount = amount;
		}

		public ParasiticPlant( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}


	public class Muculent : Item
	{
		 
		public Muculent()
			: this( 1 )
		{
		}

		 
		public Muculent( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public Muculent( int amount )
			: base( 0x3188 )
		{
			Stackable = true;
			Amount = amount;
		}

		public Muculent( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}


	public class DiseasedBark : Item
	{
		 
		public DiseasedBark()
			: this( 1 )
		{
		}

		 
		public DiseasedBark( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public DiseasedBark( int amount )
			: base( 0x318B )
		{
			Stackable = true;
			Amount = amount;
		}

		public DiseasedBark( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}


	public class BarkFragment : Item
	{
		 
		public BarkFragment()
			: this( 1 )
		{
		}

		 
		public BarkFragment( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public BarkFragment( int amount )
			: base( 0x318F )
		{
			Stackable = true;
			Amount = amount;
		}

		public BarkFragment( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}


	public class GrizzledBones : Item
	{
		 
		public GrizzledBones()
			: this( 1 )
		{
		}

		 
		public GrizzledBones( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public GrizzledBones( int amount )
			: base( 0x318C )
		{
			Stackable = true;
			Amount = amount;
		}

		public GrizzledBones( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if( version <= 0 && ItemID == 0x318F )
				ItemID = 0x318C;
		}
	}


	public class LardOfParoxysmus : Item
	{
		 
		public LardOfParoxysmus()
			: this( 1 )
		{
		}

		 
		public LardOfParoxysmus( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public LardOfParoxysmus( int amount )
			: base( 0x3189 )
		{
			Stackable = true;
			Amount = amount;
		}

		public LardOfParoxysmus( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class PerfectEmerald : Item
	{
		 
		public PerfectEmerald()
			: this( 1 )
		{
		}

		 
		public PerfectEmerald( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public PerfectEmerald( int amount )
			: base( 0x3194 )
		{
			Stackable = true;
			Amount = amount;
		}

		public PerfectEmerald( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class DarkSapphire : Item
	{
		 
		public DarkSapphire()
			: this( 1 )
		{
		}

		 
		public DarkSapphire( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public DarkSapphire( int amount )
			: base( 0x3192 )
		{
			Stackable = true;
			Amount = amount;
		}

		public DarkSapphire( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}


	public class Turquoise : Item
	{
		 
		public Turquoise()
			: this( 1 )
		{
		}

		 
		public Turquoise( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public Turquoise( int amount )
			: base( 0x3193 )
		{
			Stackable = true;
			Amount = amount;
		}

		public Turquoise( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}


	public class EcruCitrine : Item
	{
		 
		public EcruCitrine()
			: this( 1 )
		{
		}

		 
		public EcruCitrine( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public EcruCitrine( int amount )
			: base( 0x3195 )
		{
			Stackable = true;
			Amount = amount;
		}

		public EcruCitrine( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}


	public class WhitePearl : Item
	{
		 
		public WhitePearl()
			: this( 1 )
		{
		}

		 
		public WhitePearl( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public WhitePearl( int amount )
			: base( 0x3196 )
		{
			Stackable = true;
			Amount = amount;
		}

		public WhitePearl( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}


	public class FireRuby : Item
	{
		 
		public FireRuby()
			: this( 1 )
		{
		}

		 
		public FireRuby( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public FireRuby( int amount )
			: base( 0x3197 )
		{
			Stackable = true;
			Amount = amount;
		}

		public FireRuby( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}


	public class BlueDiamond : Item
	{
		 
		public BlueDiamond()
			: this( 1 )
		{
		}

		 
		public BlueDiamond( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public BlueDiamond( int amount )
			: base( 0x3198 )
		{
			Stackable = true;
			Amount = amount;
		}

		public BlueDiamond( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}


	public class BrilliantAmber : Item
	{
		 
		public BrilliantAmber()
			: this( 1 )
		{
		}

		 
		public BrilliantAmber( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public BrilliantAmber( int amount )
			: base( 0x3199 )
		{
			Stackable = true;
			Amount = amount;
		}

		public BrilliantAmber( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class Scourge : Item
	{
		 
		public Scourge()
			: this( 1 )
		{
		}

		 
		public Scourge( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public Scourge( int amount )
			: base( 0x3185 )
		{
			Stackable = true;
			Amount = amount;
			Hue = 150;
		}

		public Scourge( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}


	public class Putrefication : Item
	{
		 
		public Putrefication()
			: this( 1 )
		{
		}

		 
		public Putrefication( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public Putrefication( int amount )
			: base( 0x3186 )
		{
			Stackable = true;
			Amount = amount;
			Hue = 883;
		}

		public Putrefication( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}


	public class Taint : Item
	{
		 
		public Taint()
			: this( 1 )
		{
		}

		 
		public Taint( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public Taint( int amount )
			: base( 0x3187 )
		{
			Stackable = true;
			Amount = amount;
			Hue = 731;
		}

		public Taint( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	[Flipable( 0x315A, 0x315B )]
	public class PristineDreadHorn : Item
	{
		 
		public PristineDreadHorn()
			: base( 0x315A )
		{
			
		}

		public PristineDreadHorn( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class SwitchItem : Item
	{
		 
		public SwitchItem()
			: this( 1 )
		{
		}

		 
		public SwitchItem( int amountFrom, int amountTo )
			: this( Utility.RandomMinMax( amountFrom, amountTo ) )
		{
		}

		 
		public SwitchItem( int amount )
			: base( 0x2F5F )
		{
			Stackable = true;
			Amount = amount;
		}

		public SwitchItem( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
