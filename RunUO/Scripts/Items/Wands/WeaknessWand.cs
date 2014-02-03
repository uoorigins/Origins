using System;
using Server;
using Server.Spells.First;
using Server.Targeting;
using Server.Network;

namespace Server.Items
{
	public class WeaknessWand : BaseWand
	{
		[Constructable]
		public WeaknessWand() : base( WandEffect.Weakness, 1, 60 )
		{
		}

		public WeaknessWand( Serial serial ) : base( serial )
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

		public override void OnWandUse( Mobile from )
		{
			Cast( new WeakenSpell( from, this ) );
		}
	}
}