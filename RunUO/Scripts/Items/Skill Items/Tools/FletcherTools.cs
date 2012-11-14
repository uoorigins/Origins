using System;
using Server;
using Server.Engines.Craft;
using Server.Network;
using Server.Menus.ItemLists;

namespace Server.Items
{
	[FlipableAttribute( 0x1022, 0x1023 )]
	public class FletcherTools : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefBowFletching.CraftSystem; } }

		[Constructable]
		public FletcherTools() : base( 0x1022 )
		{
			Weight = 2.0;
		}

		[Constructable]
		public FletcherTools( int uses ) : base( uses, 0x1022 )
		{
			Weight = 2.0;
		}

		public FletcherTools( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "fletcher tools"));
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            BaseTool m_Tool = this;
            string IsFrom = "Main";
            from.SendMenu(new BowFletchingMenu(from, BowFletchingMenu.Main(from), IsFrom, m_Tool));
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

			if ( Weight == 1.0 )
				Weight = 2.0;
		}
	}
}