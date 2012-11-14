using System; 
using System.Collections;
using System.Collections.Generic;
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network; 

namespace Server.Mobiles 
{ 
    public class HireSailor : BaseHire 
    {
        [Constructable]
        public HireSailor()
        {
            if (this.Female = Utility.RandomBool())
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
                AddItem(Skirt(Utility.WhiteHue()));
            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");
                AddItem(PlainPants(Utility.WhiteHue()));
                Utility.AssignRandomFacialHair(this);
            }

            SpeechHue = Utility.RandomDyedHue();
            Hue = Utility.RandomSkinHue();

            Title = "the sailor";

            Utility.AssignRandomHair(this);

            SetStr(66, 80);
            SetDex(66, 80);
            SetInt(41, 55);

            SetDamage(8, 20);

            SetSkill(SkillName.Stealing, 66.0, 97.5);
            SetSkill(SkillName.Peacemaking, 65.0, 87.5);
            SetSkill(SkillName.MagicResist, 25.0, 47.5);
            SetSkill(SkillName.Healing, 65.0, 87.5);
            SetSkill(SkillName.Tactics, 65.0, 87.5);
            SetSkill(SkillName.Fencing, 65.0, 87.5);
            SetSkill(SkillName.Parry, 45.0, 60.5);
            SetSkill(SkillName.Lockpicking, 65, 87);
            SetSkill(SkillName.Hiding, 65, 87);
            SetSkill(SkillName.Snooping, 65, 87);

            Karma = 1;

            AddItem(new Shoes(Utility.RandomNeutralHue()));
            AddItem(new Cutlass());

            AddItem(PlainShirt(Utility.WhiteHue()));


            PackGold(0, 25);
        }

	    public override bool ClickTitle{ get{ return false; } }
        public HireSailor( Serial serial ) : base( serial ) 
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
