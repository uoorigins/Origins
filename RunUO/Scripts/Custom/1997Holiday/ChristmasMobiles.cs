using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName("a corpse of Santa")] 
    public class Santa : BaseCreature
    {
        [Constructable]
        public Santa()
            : base(AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.2, 0.4) 
        {
            Name = "Santa";
            Title = "the bringer of cheer";

            Karma = 1;

            Hue = 0x83ea;
            SpeechHue = Utility.RandomDyedHue();

            SetStr(90, 100);
            SetDex(90, 100);
            SetInt(15, 25);

            Body = 400;

            HairItemID = 8251;
            HairHue = 0;

            FacialHairItemID = 8267;
            FacialHairHue = 0;

            AddItem(new Tunic(0x26));
            AddItem(new LongPants(0x26));
            AddItem(new LeatherGloves() { Hue = 0x455 });
            AddItem(new Boots(0x455));

            SetSkill(SkillName.Tactics, 65, 87.5);
            SetSkill(SkillName.MagicResist, 75, 97.5);
            SetSkill(SkillName.Parry, 65, 87.5);
            SetSkill(SkillName.Magery, 95.1, 105.0);
            SetSkill(SkillName.Wrestling, 20.2, 60);
        }

        public override bool CanTeach { get { return false; } }
        public override bool ClickTitle { get { return false; } }

        public override bool HandlesOnSpeech(Mobile from)
        {
            return false;
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            base.OnMovement(m, oldLocation);

            if (!m.Player || (m.Hidden && m.AccessLevel > AccessLevel.Player))
                return;

            if (!this.InRange(m, 5) || this.InRange(oldLocation, 5))
                return; // only talk when they enter 5 tile range

            //if (50 > Utility.Random(100))
            //    return; // 50% chance to do nothing; 50% chance to talk

            switch (Utility.Random(17))
            {
                case 0: Say(true, "Ho ho ho!"); break;
                case 1: Say(true, "Happy holidays!"); break;
                case 2: Say(true, "A merry season to thee!"); break;
                case 3: Say(true, "Enjoy the holidays!"); break;
                case 4: Say(true, "Ho ho ho! Happy holidays!"); break;
                case 5: Say(true, "May thy holidays be joyful!"); break;
                case 6: Say(true, "Enjoy the season!"); break;
                case 7: Say(true, "I wish thee the joy of the season!"); break;
                case 8: Say(true, "I wish thee joy! Ho ho ho!"); break;
                case 9: Say(true, "Naughty or nice? Hmm."); break;
                case 10: Say(true, "Where IS Rudolph? He's never this late."); break;
                case 11: Say(true, "Dancer, Prancer, don't wander off."); break;
                case 12: Say(true, "If I only had a sleigh..."); break;
                case 13: Say(true, "I think I'm going to cause a worldwide shortage of coal this year."); break;
                case 14: Say(true, "British? Coal or a fruitcake? Hmm."); break;
                case 15: Say(true, "Hmm, coal, or a fruitcake for Blackthorn?"); break;
                case 16: Say(true, "Hmm, I seem to have lost some weight."); break;
            }

            Animate(33, 5, 1, true, false, 0);
        }

        private static Item MakeNewbie(Item item)
        {
            item.LootType = LootType.Newbied;
            return item;
        }

        public override bool CheckGold(Mobile from, Item dropped)
        {
            base.CheckGold(from, dropped);

            HolidayTicket ticket = dropped as HolidayTicket;

            if (ticket != null)
            { 
                if (ticket.Player == from)
                {
                    from.SendAsciiMessage("You recieve a gift from Santa.");

                    switch (Utility.RandomMinMax(0,9))
                    {
                        case 9:
                            from.AddToBackpack(MakeNewbie(new Cake() { ItemID = 4164, Name = "spam" }));
                            break;
                        case 8:
                            from.AddToBackpack(new Coal());
                            break;
                        case 7:
                            from.AddToBackpack(MakeNewbie(new Spyglass()));
                            break;
                        case 6:
                            from.AddToBackpack(new RangerLegs());
                            break;
                        case 5:
                            from.AddToBackpack(new RangerGorget());
                            break;
                        case 4:
                            from.AddToBackpack(new RangerGloves());
                            break;
                        case 3:
                            from.AddToBackpack(new RangerChest());
                            break;
                        case 2:
                            from.AddToBackpack(new RangerArms());
                            break;
                        case 1:
                            from.AddToBackpack(MakeNewbie(new ClothingBlessDeed()));
                            break;
                        default:
                        case 0:
                            from.AddToBackpack(MakeNewbie(new HolidayTreeDeed()));
                            break;
                    }

                    dropped.Delete();

                    return true;
                }
                else
                {
                    Say(true, "Ho ho ho! That is not your ticket.");
                    return false;
                }
            }

            return false;
        }

        private static int GetRandomHue()
        {
            switch (Utility.Random(6))
            {
                default:
                case 0: return 0;
                case 1: return Utility.RandomBlueHue();
                case 2: return Utility.RandomGreenHue();
                case 3: return Utility.RandomRedHue();
                case 4: return Utility.RandomYellowHue();
                case 5: return Utility.RandomNeutralHue();
            }
        }

        public Santa(Serial serial) : base(serial)
        {
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
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

    [CorpseName("a reindeer corpse")]
    public class Reindeer : BaseCreature
    {
        [Constructable]
        public Reindeer()
            : base(AIType.AI_Melee, FightMode.Aggressor, 15, 7, 0.4, 0.75)
        {
            Body = 234;
            Name = Utility.RandomBool() ? "Prancer" : "Dancer";

            SetStr(9000);
            SetHits(41, 71);
            SetDex(9000);
            SetStam(10);
            SetInt(9000);
            SetMana(0);

            SetSkill(SkillName.Wrestling, 90.1, 100);
            SetSkill(SkillName.Fencing, 90.1, 100);
            SetSkill(SkillName.Macing, 90.1, 100);
            SetSkill(SkillName.Swords, 90.1, 100);
            SetSkill(SkillName.DetectHidden, 90.1, 100);
            SetSkill(SkillName.Archery, 90.1, 100);
            SetSkill(SkillName.Parry, 90.1, 100);
            SetSkill(SkillName.Tactics, 90.1, 100);
            SetSkill(SkillName.MagicResist, 90.1, 100);

            VirtualArmor = 32;
            SetDamage(4, 10);
        }

        public override int GetAttackSound()
        {
            return 0x82;
        }

        public override int GetHurtSound()
        {
            return 0x83;
        }

        public override int GetDeathSound()
        {
            return 0x84;
        }

        public override bool Unprovokable { get { return true; } }

        public Reindeer(Serial serial)
            : base(serial)
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