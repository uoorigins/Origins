using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class BloodPentagram : BaseAddon
	{
		[Constructable]
		public BloodPentagram ()
		{
            AddComponent(new AddonComponent(0x1CF9, "a blood pentagram"), 0, 1, 0);
            AddComponent(new AddonComponent(0x1CF8, "a blood pentagram"), 0, 2, 0);
            AddComponent(new AddonComponent(0x1CF7, "a blood pentagram"), 0, 3, 0);
            AddComponent(new AddonComponent(0x1CF6, "a blood pentagram"), 0, 4, 0);
            AddComponent(new AddonComponent(0x1CF5, "a blood pentagram"), 0, 5, 0);

            AddComponent(new AddonComponent(0x1CFB, "a blood pentagram"), 1, 0, 0);
            AddComponent(new AddonComponent(0x1CFA, "a blood pentagram"), 1, 1, 0);
            AddComponent(new AddonComponent(0x1D09, "a blood pentagram"), 1, 2, 0);
            AddComponent(new AddonComponent(0x1D08, "a blood pentagram"), 1, 3, 0);
            AddComponent(new AddonComponent(0x1D07, "a blood pentagram"), 1, 4, 0);
            AddComponent(new AddonComponent(0x1CF4, "a blood pentagram"), 1, 5, 0);

            AddComponent(new AddonComponent(0x1CFC, "a blood pentagram"), 2, 0, 0);
            AddComponent(new AddonComponent(0x1D0A, "a blood pentagram"), 2, 1, 0);
            AddComponent(new AddonComponent(0x1D11, "a blood pentagram"), 2, 2, 0);
            AddComponent(new AddonComponent(0x1D10, "a blood pentagram"), 2, 3, 0);
            AddComponent(new AddonComponent(0x1D06, "a blood pentagram"), 2, 4, 0);
            AddComponent(new AddonComponent(0x1CF3, "a blood pentagram"), 2, 5, 0);

            AddComponent(new AddonComponent(0x1CFD, "a blood pentagram"), 3, 0, 0);
            AddComponent(new AddonComponent(0x1D0B, "a blood pentagram"), 3, 1, 0);
            AddComponent(new AddonComponent(0x1D12, "a blood pentagram"), 3, 2, 0);
            AddComponent(new AddonComponent(0x1D0F, "a blood pentagram"), 3, 3, 0);
            AddComponent(new AddonComponent(0x1D05, "a blood pentagram"), 3, 4, 0);
            AddComponent(new AddonComponent(0x1CF2, "a blood pentagram"), 3, 5, 0);

            AddComponent(new AddonComponent(0x1CFE, "a blood pentagram"), 4, 0, 0);
            AddComponent(new AddonComponent(0x1D0C, "a blood pentagram"), 4, 1, 0);
            AddComponent(new AddonComponent(0x1D0D, "a blood pentagram"), 4, 2, 0);
            AddComponent(new AddonComponent(0x1D0E, "a blood pentagram"), 4, 3, 0);
            AddComponent(new AddonComponent(0x1D04, "a blood pentagram"), 4, 4, 0);
            AddComponent(new AddonComponent(0x1CF1, "a blood pentagram"), 4, 5, 0);

            AddComponent(new AddonComponent(0x1CFF, "a blood pentagram"), 5, 0, 0);
            AddComponent(new AddonComponent(0x1D00, "a blood pentagram"), 5, 1, 0);
            AddComponent(new AddonComponent(0x1D01, "a blood pentagram"), 5, 2, 0);
            AddComponent(new AddonComponent(0x1D02, "a blood pentagram"), 5, 3, 0);
            AddComponent(new AddonComponent(0x1D03, "a blood pentagram"), 5, 4, 0);
		}

		public BloodPentagram( Serial serial ) : base( serial )
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