using Server.Commands;
using Server.Network;
using System;

namespace Server.Items
{
	public interface IShipwreckedItem
	{
		bool IsShipwreckedItem { get; set; }
	}

	public class ShipwreckedItem : Item, IDyable, IShipwreckedItem
	{
		public ShipwreckedItem( int itemID ) : base( itemID )
		{
			int weight = this.ItemData.Weight;

			if ( weight >= 255 )
				weight = 1;

			this.Weight = weight;
		}

		public override void OnSingleClick( Mobile from )
		{
            string name = this.ItemData.Name;

            if ( ( this.ItemData.Flags & TileFlag.ArticleA ) != 0 )
                name = "a " + this.ItemData.Name;
            else if ( ( this.ItemData.Flags & TileFlag.ArticleAn ) != 0 )
                name = "an " + this.ItemData.Name;

            name += " recovered from a shipwreck";

            if ( this.Name != null )
            {
                from.Send( new AsciiMessage( Serial, ItemID, MessageType.Label, 0, 3, "", this.Name ) );
            }
            else
            {
                from.Send( new AsciiMessage( Serial, ItemID, MessageType.Label, 0, 3, "", name));
            }
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1041645 ); // recovered from a shipwreck
		}

		public ShipwreckedItem( Serial serial ) : base( serial )
		{
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

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			if ( ItemID >= 0x13A4 && ItemID <= 0x13AE )
			{
				Hue = sender.DyedHue;
				return true;
			}

			from.SendAsciiMessage( sender.FailMessage );
			return false;
		}

		#region IShipwreckedItem Members

		public bool IsShipwreckedItem
		{
			get
			{
				return true;	//It's a ShipwreckedItem item.  'Course it's gonna be a Shipwreckeditem
			}
			set
			{
			}
		}

		#endregion
	}
}