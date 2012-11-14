using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class BrownBearRugEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new BrownBearRugEastDeed(); } }

		[Constructable]
		public BrownBearRugEastAddon()
		{
            Name = "a bearskin rug";
			AddComponent( new AddonComponent( 0x1E40, Name ), 1, 1, 0 );
            AddComponent(new AddonComponent(0x1E41, Name), 1, 0, 0);
            AddComponent(new AddonComponent(0x1E42, Name), 1, -1, 0);
            AddComponent(new AddonComponent(0x1E43, Name), 0, -1, 0);
            AddComponent(new AddonComponent(0x1E44, Name), 0, 0, 0);
            AddComponent(new AddonComponent(0x1E45, Name), 0, 1, 0);
            AddComponent(new AddonComponent(0x1E46, Name), -1, 1, 0);
            AddComponent(new AddonComponent(0x1E47, Name), -1, 0, 0);
            AddComponent(new AddonComponent(0x1E48, Name), -1, -1, 0);
		}

		public BrownBearRugEastAddon( Serial serial ) : base( serial )
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

	public class BrownBearRugEastDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new BrownBearRugEastAddon(); } }
		public override int LabelNumber{ get{ return 1049397; } } // a brown bear rug deed facing east

		[Constructable]
		public BrownBearRugEastDeed()
		{
		}

		public BrownBearRugEastDeed( Serial serial ) : base( serial )
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

	public class BrownBearRugSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new BrownBearRugSouthDeed(); } }

		[Constructable]
		public BrownBearRugSouthAddon()
		{
            Name = "a bearskin rug";
			AddComponent( new AddonComponent( 0x1E36, Name ), 1, 1, 0 );
            AddComponent(new AddonComponent(0x1E37, Name), 0, 1, 0);
            AddComponent(new AddonComponent(0x1E38, Name), -1, 1, 0);
            AddComponent(new AddonComponent(0x1E39, Name), -1, 0, 0);
            AddComponent(new AddonComponent(0x1E3A, Name), 0, 0, 0);
            AddComponent(new AddonComponent(0x1E3B, Name), 1, 0, 0);
            AddComponent(new AddonComponent(0x1E3C, Name), 1, -1, 0);
            AddComponent(new AddonComponent(0x1E3D, Name), 0, -1, 0);
            AddComponent(new AddonComponent(0x1E3E, Name), -1, -1, 0);
		}

		public BrownBearRugSouthAddon( Serial serial ) : base( serial )
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

	public class BrownBearRugSouthDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new BrownBearRugSouthAddon(); } }
		public override int LabelNumber{ get{ return 1049398; } } // a brown bear rug deed facing south

		[Constructable]
		public BrownBearRugSouthDeed()
		{
		}

		public BrownBearRugSouthDeed( Serial serial ) : base( serial )
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

	public class PolarBearRugEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new PolarBearRugEastDeed(); } }

		[Constructable]
		public PolarBearRugEastAddon()
		{
            Name = "a bearskin rug";
			AddComponent( new AddonComponent( 0x1E53, Name ), 1, 1, 0 );
            AddComponent(new AddonComponent(0x1E54, Name), 1, 0, 0);
            AddComponent(new AddonComponent(0x1E55, Name), 1, -1, 0);
            AddComponent(new AddonComponent(0x1E56, Name), 0, -1, 0);
            AddComponent(new AddonComponent(0x1E57, Name), 0, 0, 0);
            AddComponent(new AddonComponent(0x1E58, Name), 0, 1, 0);
            AddComponent(new AddonComponent(0x1E59, Name), -1, 1, 0);
            AddComponent(new AddonComponent(0x1E5A, Name), -1, 0, 0);
            AddComponent(new AddonComponent(0x1E5B, Name), -1, -1, 0);
		}

		public PolarBearRugEastAddon( Serial serial ) : base( serial )
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

	public class PolarBearRugEastDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new PolarBearRugEastAddon(); } }
		public override int LabelNumber{ get{ return 1049399; } } // a polar bear rug deed facing east

		[Constructable]
		public PolarBearRugEastDeed()
		{
		}

		public PolarBearRugEastDeed( Serial serial ) : base( serial )
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

	public class PolarBearRugSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new PolarBearRugSouthDeed(); } }

		[Constructable]
		public PolarBearRugSouthAddon()
		{
            Name = "a bearskin rug";
			AddComponent( new AddonComponent( 0x1E49, Name ), 1, 1, 0 );
            AddComponent(new AddonComponent(0x1E4A, Name), 0, 1, 0);
            AddComponent(new AddonComponent(0x1E4B, Name), -1, 1, 0);
            AddComponent(new AddonComponent(0x1E4C, Name), -1, 0, 0);
            AddComponent(new AddonComponent(0x1E4D, Name), 0, 0, 0);
            AddComponent(new AddonComponent(0x1E4E, Name), 1, 0, 0);
            AddComponent(new AddonComponent(0x1E4F, Name), 1, -1, 0);
            AddComponent(new AddonComponent(0x1E50, Name), 0, -1, 0);
            AddComponent(new AddonComponent(0x1E51, Name), -1, -1, 0);
		}

		public PolarBearRugSouthAddon( Serial serial ) : base( serial )
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

	public class PolarBearRugSouthDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new PolarBearRugSouthAddon(); } }
		public override int LabelNumber{ get{ return 1049400; } } // a polar bear rug deed facing south

		[Constructable]
		public PolarBearRugSouthDeed()
		{
		}

		public PolarBearRugSouthDeed( Serial serial ) : base( serial )
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