using System;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class Miner : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public Miner() : base( "the miner" )
		{
			SetSkill( SkillName.Mining, 65.0, 88.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBMiner() );
		}

        public override Item ShoeType
        {
            get
            {
                return RandomBoots(Utility.BrownHue());
            }
        }

		public override void InitOutfit()
		{
			AddItem( PlainShirt( Utility.GreyHue()));
            if (Female)
                AddItem(PlainPants(Utility.RandomBlueHue()));
            else
                AddItem(Skirt(Utility.RandomBlueHue()));

			AddItem( new Server.Items.Pickaxe() );

            int hairHue = GetHairHue();

            Utility.AssignRandomHair(this, hairHue);
            Utility.AssignRandomFacialHair(this, hairHue);
		}

		public Miner( Serial serial ) : base( serial )
		{
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