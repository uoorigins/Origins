using System;
using Server;
using Server.Engines.Craft;
using Server.Network;
using Server.Targeting;
using Server.Menus.ItemLists;

namespace Server.Items
{
    public class TinkeringTarget : Target // Create our targeting class (which we derive from the base target class)
    {
        private Item m_Tool;

        public TinkeringTarget(Item tool) : base(1, false, TargetFlags.None)
        {
            m_Tool = tool;
        }

        protected override void OnTarget(Mobile from, object target)
        {
            if (m_Tool.Deleted || m_Tool.RootParent != from)
                return;

            if (target is Log)
            {
                Item item = (Item)target;

                if (item.RootParent != from)
                    from.SendAsciiMessage("That must be in your pack for you to use it.");
                else
                {
                    if (from.Backpack.GetAmount(typeof(Log)) >= 2)
                        from.SendMenu(new TinkeringMenu(from, TinkeringMenu.Wood(from), "Wood", (BaseTool)m_Tool));
                    else
                        from.SendAsciiMessage("You don't have the resources required to make anything from that.");
                }
            }
            else if (target is IronIngot)
            {
                Item item = (Item)target;

                if (item.RootParent != from)
                    from.SendAsciiMessage("That must be in your pack for you to use it.");
                else
                {
                    if (from.Backpack.GetAmount(typeof(IronIngot)) >= 2)
                        from.SendMenu(new TinkeringMenu(from, TinkeringMenu.Metal(from), "Metal", (BaseTool)m_Tool));
                    else
                        from.SendAsciiMessage("You don't have the resources required to make anything from that.");
                }
            }
            else
                from.SendAsciiMessage("That is not proper material for tinkering items.");
        }
    }

	//[Flipable( 0x1EB8, 0x1EB9 )]
	public class TinkerTools : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefTinkering.CraftSystem; } }

		[Constructable]
		public TinkerTools() : base( 0x1EBC )
		{
			Weight = 1.0;
		}

		[Constructable]
		public TinkerTools( int uses ) : base( uses, 0x1EBC )
		{
			Weight = 1.0;
		}

		public TinkerTools( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "tinker's tools"));
            }
        }

        public override void OnDoubleClick(Mobile from)
        {

            if (IsChildOf(from.Backpack) || Parent == from)
            {
                //from.SendAsciiMessage("What materials would you like to work with?");
                from.Target = new TinkeringTarget(this);
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