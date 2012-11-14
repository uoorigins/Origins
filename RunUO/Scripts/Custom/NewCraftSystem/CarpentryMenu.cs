using System;
using Server;
using System.Collections;
using Server.Engines.Craft;
using Server.Network;
using Server.Items;

namespace Server.Menus.ItemLists
{
    public class CarpentryMenu : ItemListMenu
    {
        private Mobile m_Mobile;
        private string IsFrom;
        private BaseTool m_Tool;
        private ItemListEntry[] m_Entries;

        public CarpentryMenu(Mobile m, ItemListEntry[] entries, string Is, BaseTool tool) : base("What would you like to make?", entries)
        {
            m_Mobile = m;
            IsFrom = Is;
            m_Tool = tool;
            m_Entries = entries;
        }

        public static ItemListEntry[] Main(Mobile from)
        {
            Type type;
            CraftRes craftResource;
            int missing = 0;

            bool Chairs = true;
            bool Tables = true;
            bool Containers = true;
            bool Misc = true;

            int resHue = 0;
            int maxAmount = 0;
            Object message = null;
            bool allRequiredSkills = true;
            double chance = 1;
            int ResAmount = 0;


            //Chairs
            type = DefCarpentry.CraftSystem.CraftItems.GetAt(6).ItemType;
            craftResource = DefCarpentry.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
            ResAmount = from.Backpack.GetAmount(typeof(Log)) + from.Backpack.GetAmount(typeof(Board));

            if ((chance > 0) && (ResAmount < craftResource.Amount))
            {
                Chairs = false;
            }

            //Tables
            type = DefCarpentry.CraftSystem.CraftItems.GetAt(15).ItemType;
            craftResource = DefCarpentry.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
            ResAmount = from.Backpack.GetAmount(typeof(Log)) + from.Backpack.GetAmount(typeof(Board));

            if ((chance > 0) && (ResAmount < craftResource.Amount))
            {
                Tables = false;
            }

            //Containers
            type = DefCarpentry.CraftSystem.CraftItems.GetAt(21).ItemType;
            craftResource = DefCarpentry.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
            ResAmount = from.Backpack.GetAmount(typeof(Log)) + from.Backpack.GetAmount(typeof(Board));

            if ((chance > 0) && (ResAmount < craftResource.Amount))
            {
                Containers = false;
            }

            //Misc
            type = DefCarpentry.CraftSystem.CraftItems.GetAt(29).ItemType;
            craftResource = DefCarpentry.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
            ResAmount = from.Backpack.GetAmount(typeof(Log)) + from.Backpack.GetAmount(typeof(Board));

            if ((chance > 0) && (ResAmount < craftResource.Amount))
            {
                Misc = false;
            }

            ItemListEntry[] entries = new ItemListEntry[3];

            if (Chairs)
                entries[0-missing] = new ItemListEntry("Chairs", 2902, 0, 0);
            else
                missing++;// entries[0] = new ItemListEntry("", -1);

            if (Tables)
                entries[1 - missing] = new ItemListEntry("Tables", 2940,0 ,1);
            else
                missing++;//entries[1] = new ItemListEntry("", -1);

            //Called Containers, labeled Misc... There is no actual Misc category
            if (Containers)
                entries[2 - missing] = new ItemListEntry("Miscellaneous", 3650, 0, 2);
            else
                missing++;//entries[2] = new ItemListEntry("", -1);

            /*if (Misc)
                entries[3] = new ItemListEntry("Miscellaneous", 7034);
            else
                entries[3] = new ItemListEntry("", -1);*/

            Array.Resize(ref entries, entries.Length - missing);
            return entries;
        }

        public static ItemListEntry[] Chairs(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item;
            CraftRes craftResource;
            bool allRequiredSkills = true;
            double chance;
            int ResAmount = 0;
            int missing = 0;

            ItemListEntry[] entries = new ItemListEntry[9];

            for (int i = 0; i < 9; ++i)
            {
                chance = DefCarpentry.CraftSystem.CraftItems.GetAt(i+6).GetSuccessChance(from, typeof(Board), DefCarpentry.CraftSystem, false, ref allRequiredSkills);
                type = DefCarpentry.CraftSystem.CraftItems.GetAt(i + 6).ItemType;
                craftResource = DefCarpentry.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
                ResAmount = from.Backpack.GetAmount(typeof(Log)) + from.Backpack.GetAmount(typeof(Board));

                if ((chance > 0) && (ResAmount >= craftResource.Amount))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("tS", "t S");
                    name = name.Replace("oC", "o C");
                    name = name.Replace("nC", "n C");
                    name = name.Replace("nB", "n B");
                    name = name.Replace("nT", "n T");
                    name = name.ToLower();
                    if (name == "fancywooden chaircushion")
                        name = "wooden chair";
                    if (name == "wooden chaircushion")
                        name = "wooden chair";

                    itemid = item.ItemID;
                    /*if (itemid == 2903)
                        itemid--;*/

                    entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} wood)", name, craftResource.Amount), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i]= new ItemListEntry("", -1);
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        public static ItemListEntry[] Tables(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item;
            CraftRes craftResource;
            bool allRequiredSkills = true;
            double chance;
            int ResAmount = 0;
            int missing = 0;
            ItemListEntry[] entries = new ItemListEntry[4];

            for (int i = 0; i < 4; ++i)
            {
                chance = DefCarpentry.CraftSystem.CraftItems.GetAt(i + 15).GetSuccessChance(from, typeof(Board), DefCarpentry.CraftSystem, false, ref allRequiredSkills);
                type = DefCarpentry.CraftSystem.CraftItems.GetAt(i + 15).ItemType;
                craftResource = DefCarpentry.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
                ResAmount = from.Backpack.GetAmount(typeof(Log)) + from.Backpack.GetAmount(typeof(Board));

                if ((chance > 0) && (ResAmount >= craftResource.Amount))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("gT", "g T");
                    name = name.ToLower();
                    if (name == "yewwoodtable")
                        name = "wooden table";
                    if (name == "largetable")
                        name = "large wooden table";
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} wood)", name, craftResource.Amount), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i]= new ItemListEntry("", -1);
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        public static ItemListEntry[] Containers(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item;
            CraftRes craftResource;
            bool allRequiredSkills = true;
            double chance;
            int ResAmount = 0;
            int missing = 0;

            ItemListEntry[] entries = new ItemListEntry[9];

            for (int i = 0; i < 9; ++i)
            {
                chance = DefCarpentry.CraftSystem.CraftItems.GetAt(i + 19).GetSuccessChance(from, typeof(Board), DefCarpentry.CraftSystem, false, ref allRequiredSkills);
                type = DefCarpentry.CraftSystem.CraftItems.GetAt(i + 19).ItemType;
                craftResource = DefCarpentry.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
                ResAmount = from.Backpack.GetAmount(typeof(Log)) + from.Backpack.GetAmount(typeof(Board));

                if ((chance > 0) && (ResAmount >= craftResource.Amount))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("nB", "n B");
                    name = name.Replace("lC", "l C");
                    name = name.Replace("eC", "e C");
                    name = name.Replace("mC", "m C");
                    name = name.Replace("nC", "n C");
                    name = name.Replace("yA", "y A");
                    name = name.Replace("yB", "y B");
                    name = name.Replace("nS", "n S");
                    name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} wood)", name, craftResource.Amount), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i]= new ItemListEntry("", -1);
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        public static ItemListEntry[] Misc(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item;
            CraftRes craftResource;
            bool allRequiredSkills = true;
            double chance;
            int ResAmount = 0;
            int missing = 0;

            ItemListEntry[] entries = new ItemListEntry[5];

            for (int i = 0; i < 5; ++i)
            {
                chance = DefCarpentry.CraftSystem.CraftItems.GetAt(i + 28).GetSuccessChance(from, typeof(Board), DefCarpentry.CraftSystem, false, ref allRequiredSkills);
                type = DefCarpentry.CraftSystem.CraftItems.GetAt(i + 28).ItemType;
                craftResource = DefCarpentry.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
                ResAmount = from.Backpack.GetAmount(typeof(Log)) + from.Backpack.GetAmount(typeof(Board));

                if ((chance > 0) && (ResAmount >= craftResource.Amount))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("sC", "s C");
                    name = name.Replace("rS", "r S");
                    name = name.Replace("dS", "d S");
                    name = name.Replace("nS", "n S");
                    name = name.Replace("gP", "g P");
                    name = name.ToLower();
                    itemid = item.ItemID;

                    if (i==4)
                        entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} logs, {1} cloth)", name, craftResource.Amount), itemid, 0, i);
                    else
                        entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} logs)", name, craftResource.Amount), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i]= new ItemListEntry("", -1);
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        /*public static ItemListEntry[] Other(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item;
            CraftRes craftResource;
            bool allRequiredSkills = true;
            double chance;

            ItemListEntry[] entries = new ItemListEntry[DefCarpentry.CraftSystem.CraftItems.Count];

            for (int i = 0; i < DefCarpentry.CraftSystem.CraftItems.Count; ++i)
            {
                chance = DefCarpentry.CraftSystem.CraftItems.GetAt(i).GetSuccessChance(from, typeof(Board), DefCarpentry.CraftSystem, false, ref allRequiredSkills);

                if (chance > 0)
                {
                    type = DefCarpentry.CraftSystem.CraftItems.GetAt(i).ItemType;
                    craftResource = DefCarpentry.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0}", name), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i]= new ItemListEntry("", -1);
            }

            return entries;
        }*/

        public override void OnResponse(NetState state, int index)
        {
            if (IsFrom == "Main")
            {
                if (m_Entries[index].craftIndex == 0)
                {
                    IsFrom = "Chairs";
                    m_Mobile.SendMenu(new CarpentryMenu(m_Mobile, Chairs(m_Mobile), IsFrom, m_Tool));
                }
                if (m_Entries[index].craftIndex == 1)
                {
                    IsFrom = "Tables";
                    m_Mobile.SendMenu(new CarpentryMenu(m_Mobile, Tables(m_Mobile), IsFrom, m_Tool));
                }
                else if (m_Entries[index].craftIndex == 2)
                {
                    IsFrom = "Containers";
                    m_Mobile.SendMenu(new CarpentryMenu(m_Mobile, Containers(m_Mobile), IsFrom, m_Tool));
                }
                else if (m_Entries[index].craftIndex == 3)
                {
                    IsFrom = "Misc";
                    m_Mobile.SendMenu(new CarpentryMenu(m_Mobile, Misc(m_Mobile), IsFrom, m_Tool));
                }
            }
            else if (IsFrom == "Chairs")
            {
                Type type = null;

                CraftContext context = DefCarpentry.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefCarpentry.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)+6).UseSubRes2 ? DefCarpentry.CraftSystem.CraftSubRes2 : DefCarpentry.CraftSystem.CraftSubRes);
                int resIndex = (DefCarpentry.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)+6).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefCarpentry.CraftSystem.CreateItem(m_Mobile, DefCarpentry.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)+6).ItemType, type, m_Tool, DefCarpentry.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)+6));
            }
            else if (IsFrom == "Tables")
            {
                Type type = null;

                CraftContext context = DefCarpentry.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefCarpentry.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 15).UseSubRes2 ? DefCarpentry.CraftSystem.CraftSubRes2 : DefCarpentry.CraftSystem.CraftSubRes);
                int resIndex = (DefCarpentry.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 15).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefCarpentry.CraftSystem.CreateItem(m_Mobile, DefCarpentry.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 15).ItemType, type, m_Tool, DefCarpentry.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 15));
            }
            else if (IsFrom == "Containers")
            {
                Type type = null;

                CraftContext context = DefCarpentry.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefCarpentry.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 19).UseSubRes2 ? DefCarpentry.CraftSystem.CraftSubRes2 : DefCarpentry.CraftSystem.CraftSubRes);
                int resIndex = (DefCarpentry.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 19).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefCarpentry.CraftSystem.CreateItem(m_Mobile, DefCarpentry.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 19).ItemType, type, m_Tool, DefCarpentry.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 19));
            }
            else if (IsFrom == "Misc")
            {
                Type type = null;

                CraftContext context = DefCarpentry.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefCarpentry.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 28).UseSubRes2 ? DefCarpentry.CraftSystem.CraftSubRes2 : DefCarpentry.CraftSystem.CraftSubRes);
                int resIndex = (DefCarpentry.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 28).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefCarpentry.CraftSystem.CreateItem(m_Mobile, DefCarpentry.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 28).ItemType, type, m_Tool, DefCarpentry.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 28));
            }
        }
    }
}