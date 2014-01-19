using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class ClothingBlessTarget : Target // Create our targeting class (which we derive from the base target class)
	{
		private ClothingBlessDeed m_Deed;

		public ClothingBlessTarget( ClothingBlessDeed deed ) : base( 1, false, TargetFlags.None )
		{
			m_Deed = deed;
		}

		protected override void OnTarget( Mobile from, object target ) // Override the protected OnTarget() for our feature
		{
			if ( m_Deed.Deleted || m_Deed.RootParent != from )
				return;

			if ( target is BaseClothing )
			{
				BaseClothing item = (BaseClothing)target;

				if ( item.LootType == LootType.Blessed || item.BlessedFor == from || (Mobile.InsuranceEnabled && item.Insured) ) // Check if its already newbied (blessed)
				{
					from.SendAsciiMessage( "That item is already blessed" ); // That item is already blessed
				}
				else if ( item.LootType != LootType.Regular )
				{
					from.SendAsciiMessage( "You can not bless that item" ); // You can not bless that item
				}
				else if ( !item.CanBeBlessed || item.RootParent != from )
				{
					from.SendAsciiMessage( "You cannot bless that object" ); // You cannot bless that object
				}
				else
				{
					item.LootType = LootType.Blessed;
					from.SendAsciiMessage( "You bless the item...." ); // You bless the item....

					m_Deed.Delete(); // Delete the bless deed
				}
			}
			else
			{
				from.SendAsciiMessage( "You cannot bless that object" ); // You cannot bless that object
			}
		}
	}

	public class ClothingBlessDeed : Item // Create the item class which is derived from the base item class
	{
		public override string DefaultName
		{
			get { return "a clothing bless deed"; }
		}

		[Constructable]
		public ClothingBlessDeed() : base( 0x14F0 )
		{
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public ClothingBlessDeed( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a clothing bless deed"));
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
			LootType = LootType.Blessed;

			int version = reader.ReadInt();
		}

		public override bool DisplayLootType{ get{ return false; } }

		public override void OnDoubleClick( Mobile from ) // Override double click of the deed to call our target
		{
			if ( !IsChildOf( from.Backpack ) ) // Make sure its in their pack
			{
				 from.SendAsciiMessage( "That must be in your pack for you to use it." ); // That must be in your pack for you to use it.
			}
			else
			{
				from.SendAsciiMessage( "What would you like to bless? (Clothes Only)" ); // What would you like to bless? (Clothes Only)
				from.Target = new ClothingBlessTarget( this ); // Call our target
			 }
		}	
	}
}