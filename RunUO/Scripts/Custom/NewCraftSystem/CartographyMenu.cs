using System;
using Server;
using System.Collections;
using Server.Engines.Craft;
using Server.Network;
using Server.Items;

namespace Server.Menus.ItemLists
{
    public class CartographyMenu : ItemListMenu
    {
        private Mobile m_Mobile;
        private string IsFrom;
        private BaseTool m_Tool;
        private ItemListEntry[] m_Entries;

        public CartographyMenu(Mobile m, ItemListEntry[] entries, string Is, BaseTool tool)
            : base("Attempt what scale of map?", entries)
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
            int ResAmount;
            int missing = 0;

            ItemListEntry[] entries = new ItemListEntry[DefCartography.CraftSystem.CraftItems.Count];

            for (int i = 0; i < DefCartography.CraftSystem.CraftItems.Count; ++i)
            {
                chance = DefCartography.CraftSystem.CraftItems.GetAt(i).GetSuccessChance(from, typeof(BlankMap), DefCartography.CraftSystem, false, ref allRequiredSkills);

                if (chance > 0)
                {
                    type = DefCartography.CraftSystem.CraftItems.GetAt(i).ItemType;
                    craftResource = DefCartography.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("lM", "l M");
                    name = name.Replace("yM", "y M");
                    name = name.Replace("aC", "a C");
                    name = name.Replace("dM", "d M");
                    name = name.Replace("local map", "A map of the local environs.");
                    name = name.Replace("city map", "A map of suitable for cities.");
                    name = name.Replace("sea chart", "A moderately sized sea chart.");
                    name = name.Replace("world map", "A map of the world.");
                    name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0}", name), 6511 + i,0,i);

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
            Type type = null;

            CraftContext context = DefCartography.CraftSystem.GetContext(m_Mobile);
            CraftSubResCol res = (DefCartography.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)).UseSubRes2 ? DefCartography.CraftSystem.CraftSubRes2 : DefCartography.CraftSystem.CraftSubRes);
            int resIndex = (DefCartography.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

            if (resIndex > -1)
            {
                type = res.GetAt(resIndex).ItemType;
            }

            DefCartography.CraftSystem.CreateItem(m_Mobile, DefCartography.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)).ItemType, typeof(BlankMap), m_Tool, DefCartography.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)));
        }
    }
}