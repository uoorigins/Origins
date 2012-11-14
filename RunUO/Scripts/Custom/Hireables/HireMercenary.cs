using System; 
using System.Collections;
using System.Collections.Generic;
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network; 

namespace Server.Mobiles 
{ 
    public class HireMercenary : BaseHire 
    {
        [Constructable]
        public HireMercenary()
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

            SetStr(25, 88);
            SetDex(25, 88);
            SetInt(7, 42);

            SetDamage(8, 20);

            SetSkill(SkillName.Tactics, 45.0, 70.0);
            SetSkill(SkillName.Swords, 45.0, 70.0);
            SetSkill(SkillName.Parry, 45.0, 70.0);
            SetSkill(SkillName.Macing, 45.0, 70.0);
            SetSkill(SkillName.Wrestling, 45.0, 70.0);
            SetSkill(SkillName.ArmsLore, 45.0, 70.0);

            Item weapon = RandomWeapon();
            AddItem(weapon);
            AddItem(RandomChestArmor());
            AddItem(RandomLegArmor());
            AddItem(RandomGloves());
            AddItem(RandomHeadArmor());

            if (weapon.Layer != Layer.TwoHanded)
                AddItem(RandomShield());

            AddItem(RandomGorget());

            Karma = 1;

            PackGold(25, 100);
        }

	    public override bool ClickTitle{ get{ return false; } }
        public HireMercenary(Serial serial)
            : base(serial) 
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
