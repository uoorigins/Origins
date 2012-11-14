using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server.Menus.ItemLists;

namespace Server.Items
{
    public class ShaftTarget : Target // Create our targeting class (which we derive from the base target class)
    {
        private Item m_Shaft;

        public ShaftTarget(Item shaft) : base(1, false, TargetFlags.None)
        {
            m_Shaft = shaft;
        }

        protected override void OnTarget(Mobile from, object target)
        {
            if (m_Shaft.Deleted || m_Shaft.RootParent != from)
                return;

            if (target is Feather)
            {
                Item item = (Item)target;
                BaseTool tools = new FletcherTools();

                if (item.RootParent != from)
                {
                    from.SendAsciiMessage("Can't use shafts on that.");
                    //from.SendLocalizedMessage(500509); // You cannot bless that object
                }
                else
                {
                    from.SendMenu(new BowFletchingMenu(from, BowFletchingMenu.Arrows(from), "Main", tools));
                }
            }
            else
                from.SendAsciiMessage("Can't use shafts on that.");
        }
    }

	public class Shaft : Item, ICommodity
	{
		string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} shaft" : "{0} shafts", Amount );
			}
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public Shaft() : this( 1 )
		{
		}

		[Constructable]
		public Shaft( int amount ) : base( 0x1BD4 )
		{
			Stackable = true;
			Amount = amount;
		}

		public Shaft( Serial serial ) : base( serial )
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
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " shafts"));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a shaft"));
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
                from.SendAsciiMessage("Select the feathers you wish to use this on.");
                //from.SendLocalizedMessage(1005018); // What would you like to bless? (Clothes Only)
                from.Target = new ShaftTarget(this); // Call our target
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