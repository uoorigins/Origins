using System;
using Server;
using Server.Engines.Craft;
using Server.Menus.ItemLists;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
    public class AlcTarget : Target // Create our targeting class (which we derive from the base target class)
    {
        private Item m_Pestle;

        public AlcTarget(Item pestle) : base(1, false, TargetFlags.None)
        {
            m_Pestle = pestle;
        }

        protected override void OnTarget(Mobile from, object target)
        {
            if (m_Pestle.Deleted || m_Pestle.RootParent != from)
                return;

            //refresh
            if (target is BlackPearl)
            {
                Item item = (Item)target;

                if (item.RootParent != from)
                    from.SendAsciiMessage("That must be in your pack for you to use it.");
                else
                {
                    bool allRequiredSkills = true;
                    if (DefAlchemy.CraftSystem.CraftItems.GetAt(0).GetSuccessChance(from, typeof(BlackPearl), DefAlchemy.CraftSystem, false, ref allRequiredSkills) > 0)
                    {
                        if (from.Backpack.GetAmount(typeof(BlackPearl)) >= DefAlchemy.CraftSystem.CraftItems.SearchFor(DefAlchemy.CraftSystem.CraftItems.GetAt(0).ItemType).Ressources.GetAt(0).Amount)
                            from.SendMenu(new AlchemyMenu(from, AlchemyMenu.Refresh(from), "Refresh", (BaseTool)m_Pestle));
                        else
                            from.SendAsciiMessage("You don't have the resources required to make that item.");
                    }
                    else
                        from.SendAsciiMessage("You are not skilled enough to make anything out of that.");
                }
            }
            //agility
            else if (target is Bloodmoss)
            {
                Item item = (Item)target;

                if (item.RootParent != from)
                    from.SendAsciiMessage("That must be in your pack for you to use it.");
                else
                {
                    bool allRequiredSkills = true;
                    if (DefAlchemy.CraftSystem.CraftItems.GetAt(2).GetSuccessChance(from, typeof(Bloodmoss), DefAlchemy.CraftSystem, false, ref allRequiredSkills) > 0)
                    {
                        if (from.Backpack.GetAmount(typeof(Bloodmoss)) >= DefAlchemy.CraftSystem.CraftItems.SearchFor(DefAlchemy.CraftSystem.CraftItems.GetAt(2).ItemType).Ressources.GetAt(0).Amount)
                            from.SendMenu(new AlchemyMenu(from, AlchemyMenu.Agility(from), "Agility", (BaseTool)m_Pestle));
                        else
                            from.SendAsciiMessage("You don't have the resources required to make that item.");
                    }
                    else
                        from.SendAsciiMessage("You are not skilled enough to make anything out of that.");

                }
            }
            //Nightsight
            else if (target is SpidersSilk)
            {
                Item item = (Item)target;

                if (item.RootParent != from)
                    from.SendAsciiMessage("That must be in your pack for you to use it.");
                else
                {
                    bool allRequiredSkills = true;
                    if (DefAlchemy.CraftSystem.CraftItems.GetAt(4).GetSuccessChance(from, typeof(SpidersSilk), DefAlchemy.CraftSystem, false, ref allRequiredSkills) > 0)
                    {
                        if (from.Backpack.GetAmount(typeof(SpidersSilk)) >= DefAlchemy.CraftSystem.CraftItems.SearchFor(DefAlchemy.CraftSystem.CraftItems.GetAt(4).ItemType).Ressources.GetAt(0).Amount)
                            from.SendMenu(new AlchemyMenu(from, AlchemyMenu.NightSight(from), "NightSight", (BaseTool)m_Pestle));
                        else
                            from.SendAsciiMessage("You don't have the resources required to make that item.");
                    }
                    else
                        from.SendAsciiMessage("You are not skilled enough to make anything out of that.");
                }
            }
            //heal
            else if (target is Ginseng)
            {
                Item item = (Item)target;

                if (item.RootParent != from)
                    from.SendAsciiMessage("That must be in your pack for you to use it.");
                else
                {
                    bool allRequiredSkills = true;
                    if (DefAlchemy.CraftSystem.CraftItems.GetAt(5).GetSuccessChance(from, typeof(Ginseng), DefAlchemy.CraftSystem, false, ref allRequiredSkills) > 0)
                    {
                        if (from.Backpack.GetAmount(typeof(Ginseng)) >= DefAlchemy.CraftSystem.CraftItems.SearchFor(DefAlchemy.CraftSystem.CraftItems.GetAt(5).ItemType).Ressources.GetAt(0).Amount)
                            from.SendMenu(new AlchemyMenu(from, AlchemyMenu.Heal(from), "Heal", (BaseTool)m_Pestle));
                        else
                            from.SendAsciiMessage("You don't have the resources required to make that item.");
                    }
                    else
                        from.SendAsciiMessage("You are not skilled enough to make anything out of that.");
                }
            }
            //strength
            else if (target is MandrakeRoot)
            {
                Item item = (Item)target;

                if (item.RootParent != from)
                    from.SendAsciiMessage("That must be in your pack for you to use it.");
                else
                {
                    bool allRequiredSkills = true;
                    if (DefAlchemy.CraftSystem.CraftItems.GetAt(8).GetSuccessChance(from, typeof(MandrakeRoot), DefAlchemy.CraftSystem, false, ref allRequiredSkills) > 0)
                    {
                        if (from.Backpack.GetAmount(typeof(MandrakeRoot)) >= DefAlchemy.CraftSystem.CraftItems.SearchFor(DefAlchemy.CraftSystem.CraftItems.GetAt(8).ItemType).Ressources.GetAt(0).Amount)
                            from.SendMenu(new AlchemyMenu(from, AlchemyMenu.Strength(from), "Strength", (BaseTool)m_Pestle));
                        else
                            from.SendAsciiMessage("You don't have the resources required to make that item.");
                    }
                    else
                        from.SendAsciiMessage("You are not skilled enough to make anything out of that.");
                }
            }
            //poison
            else if (target is Nightshade)
            {
                Item item = (Item)target;

                if (item.RootParent != from)
                    from.SendAsciiMessage("That must be in your pack for you to use it.");
                else
                {
                    bool allRequiredSkills = true;
                    if (DefAlchemy.CraftSystem.CraftItems.GetAt(10).GetSuccessChance(from, typeof(Nightshade), DefAlchemy.CraftSystem, false, ref allRequiredSkills) > 0)
                    {
                        if (from.Backpack.GetAmount(typeof(Nightshade)) >= DefAlchemy.CraftSystem.CraftItems.SearchFor(DefAlchemy.CraftSystem.CraftItems.GetAt(10).ItemType).Ressources.GetAt(0).Amount)
                            from.SendMenu(new AlchemyMenu(from, AlchemyMenu.Poison(from), "Poison", (BaseTool)m_Pestle));
                        else
                            from.SendAsciiMessage("You don't have the resources required to make that item.");
                    }
                    else
                        from.SendAsciiMessage("You are not skilled enough to make anything out of that.");
                }
            }
            //cure
            else if (target is Garlic)
            {
                Item item = (Item)target;

                if (item.RootParent != from)
                    from.SendAsciiMessage("That must be in your pack for you to use it.");
                else
                {
                    bool allRequiredSkills = true;
                    if (DefAlchemy.CraftSystem.CraftItems.GetAt(14).GetSuccessChance(from, typeof(Garlic), DefAlchemy.CraftSystem, false, ref allRequiredSkills) > 0)
                    {
                        if (from.Backpack.GetAmount(typeof(Garlic)) >= DefAlchemy.CraftSystem.CraftItems.SearchFor(DefAlchemy.CraftSystem.CraftItems.GetAt(14).ItemType).Ressources.GetAt(0).Amount)
                            from.SendMenu(new AlchemyMenu(from, AlchemyMenu.Cure(from), "Cure", (BaseTool)m_Pestle));
                        else
                            from.SendAsciiMessage("You don't have the resources required to make that item.");
                    }
                    else
                        from.SendAsciiMessage("You are not skilled enough to make anything out of that.");
                }
            }
            //explosion
            else if (target is SulfurousAsh)
            {
                Item item = (Item)target;

                if (item.RootParent != from)
                    from.SendAsciiMessage("That must be in your pack for you to use it.");
                else
                {
                    bool allRequiredSkills = true;
                    if (DefAlchemy.CraftSystem.CraftItems.GetAt(17).GetSuccessChance(from, typeof(SulfurousAsh), DefAlchemy.CraftSystem, false, ref allRequiredSkills) > 0)
                    {
                        if (from.Backpack.GetAmount(typeof(SulfurousAsh)) >= DefAlchemy.CraftSystem.CraftItems.SearchFor(DefAlchemy.CraftSystem.CraftItems.GetAt(17).ItemType).Ressources.GetAt(0).Amount)
                            from.SendMenu(new AlchemyMenu(from, AlchemyMenu.Explosion(from), "Explosion", (BaseTool)m_Pestle));
                        else
                            from.SendAsciiMessage("You don't have the resources required to make that item.");
                    }
                    else
                        from.SendAsciiMessage("You are not skilled enough to make anything out of that.");
                }
            }
            else
                from.SendAsciiMessage("That's not something you can grind with a mortar and pestle!");
        }
    }

	public class MortarPestle : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefAlchemy.CraftSystem; } }

		[Constructable]
		public MortarPestle() : base( 0xE9B )
		{
			Weight = 1.0;
		}

		[Constructable]
		public MortarPestle( int uses ) : base( uses, 0xE9B )
		{
			Weight = 1.0;
		}

		public MortarPestle( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a mortar and pestle"));
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
                from.SendAsciiMessage("That must be in your pack for you to use it.");
            else
            {
                from.SendAsciiMessage("What reagent would you like to make the potion out of?");
                from.Target = new AlcTarget(this);
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