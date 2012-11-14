using System;
using Server;
using Server.Engines.Craft;
using Server.Network;

namespace Server.Items
{
	public class RollingPin : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefCooking.CraftSystem; } }

		[Constructable]
		public RollingPin() : base( 0x1043 )
		{
			Weight = 1.0;
		}

		[Constructable]
		public RollingPin( int uses ) : base( uses, 0x1043 )
		{
			Weight = 1.0;
		}

		public RollingPin( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a rolling pin"));
            }
        }

        public override void OnDoubleClick(Mobile from)
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