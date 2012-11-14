using System;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class BlacksmithGuildmaster : BaseGuildmaster
	{
		public override NpcGuild NpcGuild{ get{ return NpcGuild.BlacksmithsGuild; } }

		public override bool IsActiveVendor{ get{ return true; } }

		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public BlacksmithGuildmaster() : base( "blacksmith" )
		{
			SetSkill( SkillName.ArmsLore, 65.0, 88.0 );
			SetSkill( SkillName.Blacksmith, 90.0, 100.0 );
			SetSkill( SkillName.Macing, 36.0, 68.0 );
			SetSkill( SkillName.Parry, 36.0, 68.0 );
		}
		public override void InitSBInfo()
		{
			SBInfos.Add( new SBBlacksmith() );
		}

		public override Item ShoeType
		{
			get{ return null; }
		}

		public override void InitOutfit()
		{
			base.InitOutfit();
            
            AddItem(new Server.Items.FullApron(Utility.BlackHue()));

            AddItem(RandomWeapon());

            AddItem(new Server.Items.Bascinet() { Hue = Utility.RandomBool() ? 0 : Utility.RandomMetalHue() } );
		}

		public BlacksmithGuildmaster( Serial serial ) : base( serial )
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