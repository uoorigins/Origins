using System;
using Server;
using Server.Engines.Craft;
using Server.Network;
using Server.Menus.ItemLists;
using Server.Targeting;

namespace Server.Items
{
    public class TailoringTarget : Target // Create our targeting class (which we derive from the base target class)
    {
        private Item m_Tool;

        public TailoringTarget(Item tool) : base(1, false, TargetFlags.None)
        {
            m_Tool = tool;
        }

        protected override void OnTarget(Mobile from, object target)
        {
            if (m_Tool.Deleted || m_Tool.RootParent != from)
                return;

            if (target is Cloth)
            {
                Item item = (Item)target;

                if (item.RootParent != from)
                    from.SendAsciiMessage("That must be in your pack for you to use it.");
                else
                {
                    if (from.Backpack.GetAmount(typeof(Cloth)) >= 2)
                        from.SendMenu(new TailoringMenu(from, TailoringMenu.Main(from), "Main", (BaseTool)m_Tool));
                    else
                        from.SendAsciiMessage("You don't have the resources required to make anything from that.");
                }
            }
            else if (target is Leather || target is Hides)
            {
                Item item = (Item)target;

                if (item.RootParent != from)
                    from.SendAsciiMessage("That must be in your pack for you to use it.");
                else
                {
                    if (from.Backpack.GetAmount(typeof(Leather)) >= 2 || from.Backpack.GetAmount(typeof(Hides)) >= 2)
                        from.SendMenu(new TailoringMenu(from, TailoringMenu.LeatherMain(from), "LeatherMain", (BaseTool)m_Tool));
                    else
                        from.SendAsciiMessage("You don't have the resources required to make anything from that.");
                }
            }
        }
    }

	public class SewingKit : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefTailoring.CraftSystem; } }

		[Constructable]
		public SewingKit() : base( 0xF9D )
		{
			Weight = 2.0;
		}

		[Constructable]
		public SewingKit( int uses ) : base( uses, 0xF9D )
		{
			Weight = 2.0;
		}

		public SewingKit( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a sewing kit"));
            }
        }

        public override void OnDoubleClick(Mobile from)
        {

            if (IsChildOf(from.Backpack) || Parent == from)
            {
                from.SendAsciiMessage("Please select the cloth you would like to use.");
                from.Target = new TailoringTarget(this);
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