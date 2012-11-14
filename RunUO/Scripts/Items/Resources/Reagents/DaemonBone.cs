using System;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	// TODO: Commodity?
	public class DaemonBone : BaseReagent
	{
		public override double DefaultWeight
		{
			get { return 1.0; }
		}

		[Constructable]
		public DaemonBone() : this( 1 )
		{
		}

		[Constructable]
		public DaemonBone( int amount ) : base( 0xF80, amount )
		{
		}

		public DaemonBone( Serial serial ) : base( serial )
		{
		}

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                if (Amount >= 2)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " " + this.Name));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
                }
            }
            else
            {
                if (Amount >= 2)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " Daemon Bone"));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Daemon Bone"));
                }
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