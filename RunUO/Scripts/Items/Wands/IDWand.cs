using System;
using Server;
using Server.Targeting;
using Server.Network;

namespace Server.Items
{
	public class IDWand : BaseWand
	{
		public override TimeSpan GetUseDelay{ get{ return TimeSpan.Zero; } }

		[Constructable]
		public IDWand() : base( WandEffect.Identification, 50, 200 )
		{
		}

		public IDWand( Serial serial ) : base( serial )
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

		public override bool OnWandTarget( Mobile from, object o )
		{
            bool inlist = false;
            if (o is Item)
            {
                if (o is BaseWeapon)
                    inlist = ((BaseWeapon)o).IsInIDList(from);
                else if (o is BaseArmor)
                    inlist = ((BaseArmor)o).IsInIDList(from);
                else if (o is BaseClothing)
                    inlist = ((BaseClothing)o).IsInIDList(from);
                else if (o is BaseJewel)
                    inlist = ((BaseJewel)o).IsInIDList(from);

                if (o is BaseWeapon)
                    ((BaseWeapon)o).AddToIDList(from);
                else if (o is BaseArmor)
                    ((BaseArmor)o).AddToIDList(from);
                else if (o is BaseClothing)
                    ((BaseClothing)o).AddToIDList(from);
                else if (o is BaseJewel)
                    ((BaseJewel)o).AddToIDList(from);

                if (!Core.AOS)
                    ((Item)o).OnSingleClick(from);

                if (o is BaseWeapon && (((BaseWeapon)o).IDList.Count > 50 && !inlist || from.AccessLevel > AccessLevel.Player))
                    ((BaseWeapon)o).RemoveFromIDList(from);
                else if (o is BaseArmor && (((BaseArmor)o).IDList.Count > 50 && !inlist || from.AccessLevel > AccessLevel.Player))
                    ((BaseArmor)o).RemoveFromIDList(from);
                else if (o is BaseClothing && (((BaseClothing)o).IDList.Count > 50 && !inlist || from.AccessLevel > AccessLevel.Player))
                    ((BaseClothing)o).RemoveFromIDList(from);
                else if (o is BaseJewel && (((BaseJewel)o).IDList.Count > 50 && !inlist || from.AccessLevel > AccessLevel.Player))
                    ((BaseJewel)o).RemoveFromIDList(from);

                return (o is Item);
            }
            else if (o is Mobile)
            {
                ((Mobile)o).OnSingleClick(from);
                return (o is Mobile);
            }
            return true;
		}
	}
}