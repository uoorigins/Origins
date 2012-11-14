using System;
using Server;
using System.Collections;
using Server.Engines.Craft;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Targets;

namespace Server.Menus.ItemLists
{
    public class PolymorphMenu : ItemListMenu
    {
        private class PolymorphCategory
        {
            private PolymorphEntry[] m_Entries;

            public PolymorphCategory(params PolymorphEntry[] entries)
            {
                m_Entries = entries;
            }

            public PolymorphEntry[] Entries { get { return m_Entries; } }
        }

        private static PolymorphCategory[] Categories = new PolymorphCategory[]
			{
				new PolymorphCategory( PolymorphEntry.BlackBear,
                    PolymorphEntry.GrizzlyBear,
                    PolymorphEntry.PolarBear,
                    PolymorphEntry.Chicken,
                    PolymorphEntry.Daemon,
					PolymorphEntry.Dog,
                    PolymorphEntry.Ettin,
                    PolymorphEntry.Gargoyle,
                    PolymorphEntry.Gorilla,
                    PolymorphEntry.HumanMale,
                    PolymorphEntry.HumanFemale,
                    PolymorphEntry.LizardMan,
                    PolymorphEntry.Ogre,
                    PolymorphEntry.Orc,
                    PolymorphEntry.Panther,
                    PolymorphEntry.Slime,
                    PolymorphEntry.Troll,
					PolymorphEntry.Wolf )
			};

        private Mobile m_Caster;
        private Item m_Scroll;

        public PolymorphMenu(Mobile caster, Item scroll, ItemListEntry[] entries) : base("Choose a Creature", entries)
        {
            m_Caster = caster;
            m_Scroll = scroll;
        }

        public static ItemListEntry[] Main()
        {
            PolymorphCategory cat = (PolymorphCategory)Categories[0];
            ItemListEntry[] entries = new ItemListEntry[cat.Entries.Length];

            for (int i = 0; i < cat.Entries.Length; i++)
            {
                PolymorphEntry entry = (PolymorphEntry)cat.Entries[i];

                entries[i] = new ItemListEntry(entry.StringName, entry.ArtID);
            }

            return entries;
        }

        public override void OnResponse(NetState state, int index)
        {
            Spell spell = new PolymorphSpell(m_Caster, m_Scroll, Categories[0].Entries[index].BodyID);
            spell.Cast();
        }
    }
}