using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Accounting;
using Server.Mobiles;

namespace Server.Misc
{
    public class Coal : IronOre
    {
        [Constructable]
        public Coal()
            : this(1)
        {
        }

        [Constructable]
        public Coal(int amount)
            : base(amount)
        {
            Name = "coal";
            Hue = 1109;
        }

        public Coal(Serial serial) : base(serial)
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

        public static bool CanMakeDyetub(Mobile from)
        {
            return (DateTime.Now.Day == 17 || DateTime.Now.Day == 11 || DateTime.Now.DayOfWeek == DayOfWeek.Wednesday) && Utility.RandomBool();
        }
    }

    public class GiftBag : Bag
    {
        [Constructable]
        public GiftBag(bool nice)
        {
            Item item = null;

            Hue = Utility.RandomList(32, 77, 2301);

            if (nice)
            {
                Name = "Happy Holidays!";
                DropItem(MakeNewbie(new WristWatch()));
                if (Utility.RandomBool())
                {
                    item = new Cake() { ItemID = 4164 };
                    item.Hue = 432;
                    item.Name = "fruit cake";
                    DropItem(MakeNewbie(item));
                }
                else
                {
                    DropItem(MakeNewbie(new Pizza()));
                }

                if (Utility.RandomBool())
                    DropItem(MakeNewbie(new BeverageBottle(BeverageType.Champagne)));
                else
                    DropItem(MakeNewbie(new BeverageBottle(BeverageType.Eggnog)));

                DropItem(MakeNewbie(new Dates()));

                item = new Goblet();
                item.Name = "a champagne glass";
                item.Hue = Utility.RandomList(77, 34);
                DropItem(MakeNewbie(item));

                /*item = new Goblet();
                item.Name = "a champagne glass";
                item.Hue = 34;
                DropItem(MakeNewbie(item));*/

                DropItem(MakeNewbie(new FireworksWand(100)));

                item = new Item(5359);
                item.Hue = Utility.RandomList(32, 77, 2301);
                item.Name = "Seasons Greetings";
                DropItem(MakeNewbie(item));
            }
            else
            {
                Name = "You were naughty this year!";

                DropItem(MakeNewbie(new Cake() { ItemID = 4164, Name = "spam" })); // spam

                DropItem(MakeNewbie(new Coal()));

                item = new Kindling();
                item.Name = "switches";
                DropItem(item); // not newbied...

                item = new Item(5359);
                item.Hue = Utility.RandomList(32, 77, 2301);
                item.Name = "Maybe next year you will get a nicer gift.";
                DropItem(MakeNewbie(item));
            }
        }

        private static Item MakeNewbie(Item item)
        {
            item.LootType = LootType.Newbied;
            return item;
        }

        public GiftBag(Serial serial)
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

    public class WristWatch : Clock
    {
        [Constructable]
        public WristWatch()
            : base(4230)
        {
            Weight = 1.0;
            Layer = Layer.Bracelet;
            Name = "a wrist watch";
        }

        public WristWatch(Serial serial)
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

    public class ChristmasGifts
    {
        public static void AddGifts(Mobile m)
        {

            /*if (!m.Player || !(m.Account is Account))
                return;
            Account acct = (Account)m.Account;

            if (acct.LastLogin + TimeSpan.FromDays(90) < DateTime.Now)
                return;

            if (m is PlayerMobile && ((PlayerMobile)m).GameTime < TimeSpan.FromHours(1))
                return;*/

            m.AddToBackpack(new GiftBag(m.Karma > -40));
        }
    }
}
