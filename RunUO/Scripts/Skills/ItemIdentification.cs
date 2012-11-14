using System;
using Server;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
	public class ItemIdentification
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.ItemID].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile from )
		{
            from.SendAsciiMessage("What do you wish to appraise and identify?");
			//from.SendLocalizedMessage( 500343 ); // What do you wish to appraise and identify?
			from.Target = new InternalTarget();

			return TimeSpan.FromSeconds( 10.0 );
		}

		[PlayerVendorTarget]
		private class InternalTarget : Target
		{
			public InternalTarget() :  base ( 8, false, TargetFlags.None )
			{
				AllowNonlocal = true;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Item )
				{
                    if (from.CheckTargetSkill(SkillName.ItemID, o, 0, 100))
                    {
                        bool inlist = false;

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
                    }
                    else
                    {
                        from.SendAsciiMessage("You are not certain...");
                        //from.SendLocalizedMessage( 500353 ); // You are not certain...
                    }
				}
				else if ( o is Mobile )
				{
					((Mobile)o).OnSingleClick( from );
				}
				else
				{
                    from.SendAsciiMessage("You are not certain...");
					//from.SendLocalizedMessage( 500353 ); // You are not certain...
				}
			}
		}
	}
}