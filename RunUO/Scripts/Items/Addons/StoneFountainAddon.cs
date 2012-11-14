using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class StoneFountainAddon : BaseAddon
	{
		[Constructable]
		public StoneFountainAddon()
		{
			int itemID = 0x1731;

            AddComponent(new AddonComponent(itemID++, "a fountain"), -2, +1, 0);
            AddComponent(new AddonComponent(itemID++, "a fountain"), -1, +1, 0);
            AddComponent(new AddonComponent(itemID++, "a fountain"), +0, +1, 0);
            AddComponent(new AddonComponent(itemID++, "a fountain"), +1, +1, 0);

            AddComponent(new AddonComponent(itemID++, "a fountain"), +1, +0, 0);
            AddComponent(new AddonComponent(itemID++, "a fountain"), +1, -1, 0);
            AddComponent(new AddonComponent(itemID++, "a fountain"), +1, -2, 0);

            AddComponent(new AddonComponent(itemID++, "a fountain"), +0, -2, 0);
            AddComponent(new AddonComponent(itemID++, "a fountain"), +0, -1, 0);
            AddComponent(new AddonComponent(itemID++, "a fountain"), +0, +0, 0);

            AddComponent(new AddonComponent(itemID++, "a fountain"), -1, +0, 0);
            AddComponent(new AddonComponent(itemID++, "a fountain"), -2, +0, 0);

            AddComponent(new AddonComponent(itemID++, "a fountain"), -2, -1, 0);
            AddComponent(new AddonComponent(itemID++, "a fountain"), -1, -1, 0);

            AddComponent(new AddonComponent(itemID++, "a fountain"), -1, -2, 0);
            AddComponent(new AddonComponent(++itemID, "a fountain"), -2, -2, 0);
		}

		public StoneFountainAddon( Serial serial ) : base( serial )
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