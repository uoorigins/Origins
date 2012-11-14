using System;
using Server;
using System.Collections;
using Server.Engines.Craft;
using Server.Network;
using Server.Items;

namespace Server.Menus.ItemLists
{
    public class BowFletchingMenu : ItemListMenu
    {
        private Mobile m_Mobile;
        private string IsFrom;
        private BaseTool m_Tool;
        private ItemListEntry[] m_Entries;

        public BowFletchingMenu(Mobile m, ItemListEntry[] entries, string Is, BaseTool tool)
            : base("Choose an item to make.", entries)
        {
            m_Mobile = m;
            IsFrom = Is;
            m_Tool = tool;
            m_Entries = entries;
        }

        public static ItemListEntry[] Main(Mobile from)
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

            ItemListEntry[] entries = new ItemListEntry[DefBowFletching.CraftSystem.CraftItems.Count];

            for (int i = 0; i < DefBowFletching.CraftSystem.CraftItems.Count; ++i)
            {
                chance = DefBowFletching.CraftSystem.CraftItems.GetAt(i).GetSuccessChance(from, typeof(Log), DefBowFletching.CraftSystem, false, ref allRequiredSkills);

                if (chance > 0)
                {
                    type = DefBowFletching.CraftSystem.CraftItems.GetAt(i).ItemType;
                    craftResource = DefBowFletching.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);
                    ResAmount = from.Backpack.GetAmount(typeof(Log));

                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("yC", "y C");
                    name = name.ToLower();
                    itemid = item.ItemID;

                    //shafts
                    if (i == 1)
                    {
                        if (ResAmount != 0)
                            entries[i-missing] = new ItemListEntry(String.Format("arrow shafts using all wood", name), itemid, 0, i);
                        else
                            missing++;// entries[i-missing] = new ItemListEntry("", -1);
                    }
                    //arrows bolts
                    else if (i == 2 || i == 3)
                    {
                        missing++;//entries[i-missing] = new ItemListEntry("", -1);
                    }
                    //kindling and bows
                    else
                    {
                        if ((ResAmount != 0) && (ResAmount >= craftResource.Amount))
                        {
                            if (craftResource.Amount > 1)
                                entries[i-missing] = new ItemListEntry(String.Format("{0}", name, craftResource.Amount), itemid, 0, i);
                            else
                                entries[i - missing] = new ItemListEntry(String.Format("{0}", name, craftResource.Amount), itemid, 0, i);
                        }
                        else
                            missing++;//entries[i-missing] = new ItemListEntry("", -1);

                    }

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
            }
            Array.Resize(ref entries, (DefBowFletching.CraftSystem.CraftItems.Count - missing));
            return entries;
        }

        public static ItemListEntry[] Arrows(Mobile from)
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

            ItemListEntry[] entries = new ItemListEntry[DefBowFletching.CraftSystem.CraftItems.Count];
            for (int i = 0; i < DefBowFletching.CraftSystem.CraftItems.Count; ++i)
            {
                chance = DefBowFletching.CraftSystem.CraftItems.GetAt(i).GetSuccessChance(from, typeof(Log), DefBowFletching.CraftSystem, false, ref allRequiredSkills);

                if (chance > 0)
                {
                    type = DefBowFletching.CraftSystem.CraftItems.GetAt(i).ItemType;
                    craftResource = DefBowFletching.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.ToLower();
                    itemid = item.ItemID;

                    if (i == 2 || i == 3)
                        entries[i-missing] = new ItemListEntry(String.Format("{0}s", name), itemid, 0, i);
                    else
                        missing++;//entries[i-missing] = new ItemListEntry("", -1);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
            }
            Array.Resize(ref entries, (DefBowFletching.CraftSystem.CraftItems.Count - missing));
            return entries;
        }

        public override void OnResponse(NetState state, int index)
        {
            Type type = null;

            CraftContext context = DefBowFletching.CraftSystem.GetContext(m_Mobile);
            CraftSubResCol res = (DefBowFletching.CraftSystem.CraftItems.GetAt(m_Entries[index].craftIndex).UseSubRes2 ? DefBlacksmithy.CraftSystem.CraftSubRes2 : DefBowFletching.CraftSystem.CraftSubRes);
            int resIndex = (DefBowFletching.CraftSystem.CraftItems.GetAt(m_Entries[index].craftIndex).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

            if (resIndex > -1)
            {
                type = res.GetAt(resIndex).ItemType;
            }
            DefBowFletching.CraftSystem.CreateItem(m_Mobile, DefBowFletching.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)).ItemType, type, m_Tool, DefBowFletching.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)));
        }
    }
}