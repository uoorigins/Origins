using System;
using Server;
using Server.Engines.Craft;
using Server.Network;
using Server.Menus.ItemLists;
using Server.Targeting;

namespace Server.Items
{
    public class CarpentryTarget : Target // Create our targeting class (which we derive from the base target class)
    {
        private Item m_Tool;

        public CarpentryTarget(Item tool) : base(1, false, TargetFlags.None)
        {
            m_Tool = tool;
        }

        protected override void OnTarget(Mobile from, object target)
        {
            if (m_Tool.Deleted || m_Tool.RootParent != from)
                return;

            if (target is Board || target is Log)
            {
                Item item = (Item)target;

                if (item.RootParent != from)
                    from.SendAsciiMessage("You cannot use your tool on that.");
                else
                {
                    if (from.Backpack.GetAmount(typeof(Log)) + from.Backpack.GetAmount(typeof(Board)) >= 6)
                        from.SendMenu(new CarpentryMenu(from, CarpentryMenu.Main(from), "Main", (BaseTool)m_Tool));
                    else
                        from.SendAsciiMessage("You don't have the resources required to make anything from that.");
                }
            }
            else
                from.SendAsciiMessage("You cannot use your tool on that.");
        }
    }

	[FlipableAttribute( 0x1034, 0x1035 )]
	public class Saw : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefCarpentry.CraftSystem; } }

		[Constructable]
		public Saw() : base( 0x1034 )
		{
			Weight = 2.0;
		}

		[Constructable]
		public Saw( int uses ) : base( uses, 0x1034 )
		{
			Weight = 2.0;
		}

		public Saw( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a saw"));
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
                from.SendAsciiMessage("That must be in your pack for you to use it.");
            else
                from.Target = new CarpentryTarget(this);
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