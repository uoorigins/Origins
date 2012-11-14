using System;
using Server;
using System.Collections;
using Server.Engines.Craft;
using Server.Network;
using Server.Items;

namespace Server.Menus.ItemLists
{
    public class TailoringMenu : ItemListMenu
    {
        private Mobile m_Mobile;
        private string IsFrom;
        private BaseTool m_Tool;
        private ItemListEntry[] m_Entries;

        public TailoringMenu(Mobile m, ItemListEntry[] entries, string Is, BaseTool tool) : base("What would you like to make?", entries)
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
            ItemListEntry[] entries = new ItemListEntry[4];
            int ResAmount = from.Backpack.GetAmount(typeof(Cloth));
            int missing = 0;

            type = DefTailoring.CraftSystem.CraftItems.GetAt(12).ItemType;
            craftResource = DefTailoring.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
            if (ResAmount >= craftResource.Amount)
                entries[0-missing] = new ItemListEntry("Shirts", 0x1517,0,0);
            else
                missing++;// entries[0] = new ItemListEntry("", -1);

            type = DefTailoring.CraftSystem.CraftItems.GetAt(6).ItemType;
            craftResource = DefTailoring.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
            if (ResAmount >= craftResource.Amount)
                entries[1 - missing] = new ItemListEntry("Pants", 0x1539, 0, 1);
            else
                missing++;// entries[1] = new ItemListEntry("", -1);

            type = DefTailoring.CraftSystem.CraftItems.GetAt(9).ItemType;
            craftResource = DefTailoring.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
            if (ResAmount >= craftResource.Amount)
                entries[2 - missing] = new ItemListEntry("Miscellaneous", 0x153D, 0, 2);
            else
                missing++;// entries[2] = new ItemListEntry("", -1);

            type = DefTailoring.CraftSystem.CraftItems.GetAt(14).ItemType;
            craftResource = DefTailoring.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
            if (ResAmount >= craftResource.Amount)
                entries[3 - missing] = new ItemListEntry("Bolt of Cloth", 0x0F95, 0, 3);
            else
                missing++;// entries[3] = new ItemListEntry("", -1);

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        public static ItemListEntry[] Shirts(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item;
            CraftRes craftResource;
            bool allRequiredSkills = true;
            double chance;
            int ResAmount;
            int missing = 0;

            ItemListEntry[] entries = new ItemListEntry[6];

            for (int i = 0; i < 6; ++i)
            {
                chance = DefTailoring.CraftSystem.CraftItems.GetAt(i).GetSuccessChance(from, typeof(Cloth), DefTailoring.CraftSystem, false, ref allRequiredSkills);
                type = DefTailoring.CraftSystem.CraftItems.GetAt(i).ItemType;
                craftResource = DefTailoring.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                if ((chance > 0) && (from.Backpack.GetAmount(typeof(Cloth)) >= (craftResource).Amount))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("yS", "y S");
                    name = name.Replace("nD", "n D");
                    name = name.Replace("yD", "y D");
                    name = name.Replace("rS", "r S");
                    name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} cloth)", name, craftResource.Amount), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        public static ItemListEntry[] Pants(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item;
            CraftRes craftResource;
            bool allRequiredSkills = true;
            double chance;
            int ResAmount;
            int missing = 0;

            ItemListEntry[] entries = new ItemListEntry[3];

            for (int i = 0; i < 3; ++i)
            {
                chance = DefTailoring.CraftSystem.CraftItems.GetAt(i + 6).GetSuccessChance(from, typeof(Cloth), DefTailoring.CraftSystem, false, ref allRequiredSkills);
                type = DefTailoring.CraftSystem.CraftItems.GetAt(i + 6).ItemType;
                craftResource = DefTailoring.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                if ((chance > 0) && (from.Backpack.GetAmount(typeof(Cloth)) >= (craftResource).Amount))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("tP", "t P");
                    name = name.Replace("gP", "g P");
                    name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} cloth)", name, craftResource.Amount), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
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
            int ResAmount;
            int missing = 0;

            ItemListEntry[] entries = new ItemListEntry[5];

            for (int i = 0; i < 5; ++i)
            {
                chance = DefTailoring.CraftSystem.CraftItems.GetAt(i + 9).GetSuccessChance(from, typeof(Cloth), DefTailoring.CraftSystem, false, ref allRequiredSkills);
                type = DefTailoring.CraftSystem.CraftItems.GetAt(i + 9).ItemType;
                craftResource = DefTailoring.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                if ((chance > 0) && (from.Backpack.GetAmount(typeof(Cloth)) >= (craftResource).Amount))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("yS", "y S");
                    name = name.Replace("fA", "f A");
                    name = name.Replace("lA", "l A");
                    name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} cloth)", name, craftResource.Amount), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        public static ItemListEntry[] Bolt(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item;
            CraftRes craftResource;
            bool allRequiredSkills = true;
            double chance;
            int ResAmount;
            int missing = 0;

            ItemListEntry[] entries = new ItemListEntry[3];

            for (int i = 0; i < 3; ++i)
            {
                chance = DefTailoring.CraftSystem.CraftItems.GetAt(i + 14).GetSuccessChance(from, typeof(Cloth), DefTailoring.CraftSystem, false, ref allRequiredSkills);
                type = DefTailoring.CraftSystem.CraftItems.GetAt(i + 14).ItemType;
                craftResource = DefTailoring.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                if ((chance > 0) && (from.Backpack.GetAmount(typeof(Cloth)) >= (craftResource).Amount))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("tO", "t O");
                    name = name.Replace("fC", "f C");
                    name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} cloth)", name, craftResource.Amount), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        //LEATHER////////////////////////////////////////////////////////
        public static ItemListEntry[] LeatherMain(Mobile from)
        {
            Type type;
            CraftRes craftResource;
            ItemListEntry[] entries = new ItemListEntry[4];
            int ResAmount = from.Backpack.GetAmount(typeof(Leather));
            ResAmount += from.Backpack.GetAmount(typeof(Hides));
            int missing = 0;

            type = DefTailoring.CraftSystem.CraftItems.GetAt(17).ItemType;
            craftResource = DefTailoring.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
            if (ResAmount >= craftResource.Amount)
                entries[0 - missing] = new ItemListEntry("Footwear", 5904, 0, 0);
            else
                missing++;// entries[0] = new ItemListEntry("", -1);

            type = DefTailoring.CraftSystem.CraftItems.GetAt(22).ItemType;
            craftResource = DefTailoring.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
            if (ResAmount >= craftResource.Amount)
                entries[1 - missing] = new ItemListEntry("Leather Armor", 5068, 0, 1);
            else
                missing++;//entries[1] = new ItemListEntry("", -1);

            type = DefTailoring.CraftSystem.CraftItems.GetAt(27).ItemType;
            craftResource = DefTailoring.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
            if (ResAmount >= craftResource.Amount)
                entries[2 - missing] = new ItemListEntry("Studded Armor", 5083, 0, 2);
            else
                missing++;//entries[2] = new ItemListEntry("", -1);

            type = DefTailoring.CraftSystem.CraftItems.GetAt(32).ItemType;
            craftResource = DefTailoring.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
            if (ResAmount >= craftResource.Amount)
                entries[3-missing] = new ItemListEntry("Female Armor", 7172,0,3);
            else
                missing++;//entries[3] = new ItemListEntry("", -1);

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        public static ItemListEntry[] Footwear(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item;
            CraftRes craftResource;
            bool allRequiredSkills = true;
            double chance;
            int ResAmount;
            int missing = 0;

            ItemListEntry[] entries = new ItemListEntry[4];

            for (int i = 0; i < 4; ++i)
            {
                chance = DefTailoring.CraftSystem.CraftItems.GetAt(i + 17).GetSuccessChance(from, typeof(Leather), DefTailoring.CraftSystem, false, ref allRequiredSkills);
                type = DefTailoring.CraftSystem.CraftItems.GetAt(i + 17).ItemType;
                craftResource = DefTailoring.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                if ((chance > 0) && ((from.Backpack.GetAmount(typeof(Leather)) >= (craftResource).Amount) || (from.Backpack.GetAmount(typeof(Hides)) >= (craftResource).Amount)))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("hB", "h B");
                    name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} leather)", name, craftResource.Amount), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        public static ItemListEntry[] Leather(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item;
            CraftRes craftResource;
            bool allRequiredSkills = true;
            double chance;
            int ResAmount;
            int missing = 0;

            ItemListEntry[] entries = new ItemListEntry[5];

            for (int i = 0; i < 5; ++i)
            {
                chance = DefTailoring.CraftSystem.CraftItems.GetAt(i + 21).GetSuccessChance(from, typeof(Leather), DefTailoring.CraftSystem, false, ref allRequiredSkills);
                type = DefTailoring.CraftSystem.CraftItems.GetAt(i + 21).ItemType;
                craftResource = DefTailoring.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                if ((chance > 0) && ((from.Backpack.GetAmount(typeof(Leather)) >= (craftResource).Amount) ||(from.Backpack.GetAmount(typeof(Hides)) >= (craftResource).Amount)))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("rG", "r G");
                    name = name.Replace("rC", "r C");
                    name = name.Replace("rA", "r A");
                    name = name.Replace("rL", "r L");
                    name = name.Replace("Arms", "Sleeves");
                    name = name.Replace("Legs", "Leggings");
                    name = name.Replace("Chest", "Armor");
                    name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} leather)", name, craftResource.Amount), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        public static ItemListEntry[] Studded(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item;
            CraftRes craftResource;
            bool allRequiredSkills = true;
            double chance;
            int ResAmount;
            int missing = 0;

            ItemListEntry[] entries = new ItemListEntry[5];

            for (int i = 0; i < 5; ++i)
            {
                chance = DefTailoring.CraftSystem.CraftItems.GetAt(i + 26).GetSuccessChance(from, typeof(Leather), DefTailoring.CraftSystem, false, ref allRequiredSkills);
                type = DefTailoring.CraftSystem.CraftItems.GetAt(i + 26).ItemType;
                craftResource = DefTailoring.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                if ((chance > 0) && ((from.Backpack.GetAmount(typeof(Leather)) >= (craftResource).Amount) || (from.Backpack.GetAmount(typeof(Hides)) >= (craftResource).Amount)))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("dG", "d G");
                    name = name.Replace("dC", "d C");
                    name = name.Replace("dA", "d A");
                    name = name.Replace("dL", "d L");
                    name = name.Replace("Arms", "Sleeves");
                    name = name.Replace("Legs", "Leggings");
                    name = name.Replace("Chest", "Armor");
                    name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} leather)", name, craftResource.Amount), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        public static ItemListEntry[] Female(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item;
            CraftRes craftResource;
            bool allRequiredSkills = true;
            double chance;
            int ResAmount;
            int missing = 0;

            ItemListEntry[] entries = new ItemListEntry[6];

            for (int i = 0; i < 6; ++i)
            {
                chance = DefTailoring.CraftSystem.CraftItems.GetAt(i + 31).GetSuccessChance(from, typeof(Leather), DefTailoring.CraftSystem, false, ref allRequiredSkills);
                type = DefTailoring.CraftSystem.CraftItems.GetAt(i + 31).ItemType;
                craftResource = DefTailoring.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                if ((chance > 0) && ((from.Backpack.GetAmount(typeof(Leather)) >= (craftResource).Amount) || from.Backpack.GetAmount(typeof(Hides)) >= (craftResource).Amount) )
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("rS", "r S");
                    name = name.Replace("rB", "r B");
                    name = name.Replace("dB", "d B");
                    name = name.Replace("rA", "r A");
                    name = name.Replace("eL", "e L");
                    name = name.Replace("eS", "e S");
                    name = name.Replace("rC", "r C");
                    name = name.Replace("dC", "d C");
                    name = name.Replace("Chest", "Armor");
                    name = name.Replace(" Arms", "");
                    name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0} ({1} leather)", name, craftResource.Amount), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        public override void OnResponse(NetState state, int index)
        {
            if (IsFrom == "Main")
            {
                if(Entries[index].craftIndex == 0)
                {
                    IsFrom = "Shirts";
                    m_Mobile.SendMenu(new TailoringMenu(m_Mobile, Shirts(m_Mobile), IsFrom, m_Tool));
                }
                else if(Entries[index].craftIndex == 1)
                {
                    IsFrom = "Pants";
                    m_Mobile.SendMenu(new TailoringMenu(m_Mobile, Pants(m_Mobile), IsFrom, m_Tool));
                }
                else if(Entries[index].craftIndex == 2)
                {
                    IsFrom = "Misc";
                    m_Mobile.SendMenu(new TailoringMenu(m_Mobile, Misc(m_Mobile), IsFrom, m_Tool));
                }
                else if(Entries[index].craftIndex == 3)
                {
                    IsFrom = "BoltOfCloth";
                    m_Mobile.SendMenu(new TailoringMenu(m_Mobile, Bolt(m_Mobile), IsFrom, m_Tool));
                }

            }
            else if (IsFrom == "LeatherMain")
            {
                if(Entries[index].craftIndex == 0)
                {
                    IsFrom = "Footwear";
                    m_Mobile.SendMenu(new TailoringMenu(m_Mobile, Footwear(m_Mobile), IsFrom, m_Tool));
                }
                else if(Entries[index].craftIndex == 1)
                {
                    IsFrom = "Leather";
                    m_Mobile.SendMenu(new TailoringMenu(m_Mobile, Leather(m_Mobile), IsFrom, m_Tool));
                }
                else if(Entries[index].craftIndex == 2)
                {
                    IsFrom = "Studded";
                    m_Mobile.SendMenu(new TailoringMenu(m_Mobile, Studded(m_Mobile), IsFrom, m_Tool));
                }
                else if(Entries[index].craftIndex == 3)
                {
                    IsFrom = "Female";
                    m_Mobile.SendMenu(new TailoringMenu(m_Mobile, Female(m_Mobile), IsFrom, m_Tool));
                }
            }
            else if (IsFrom == "Shirts")
            {
                Type type = null;

                CraftContext context = DefTailoring.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)).UseSubRes2 ? DefTailoring.CraftSystem.CraftSubRes2 : DefTailoring.CraftSystem.CraftSubRes);
                int resIndex = (DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefTailoring.CraftSystem.CreateItem(m_Mobile, DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)).ItemType, typeof(Cloth), m_Tool, DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)));
            }
            else if (IsFrom == "Pants")
            {
                Type type = null;

                CraftContext context = DefTailoring.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 6).UseSubRes2 ? DefTailoring.CraftSystem.CraftSubRes2 : DefTailoring.CraftSystem.CraftSubRes);
                int resIndex = (DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 6).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefTailoring.CraftSystem.CreateItem(m_Mobile, DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 6).ItemType, typeof(Cloth), m_Tool, DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 6));
            }
            else if (IsFrom == "Misc")
            {
                Type type = null;

                CraftContext context = DefTailoring.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 9).UseSubRes2 ? DefTailoring.CraftSystem.CraftSubRes2 : DefTailoring.CraftSystem.CraftSubRes);
                int resIndex = (DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 9).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefTailoring.CraftSystem.CreateItem(m_Mobile, DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 9).ItemType, typeof(Cloth), m_Tool, DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 9));
            }
            else if (IsFrom == "BoltOfCloth")
            {
                Type type = null;

                CraftContext context = DefTailoring.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 14).UseSubRes2 ? DefTailoring.CraftSystem.CraftSubRes2 : DefTailoring.CraftSystem.CraftSubRes);
                int resIndex = (DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 14).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefTailoring.CraftSystem.CreateItem(m_Mobile, DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 14).ItemType, typeof(Cloth), m_Tool, DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 14));
            }
            else if (IsFrom == "Footwear")
            {
                Type type = null;

                CraftContext context = DefTailoring.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 17).UseSubRes2 ? DefTailoring.CraftSystem.CraftSubRes2 : DefTailoring.CraftSystem.CraftSubRes);
                int resIndex = (DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 17).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefTailoring.CraftSystem.CreateItem(m_Mobile, DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 17).ItemType, typeof(Leather), m_Tool, DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 17));
            }
            else if (IsFrom == "Leather")
            {
                Type type = null;

                CraftContext context = DefTailoring.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 21).UseSubRes2 ? DefTailoring.CraftSystem.CraftSubRes2 : DefTailoring.CraftSystem.CraftSubRes);
                int resIndex = (DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 21).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefTailoring.CraftSystem.CreateItem(m_Mobile, DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 21).ItemType, typeof(Leather), m_Tool, DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 21));
            }
            else if (IsFrom == "Studded")
            {
                Type type = null;

                CraftContext context = DefTailoring.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 26).UseSubRes2 ? DefTailoring.CraftSystem.CraftSubRes2 : DefTailoring.CraftSystem.CraftSubRes);
                int resIndex = (DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 26).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefTailoring.CraftSystem.CreateItem(m_Mobile, DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 26).ItemType, typeof(Leather), m_Tool, DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 26));
            }
            else if (IsFrom == "Female")
            {
                Type type = null;

                CraftContext context = DefTailoring.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 31).UseSubRes2 ? DefTailoring.CraftSystem.CraftSubRes2 : DefTailoring.CraftSystem.CraftSubRes);
                int resIndex = (DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 31).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefTailoring.CraftSystem.CreateItem(m_Mobile, DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 31).ItemType, typeof(Leather), m_Tool, DefTailoring.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 31));
            }
        }
    }
}