using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class PieceWhiteChecker : BasePiece
	{
		public override string DefaultName
		{
			get { return "white checker"; }
		}

		public PieceWhiteChecker( BaseBoard board ) : base( 0x3584, board )
		{
		}

		public PieceWhiteChecker( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "white checker"));
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

	public class PieceBlackChecker : BasePiece
	{
		public override string DefaultName
		{
			get { return "black checker"; }
		}

		public PieceBlackChecker( BaseBoard board ) : base( 0x358B, board )
		{
		}

		public PieceBlackChecker( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "black checker"));
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