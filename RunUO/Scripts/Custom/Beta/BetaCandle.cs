using System;
using Server;
using Server.Network;
using System.Collections;

namespace Server.Items
{
	public class BetaCandle : BaseEquipableLight
	{
		public override int LitItemID{ get { return 0xA0F; } }
		public override int UnlitItemID{ get { return 0xA28; } }

		[Constructable]
		public BetaCandle() : base( 0xA28 )
		{
            Duration = TimeSpan.Zero;

			Burning = false;
			Light = LightType.Circle150;
			Weight = 1.0;
            Hue = 2101;
            LootType = LootType.Blessed;
		}

        public BetaCandle(Serial serial) : base(serial)
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "I lit the way, Origins Beta Tester"));
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

            if (LootType != LootType.Blessed)
                LootType = LootType.Blessed;
		}
	}
}