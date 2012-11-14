using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server.Menus.ItemLists;

namespace Server.Items
{
    public class FeatherTarget : Target // Create our targeting class (which we derive from the base target class)
    {
        private Item m_Feather;

        public FeatherTarget(Item feather) : base(1, false, TargetFlags.None)
        {
            m_Feather = feather;
        }

        protected override void OnTarget(Mobile from, object target)
        {
            if (m_Feather.Deleted || m_Feather.RootParent != from)
                return;

            if (target is Shaft)
            {
                Item item = (Item)target;
                BaseTool tools = new FletcherTools();

                if (item.RootParent != from)
                {
                    from.SendAsciiMessage("Can't use feathers on that.");
                    //from.SendLocalizedMessage(500509); // You cannot bless that object
                }
                else
                {
                    from.SendMenu(new BowFletchingMenu(from, BowFletchingMenu.Arrows(from), "Main", tools));
                }
            }
            else
                from.SendAsciiMessage("Can't use feathers on that.");
        }
    }

	public class Feather : Item, ICommodity
	{
		string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} feather" : "{0} feathers", Amount );
			}
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public Feather() : this( 1 )
		{
		}

		[Constructable]
		public Feather( int amount ) : base( 0x1BD1 )
		{
			Stackable = true;
			Amount = amount;
		}

		public Feather( Serial serial ) : base( serial )
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
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " feathers"));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a feather"));
                }
            }
        }

        public override void OnDoubleClick(Mobile from) // Override double click of the deed to call our target
        {
            if (!IsChildOf(from.Backpack)) // Make sure its in their pack
            {
                //from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
                from.SendAsciiMessage("That must be in your pack for you to use it.");
            }
            else
            {
                from.SendAsciiMessage("Select the shafts you wish to use this on.");
                //from.SendLocalizedMessage(1005018); // What would you like to bless? (Clothes Only)
                from.Target = new FeatherTarget(this); // Call our target
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