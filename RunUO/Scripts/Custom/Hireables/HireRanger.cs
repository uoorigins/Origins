using System; 
using System.Collections;
using System.Collections.Generic;
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network; 

namespace Server.Mobiles 
{ 
    public class HireRanger : BaseHire 
    {
        [Constructable]
        public HireRanger() : base(AIType.AI_Archer)
        {
            Title = "the ranger";
            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();

            if (this.Female = Utility.RandomBool())
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
                AddItem(Skirt(Utility.BrownHue()));
            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");
                AddItem(PlainPants(Utility.BrownHue()));
                Utility.AssignRandomFacialHair(this);
            }

            Utility.AssignRandomHair(this);

            SetStr(71, 85);
            SetDex(76, 90);
            SetInt(61, 75);

            SetDamage(13, 24);

            SetSkill(SkillName.Wrestling, 15, 37);
            SetSkill(SkillName.Parry, 45, 60);
            SetSkill(SkillName.Archery, 66, 97);
            SetSkill(SkillName.Magery, 62, 62);
            SetSkill(SkillName.Swords, 35, 57);
            SetSkill(SkillName.Fencing, 15, 37);
            SetSkill(SkillName.Tactics, 65, 87);

            Karma = 1;

            AddItem(PlainShirt(Utility.RandomGreenHue()));

            AddItem(new Bow());
            PackItem(new Arrow(40));
            PackGold(10, 75);

            VirtualArmor = 16;
        }
        
        public override bool ClickTitle{ get{ return false; } }

        public HireRanger( Serial serial ) : base( serial ) 
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
