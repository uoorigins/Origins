using System;
using Server;
using Server.Engines.Craft;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	[Flipable( 0x1030, 0x1031 )]
	public class JointingPlane : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefCarpentry.CraftSystem; } }

		[Constructable]
		public JointingPlane() : base( 0x1030 )
		{
			Weight = 2.0;
		}

		[Constructable]
		public JointingPlane( int uses ) : base( uses, 0x1030 )
		{
			Weight = 2.0;
		}

		public JointingPlane( Serial serial ) : base( serial )
		{
		}

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a jointing plane"));
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
                from.SendAsciiMessage("That must be in your pack for you to use it.");
            else
                from.Target = new CarpentryTarget(this);
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

			if ( Weight == 1.0 )
				Weight = 2.0;
		}
	}
}