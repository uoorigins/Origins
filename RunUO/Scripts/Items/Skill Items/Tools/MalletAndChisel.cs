using System;
using Server;
using Server.Engines.Craft;
using Server.Network;

namespace Server.Items
{
	public class MalletAndChisel : BaseTool
	{
		public override CraftSystem CraftSystem { get { return DefMasonry.CraftSystem; } }

		[Constructable]
		public MalletAndChisel() : base( 0x12B3 )
		{
			Weight = 1.0;
		}

		[Constructable]
		public MalletAndChisel( int uses ) : base( uses, 0x12B3 )
		{
			Weight = 1.0;
		}

		public MalletAndChisel( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a mallet and chisel"));
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