using System;

namespace Server.Items
{
	[FlipableAttribute( 0x1f03, 0x1f04 )]
	public class CTFRobe : BaseOuterTorso
	{
		public CTFRobe( CTFTeam team ) : base( 0x1F03, team.Hue )
		{
			Name = team.Name + " Game Robe";
			Weight = 0.0;
			Movable = false;
		}

		public CTFRobe( Serial serial ) : base( serial )
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
