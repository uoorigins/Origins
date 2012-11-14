using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class Dices : Item
	{
		[Constructable]
		public Dices() : base( 0xFA7 )
		{
			Weight = 1.0;
		}

		public Dices( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "dices"));
            }
        }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( this.GetWorldLocation(), 2 ) )
				return;

			this.PublicOverheadMessage( MessageType.Regular, 0, true, string.Format( "*{0} rolls {1}, {2}*", from.Name, Utility.Random( 1, 6 ), Utility.Random( 1, 6 ) ) );
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