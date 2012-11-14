using System;
using Server;
using Server.Engines.Craft;
using Server.Network;
using Server.Menus.ItemLists;

namespace Server.Items
{
	[FlipableAttribute( 0xfbb, 0xfbc )]
	public class Tongs : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefBlacksmithy.CraftSystem; } }

		[Constructable]
		public Tongs() : base( 0xFBB )
		{
			Weight = 2.0;
		}

		[Constructable]
		public Tongs( int uses ) : base( uses, 0xFBB )
		{
			Weight = 2.0;
		}

		public Tongs( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "tongs"));
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack) || Parent == from)
            {
                bool anvil, forge;
                DefBlacksmithy.CheckAnvilAndForge(from, 2, out anvil, out forge);

                if (anvil && forge)
                {
                    BaseTool m_Tool = this;
                    string IsFrom = "Main";
                    from.SendMenu(new BlacksmithMenu(from, BlacksmithMenu.Main(from), IsFrom, m_Tool));
                }
                else
                    from.SendAsciiMessage("You must be near an anvil and a forge to smith items.");
            }
            else
                from.SendAsciiMessage("That must be in your pack for you to use it.");
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