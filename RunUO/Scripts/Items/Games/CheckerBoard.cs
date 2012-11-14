using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class CheckerBoard : BaseBoard
	{
		public override int LabelNumber{ get{ return 1016449; } } // a checker board

		[Constructable]
		public CheckerBoard() : base( 0xFA6 )
		{
		}

		public override void CreatePieces()
		{
			for ( int i = 0; i < 4; i++ )
			{
				CreatePiece( new PieceWhiteChecker( this ), ( 50 * i ) + 45, 25 );
				CreatePiece( new PieceWhiteChecker( this ), ( 50 * i ) + 70, 50 );
				CreatePiece( new PieceWhiteChecker( this ), ( 50 * i ) + 45, 75 );
				CreatePiece( new PieceBlackChecker( this ), ( 50 * i ) + 70, 150 );
				CreatePiece( new PieceBlackChecker( this ), ( 50 * i ) + 45, 175 );
				CreatePiece( new PieceBlackChecker( this ), ( 50 * i ) + 70, 200 );
			}
		}

		public CheckerBoard( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a checker board"));
            }
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