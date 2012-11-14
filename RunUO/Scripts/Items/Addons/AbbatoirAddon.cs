using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class AbbatoirAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new AbbatoirDeed(); } }

		[Constructable]
		public AbbatoirAddon()
		{
            Name = "altar";
			AddComponent( new AddonComponent( 0x120E, Name ), -1, -1, 0 );
            AddComponent(new AddonComponent(0x120F, Name), 0, -1, 0);
            AddComponent(new AddonComponent(0x1210, Name), 1, -1, 0);
            AddComponent(new AddonComponent(0x1215, Name), -1, 0, 0);
            AddComponent(new AddonComponent(0x1216, Name), 0, 0, 0);
            AddComponent(new AddonComponent(0x1211, Name), 1, 0, 0);
            AddComponent(new AddonComponent(0x1214, Name), -1, 1, 0);
            AddComponent(new AddonComponent(0x1213, Name), 0, 1, 0);
            AddComponent(new AddonComponent(0x1212, Name), 1, 1, 0);
		}

		public AbbatoirAddon( Serial serial ) : base( serial )
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

	public class AbbatoirDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new AbbatoirAddon(); } }
		public override int LabelNumber{ get{ return 1044329; } } // abbatoir

		[Constructable]
		public AbbatoirDeed()
		{
		}

		public AbbatoirDeed( Serial serial ) : base( serial )
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