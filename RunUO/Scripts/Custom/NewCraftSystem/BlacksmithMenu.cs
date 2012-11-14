using System;
using Server;
using System.Collections;
using Server.Engines.Craft;
using Server.Network;
using Server.Items;

namespace Server.Menus.ItemLists
{
    public class BlacksmithMenu : ItemListMenu
    {
        private Mobile m_Mobile;
        private string IsFrom;
        private BaseTool m_Tool;
        private ItemListEntry[] m_Entries;

        public BlacksmithMenu(Mobile m, ItemListEntry[] entries, string Is, BaseTool tool) : base("What would you like to make?", entries)
        {
            m_Mobile = m;
            IsFrom = Is;
            m_Tool = tool;
            m_Entries = entries;
        }

        //MAIN
        public static ItemListEntry[] Main(Mobile from)
        {
            bool Shields = true;
            bool Armor = true;
            bool Weapons = true;
            int missing = 0;

            int resHue = 0;
            int maxAmount = 0;
            Object message = null;
            bool allRequiredSkills = true;
            double chance;

            //Shields
            chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(23).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);
            if ((from.Backpack.GetAmount(typeof(IronIngot)) < DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(23).ItemType).Ressources.GetAt(0).Amount) || (chance <= 0.0)) //WoodenKiteShield
            {
                Shields = false;
            }

            //Armor
            chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(0).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);
            if ((from.Backpack.GetAmount(typeof(IronIngot)) < DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(0).ItemType).Ressources.GetAt(0).Amount) || (chance <= 0.0)) //RingmailGloves
            {
                Armor = false;
            }

            //Weapons
            chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(26).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);
            if ((from.Backpack.GetAmount(typeof(IronIngot)) < DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(26).ItemType).Ressources.GetAt(0).Amount) || (chance <= 0.0)) //Dagger
            {
                Weapons = false;
            }

            ItemListEntry[] entries = new ItemListEntry[4];

            entries[0] = new ItemListEntry("Repair", 4015, 0, 0);

            if (Shields)
                entries[1-missing] = new ItemListEntry("Shields", 7026, 0, 1);
            else
                missing++;// entries[1] = new ItemListEntry("", -1);

            if (Armor)
                entries[2 - missing] = new ItemListEntry("Armor", 5141, 0, 2);
            else
                missing++;// entries[2] = new ItemListEntry("", -1);

            if (Weapons)
                entries[3 - missing] = new ItemListEntry("Weapons", 5049, 0, 3);
            else
                missing++;// entries[3] = new ItemListEntry("", -1);

            Array.Resize(ref entries, entries.Length - missing);
            return entries;
        }

        //SHEILDS
        private static ItemListEntry[] Shields(Mobile from)
        {
            Type type;
            Item item = null;
            int itemid;
            string name;
            int missing = 0;

            CraftRes craftResource;
            int resHue = 0;
            int maxAmount = 0;
            Object message = null;

            bool allRequiredSkills = true;
            double chance;

            ItemListEntry[] entries = new ItemListEntry[6];

            for (int i = 0; i < 6; ++i)
            {
                type = DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 18).ItemType;
                craftResource = DefBlacksmithy.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
                chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 18).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);

                if ((from.Backpack.GetAmount(typeof(IronIngot)) >= DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 18).ItemType).Ressources.GetAt(0).Amount) && (chance > 0.0))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("S", " S");
                    name = name.Replace("K", " K");
                    name = name.ToLower();
                    itemid = item.ItemID;
                    if (itemid == 7033)
                        itemid = 7032;

                    entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} ingots)", name, craftResource.Amount), itemid, 0, i);
                }
                else
                {
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
                }

                if (item != null)
                    item.Delete();
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        //WEAPONS
        private static ItemListEntry[] Weapons(Mobile from)
        {
            bool Swords = true;
            bool Axes = true;
            bool Maces = true;
            bool Polearms = true;
            int missing = 0;

            CraftRes craftResource;
            int resHue = 0;
            int maxAmount = 0;
            Object message = null;
            bool allRequiredSkills = true;
            double chance;

            //Swords
            chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(26).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);
            if ((from.Backpack.GetAmount(typeof(IronIngot)) < DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(26).ItemType).Ressources.GetAt(0).Amount) || (chance <= 0.0)) //Dagger
            {
                Swords = false;
            }
            //Axes
            chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(34).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);
            if ((from.Backpack.GetAmount(typeof(IronIngot)) < DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(34).ItemType).Ressources.GetAt(0).Amount) || (chance <= 0.0)) //Double Axe
            {
                Axes = false;
            }
            //Maces
            chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(45).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);
            if ((from.Backpack.GetAmount(typeof(IronIngot)) < DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(45).ItemType).Ressources.GetAt(0).Amount) || (chance <= 0.0)) //Mace
            {
                Maces = false;
            }
            //Polearms
            chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(42).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);
            if ((from.Backpack.GetAmount(typeof(IronIngot)) < DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(42).ItemType).Ressources.GetAt(0).Amount) || (chance <= 0.0)) //Spear
            {
                Polearms = false;
            }

            ItemListEntry[] entries = new ItemListEntry[4];

            if (Swords)
                entries[0-missing] = new ItemListEntry("Swords & Blades", 5049, 0, 0);
            else
                missing++;//entries[0] = new ItemListEntry("", -1);

            if (Axes)
                entries[1 - missing] = new ItemListEntry("Axes", 3913, 0, 1);
            else
                missing++;//entries[1] = new ItemListEntry("", -1);

            if (Maces)
                entries[2 - missing] = new ItemListEntry("Maces & Hammers", 5127, 0, 2);
            else
                missing++;//entries[2] = new ItemListEntry("", -1);

            if (Polearms)
                entries[3-missing] = new ItemListEntry("Polearms", 3917, 0, 3);
            else
                missing++;//entries[3] = new ItemListEntry("", -1);

            Array.Resize(ref entries, entries.Length - missing);
            return entries;
        }

        //BLADES
        private static ItemListEntry[] Blades(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item = null;

            CraftRes craftResource;
            int resHue = 0;
            int maxAmount = 0;
            Object message = null;
            bool allRequiredSkills = true;
            double chance;
            int missing = 0;

            ItemListEntry[] entries = new ItemListEntry[8];

            for (int i = 0; i < 8; ++i)
            {
                type = DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 24).ItemType;
                craftResource = DefBlacksmithy.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
                chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 24).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);

                if ((from.Backpack.GetAmount(typeof(IronIngot)) >= DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(i+24).ItemType).Ressources.GetAt(0).Amount) && (chance > 0.0))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} ingots)", name, craftResource.Amount), itemid, 0, i);
                }
                else
                {
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
                }

                if (item != null)
                    item.Delete();
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        //AXES
        private static ItemListEntry[] Axes(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item = null;

            CraftRes craftResource;
            int resHue = 0;
            int maxAmount = 0;
            Object message = null;
            bool allRequiredSkills = true;
            double chance;
            int missing = 0;

            ItemListEntry[] entries = new ItemListEntry[7];

            for (int i = 0; i < 7; ++i)
            {
                type = DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 32).ItemType;
                craftResource = DefBlacksmithy.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
                chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 32).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);

                if ((from.Backpack.GetAmount(typeof(IronIngot)) >= DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 32).ItemType).Ressources.GetAt(0).Amount) && (chance > 0.0))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("B", " B");
                    name = name.Replace("H", " H");
                    name = name.Replace("A", " A");
                    name = name.Trim();
                    name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} ingots)", name, craftResource.Amount), itemid, 0, i);
                }
                else
                {
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
                }

                if (item != null)
                    item.Delete();
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        //MACES
        private static ItemListEntry[] Maces(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item = null;
            int missing = 0;

            CraftRes craftResource;
            int resHue = 0;
            int maxAmount = 0;
            Object message = null;
            bool allRequiredSkills = true;
            double chance;

            ItemListEntry[] entries = new ItemListEntry[6];

            for (int i = 0; i < 6; ++i)
            {
                type = DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 43).ItemType;
                craftResource = DefBlacksmithy.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
                chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 43).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);

                if ((from.Backpack.GetAmount(typeof(IronIngot)) >= DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 43).ItemType).Ressources.GetAt(0).Amount) && (chance > 0.0))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("P", " P");
                    name = name.Replace("F", " F");
                    name = name.Replace("M", " M");
                    name = name.Replace("H", " H");
                    name = name.Trim();
                    name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} ingots)", name, craftResource.Amount), itemid, 0, i);
                }
                else
                {
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
                }

                if (item != null)
                    item.Delete();
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        //POLEARMS
        private static ItemListEntry[] Polearms(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item = null;
            int missing = 0;

            CraftRes craftResource;
            int resHue = 0;
            int maxAmount = 0;
            Object message = null;
            bool allRequiredSkills = true;
            double chance;

            ItemListEntry[] entries = new ItemListEntry[4];

            for (int i = 0; i < 4; ++i)
            {
                type = DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 39).ItemType;
                craftResource = DefBlacksmithy.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
                chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 39).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);


                if ((from.Backpack.GetAmount(typeof(IronIngot)) >= DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 39).ItemType).Ressources.GetAt(0).Amount) && (chance > 0.0))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("S", " S");
                    name = name.Trim();
                    name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} ingots)", name, craftResource.Amount), itemid, 0, i);
                }
                else
                {
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
                }

                if (item != null)
                    item.Delete();
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        //ARMOR
        private static ItemListEntry[] Armor(Mobile from)
        {
            bool Platemail = true;
            bool Chainmail = true;
            bool Ringmail = true;
            bool Helmets = true;
            int missing = 0;

            CraftRes craftResource;
            int resHue = 0;
            int maxAmount = 0;
            Object message = null;
            bool allRequiredSkills = true;
            double chance;

            //Platemail
            chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(9).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);
            if ((from.Backpack.GetAmount(typeof(IronIngot)) < DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(9).ItemType).Ressources.GetAt(0).Amount) || (chance <= 0.0)) //Gorget
            {
                Platemail = false;
            }
            //Chainmail
            chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(5).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);
            if ((from.Backpack.GetAmount(typeof(IronIngot)) < DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(5).ItemType).Ressources.GetAt(0).Amount) || (chance <= 0.0)) //Legs
            {
                Chainmail = false;
            }
            //Ringmail
            chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(0).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);
            if ((from.Backpack.GetAmount(typeof(IronIngot)) < DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(0).ItemType).Ressources.GetAt(0).Amount) || (chance <= 0.0)) //Gloves
            {
                Ringmail = false;
            }
            //Helmets
            chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(4).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);
            if ((from.Backpack.GetAmount(typeof(IronIngot)) < DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(4).ItemType).Ressources.GetAt(0).Amount) || (chance <= 0.0)) //Gloves
            {
                Helmets = false;
            }

            ItemListEntry[] entries = new ItemListEntry[4];

            if (Platemail)
                entries[0-missing] = new ItemListEntry("Platemail", 5141, 0, 0);
            else
                missing++;// entries[0] = new ItemListEntry("", -1);

            if (Chainmail)
                entries[1 - missing] = new ItemListEntry("Chainmail", 5055, 0, 1);
            else
                missing++;//entries[1] = new ItemListEntry("", -1);

            if (Ringmail)
                entries[2 - missing] = new ItemListEntry("Ringmail", 5100, 0, 2);
            else
                missing++;//entries[2] = new ItemListEntry("", -1);

            if (Helmets)
                entries[3 - missing] = new ItemListEntry("Helmets", 5138, 0, 3);
            else
            {
                missing++;//entries[3] = new ItemListEntry("", -1);
            }

            Array.Resize(ref entries, entries.Length - missing);
            return entries;
        }

        //PLATEMAIL
        private static ItemListEntry[] Platemail(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item = null;
            int missing = 0;

            CraftRes craftResource;
            int resHue = 0;
            int maxAmount = 0;
            Object message = null;
            double chance;
            bool allRequiredSkills = true;

            ItemListEntry[] entries = new ItemListEntry[6];

            for (int i = 0; i < 6; ++i)
            {
                type = DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 7).ItemType;
                craftResource = DefBlacksmithy.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
                chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 7).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);

                if ((from.Backpack.GetAmount(typeof(IronIngot)) >= DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 7).ItemType).Ressources.GetAt(0).Amount) && (chance > 0.0))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("A", " A");
                    name = name.Replace("G", " G");
                    name = name.Replace("L", " L");
                    name = name.Replace("C", " C");
                    name = name.Replace("Female", "");
                    name = name.Replace("Plate", "Platemail");
                    name = name.Trim();
                    name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} ingots)", name, craftResource.Amount), itemid, 0, i);
                }
                else
                {
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
                }

                if (item != null)
                    item.Delete();
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }
        //CHAINMAIL
        private static ItemListEntry[] Chainmail(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item = null;
            int missing = 0;

            CraftRes craftResource;
            int resHue = 0;
            int maxAmount = 0;
            Object message = null;
            double chance;
            bool allRequiredSkills = true;

            ItemListEntry[] entries = new ItemListEntry[2];

            for (int i = 0; i < 2; ++i)
            {
                type = DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 5).ItemType;
                craftResource = DefBlacksmithy.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
                chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 5).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);

                if ((from.Backpack.GetAmount(typeof(IronIngot)) >= DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 5).ItemType).Ressources.GetAt(0).Amount) && (chance > 0.0))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("Chest", " Tunic");
                    name = name.Replace("L", " L");
                    name = name.Replace("Chain", " Chainmail");
                    name = name.Trim();
                    name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} ingots)", name, craftResource.Amount), itemid, 0, i);
                }
                else
                {
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
                }

                if (item != null)
                    item.Delete();
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        //RINGMAIL
        private static ItemListEntry[] Ringmail(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item = null;
            int missing = 0;
            CraftRes craftResource;
            int resHue = 0;
            int maxAmount = 0;
            Object message = null;
            double chance;
            bool allRequiredSkills = true;

            ItemListEntry[] entries = new ItemListEntry[4];

            for (int i = 0; i < 4; ++i)
            {
                type = DefBlacksmithy.CraftSystem.CraftItems.GetAt(i).ItemType;
                craftResource = DefBlacksmithy.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
                chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(i).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);

                if ((from.Backpack.GetAmount(typeof(IronIngot)) >= DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(i).ItemType).Ressources.GetAt(0).Amount) && (chance > 0.0))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("Chest", "Tunic");
                    name = name.Replace("T", " T");
                    name = name.Replace("L", " L");
                    name = name.Replace("S", " S");
                    name = name.Replace("G", " G");
                    name = name.Replace("A", " A");
                    name = name.Trim();
                    name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} ingots)", name, craftResource.Amount), itemid, 0, i);
                }
                else
                {
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
                }

                if (item != null)
                    item.Delete();
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        //HELMETS
        private static ItemListEntry[] Helmets(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item = null;
            int missing = 0;

            CraftRes craftResource;
            int resHue = 0;
            int maxAmount = 0;
            Object message = null;
            double chance;
            bool allRequiredSkills = true;

            ItemListEntry[] entries = new ItemListEntry[6];

            //chainmail coif
            type = DefBlacksmithy.CraftSystem.CraftItems.GetAt(4).ItemType;
            craftResource = DefBlacksmithy.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
            chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(4).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);
            if ((from.Backpack.GetAmount(typeof(IronIngot)) >= DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(4).ItemType).Ressources.GetAt(0).Amount) && (chance > 0.0))
            {
                type = DefBlacksmithy.CraftSystem.CraftItems.GetAt(4).ItemType;
                item = null;
                try { item = Activator.CreateInstance(type) as Item; }
                catch { }
                name = item.GetType().Name;
                name = name.Replace("Chain", "Chainmail");
                name = name.Replace("C", " C");
                name = name.Trim();
                name = name.ToLower();
                itemid = item.ItemID;

                entries[0] = new ItemListEntry(String.Format("{0} ({1} ingots)", name, craftResource.Amount), itemid, 0, 4);
            }
            else
            {
                missing++;// entries[0] = new ItemListEntry("", -1);
            }

            if (item != null)
                item.Delete();

            //the rest
            for (int i = 1; i < 6; ++i)
            {
                type = DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 12).ItemType;
                craftResource = DefBlacksmithy.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
                chance = DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 12).GetSuccessChance(from, typeof(IronIngot), DefBlacksmithy.CraftSystem, false, ref allRequiredSkills);

                if ((from.Backpack.GetAmount(typeof(IronIngot)) >= DefBlacksmithy.CraftSystem.CraftItems.SearchFor(DefBlacksmithy.CraftSystem.CraftItems.GetAt(i + 12).ItemType).Ressources.GetAt(0).Amount) && (chance > 0.0))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("C", " C");
                    name = name.Replace("H", " H");
                    name = name.Trim();
                    name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} ingots)", name, craftResource.Amount), itemid, 0, i);
                }
                else
                {
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
                }

                if (item != null)
                    item.Delete();
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        /*private static ItemListEntry[] Test()
        {
            Type type;
            int itemid;
            string name;
            Item item;

            ItemListEntry[] entries = new ItemListEntry[DefBlacksmithy.CraftSystem.CraftItems.Count];

            for (int i = 0; i < DefBlacksmithy.CraftSystem.CraftItems.Count; ++i)
            {
                type = DefBlacksmithy.CraftSystem.CraftItems.GetAt(i).ItemType;

                item = null;
                try { item = Activator.CreateInstance(type) as Item; }
                catch { }
                name = item.Name;
                itemid = item.ItemID;

                entries[i-missing] = new ItemListEntry(name, itemid, 0, i);

                if (item != null)
                    item.Delete();
            }

            return entries;
        }*/

        public override void OnResponse(NetState state, int index)
        {
            if (IsFrom == "Main")
            {
                if (m_Entries[index].craftIndex == 0)
                {
                    Repair.Do(m_Mobile, DefBlacksmithy.CraftSystem, m_Tool);
                }
                if (m_Entries[index].craftIndex == 1)
                {
                    IsFrom = "Shields";
                    m_Mobile.SendMenu(new BlacksmithMenu(m_Mobile, Shields(m_Mobile), IsFrom, m_Tool));
                }
                else if (m_Entries[index].craftIndex == 2)
                {
                    IsFrom = "Armor";
                    m_Mobile.SendMenu(new BlacksmithMenu(m_Mobile, Armor(m_Mobile), IsFrom, m_Tool));
                }
                else if (m_Entries[index].craftIndex == 3)
                {
                    IsFrom = "Weapons";
                    m_Mobile.SendMenu(new BlacksmithMenu(m_Mobile, Weapons(m_Mobile), IsFrom, m_Tool));
                }
            }
            else if (IsFrom == "Shields")
            {
                Type type = null;

                CraftContext context = DefBlacksmithy.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 18).UseSubRes2 ? DefBlacksmithy.CraftSystem.CraftSubRes2 : DefBlacksmithy.CraftSystem.CraftSubRes);
                int resIndex = (DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 18).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefBlacksmithy.CraftSystem.CreateItem(m_Mobile, DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 18).ItemType, type, m_Tool, DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 18));
            }
            else if (IsFrom == "Weapons")
            {
                if (m_Entries[index].craftIndex == 0)
                {
                    IsFrom = "Blades";
                    m_Mobile.SendMenu(new BlacksmithMenu(m_Mobile, Blades(m_Mobile), IsFrom, m_Tool));
                }
                else if (m_Entries[index].craftIndex == 1)
                {
                    IsFrom = "Axes";
                    m_Mobile.SendMenu(new BlacksmithMenu(m_Mobile, Axes(m_Mobile), IsFrom, m_Tool));
                }
                else if (m_Entries[index].craftIndex == 2)
                {
                    IsFrom = "Maces";
                    m_Mobile.SendMenu(new BlacksmithMenu(m_Mobile, Maces(m_Mobile), IsFrom, m_Tool));
                }
                else if (m_Entries[index].craftIndex == 3)
                {
                    IsFrom = "Polearms";
                    m_Mobile.SendMenu(new BlacksmithMenu(m_Mobile, Polearms(m_Mobile), IsFrom, m_Tool));
                }

            }
            else if (IsFrom == "Blades")
            {
                Type type = null;

                CraftContext context = DefBlacksmithy.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 24).UseSubRes2 ? DefBlacksmithy.CraftSystem.CraftSubRes2 : DefBlacksmithy.CraftSystem.CraftSubRes);
                int resIndex = (DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 24).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefBlacksmithy.CraftSystem.CreateItem(m_Mobile, DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 24).ItemType, type, m_Tool, DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 24));
            }
            else if (IsFrom == "Axes")
            {
                Type type = null;

                CraftContext context = DefBlacksmithy.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 32).UseSubRes2 ? DefBlacksmithy.CraftSystem.CraftSubRes2 : DefBlacksmithy.CraftSystem.CraftSubRes);
                int resIndex = (DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 32).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefBlacksmithy.CraftSystem.CreateItem(m_Mobile, DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 32).ItemType, type, m_Tool, DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 32));
            }
            else if (IsFrom == "Maces")
            {
                Type type = null;

                CraftContext context = DefBlacksmithy.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 43).UseSubRes2 ? DefBlacksmithy.CraftSystem.CraftSubRes2 : DefBlacksmithy.CraftSystem.CraftSubRes);
                int resIndex = (DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 43).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefBlacksmithy.CraftSystem.CreateItem(m_Mobile, DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 43).ItemType, type, m_Tool, DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 43));
            }
            else if (IsFrom == "Polearms")
            {
                Type type = null;

                CraftContext context = DefBlacksmithy.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 39).UseSubRes2 ? DefBlacksmithy.CraftSystem.CraftSubRes2 : DefBlacksmithy.CraftSystem.CraftSubRes);
                int resIndex = (DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 39).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefBlacksmithy.CraftSystem.CreateItem(m_Mobile, DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 39).ItemType, type, m_Tool, DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 39));
            }
            else if (IsFrom == "Armor")
            {
                if (m_Entries[index].craftIndex == 0)
                {
                    IsFrom = "Platemail";
                    m_Mobile.SendMenu(new BlacksmithMenu(m_Mobile, Platemail(m_Mobile), IsFrom, m_Tool));
                }
                else if (m_Entries[index].craftIndex == 1)
                {
                    IsFrom = "Chainmail";
                    m_Mobile.SendMenu(new BlacksmithMenu(m_Mobile, Chainmail(m_Mobile), IsFrom, m_Tool));
                }
                else if (m_Entries[index].craftIndex == 2)
                {
                    IsFrom = "Ringmail";
                    m_Mobile.SendMenu(new BlacksmithMenu(m_Mobile, Ringmail(m_Mobile), IsFrom, m_Tool));
                }
                else if (m_Entries[index].craftIndex == 3)
                {
                    IsFrom = "Helmets";
                    m_Mobile.SendMenu(new BlacksmithMenu(m_Mobile, Helmets(m_Mobile), IsFrom, m_Tool));
                }

            }
            else if (IsFrom == "Platemail")
            {
                Type type = null;

                CraftContext context = DefBlacksmithy.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 7).UseSubRes2 ? DefBlacksmithy.CraftSystem.CraftSubRes2 : DefBlacksmithy.CraftSystem.CraftSubRes);
                int resIndex = (DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 7).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefBlacksmithy.CraftSystem.CreateItem(m_Mobile, DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 7).ItemType, type, m_Tool, DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 7));
            }
            else if (IsFrom == "Chainmail")
            {
                Type type = null;

                CraftContext context = DefBlacksmithy.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 5).UseSubRes2 ? DefBlacksmithy.CraftSystem.CraftSubRes2 : DefBlacksmithy.CraftSystem.CraftSubRes);
                int resIndex = (DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 5).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefBlacksmithy.CraftSystem.CreateItem(m_Mobile, DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 5).ItemType, type, m_Tool, DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 5));
            }
            else if (IsFrom == "Ringmail")
            {
                Type type = null;

                CraftContext context = DefBlacksmithy.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)).UseSubRes2 ? DefBlacksmithy.CraftSystem.CraftSubRes2 : DefBlacksmithy.CraftSystem.CraftSubRes);
                int resIndex = (DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefBlacksmithy.CraftSystem.CreateItem(m_Mobile, DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)).ItemType, type, m_Tool, DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)));
            }
            else if (IsFrom == "Helmets")
            {
                Type type = null;

                CraftContext context = DefBlacksmithy.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 12).UseSubRes2 ? DefBlacksmithy.CraftSystem.CraftSubRes2 : DefBlacksmithy.CraftSystem.CraftSubRes);
                int resIndex = (DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 12).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                //chainmail coif
                if (index == 0)
                {
                    res = (DefBlacksmithy.CraftSystem.CraftItems.GetAt(4).UseSubRes2 ? DefBlacksmithy.CraftSystem.CraftSubRes2 : DefBlacksmithy.CraftSystem.CraftSubRes);
                    resIndex = (DefBlacksmithy.CraftSystem.CraftItems.GetAt(4).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);
                }

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                //chainmail coif
                if (index == 0)
                {
                    DefBlacksmithy.CraftSystem.CreateItem(m_Mobile, DefBlacksmithy.CraftSystem.CraftItems.GetAt(4).ItemType, type, m_Tool, DefBlacksmithy.CraftSystem.CraftItems.GetAt(4));
                }
                //the rest
                else
                {
                    DefBlacksmithy.CraftSystem.CreateItem(m_Mobile, DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 12).ItemType, type, m_Tool, DefBlacksmithy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 12));
                }
            }

        }
    }
}