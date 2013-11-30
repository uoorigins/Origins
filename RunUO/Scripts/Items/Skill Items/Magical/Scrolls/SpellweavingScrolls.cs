using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ArcaneCircleScroll : SpellScroll
	{
		 
		public ArcaneCircleScroll()
			: this( 1 )
		{
		}

		 
		public ArcaneCircleScroll( int amount )
			: base( 600, 0x2D51, amount )
		{
		}

		public ArcaneCircleScroll( Serial serial )
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

	public class GiftOfRenewalScroll : SpellScroll
	{
		 
		public GiftOfRenewalScroll()
			: this( 1 )
		{
		}

		 
		public GiftOfRenewalScroll( int amount )
			: base( 601, 0x2D52, amount )
		{
		}

		public GiftOfRenewalScroll( Serial serial )
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

	public class ImmolatingWeaponScroll : SpellScroll
	{
		 
		public ImmolatingWeaponScroll()
			: this( 1 )
		{
		}

		 
		public ImmolatingWeaponScroll( int amount )
			: base( 602, 0x2D53, amount )
		{
		}

		public ImmolatingWeaponScroll( Serial serial )
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

	public class AttuneWeaponScroll : SpellScroll
	{
		 
		public AttuneWeaponScroll()
			: this( 1 )
		{
		}

		 
		public AttuneWeaponScroll( int amount )
			: base( 603, 0x2D54, amount )
		{
		}

		public AttuneWeaponScroll( Serial serial )
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

	public class ThunderstormScroll : SpellScroll
	{
		 
		public ThunderstormScroll()
			: this( 1 )
		{
		}

		 
		public ThunderstormScroll( int amount )
			: base( 604, 0x2D55, amount )
		{
		}

		public ThunderstormScroll( Serial serial )
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

	public class NatureFuryScroll : SpellScroll
	{
		 
		public NatureFuryScroll()
			: this( 1 )
		{
		}

		 
		public NatureFuryScroll( int amount )
			: base( 605, 0x2D56, amount )
		{
		}

		public NatureFuryScroll( Serial serial )
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

	public class SummonFeyScroll : SpellScroll
	{
		 
		public SummonFeyScroll()
			: this( 1 )
		{
		}

		 
		public SummonFeyScroll( int amount )
			: base( 606, 0x2D57, amount )
		{
		}

		public SummonFeyScroll( Serial serial )
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

	public class SummonFiendScroll : SpellScroll
	{
		 
		public SummonFiendScroll()
			: this( 1 )
		{
		}

		 
		public SummonFiendScroll( int amount )
			: base( 606, 0x2D57, amount )
		{
		}

		public SummonFiendScroll( Serial serial )
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

	public class ReaperFormScroll : SpellScroll
	{
		 
		public ReaperFormScroll()
			: this( 1 )
		{
		}

		 
		public ReaperFormScroll( int amount )
			: base( 608, 0x2D59, amount )
		{
		}

		public ReaperFormScroll( Serial serial )
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

	public class WildfireScroll : SpellScroll
	{
		 
		public WildfireScroll()
			: this( 1 )
		{
		}

		 
		public WildfireScroll( int amount )
			: base( 609, 0x2D5A, amount )
		{
		}

		public WildfireScroll( Serial serial )
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

	public class EssenceOfWindScroll : SpellScroll
	{
		 
		public EssenceOfWindScroll()
			: this( 1 )
		{
		}

		 
		public EssenceOfWindScroll( int amount )
			: base( 610, 0x2D5B, amount )
		{
		}

		public EssenceOfWindScroll( Serial serial )
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

	public class DryadAllureScroll : SpellScroll
	{
		 
		public DryadAllureScroll()
			: this( 1 )
		{
		}

		 
		public DryadAllureScroll( int amount )
			: base( 611, 0x2D5C, amount )
		{
		}

		public DryadAllureScroll( Serial serial )
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

	public class EtherealVoyageScroll : SpellScroll
	{
		 
		public EtherealVoyageScroll()
			: this( 1 )
		{
		}

		 
		public EtherealVoyageScroll( int amount )
			: base( 612, 0x2D5D, amount )
		{
		}

		public EtherealVoyageScroll( Serial serial )
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

	public class WordOfDeathScroll : SpellScroll
	{
		 
		public WordOfDeathScroll()
			: this( 1 )
		{
		}

		 
		public WordOfDeathScroll( int amount )
			: base( 613, 0x2D5E, amount )
		{
		}

		public WordOfDeathScroll( Serial serial )
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

	public class GiftOfLifeScroll : SpellScroll
	{
		 
		public GiftOfLifeScroll()
			: this( 1 )
		{
		}

		 
		public GiftOfLifeScroll( int amount )
			: base( 614, 0x2D5F, amount )
		{
		}

		public GiftOfLifeScroll( Serial serial )
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

	public class ArcaneEmpowermentScroll : SpellScroll
	{
		 
		public ArcaneEmpowermentScroll()
			: this( 1 )
		{
		}

		 
		public ArcaneEmpowermentScroll( int amount )
			: base( 615, 0x2D60, amount )
		{
		}

		public ArcaneEmpowermentScroll( Serial serial )
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