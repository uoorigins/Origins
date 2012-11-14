using System;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class RangerGuildmaster : BaseGuildmaster
	{
		public override NpcGuild NpcGuild{ get{ return NpcGuild.RangersGuild; } }

		[Constructable]
		public RangerGuildmaster() : base( "ranger" )
		{
			SetSkill( SkillName.AnimalLore, 64.0, 100.0 );
			SetSkill( SkillName.Camping, 75.0, 98.0 );
			SetSkill( SkillName.Hiding, 75.0, 98.0 );
			SetSkill( SkillName.MagicResist, 75.0, 98.0 );
			SetSkill( SkillName.Tactics, 65.0, 88.0 );
			SetSkill( SkillName.Archery, 90.0, 100.0 );
			SetSkill( SkillName.Tracking, 90.0, 100.0 );
			SetSkill( SkillName.Stealth, 60.0, 83.0 );
			SetSkill( SkillName.Fencing, 36.0, 68.0 );
			SetSkill( SkillName.Herding, 36.0, 68.0 );
			SetSkill( SkillName.Swords, 45.0, 68.0 );
		}

		public RangerGuildmaster( Serial serial ) : base( serial )
		{
		}

        public override void InitOutfit()
        {
            AddItem(new Server.Items.FancyShirt(Utility.RandomGreenHue()));
            if (Female)
                AddItem(Skirt(Utility.BrownHue()));
            else
                AddItem(new Server.Items.LongPants(Utility.BrownHue()));

            int hairHue = GetHairHue();

            Utility.AssignRandomHair(this, hairHue);
            Utility.AssignRandomFacialHair(this, hairHue);
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