using System;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class TinkerGuildmaster : BaseGuildmaster
	{
		public override NpcGuild NpcGuild{ get{ return NpcGuild.TinkersGuild; } }

		[Constructable]
		public TinkerGuildmaster() : base( "tinker" )
		{
			SetSkill( SkillName.Lockpicking, 65.0, 88.0 );
			SetSkill( SkillName.Tinkering, 90.0, 100.0 );
			SetSkill( SkillName.RemoveTrap, 85.0, 100.0 );
		}

		public TinkerGuildmaster( Serial serial ) : base( serial )
		{
		}

        public override void InitOutfit()
        {
            AddItem(new Server.Items.FancyShirt(Utility.RandomOrangeHue()));
            if (Female)
                AddItem(Skirt(Utility.BlackHue()));
            else
                AddItem(new Server.Items.LongPants(Utility.BlackHue()));

            AddItem(new Server.Items.Shoes(Utility.RandomNeutralHue()));

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