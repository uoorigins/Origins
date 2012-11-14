using System;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class Jeweler : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public Jeweler() : base( "the jeweler" )
		{
			SetSkill( SkillName.ItemID, 64.0, 100.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBJewel() );
		}

		public Jeweler( Serial serial ) : base( serial )
		{
		}

        public override void InitOutfit()
        {
            AddItem(new Server.Items.FancyShirt(Utility.RandomAllColors()));
            AddItem(new Server.Items.LongPants(Utility.RandomAllColors()));
            AddItem(RandomShoes(Utility.RandomNeutralHue()));

            int hairHue = GetHairHue();

            Utility.AssignRandomHair(this, hairHue);
            Utility.AssignRandomFacialHair(this, hairHue);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
            AddLootPouch(LootPack.PoorPouch);
            AddLoot(LootPack.AveragePile);
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}