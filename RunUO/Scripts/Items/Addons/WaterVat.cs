using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class WaterVatEast : BaseAddon
	{
		[Constructable]
		public WaterVatEast()
		{
            Name = "water vat";
			AddComponent( new AddonComponent( 0x1558, Name ), 0, 0, 0 );
            AddComponent(new AddonComponent(0x14DE, Name), -1, 1, 0);
            AddComponent(new AddonComponent(0x1552, Name), 0, 1, 0);
            AddComponent(new AddonComponent(0x14DF, Name), 1, -1, 0);
            AddComponent(new AddonComponent(0x1554, Name), 1, 0, 0);
            AddComponent(new AddonComponent(0x1559, Name), 1, 1, 0);
            AddComponent(new AddonComponent(0x1550, Name), 1, 3, 0);
            AddComponent(new AddonComponent(0x1555, Name), 3, 1, 0);
            AddComponent(new AddonComponent(0x14D7, Name), 2, 2, 0);

			// Blockers
            AddComponent(new AddonComponent(0x21A4, Name), 2, -1, 0);
            AddComponent(new AddonComponent(0x21A4, Name), 3, 0, 0);
            AddComponent(new AddonComponent(0x21A4, Name), -1, 2, 0);
            AddComponent(new AddonComponent(0x21A4, Name), 0, 3, 0);
		}

		public WaterVatEast( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class WaterVatSouth : BaseAddon
	{
		[Constructable]
		public WaterVatSouth()
		{
            Name = "water vat";
            AddComponent(new AddonComponent(0x1558, Name), 0, 0, 0);
            AddComponent(new AddonComponent(0x14DE, Name), -1, 1, 0);
            AddComponent(new AddonComponent(0x1552, Name), 0, 1, 0);
            AddComponent(new AddonComponent(0x14DF, Name), 1, -1, 0);
            AddComponent(new AddonComponent(0x1554, Name), 1, 0, 0);
            AddComponent(new AddonComponent(0x1559, Name), 1, 1, 0);
            AddComponent(new AddonComponent(0x1551, Name), 1, 3, 0);
            AddComponent(new AddonComponent(0x1556, Name), 3, 1, 0);
            AddComponent(new AddonComponent(0x14D7, Name), 2, 2, 0);

			// Blockers
            AddComponent(new AddonComponent(0x21A4, Name), 2, -1, 0);
            AddComponent(new AddonComponent(0x21A4, Name), 3, 0, 0);
            AddComponent(new AddonComponent(0x21A4, Name), -1, 2, 0);
            AddComponent(new AddonComponent(0x21A4, Name), 0, 3, 0);
		}

		public WaterVatSouth( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}