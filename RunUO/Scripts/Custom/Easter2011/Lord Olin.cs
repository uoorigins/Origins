using System;
using Server.Items;
using Server;
using Server.Misc;

namespace Server.Mobiles
{
    public class Olin : BaseCreature
    {
        [Constructable]
        public Olin() : base(AIType.AI_Archer, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            InitStats(500, 400, 100);
            Title = "the head hunter";

            SpeechHue = Utility.RandomDyedHue();

            Hue = 0x840E;

            Body = 0x190;
            Name = "Lord Olin";

            HairItemID = 8252;
            HairHue = 1107;
            FacialHairItemID = 0;

            AddItem(new StuddedChest());
            AddItem(new PlateArms());
            AddItem(new StuddedGloves());
            AddItem(new PlateGorget());
            AddItem(new StuddedLegs());
            AddItem(new ThighBoots(1907));
            AddItem(new Cloak(2118));
            AddItem(new BodySash(2118));
            AddItem(new Bow());

            Container pack = new Backpack();

            pack.Movable = false;

            Arrow arrows = new Arrow(250);

            arrows.LootType = LootType.Newbied;

            pack.DropItem(arrows);
            pack.DropItem(new Gold(10, 25));

            AddItem(pack);

            Skills[SkillName.Anatomy].Base = 120.0;
            Skills[SkillName.Tactics].Base = 120.0;
            Skills[SkillName.Archery].Base = 120.0;
            Skills[SkillName.MagicResist].Base = 120.0;
            Skills[SkillName.DetectHidden].Base = 100.0;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
            AddLootPouch(LootPack.PoorPouch);
            AddLoot(LootPack.PoorPile);
        }


        public override bool CanTeach { get { return false; } }
        public override bool ClickTitle { get { return false; } }
        public override bool CanBeDamaged() { return false; }
        public override bool Unprovokable { get { return true; } }

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
                if (Insensitive.Speech(e.Speech, "burian") || Insensitive.Speech(e.Speech, "enemy"))
                {
                    this.Say(true, "This fool raises the rabbits.. He will never solve the infestation!");
                }
                else if (Insensitive.Speech(e.Speech, "easter"))
                {
                    this.Say(true, "The easter bunny can be lured if enough offspring disappear. It would make a nice trophy.");
                }
                else if (Insensitive.Speech(e.Speech, "bunny") || Insensitive.Speech(e.Speech, "rabbit") || Insensitive.Speech(e.Speech, "bunnies") || Insensitive.Speech(e.Speech, "rabbits"))
                {
                    this.Say(true, "Hah. Grab a weapon and help us rid of them. You'll be doing these lands a favor.");
                }
                else if (Insensitive.Speech(e.Speech, "job") || (Insensitive.Speech(e.Speech, "who are you")))
                {
                    this.Say(true, "I am the head hunter, in charge of riding these lands of the pests.");
                }
                else if (Insensitive.Contains(e.Speech, "document"))
                {
                    this.Say(true, "Hmm. I have been missing a report now that you mention it...");
                }
                else
                    base.OnSpeech(e);
            }
        }

        public Olin(Serial serial) : base(serial)
        {
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
