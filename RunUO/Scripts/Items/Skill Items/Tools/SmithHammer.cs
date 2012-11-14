using System;
using Server;
using Server.Engines.Craft;
using Server.Network;
using Server.Menus.ItemLists;

namespace Server.Items
{
	[FlipableAttribute( 0x13E3, 0x13E4 )]
	public class SmithHammer : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefBlacksmithy.CraftSystem; } }

        private int m_Hits;
        [CommandProperty(AccessLevel.GameMaster)]
        public int HitPoints
        {
            get { return m_Hits; }
            set
            {
                if (m_Hits == value)
                    return;

                if (value > m_MaxHits)
                    value = m_MaxHits;

                m_Hits = value;

                InvalidateProperties();
            }
        }

        private int m_MaxHits;
        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxHitPoints
        {
            get { return m_MaxHits; }
            set { m_MaxHits = value; InvalidateProperties(); }
        }

		[Constructable]
		public SmithHammer() : base( 0x13E3 )
		{
			Weight = 8.0;
			Layer = Layer.OneHanded;

            m_Hits = Utility.RandomMinMax(31, 60);
            m_MaxHits = m_Hits;
		}

		[Constructable]
		public SmithHammer( int uses ) : base( uses, 0x13E3 )
		{
			Weight = 8.0;
			Layer = Layer.OneHanded;

            m_Hits = Utility.RandomMinMax(31, 60);
            m_MaxHits = m_Hits;
		}

		public SmithHammer( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a smith's hammer"));
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack) || Parent == from)
            {
                bool anvil, forge;
                DefBlacksmithy.CheckAnvilAndForge(from, 2, out anvil, out forge);

                if (anvil && forge)
                {
                    BaseTool m_Tool = this;
                    string IsFrom = "Main";
                    from.SendMenu(new BlacksmithMenu(from, BlacksmithMenu.Main(from), IsFrom, m_Tool));
                }
                else
                    from.SendAsciiMessage("You must be near an anvil and a forge to smith items.");
            }
            else
                from.SendAsciiMessage("That must be in your pack for you to use it.");
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

            writer.Write((int)m_Hits);
            writer.Write((int)m_MaxHits);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

            if (version > 0)
            {
                m_Hits = reader.ReadInt();
                m_MaxHits = reader.ReadInt();
            }
            else
            {
                m_Hits = Utility.RandomMinMax(31, 60);
                m_MaxHits = m_Hits;
            }
		}
	}
}