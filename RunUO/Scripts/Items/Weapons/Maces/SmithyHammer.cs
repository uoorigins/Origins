using System;
using Server.Engines.Craft;
using Server.Network;
using Server.Menus.ItemLists;
using Server.Items;

namespace Server.Items
{
    [FlipableAttribute(0x13E3, 0x13E4)]
    public class SmithyHammer : BaseBashing
    {
        public override string AsciiName { get { return "smith's hammer"; } }

        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ShadowStrike; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.Dismount; } }

        public override int AosStrengthReq { get { return 40; } }
        public override int AosMinDamage { get { return 11; } }
        public override int AosMaxDamage { get { return 13; } }
        public override int AosSpeed { get { return 44; } }

        public override int OldStrengthReq { get { return 30; } }
        public override int OldMinDamage { get { return 4; } }
        public override int OldMaxDamage { get { return 20; } }
        public override int OldSpeed { get { return 30; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 60; } }

        private int m_UsesRemaining;
        private bool m_ShowUsesRemaining;

        [CommandProperty(AccessLevel.GameMaster)]
        public int UsesRemaining
        {
            get { return m_UsesRemaining; }
            set { m_UsesRemaining = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ShowUsesRemaining
        {
            get { return m_ShowUsesRemaining; }
            set { m_ShowUsesRemaining = value; InvalidateProperties(); }
        }

        [Constructable]
        public SmithyHammer() : base(0x13E3)
        {
            Weight = 8.0;
            Layer = Layer.OneHanded;
        }

        [Constructable]
        public SmithyHammer(int uses) : base(0x13E3)
        {
            Weight = 8.0;
            Layer = Layer.OneHanded;
        }

        public SmithyHammer(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack) || Parent == from)
            {
                bool anvil, forge;
                DefBlacksmithy.CheckAnvilAndForge(from, 2, out anvil, out forge);

                if (anvil && forge)
                {
                    BaseTool m_Tool = new SmithHammer();
                    string IsFrom = "Main";
                    from.SendMenu(new BlacksmithMenu(from, BlacksmithMenu.Main(from), IsFrom, m_Tool));

                    if (m_Tool != null)
                        m_Tool.Delete();
                }
                else
                    from.SendAsciiMessage("You must be near an anvil and a forge to smith items.");
            }
            else
                from.SendAsciiMessage("That must be in your pack for you to use it.");

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}