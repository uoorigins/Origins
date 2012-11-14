using System;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Engines.Harvest;
using Server.Mobiles;
using Server.Engines.Quests;
using Server.Engines.Quests.Hag;
using Server.Menus.ItemLists;

namespace Server.Targets
{
	public class BladedItemTarget : Target
	{
		private Item m_Item;

		public BladedItemTarget( Item item ) : base( 2, false, TargetFlags.None )
		{
			m_Item = item;
		}

		protected override void OnTargetOutOfRange( Mobile from, object targeted )
		{
			if ( targeted is UnholyBone && from.InRange( ((UnholyBone)targeted), 12 ) )
				((UnholyBone)targeted).Carve( from, m_Item );
			else
				base.OnTargetOutOfRange (from, targeted);
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( m_Item.Deleted )
				return;

			if ( targeted is ICarvable )
			{
                from.Animate(32, 5, 1, true, false, 0);
				((ICarvable)targeted).Carve( from, m_Item );
			}
            else if (targeted is Log)
            {
                BaseTool tools = new FletcherTools();
                from.SendMenu(new BowFletchingMenu(from, BowFletchingMenu.Main(from), "Main", tools));
                if (tools != null)
                    tools.Delete();
            }
			else if ( targeted is SwampDragon && ((SwampDragon)targeted).HasBarding )
			{
				SwampDragon pet = (SwampDragon)targeted;

				if ( !pet.Controlled || pet.ControlMaster != from )
					from.SendLocalizedMessage( 1053022 ); // You cannot remove barding from a swamp dragon you do not own.
				else
					pet.HasBarding = false;
			}
			else
			{
				if ( targeted is StaticTarget )
				{
					int itemID = ((StaticTarget)targeted).ItemID;

					if ( itemID == 0xD15 || itemID == 0xD16 ) // red mushroom
					{
						PlayerMobile player = from as PlayerMobile;

						if ( player != null )
						{
							QuestSystem qs = player.Quest;

							if ( qs is WitchApprenticeQuest )
							{
								FindIngredientObjective obj = qs.FindObjective( typeof( FindIngredientObjective ) ) as FindIngredientObjective;

								if ( obj != null && !obj.Completed && obj.Ingredient == Ingredient.RedMushrooms )
								{
									player.SendLocalizedMessage( 1055036 ); // You slice a red cap mushroom from its stem.
									obj.Complete();
									return;
								}
							}
						}
					}
				}

				HarvestSystem system = Lumberjacking.System;
				HarvestDefinition def = Lumberjacking.System.Definition;

				int tileID;
				Map map;
				Point3D loc;

				if ( !system.GetHarvestDetails( from, m_Item, targeted, out tileID, out map, out loc ) )
				{
					from.SendAsciiMessage( "You can't use a bladed item on that!" ); // You can't use a bladed item on that!
				}
				else if ( !def.Validate( tileID ) )
				{
					from.SendAsciiMessage( "You can't use a bladed item on that!" ); // You can't use a bladed item on that!
				}
				else
				{
					HarvestBank bank = def.GetBank( map, loc.X, loc.Y );

					if ( bank == null )
						return;

					if ( bank.Current < 5 )
					{
						from.SendAsciiMessage( "There's not enough wood here to harvest." ); // There's not enough wood here to harvest.
					}
					else
					{
						bank.Consume( 5, from );

						Item item = new Kindling();

						if ( from.PlaceInBackpack( item ) )
						{
							from.SendAsciiMessage( "You put some kindling into your backpack." ); // You put some kindling into your backpack.
							from.SendAsciiMessage( "An axe would probably get you more wood." ); // An axe would probably get you more wood.
						}
						else
						{
							from.SendAsciiMessage( "You can't place any kindling into your backpack!" ); // You can't place any kindling into your backpack!

							item.Delete();
						}
					}
				}
			}
		}
	}
}