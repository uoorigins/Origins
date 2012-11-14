using System; 
using System.Collections;
using System.Collections.Generic;
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network; 

namespace Server.Mobiles 
{ 
    public class HireFighter : BaseHire 
    {
        [Constructable]
        public HireFighter()
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
            Title = "the fighter";

            Utility.AssignRandomHair(this);

            SetStr(11, 88);
            SetDex(11, 88);
            SetInt(7, 42);

            SetDamage(8, 20);

            SetSkill(SkillName.Tactics, 45.0, 70.0);
            SetSkill(SkillName.Swords, 45.0, 70.0);
            SetSkill(SkillName.Parry, 45.0, 70.0);
            SetSkill(SkillName.Macing, 45.0, 70.0);
            SetSkill(SkillName.Wrestling, 45.0, 70.0);

            Karma = 1;

            Item weapon = RandomAxe();
            AddItem(weapon);

            if (weapon.Layer != Layer.TwoHanded)
                AddItem(new WoodenShield());

            AddItem(RandomHeadArmor());

            AddItem(new StuddedChest());
            AddItem(new StuddedArms());
            AddItem(new StuddedGloves());
            AddItem(new StuddedGorget());
            AddItem(new StuddedLegs());

            PackGold(25, 100);
        }


	    public override bool ClickTitle{ get{ return false; } }
        public HireFighter( Serial serial ) : base( serial ) 
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
