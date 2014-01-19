using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Factions;
using Server.Misc;

namespace Server.Mobiles
{
    [CorpseName("an abominable snowman corpse")]
    public class AbominableSnowman : BaseCreature
    {
        //public override Faction FactionAllegiance { get { return Minax.Instance; } }
        public override Ethics.Ethic EthicAllegiance { get { return Ethics.Ethic.Evil; } }

        [Constructable]
        public AbominableSnowman()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an abominable snowman";
            Body = 83;
            BaseSoundID = 427;

            Hue = 1154;

            SetStr(767*2, 945*2);
            SetDex(66*2, 75*2);
            SetInt(46*2, 70*2);

            SetHits(666*2, 755*2);

            SetDamage(30, 35);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 45, 55);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.MagicResist, 125.1, 140.0);
            SetSkill(SkillName.Tactics, 90.1, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 100.0);

            Fame = 15000;
            Karma = -15000;

            VirtualArmor = 50;

            // 25% chance of drop
            if (Utility.RandomDouble() > 0.75)
            {
                switch (Utility.RandomMinMax(0, 9))
                {
                    case 9:
                        PackItem(MakeNewbie(new Cake() { ItemID = 4164, Name = "spam" }));
                        break;
                    case 8:
                        PackItem(new Coal());
                        break;
                    case 7:
                        PackItem(MakeNewbie(new Spyglass()));
                        break;
                    case 6:
                        PackItem(new RangerLegs());
                        break;
                    case 5:
                        PackItem(new RangerGorget());
                        break;
                    case 4:
                        PackItem(new RangerGloves());
                        break;
                    case 3:
                        PackItem(new RangerChest());
                        break;
                    case 2:
                        PackItem(new RangerArms());
                        break;
                    case 1:
                        PackItem(MakeNewbie(new ClothingBlessDeed()));
                        break;
                    default:
                    case 0:
                        PackItem(MakeNewbie(new HolidayTreeDeed()));
                        break;
                }
            }
        }

        private static Item MakeNewbie(Item item)
        {
            item.LootType = LootType.Newbied;
            return item;
        }

        public override void GenerateLoot()
        {
            AddLootBackpack(LootPack.Rich);
        }

        public override bool CanRummageCorpses { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Regular; } }
        public override int TreasureMapLevel { get { return 3; } }
        public override int Meat { get { return 2; } }

        public AbominableSnowman(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}