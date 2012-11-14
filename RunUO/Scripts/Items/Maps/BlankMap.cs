using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class BlankMap : MapItem
	{
		[Constructable]
		public BlankMap()
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
            from.Send(new AsciiMessage(Serial, ItemID, MessageType.Regular, 0, 3, "", "It appears to be blank."));
			//SendLocalizedMessageTo( from, 500208 ); // It appears to be blank.
		}

		public BlankMap( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a map"));
            }
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}