using System;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Multis;
using Server.Mobiles;
using Server.Engines.Quests;
using Server.Engines.Quests.Hag;
using Server.Menus.ItemLists;

namespace Server.Engines.Harvest
{
	public class HarvestTarget : Target
	{
		private Item m_Tool;
		private HarvestSystem m_System;

		public HarvestTarget( Item tool, HarvestSystem system ) : base( -1, true, TargetFlags.None )
		{
			m_Tool = tool;
			m_System = system;

			DisallowMultis = true;
            CheckLOS = false;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( m_System is Mining && targeted is StaticTarget )
			{
				int itemID = ((StaticTarget)targeted).ItemID;

				// grave
				if ( itemID == 0xED3 || itemID == 0xEDF || itemID == 0xEE0 || itemID == 0xEE1 || itemID == 0xEE2 || itemID == 0xEE8 )
				{
					PlayerMobile player = from as PlayerMobile;

					if ( player != null )
					{
						QuestSystem qs = player.Quest;

						if ( qs is WitchApprenticeQuest )
						{
							FindIngredientObjective obj = qs.FindObjective( typeof( FindIngredientObjective ) ) as FindIngredientObjective;

							if ( obj != null && !obj.Completed && obj.Ingredient == Ingredient.Bones )
							{
								player.SendLocalizedMessage( 1055037 ); // You finish your grim work, finding some of the specific bones listed in the Hag's recipe.
								obj.Complete();

								return;
							}
						}
					}
				}
			}

            if (m_System is Lumberjacking && targeted is IChopable)
            {
                if (m_Tool.Parent != from)
                {
                    from.SendAsciiMessage("The axe must be equipped for any serious wood chopping.");
                    //from.SendLocalizedMessage( 500487 ); // The axe must be equipped for any serious wood chopping.
                    return;
                }
                else
                    ((IChopable)targeted).OnChop(from);
            }
            else if (m_System is Lumberjacking && targeted is ICarvable )
			{
                from.Animate(32, 5, 1, true, false, 0);
				((ICarvable)targeted).Carve( from, m_Tool );
			}
            else if (m_System is Lumberjacking && targeted is Log)
            {
                BaseTool tools = new FletcherTools();
                from.SendMenu(new BowFletchingMenu(from, BowFletchingMenu.Main(from), "Main", tools));
                if (tools != null)
                    tools.Delete();
            }
            else if (m_System is Lumberjacking && FurnitureAttribute.Check(targeted as Item))
                DestroyFurniture(from, (Item)targeted);
            else if (m_System is Mining && targeted is TreasureMap)
                ((TreasureMap)targeted).OnBeginDig(from);
            else
                m_System.StartHarvesting(from, m_Tool, targeted);
		}

		private void DestroyFurniture( Mobile from, Item item )
		{
            if (!from.InRange(item.GetWorldLocation(), 3) || !from.InLOS(item))
			{
				from.SendAsciiMessage( "That is too far away." ); // That is too far away.
				return;
			}
            else if (!from.InLOS(item))
            {
                from.SendAsciiMessage("That is too far away."); // That is too far away.
                return;
            }
            else if (!item.IsChildOf(from.Backpack) && !item.Movable)
            {
                from.SendAsciiMessage("You can't destroy that while it is here."); // You can't destroy that while it is here.
                return;
            }

			from.SendAsciiMessage( "You destroy the item." ); // You destroy the item.
			Effects.PlaySound( item.GetWorldLocation(), item.Map, 0x3B3 );

			if ( item is Container )
			{
				if ( item is TrapableContainer )
					(item as TrapableContainer).ExecuteTrap( from );

				((Container)item).Destroy();
			}
			else
			{
				item.Delete();
			}
		}
	}
}