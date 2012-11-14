using System;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class Ranger : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public Ranger() : base( "the ranger" )
		{
			SetSkill( SkillName.Camping, 55.0, 78.0 );
			SetSkill( SkillName.DetectHidden, 65.0, 88.0 );
			SetSkill( SkillName.Hiding, 45.0, 68.0 );
			SetSkill( SkillName.Archery, 65.0, 88.0 );
			SetSkill( SkillName.Tracking, 65.0, 88.0 );
			SetSkill( SkillName.Veterinary, 60.0, 83.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBRanger() );
		}

		public override void InitOutfit()
		{
			//base.InitOutfit();

			AddItem( PlainShirt( Utility.RandomGreenHue() ) );
			AddItem( PlainPants( Utility.BrownHue() ) );
			AddItem( new Server.Items.Bow() );

            PackItem(new Server.Items.Arrow(40));

            int hairHue = GetHairHue();

            Utility.AssignRandomHair(this, hairHue);
            Utility.AssignRandomFacialHair(this, hairHue);
		}

		public Ranger( Serial serial ) : base( serial )
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