using System; 
using System.Collections; 
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network; 

namespace Server.Mobiles 
{ 
    public class HireBard : BaseHire 
    {
        [Constructable]
        public HireBard()
        {
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();

            if (this.Female = Utility.RandomBool())
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
                AddItem(Skirt(Utility.RandomAllColors()));
            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");
                AddItem(PlainPants(Utility.RandomAllColors()));
                Utility.AssignRandomFacialHair(this);
            }
            Title = "the bard";

            Utility.AssignRandomHair(this);

            SetStr(16, 30);
            SetDex(16, 40);
            SetInt(16, 40);

            SetDamage(8, 20);

            SetSkill(SkillName.Tactics, 35, 57);
            SetSkill(SkillName.Magery, 22, 22);
            SetSkill(SkillName.Swords, 45, 67);
            SetSkill(SkillName.Archery, 36, 67);
            SetSkill(SkillName.Parry, 45, 60);
            SetSkill(SkillName.Musicianship, 66.0, 97.5);
            SetSkill(SkillName.Peacemaking, 65.0, 87.5);

            Karma = 1;

            AddItem(RandomShoes(Utility.RandomNeutralHue()));
            AddItem(PlainShirt(Utility.RandomAllColors()));

            switch (Utility.Random(4))
            {
                case 0: PackItem(new Harp()); break;
                case 1: PackItem(new Lute()); break;
                case 2: PackItem(new Drums()); break;
                case 3: PackItem(new Tambourine()); break;
            }

            PackGold(10, 50);

        }

	    public override bool ClickTitle{ get{ return false; } }
        public HireBard( Serial serial ) : base( serial ) 
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
