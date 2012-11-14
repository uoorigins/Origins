using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class RefreshPotion : BaseRefreshPotion
	{
		public override double Refresh{ get{ return 0.25; } }

		[Constructable]
		public RefreshPotion() : base( PotionEffect.Refresh )
		{
		}

		public RefreshPotion( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a Refresh potion"));
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