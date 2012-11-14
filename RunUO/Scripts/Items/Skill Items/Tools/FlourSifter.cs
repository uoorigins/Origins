using System;
using Server;
using Server.Engines.Craft;
using Server.Network;

namespace Server.Items
{
	public class FlourSifter : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefCooking.CraftSystem; } }

		[Constructable]
		public FlourSifter() : base( 0x103E )
		{
			Weight = 1.0;
		}

		[Constructable]
		public FlourSifter( int uses ) : base( uses, 0x103E )
		{
			Weight = 1.0;
		}

		public FlourSifter( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a flour sifter"));
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