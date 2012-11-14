using System;
using Server;
using Server.Mobiles;
using Server.Multis;
using Server.Network;

namespace Server.Items
{
	public class ContractOfEmployment : Item
	{
		public override int LabelNumber{ get{ return 1041243; } } // a contract of employment

		[Constructable]
		public ContractOfEmployment() : base( 0x14F0 )
		{
			Weight = 1.0;
			//LootType = LootType.Blessed;
		}

		public ContractOfEmployment( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a contract of employment"));
            }
        }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendAsciiMessage( "That must be in your pack for you to use it." ); // That must be in your pack for you to use it.
			}
			else if ( from.AccessLevel >= AccessLevel.GameMaster )
			{
				from.SendAsciiMessage( "Your godly powers allow you to place this vendor whereever you wish." ); // Your godly powers allow you to place this vendor whereever you wish.

				Mobile v = new PlayerVendor( from, BaseHouse.FindHouseAt( from ) );

				v.Direction = from.Direction & Direction.Mask;
				v.MoveToWorld( from.Location, from.Map );

				v.SayTo( from, true, "Ah! it feels good to be working again." ); // Ah! it feels good to be working again.

				this.Delete();
			}
			else
			{
				BaseHouse house = BaseHouse.FindHouseAt( from );

				if ( house == null )
				{
					from.SendAsciiMessage( "Vendors can only be placed in houses." ); // Vendors can only be placed in houses.	
				}
                else if (!Key.ContainsKey(from.Backpack, house.keyValue))
                {
                    from.SendAsciiMessage("You can only place this in a house that you own!");
                }
                /*else if ( !BaseHouse.NewVendorSystem && !house.IsFriend( from ) )
                {
                    from.SendAsciiMessage("You can only place this in a house that you own!"); // You must ask the owner of this building to name you a friend of the household in order to place a vendor here.
                }
                else if ( BaseHouse.NewVendorSystem && !house.IsOwner( from ) )
                {
                    from.SendAsciiMessage("You can only place this in a house that you own!"); // Only the house owner can directly place vendors.  Please ask the house owner to offer you a vendor contract so that you may place a vendor in this house.
                }
                else if ( !house.Public || !house.CanPlaceNewVendor() ) 
                {
                    from.SendAsciiMessage( "You cannot place this vendor or barkeep.  Make sure the house is public and has sufficient storage available." ); // You cannot place this vendor or barkeep.  Make sure the house is public and has sufficient storage available.
                }*/
                else
                {
                    bool vendor, contract;
                    BaseHouse.IsThereVendor(from.Location, from.Map, out vendor, out contract);

                    if (vendor)
                    {
                        from.SendAsciiMessage("You cannot place a vendor at this location."); // You cannot place a vendor or barkeep at this location.
                    }
                    else if (contract)
                    {
                        from.SendLocalizedMessage(1062678); // You cannot place a vendor or barkeep on top of a rental contract!
                    }
                    else
                    {
                        Mobile v = new PlayerVendor(from, house);

                        v.Direction = from.Direction & Direction.Mask;
                        v.MoveToWorld(from.Location, from.Map);

                        v.SayTo(from, true, "Ah! it feels good to be working again."); // Ah! it feels good to be working again.

                        this.Delete();
                    }
                }
			}
		}
	}
}