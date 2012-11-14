using System;
using Server;
using System.Collections;
using Server.Engines.Craft;
using Server.Network;
using Server.Items;

namespace Server.Menus.ItemLists
{
    public class AlchemyMenu : ItemListMenu
    {
        private Mobile m_Mobile;
        private string IsFrom;
        private BaseTool m_Tool;
        private ItemListEntry[] m_Entries;

        public AlchemyMenu(Mobile m, ItemListEntry[] entries, string Is, BaseTool tool) : base("Choose a formula.", entries)
        {
            m_Mobile = m;
            IsFrom = Is;
            m_Tool = tool;
            m_Entries = entries;
        }

        public static ItemListEntry[] Refresh(Mobile from)
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

            ItemListEntry[] entries = new ItemListEntry[2];
            
            for (int i = 0; i < 2; ++i)
            {
                chance = DefAlchemy.CraftSystem.CraftItems.GetAt(i).GetSuccessChance(from, typeof(BlackPearl), DefAlchemy.CraftSystem, false, ref allRequiredSkills);

                if ((chance > 0) && (from.Backpack.GetAmount(typeof(BlackPearl)) >= DefAlchemy.CraftSystem.CraftItems.SearchFor(DefAlchemy.CraftSystem.CraftItems.GetAt(i).ItemType).Ressources.GetAt(0).Amount))
                {
                    type = DefAlchemy.CraftSystem.CraftItems.GetAt(i).ItemType;
                    craftResource = DefAlchemy.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("hP", "h P");
                    name = name.Replace("lR", "l R");
                    name = name.Replace(" Potion", "");
                    //name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0}", name, craftResource.Amount), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
            }
            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        public static ItemListEntry[] Agility(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item;
            CraftRes craftResource;
            bool allRequiredSkills = true;
            double chance;
            int missing = 0;

            ItemListEntry[] entries = new ItemListEntry[2];

            for (int i = 0; i < 2; ++i)
            {
                chance = DefAlchemy.CraftSystem.CraftItems.GetAt(i + 2).GetSuccessChance(from, typeof(Bloodmoss), DefAlchemy.CraftSystem, false, ref allRequiredSkills);

                if ((chance > 0) && (from.Backpack.GetAmount(typeof(Bloodmoss)) >= DefAlchemy.CraftSystem.CraftItems.SearchFor(DefAlchemy.CraftSystem.CraftItems.GetAt(i+2).ItemType).Ressources.GetAt(0).Amount))
                {
                    type = DefAlchemy.CraftSystem.CraftItems.GetAt(i + 2).ItemType;
                    craftResource = DefAlchemy.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("rA", "r A");
                    name = name.Replace("yP", "y P");
                    name = name.Replace(" Potion", "");
                    //name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0}", name, craftResource.Amount), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
            }
            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        public static ItemListEntry[] NightSight(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item;
            CraftRes craftResource;
            bool allRequiredSkills = true;
            double chance;
            int missing = 0;
            ItemListEntry[] entries = new ItemListEntry[1];

            for (int i = 0; i < 1; ++i)
            {
                chance = DefAlchemy.CraftSystem.CraftItems.GetAt(i + 4).GetSuccessChance(from, typeof(SpidersSilk), DefAlchemy.CraftSystem, false, ref allRequiredSkills);

                if ((chance > 0) && (from.Backpack.GetAmount(typeof(SpidersSilk)) >= DefAlchemy.CraftSystem.CraftItems.SearchFor(DefAlchemy.CraftSystem.CraftItems.GetAt(i+4).ItemType).Ressources.GetAt(0).Amount))
                {
                    type = DefAlchemy.CraftSystem.CraftItems.GetAt(i + 4).ItemType;
                    craftResource = DefAlchemy.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("tP", "t P");
                    name = name.Replace(" Potion", "");
                    //name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0}", name, craftResource.Amount), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
            }
            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        public static ItemListEntry[] Heal(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item;
            CraftRes craftResource;
            bool allRequiredSkills = true;
            double chance;
            int missing = 0;

            ItemListEntry[] entries = new ItemListEntry[3];

            for (int i = 0; i < 3; ++i)
            {
                chance = DefAlchemy.CraftSystem.CraftItems.GetAt(i + 5).GetSuccessChance(from, typeof(Ginseng), DefAlchemy.CraftSystem, false, ref allRequiredSkills);

                if ((chance > 0) && (from.Backpack.GetAmount(typeof(Ginseng)) >= DefAlchemy.CraftSystem.CraftItems.SearchFor(DefAlchemy.CraftSystem.CraftItems.GetAt(i+5).ItemType).Ressources.GetAt(0).Amount))
                {
                    type = DefAlchemy.CraftSystem.CraftItems.GetAt(i + 5).ItemType;
                    craftResource = DefAlchemy.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("rH", "r H");
                    name = name.Replace("lP", "l P");
                    name = name.Replace(" Potion", "");
                    //name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0}", name, craftResource.Amount), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
            }
            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        public static ItemListEntry[] Strength(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item;
            CraftRes craftResource;
            bool allRequiredSkills = true;
            double chance;
            int missing = 0;

            ItemListEntry[] entries = new ItemListEntry[2];

            for (int i = 0; i < 2; ++i)
            {
                chance = DefAlchemy.CraftSystem.CraftItems.GetAt(i + 8).GetSuccessChance(from, typeof(MandrakeRoot), DefAlchemy.CraftSystem, false, ref allRequiredSkills);

                if ((chance > 0) && (from.Backpack.GetAmount(typeof(MandrakeRoot)) >= DefAlchemy.CraftSystem.CraftItems.SearchFor(DefAlchemy.CraftSystem.CraftItems.GetAt(i+8).ItemType).Ressources.GetAt(0).Amount))
                {
                    type = DefAlchemy.CraftSystem.CraftItems.GetAt(i + 8).ItemType;
                    craftResource = DefAlchemy.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("hP", "h P");
                    name = name.Replace("rS", "r S");
                    name = name.Replace(" Potion", "");
                    //name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0}", name, craftResource.Amount), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
            }
            Array.Resize(ref entries, (entries.Length - missing));

            return entries;
        }

        public static ItemListEntry[] Poison(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item;
            CraftRes craftResource;
            bool allRequiredSkills = true;
            double chance;
            int missing = 0;

            ItemListEntry[] entries = new ItemListEntry[4];

            for (int i = 0; i < 4; ++i)
            {
                chance = DefAlchemy.CraftSystem.CraftItems.GetAt(i + 10).GetSuccessChance(from, typeof(Nightshade), DefAlchemy.CraftSystem, false, ref allRequiredSkills);

                if ((chance > 0) && (from.Backpack.GetAmount(typeof(Nightshade)) >= DefAlchemy.CraftSystem.CraftItems.SearchFor(DefAlchemy.CraftSystem.CraftItems.GetAt(i+10).ItemType).Ressources.GetAt(0).Amount))
                {
                    type = DefAlchemy.CraftSystem.CraftItems.GetAt(i + 10).ItemType;
                    craftResource = DefAlchemy.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("nP", "n P");
                    name = name.Replace("rP", "r P");
                    name = name.Replace("yP", "y P");
                    name = name.Replace(" Potion", "");
                    //name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0}", name, craftResource.Amount), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
            }
            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        public static ItemListEntry[] Cure(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item;
            CraftRes craftResource;
            bool allRequiredSkills = true;
            double chance;
            int missing = 0;

            ItemListEntry[] entries = new ItemListEntry[3];

            for (int i = 0; i < 3; ++i)
            {
                chance = DefAlchemy.CraftSystem.CraftItems.GetAt(i + 14).GetSuccessChance(from, typeof(Garlic), DefAlchemy.CraftSystem, false, ref allRequiredSkills);

                if ((chance > 0) && (from.Backpack.GetAmount(typeof(Garlic)) >= DefAlchemy.CraftSystem.CraftItems.SearchFor(DefAlchemy.CraftSystem.CraftItems.GetAt(i+14).ItemType).Ressources.GetAt(0).Amount))
                {
                    type = DefAlchemy.CraftSystem.CraftItems.GetAt(i + 14).ItemType;
                    craftResource = DefAlchemy.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("eP", "e P");
                    name = name.Replace("rC", "r C");
                    name = name.Replace(" Potion", "");
                    //name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0}", name, craftResource.Amount), itemid, 0, i);

                    if (item != null)
                        item.Delete();
                }
                else
                    missing++;//entries[i-missing] = new ItemListEntry("", -1);
            }
            Array.Resize(ref entries, (entries.Length - missing));
            return entries;
        }

        public static ItemListEntry[] Explosion(Mobile from)
        {
            Type type;
            int itemid;
            string name;
            Item item;
            CraftRes craftResource;
            bool allRequiredSkills = true;
            double chance;
            int missing = 0;

            ItemListEntry[] entries = new ItemListEntry[3];

            for (int i = 0; i < 3; ++i)
            {
                chance = DefAlchemy.CraftSystem.CraftItems.GetAt(i + 17).GetSuccessChance(from, typeof(SulfurousAsh), DefAlchemy.CraftSystem, false, ref allRequiredSkills);

                if ((chance > 0) && (from.Backpack.GetAmount(typeof(SulfurousAsh)) >= DefAlchemy.CraftSystem.CraftItems.SearchFor(DefAlchemy.CraftSystem.CraftItems.GetAt(i+17).ItemType).Ressources.GetAt(0).Amount))
                {
                    type = DefAlchemy.CraftSystem.CraftItems.GetAt(i + 17).ItemType;
                    craftResource = DefAlchemy.CraftSystem.CraftItems.SearchFor(type).Ressources.GetAt(0);

                    item = null;
                    try { item = Activator.CreateInstance(type) as Item; }
                    catch { }
                    name = item.GetType().Name;
                    name = name.Replace("nP", "n P");
                    name = name.Replace("rE", "r E");
                    name = name.Replace(" Potion", "");
                    //name = name.ToLower();
                    itemid = item.ItemID;

                    entries[i-missing] = new ItemListEntry(String.Format("{0}", name, craftResource.Amount), itemid, 0, i);

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
            if (m_Mobile.Backpack.GetAmount(typeof(Bottle)) == 0)
            {
                m_Mobile.SendAsciiMessage("You need an empty bottle to make a potion.");
                return;
            }

            if (IsFrom == "Refresh")
            {
                Type type = null;

                CraftContext context = DefAlchemy.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)).UseSubRes2 ? DefAlchemy.CraftSystem.CraftSubRes2 : DefAlchemy.CraftSystem.CraftSubRes);
                int resIndex = (DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefAlchemy.CraftSystem.CreateItem(m_Mobile, DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)).ItemType, typeof(BlackPearl), m_Tool, DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)));
            }
            else if (IsFrom == "Agility")
            {
                Type type = null;

                CraftContext context = DefAlchemy.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)+2).UseSubRes2 ? DefAlchemy.CraftSystem.CraftSubRes2 : DefAlchemy.CraftSystem.CraftSubRes);
                int resIndex = (DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex)+2).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefAlchemy.CraftSystem.CreateItem(m_Mobile, DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 2).ItemType, typeof(Bloodmoss), m_Tool, DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 2));
            }
            else if (IsFrom == "NightSight")
            {
                Type type = null;

                CraftContext context = DefAlchemy.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 4).UseSubRes2 ? DefAlchemy.CraftSystem.CraftSubRes2 : DefAlchemy.CraftSystem.CraftSubRes);
                int resIndex = (DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 4).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

               /* int OldHue = m_Mobile.SpeechHue;
                m_Mobile.SpeechHue = 0x22;
                m_Mobile.SayTo(m_Mobile, true, String.Format("*{0} begins grinding some spiders' silk in a mortar*", m_Mobile.Name));
                m_Mobile.SpeechHue = OldHue;*/

                DefAlchemy.CraftSystem.CreateItem(m_Mobile, DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 4).ItemType, typeof(SpidersSilk), m_Tool, DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 4));
            }
            else if (IsFrom == "Heal")
            {
                Type type = null;

                CraftContext context = DefAlchemy.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 5).UseSubRes2 ? DefAlchemy.CraftSystem.CraftSubRes2 : DefAlchemy.CraftSystem.CraftSubRes);
                int resIndex = (DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 5).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefAlchemy.CraftSystem.CreateItem(m_Mobile, DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 5).ItemType, typeof(Ginseng), m_Tool, DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 5));
            }
            else if (IsFrom == "Strength")
            {
                Type type = null;

                CraftContext context = DefAlchemy.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 8).UseSubRes2 ? DefAlchemy.CraftSystem.CraftSubRes2 : DefAlchemy.CraftSystem.CraftSubRes);
                int resIndex = (DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 8).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefAlchemy.CraftSystem.CreateItem(m_Mobile, DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 8).ItemType, typeof(MandrakeRoot), m_Tool, DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 8));
            }
            else if (IsFrom == "Poison")
            {
                Type type = null;

                CraftContext context = DefAlchemy.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 10).UseSubRes2 ? DefAlchemy.CraftSystem.CraftSubRes2 : DefAlchemy.CraftSystem.CraftSubRes);
                int resIndex = (DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 10).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefAlchemy.CraftSystem.CreateItem(m_Mobile, DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 10).ItemType, typeof(Nightshade), m_Tool, DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 10));
            }
            else if (IsFrom == "Cure")
            {
                Type type = null;

                CraftContext context = DefAlchemy.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 14).UseSubRes2 ? DefAlchemy.CraftSystem.CraftSubRes2 : DefAlchemy.CraftSystem.CraftSubRes);
                int resIndex = (DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 14).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefAlchemy.CraftSystem.CreateItem(m_Mobile, DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 14).ItemType, typeof(Garlic), m_Tool, DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 14));
            }
            else if (IsFrom == "Explosion")
            {
                Type type = null;

                CraftContext context = DefAlchemy.CraftSystem.GetContext(m_Mobile);
                CraftSubResCol res = (DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 17).UseSubRes2 ? DefAlchemy.CraftSystem.CraftSubRes2 : DefAlchemy.CraftSystem.CraftSubRes);
                int resIndex = (DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 17).UseSubRes2 ? context.LastResourceIndex2 : context.LastResourceIndex);

                if (resIndex > -1)
                {
                    type = res.GetAt(resIndex).ItemType;
                }

                DefAlchemy.CraftSystem.CreateItem(m_Mobile, DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 17).ItemType, typeof(SulfurousAsh), m_Tool, DefAlchemy.CraftSystem.CraftItems.GetAt((m_Entries[index].craftIndex) + 17));
            }
        }
    }
}