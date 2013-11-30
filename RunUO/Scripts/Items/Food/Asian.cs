using System;

namespace Server.Items
{
	public class Wasabi : Item
	{
		 
		public Wasabi() : base( 0x24E8 )
		{
			Weight = 1.0;
		}

		public Wasabi( Serial serial ) : base( serial )
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

	public class WasabiClumps : Food
	{
		 
		public WasabiClumps() : base( 0x24EB )
		{
			Stackable = false;
			Weight = 1.0;
			FillFactor = 2;
		}

		public WasabiClumps( Serial serial ) : base( serial )
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

	public class EmptyBentoBox : Item
	{
		 
		public EmptyBentoBox() : base( 0x2834 )
		{
			Weight = 5.0;
		}

		public EmptyBentoBox( Serial serial ) : base( serial )
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

	public class BentoBox : Food
	{
		 
		public BentoBox() : base( 0x2836 )
		{
			Stackable = false;
			Weight = 5.0;
			FillFactor = 2;
		}

		public override bool Eat( Mobile from )
		{
			if ( !base.Eat( from ) )
				return false;

			from.AddToBackpack( new EmptyBentoBox() );
			return true;
		}

		public BentoBox( Serial serial ) : base( serial )
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

	public class SushiRolls : Food
	{
		 
		public SushiRolls() : base( 0x283E )
		{
			Stackable = false;
			Weight = 3.0;
			FillFactor = 2;
		}

		public SushiRolls( Serial serial ) : base( serial )
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

	public class SushiPlatter : Food
	{
		 
		public SushiPlatter() : base( 0x2840 )
		{
			Stackable = false;
			Weight = 3.0;
			FillFactor = 2;
		}

		public SushiPlatter( Serial serial ) : base( serial )
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

	public class GreenTeaBasket : Item
	{
		 
		public GreenTeaBasket() : base( 0x284B )
		{
			Weight = 10.0;
		}

		public GreenTeaBasket( Serial serial ) : base( serial )
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

	public class GreenTea : Food
	{
		 
		public GreenTea() : base( 0x284C )
		{
			Stackable = false;
			Weight = 4.0;
			FillFactor = 2;
		}

		public GreenTea( Serial serial ) : base( serial )
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

	public class MisoSoup : Food
	{
		 
		public MisoSoup() : base( 0x284D )
		{
			Stackable = false;
			Weight = 4.0;
			FillFactor = 2;
		}

		public MisoSoup( Serial serial ) : base( serial )
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

	public class WhiteMisoSoup : Food
	{
		 
		public WhiteMisoSoup() : base( 0x284E )
		{
			Stackable = false;
			Weight = 4.0;
			FillFactor = 2;
		}

		public WhiteMisoSoup( Serial serial ) : base( serial )
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

	public class RedMisoSoup : Food
	{
		 
		public RedMisoSoup() : base( 0x284F )
		{
			Stackable = false;
			Weight = 4.0;
			FillFactor = 2;
		}

		public RedMisoSoup( Serial serial ) : base( serial )
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

	public class AwaseMisoSoup : Food
	{
		 
		public AwaseMisoSoup() : base( 0x2850 )
		{
			Stackable = false;
			Weight = 4.0;
			FillFactor = 2;
		}

		public AwaseMisoSoup( Serial serial ) : base( serial )
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