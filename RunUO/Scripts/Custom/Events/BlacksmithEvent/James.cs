using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
    public class JamestheSmith : BlacksmithGuildmaster
    {
        private DateTime LastSpammed;

        [Constructable]
        public JamestheSmith()
        {
            SpeechHue = Utility.RandomDyedHue();
            
            Hue = 0x83EA;
            Body = 0x190;
            Name = "James";
            Title = "the apprentice blacksmith";
            HairItemID = 0x2047;
            HairHue = 1126;
            FacialHairItemID = 0x2040;
            FacialHairHue = 1126;

            Tag = "miningnomoongate";

            SetStr(90, 100);
            SetDex(16, 40);
            SetInt(16, 40);

            SetDamage(8, 20);

            SetSkill(SkillName.Tactics, 35, 57);
            SetSkill(SkillName.Magery, 22, 22);
            SetSkill(SkillName.Swords, 45, 67);
            SetSkill(SkillName.Archery, 36, 67);
            SetSkill(SkillName.Parry, 45, 60);
            SetSkill(SkillName.Blacksmith, 66.0, 97.5);
            SetSkill(SkillName.Mining, 65.0, 87.5);

            Karma = 1;

            AddItem(new PlateChest(1710));
            AddItem(new PlateArms(1710));
            AddItem(new PlateGorget(1710));
            AddItem(new PlateGloves(1710));
            AddItem(new SmithHammer());
            AddItem(new LongPants(1891));
            AddItem(new Boots(2101));
            AddItem(new HalfApron(1831));

            PackGold(10, 50);
        }

        public override bool ClickTitle { get { return true; } }
        public override bool CanBeDamaged() { return false; }
        public override bool Unprovokable { get { return true; } }

        public override void InitOutfit()
        {
        }

        public JamestheSmith(Serial serial) : base(serial)
        {
        }

        public override bool HandlesOnSpeech(Mobile from)
        {
            if (from.InRange(this.Location, 4))
                return true;

            return base.HandlesOnSpeech(from);
        }

        public override void OnSpeech(SpeechEventArgs e)
        {
            if (!e.Handled && e.Mobile.InRange(this.Location, 2))
            {
                if (Insensitive.Speech(e.Speech, "avatar"))
                {
                    this.Say(true, "Huh, that sounds familiar... Don't tell my master but it may be the reason we need more supplies!");
                }
                else if (Insensitive.Speech(e.Speech, "master") || Insensitive.Speech(e.Speech, "supplies") || Insensitive.Speech(e.Speech, "donate") || Insensitive.Speech(e.Speech, "ingots") || Insensitive.Speech(e.Speech, "iron"))
                {
                    this.Say(true, "Whilst my master was fighting the gargoyles, our large stock had dwindled and are seeking ingots for a prize.");
                }
                else if (Insensitive.Speech(e.Speech, "job") || Insensitive.Speech(e.Speech, "gargoyles") || Insensitive.Speech(e.Speech, "stock") || Insensitive.Speech(e.Speech, "stockpile") || (Insensitive.Speech(e.Speech, "who are you")))
                {
                    this.Say(true, "I am an apprentice blacksmith. My master has brought back a mighty gift from the gargoyles.");
                }
                else if (Insensitive.Contains(e.Speech, "gift") || Insensitive.Contains(e.Speech, "prize"))
                {
                    this.Say(true, "Alas, if thoust donate enough iron ingots inside the tent, you may receive our prize.");
                }
                else
                    base.OnSpeech(e);
            }
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (m is PlayerMobile && DateTime.Now - LastSpammed > TimeSpan.FromMinutes(5.0))
            {
                this.Say(true, "Hail adventurer! Come help us by donating iron ingots to our stockpile for a prize!");
                LastSpammed = DateTime.Now;
            }

            base.OnMovement(m, oldLocation);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
