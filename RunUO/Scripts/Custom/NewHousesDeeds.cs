using Server;
using System;
using System.Collections;
using Server.Multis;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Multis.Deeds
{
    public class BlacksmithsShopDeed : HouseDeed
    {
        [Constructable]
        public BlacksmithsShopDeed() : base(0x6C, new Point3D(0, 4, 0))
        {
        }

        public BlacksmithsShopDeed(Serial serial) : base(serial)
        {
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a deed to a blacksmith's shop"));
            }
        }

        public override BaseHouse GetHouse(Mobile owner)
        {
            return new BlacksmithsShop(owner, 0x6C);
        }

        public override int LabelNumber { get { return 1041225; } }
        public override Rectangle2D[] Area { get { return BlacksmithsShop.AreaArray; } }

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

    public class ClothiersShopDeed : HouseDeed
    {
        [Constructable]
        public ClothiersShopDeed() : base(0x6C, new Point3D(0, 4, 0))
        {
        }

        public ClothiersShopDeed(Serial serial) : base(serial)
        {
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a deed to a clothier's shop"));
            }
        }

        public override BaseHouse GetHouse(Mobile owner)
        {
            return new ClothiersShop(owner, 0x6C);
        }

        public override int LabelNumber { get { return 1041226; } }
        public override Rectangle2D[] Area { get { return ClothiersShop.AreaArray; } }

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

    public class MillersShopDeed : HouseDeed
    {
        [Constructable]
        public MillersShopDeed() : base(0x6C, new Point3D(0, 4, 0))
        {
        }

        public MillersShopDeed(Serial serial) : base(serial)
        {
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a deed to a miller's shop"));
            }
        }

        public override BaseHouse GetHouse(Mobile owner)
        {
            return new MillersShop(owner, 0x6C);
        }

        public override int LabelNumber { get { return 1041227; } }
        public override Rectangle2D[] Area { get { return MillersShop.AreaArray; } }

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

    public class LargeSmithyDeed : HouseDeed
    {
        [Constructable]
        public LargeSmithyDeed() : base(0x8C, new Point3D(-4, 7, 0))
        {
            Name = "deed to a large smithy";
        }

        public LargeSmithyDeed(Serial serial) : base(serial)
        {
        }

        public override void OnSingleClick(Mobile from)
        {
            /*if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {*/
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a deed to a large smithy"));
            //}
        }

        public override BaseHouse GetHouse(Mobile owner)
        {
            return new LargeSmithy(owner);
        }

        //public override int LabelNumber { get { return 1041231; } }
        public override Rectangle2D[] Area { get { return LargeSmithy.AreaArray; } }

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

    public class LargeMillersShopDeed : HouseDeed
    {
        [Constructable]
        public LargeMillersShopDeed() : base(0x8C, new Point3D(-4, 7, 0))
        {
        }

        public LargeMillersShopDeed(Serial serial) : base(serial)
        {
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a deed to a large bakery"));
            }
        }

        public override BaseHouse GetHouse(Mobile owner)
        {
            return new LargeMillersShop(owner);
        }

        public override int LabelNumber { get { return 1041230; } }
        public override Rectangle2D[] Area { get { return LargeMillersShop.AreaArray; } }

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

    public class LargeClothiersShopDeed : HouseDeed
    {
        [Constructable]
        public LargeClothiersShopDeed() : base(0x8C, new Point3D(-4, 7, 0))
        {
        }

        public LargeClothiersShopDeed(Serial serial) : base(serial)
        {
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a deed to a large tailor shop"));
            }
        }

        public override BaseHouse GetHouse(Mobile owner)
        {
            return new LargeClothiersShop(owner);
        }

        public override int LabelNumber { get { return 1041232; } }
        public override Rectangle2D[] Area { get { return LargeClothiersShop.AreaArray; } }

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

    public class WeaponTrainingHutDeed : HouseDeed
    {
        [Constructable]
        public WeaponTrainingHutDeed() : base(0x6C, new Point3D(0, 4, 0))
        {
        }

        public WeaponTrainingHutDeed(Serial serial) : base(serial)
        {
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a deed to a weapon training hut"));
            }
        }

        public override BaseHouse GetHouse(Mobile owner)
        {
            return new WeaponTrainingHut(owner, 0x6C);
        }

        public override int LabelNumber { get { return 1041229; } }
        public override Rectangle2D[] Area { get { return WeaponTrainingHut.AreaArray; } }

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

    public class PickpocketsDenDeed : HouseDeed
    {
        [Constructable]
        public PickpocketsDenDeed() : base(0x6C, new Point3D(0, 4, 0))
        {
        }

        public PickpocketsDenDeed(Serial serial) : base(serial)
        {
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a deed to a pickpocket's den"));
            }
        }

        public override BaseHouse GetHouse(Mobile owner)
        {
            return new PickpocketsDen(owner, 0x6C);
        }

        public override int LabelNumber { get { return 1041228; } }
        public override Rectangle2D[] Area { get { return PickpocketsDen.AreaArray; } }

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