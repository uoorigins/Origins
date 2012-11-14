using System; 
using System.Collections;
using System.Collections.Generic;
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network; 

namespace Server.Mobiles 
{ 
    public class HirePeasant : BaseHire 
    {
        [Constructable]
        public HirePeasant()
        {
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();

            if (this.Female = Utility.RandomBool())
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");
                Utility.AssignRandomFacialHair(this);
            }
            Title = "the peasant";

            Utility.AssignRandomHair(this);

            SetStr(26, 40);
            SetDex(21, 35);
            SetInt(16, 30);

            SetDamage(5, 13);

            SetSkill(SkillName.Tactics, 55, 77);
            SetSkill(SkillName.Wrestling, 55, 77);
            SetSkill(SkillName.Swords, 55, 77);

            Karma = Utility.Random(10);

            AddItem(PlainShirt(Utility.RandomAllColors()));

            if (Female)
                AddItem(Skirt(Utility.RandomAllColors()));
            else
                AddItem(PlainPants(Utility.RandomAllColors()));

            PackGold(0, 25);
        }

	    public override bool ClickTitle{ get{ return false; } }
        public HirePeasant( Serial serial ) : base( serial ) 
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
