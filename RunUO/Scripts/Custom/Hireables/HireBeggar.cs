using System; 
using System.Collections;
using System.Collections.Generic;
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network; 

namespace Server.Mobiles 
{ 
    public class HireBeggar : BaseHire 
    {
        [Constructable]
        public HireBeggar()
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
                AddItem(new ShortPants(Utility.RandomAllColors()));
                Utility.AssignRandomFacialHair(this);
            }
            Title = "the beggar";

            Utility.AssignRandomHair(this);

            SetStr(16, 40);
            SetDex(16, 35);
            SetInt(16, 45);

            SetDamage(5, 13);

            SetSkill(SkillName.Begging, 66, 97);
            SetSkill(SkillName.Tactics, 55, 77);
            SetSkill(SkillName.Wrestling, 55, 77);
            SetSkill(SkillName.Magery, 55, 77);

            Karma = 1;

            AddItem(new Sandals());

            AddItem(new Shirt(Utility.RandomAllColors()));

            PackGold(0, 25);

        }
        
	    public override bool ClickTitle{ get{ return false; } }
        public HireBeggar( Serial serial ) : base( serial ) 
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
