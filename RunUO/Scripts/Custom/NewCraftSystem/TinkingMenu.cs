using System;
using Server;
using System.Collections;
using Server.Engines.Craft;
using Server.Network;
using Server.Items;

namespace Server.Menus.ItemLists
{
    public class TinkeringMenu : ItemListMenu
    {
        private Mobile m_Mobile;
        private string IsFrom;
        private BaseTool m_Tool;
        private ItemListEntry[] m_Entries;

        public TinkeringMenu(Mobile m, ItemListEntry[] entries, string Is, BaseTool tool) : base("Choose an item.", entries)
        {
            m_Mobile = m;
            IsFrom = Is;
            m_Tool = tool;
            m_Entries = entries;
        }

        public static ItemListEntry[] Wood(Mobile from)
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
                chance = DefTinkering.CraftSystem.CraftItems.GetAt(i).GetSuccessChance(from, typeof(Log), DefTinkering.CraftSystem, false, ref allRequiredSkills);
                type = DefTinkering.CraftSystem.CraftItems.GetAt(i).ItemType;
                craftResource = DefTinkering.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                if ((chance > 0) && (from.Backpack.GetAmount(typeof(Log)) >= (craftResource).Amount))
                {
                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("gP", "g P");
                    name = name.Replace("kF", "k F");
                    name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0}", name), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        public static ItemListEntry[] Metal(Mobile from)
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

            ItemListEntry[] entries = new ItemListEntry[20];

            entries[0] = new ItemListEntry("Dart Trap", 4397, 0, 0);
            entries[1] = new ItemListEntry("Explosion Trap", 4344, 0, 1);
            entries[2] = new ItemListEntry("Poison Trap", 4424, 0, 2);

            /*if ((from.Backpack.GetAmount(typeof(Bolt)) > 0) && (from.Backpack.GetAmount(typeof(IronIngot)) > 0))
                entries[0] = new ItemListEntry("dart trap", 4397);
            else
                entries[0] = new ItemListEntry("", -1);

            if ((from.Backpack.GetAmount(typeof(BaseExplosionPotion)) > 0) && (from.Backpack.GetAmount(typeof(IronIngot)) > 0))
                entries[1] = new ItemListEntry("explosion trap", 4344);
            else
                entries[1] = new ItemListEntry("", -1);

            if ((from.Backpack.GetAmount(typeof(BasePoisonPotion)) > 0) && (from.Backpack.GetAmount(typeof(IronIngot)) > 0))
                entries[2] = new ItemListEntry("poison trap", 4424);
            else
                entries[2] = new ItemListEntry("", -1);*/

            for (int i = 3; i < 20; ++i)
            {
                chance = DefTinkering.CraftSystem.CraftItems.GetAt(i + 5).GetSuccessChance(from, typeof(IronIngot), DefTinkering.CraftSystem, false, ref allRequiredSkills);
                type = DefTinkering.CraftSystem.CraftItems.GetAt(i + 5).ItemType;
                craftResource = DefTinkering.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                /*if ((chance > 0) && (from.Backpack.GetAmount(typeof(IronIngot)) >= (craftResource).Amount))
                {*/
                item = null;
                try { item = Activator.CreateInstance(type) as Item; }
                catch { }
                name = item.GetType().Name;
                name = name.Replace("gK", "g K");
                name = name.Replace("kP", "k P");
                name = name.Replace("tP", "t P");
                name = name.Replace("wK", "w K");
                name = name.Replace("lS", "l S");
                name = name.Replace("rT", "r T");
                name = name.Replace("hH", "h H");
                name = name.Replace("eH", "e H");
                name = name.Replace("kR", "k R");
                name = name.ToLower();
                itemid = item.ItemID;

                if (name == "scissors")
                    itemid--;
                else if (name == "tongs")
                    itemid++;
                else if (name == "smith hammer")
                    itemid++;
                else if (name == "shovel")
                    itemid++;

                entries[i-missing] = new ItemListEntry(String.Format("{0}", name), itemid, 0, i);

                if (item != null)
                    item.Delete();
                //}
                //else
                //    missing++;//entries[i-missing] = new ItemListEntry("", -1);
            }

            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        public override void OnResponse(NetState state, int index)
        {
            if (IsFrom == "Wood")
            {
                Type type = null;

                CraftContext context = DefTinkering.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefTinkering.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)).UseSubRes2 ? DefTinkering.CraftSystem.CraftSubRes2 : DefTinkering.CraftSystem.CraftSubRes);
                int resIndex = (DefTinkering.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefTinkering.CraftSystem.CreateItem(m_Mobile, DefTinkering.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)).ItemType, typeof(Log), m_Tool, DefTinkering.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)));
            }
            else if (IsFrom == "Metal")
            {
                Type type = null;

                CraftContext context = DefTinkering.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefTinkering.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex+5)).UseSubRes2 ? DefTinkering.CraftSystem.CraftSubRes2 : DefTinkering.CraftSystem.CraftSubRes);
                int resIndex = (DefTinkering.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex+5)).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefTinkering.CraftSystem.CreateItem(m_Mobile, DefTinkering.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex+5)).ItemType, typeof(IronIngot), m_Tool, DefTinkering.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex+5)));
            }
        }
    }
}