using System; 
using System.Collections;
using System.Collections.Generic;
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network; 

namespace Server.Mobiles 
{ 
    public class HirePaladin : BaseHire 
    {
        [Constructable]
        public HirePaladin()
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
            Title = "the paladin";

            switch (Utility.Random(5))
            {
                case 0: break;
                case 1: AddItem(new Bascinet()); break;
                case 2: AddItem(new CloseHelm()); break;
                case 3: AddItem(new NorseHelm()); break;
                case 4: AddItem(new Helmet()); break;

            }

            Utility.AssignRandomHair(this);

            SetStr(64, 92);
            SetDex(46, 64);
            SetInt(37, 49);

            SetDamage(8, 20);

            SetSkill(SkillName.Swords, 55.0, 77.5);
            SetSkill(SkillName.MagicResist, 55.0, 77.5);
            SetSkill(SkillName.Tactics, 55.0, 77.5);
            SetSkill(SkillName.Wrestling, 55.0, 77.5);
            SetSkill(SkillName.Parry, 55.0, 77.5);

            Karma = 1;

            AddItem(RandomBoots());
            AddItem(new VikingSword());
            AddItem(new HeaterShield());

            AddItem(RandomHelmet());
            AddItem(RandomOverArmor(Utility.RandomAllColors()));

            AddItem(new PlateChest());
            AddItem(new PlateLegs());
            AddItem(new PlateArms());
            AddItem(new PlateGloves());
            AddItem(new PlateGorget());
            PackGold(20, 100);

        }

	    public override bool ClickTitle{ get{ return false; } }
        public HirePaladin( Serial serial ) : base( serial ) 
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
