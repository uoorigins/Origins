using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class Telescope : BaseAddon
	{
		[Constructable]
		public Telescope()
		{
            AddComponent(new AddonComponent(0x1494, "a telescope"), 0, 5, 0);
            AddComponent(new AddonComponent(0x145B, "a telescope"), 0, 6, 0);
            AddComponent(new AddonComponent(0x145A, "a telescope"), 0, 7, 0);

            AddComponent(new AddonComponent(0x1495, "a telescope"), 1, 4, 0);
            AddComponent(new AddonComponent(0x145C, "a telescope"), 1, 7, 0);
            AddComponent(new AddonComponent(0x145D, "a telescope"), 1, 8, 0);

            AddComponent(new AddonComponent(0x1496, "a telescope"), 2, 3, 0);
            AddComponent(new AddonComponent(0x1499, "a telescope"), 2, 4, 0);
            AddComponent(new AddonComponent(0x148E, "a telescope"), 2, 6, 0);
            AddComponent(new AddonComponent(0x1493, "a telescope"), 2, 7, 0);
            AddComponent(new AddonComponent(0x1492, "a telescope"), 2, 8, 0);
            AddComponent(new AddonComponent(0x145E, "a telescope"), 2, 9, 0);
            AddComponent(new AddonComponent(0x1459, "a telescope"), 2, 10, 0);

            AddComponent(new AddonComponent(0x1497, "a telescope"), 3, 2, 0);
            AddComponent(new AddonComponent(0x145F, "a telescope"), 3, 9, 0);
            AddComponent(new AddonComponent(0x1461, "a telescope"), 3, 10, 0);

            AddComponent(new AddonComponent(0x149A, "a telescope"), 4, 1, 0);
            AddComponent(new AddonComponent(0x1498, "a telescope"), 4, 2, 0);
            AddComponent(new AddonComponent(0x148F, "a telescope"), 4, 4, 0);
            AddComponent(new AddonComponent(0x148D, "a telescope"), 4, 6, 0);
            AddComponent(new AddonComponent(0x1488, "a telescope"), 4, 8, 0);
            AddComponent(new AddonComponent(0x1460, "a telescope"), 4, 9, 0);
            AddComponent(new AddonComponent(0x1462, "a telescope"), 4, 10, 0);

            AddComponent(new AddonComponent(0x147D, "a telescope"), 5, 0, 0);
            AddComponent(new AddonComponent(0x1490, "a telescope"), 5, 4, 0);
            AddComponent(new AddonComponent(0x148B, "a telescope"), 5, 5, 0);
            AddComponent(new AddonComponent(0x148A, "a telescope"), 5, 6, 0);
            AddComponent(new AddonComponent(0x1486, "a telescope"), 5, 7, 0);
            AddComponent(new AddonComponent(0x1485, "a telescope"), 5, 8, 0);

            AddComponent(new AddonComponent(0x147C, "a telescope"), 6, 0, 0);
            AddComponent(new AddonComponent(0x1491, "a telescope"), 6, 4, 0);
            AddComponent(new AddonComponent(0x148C, "a telescope"), 6, 5, 0);
            AddComponent(new AddonComponent(0x1489, "a telescope"), 6, 6, 0);
            AddComponent(new AddonComponent(0x1487, "a telescope"), 6, 7, 0);
            AddComponent(new AddonComponent(0x1484, "a telescope"), 6, 8, 0);
            AddComponent(new AddonComponent(0x1463, "a telescope"), 6, 10, 0);

            AddComponent(new AddonComponent(0x147B, "a telescope"), 7, 0, 0);
            AddComponent(new AddonComponent(0x147F, "a telescope"), 7, 3, 0);
            AddComponent(new AddonComponent(0x1480, "a telescope"), 7, 4, 0);
            AddComponent(new AddonComponent(0x1482, "a telescope"), 7, 5, 0);
            AddComponent(new AddonComponent(0x1469, "a telescope"), 7, 6, 0);
            AddComponent(new AddonComponent(0x1468, "a telescope"), 7, 7, 0);
            AddComponent(new AddonComponent(0x1465, "a telescope"), 7, 8, 0);
            AddComponent(new AddonComponent(0x1464, "a telescope"), 7, 9, 0);

            AddComponent(new AddonComponent(0x147A, "a telescope"), 8, 0, 0);
            AddComponent(new AddonComponent(0x1479, "a telescope"), 8, 1, 0);
            AddComponent(new AddonComponent(0x1477, "a telescope"), 8, 2, 0);
            AddComponent(new AddonComponent(0x147E, "a telescope"), 8, 3, 0);
            AddComponent(new AddonComponent(0x1481, "a telescope"), 8, 4, 0);
            AddComponent(new AddonComponent(0x1483, "a telescope"), 8, 5, 0);
            AddComponent(new AddonComponent(0x146A, "a telescope"), 8, 6, 0);
            AddComponent(new AddonComponent(0x1467, "a telescope"), 8, 7, 0);
            AddComponent(new AddonComponent(0x1466, "a telescope"), 8, 8, 0);

            AddComponent(new AddonComponent(0x1478, "a telescope"), 9, 1, 0);
            AddComponent(new AddonComponent(0x1475, "a telescope"), 9, 2, 0);
            AddComponent(new AddonComponent(0x1474, "a telescope"), 9, 3, 0);
            AddComponent(new AddonComponent(0x146F, "a telescope"), 9, 4, 0);
            AddComponent(new AddonComponent(0x146E, "a telescope"), 9, 5, 0);
            AddComponent(new AddonComponent(0x146D, "a telescope"), 9, 6, 0);
            AddComponent(new AddonComponent(0x146B, "a telescope"), 9, 7, 0);

            AddComponent(new AddonComponent(0x1476, "a telescope"), 10, 2, 0);
            AddComponent(new AddonComponent(0x1473, "a telescope"), 10, 3, 0);
            AddComponent(new AddonComponent(0x1470, "a telescope"), 10, 4, 0);
            AddComponent(new AddonComponent(0x1471, "a telescope"), 10, 5, 0);
            AddComponent(new AddonComponent(0x1472, "a telescope"), 10, 6, 0);
		}

		public Telescope( Serial serial ) : base( serial )
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