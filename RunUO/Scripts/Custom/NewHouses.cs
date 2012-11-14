using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Multis.Deeds;

namespace Server.Multis
{
    public class BlacksmithsShop : BaseHouse
    {
        public static Rectangle2D[] AreaArray = new Rectangle2D[] { new Rectangle2D(-3, -3, 7, 7), new Rectangle2D(-1, 4, 3, 1) };

        public override Rectangle2D[] Area { get { return AreaArray; } }
        public override Point3D BaseBanLocation { get { return new Point3D(2, 4, 0); } }

        public override int DefaultPrice { get { return 43800; } }

        public override HousePlacementEntry ConvertEntry { get { return HousePlacementEntry.TwoStoryFoundations[0]; } }

        public BlacksmithsShop(Mobile owner, int id) : base(id, owner, 425, 3)
        {
            uint keyValue = CreateKeys(owner);

            AddSouthDoor(0, 3, 7, keyValue);
            AddAnvil(0, 0, 7);
            AddSmallForge(1, 0, 7);

            SetSign(2, 4, 5);
        }

        public BlacksmithsShop(Serial serial) : base(serial)
        {
        }

        public override HouseDeed GetDeed() { return new BlacksmithsShopDeed(); }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);//version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class ClothiersShop : BaseHouse
    {
        public static Rectangle2D[] AreaArray = new Rectangle2D[] { new Rectangle2D(-3, -3, 7, 7), new Rectangle2D(-1, 4, 3, 1) };

        public override Rectangle2D[] Area { get { return AreaArray; } }
        public override Point3D BaseBanLocation { get { return new Point3D(2, 4, 0); } }

        public override int DefaultPrice { get { return 43800; } }

        public override HousePlacementEntry ConvertEntry { get { return HousePlacementEntry.TwoStoryFoundations[0]; } }

        public ClothiersShop(Mobile owner, int id) : base(id, owner, 425, 3)
        {
            uint keyValue = CreateKeys(owner);

            AddSouthDoor(0, 3, 7, keyValue);
            AddLoom(1, -2, 7);
            AddSpinningWheelE(-1, -2, 7);

            SetSign(2, 4, 5);
        }

        public ClothiersShop(Serial serial)
            : base(serial)
        {
        }

        public override HouseDeed GetDeed() { return new ClothiersShopDeed(); }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);//version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class MillersShop : BaseHouse
    {
        public static Rectangle2D[] AreaArray = new Rectangle2D[] { new Rectangle2D(-3, -3, 7, 7), new Rectangle2D(-1, 4, 3, 1) };

        public override Rectangle2D[] Area { get { return AreaArray; } }
        public override Point3D BaseBanLocation { get { return new Point3D(2, 4, 0); } }

        public override int DefaultPrice { get { return 43800; } }

        public override HousePlacementEntry ConvertEntry { get { return HousePlacementEntry.TwoStoryFoundations[0]; } }

        public MillersShop(Mobile owner, int id) : base(id, owner, 425, 3)
        {
            uint keyValue = CreateKeys(owner);

            AddSouthDoor(0, 3, 7, keyValue);
            AddOven(1, -2, 7);
            AddMill(-2, -1, 7);

            SetSign(2, 4, 5);
        }

        public MillersShop(Serial serial) : base(serial)
        {
        }

        public override HouseDeed GetDeed() { return new MillersShopDeed(); }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);//version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class LargeSmithy : BaseHouse
    {
        public static Rectangle2D[] AreaArray = new Rectangle2D[] { new Rectangle2D(-7, -7, 15, 14), new Rectangle2D(-5, 7, 4, 1) };

        public override int DefaultPrice { get { return 152800; } }

        public override HousePlacementEntry ConvertEntry { get { return HousePlacementEntry.ThreeStoryFoundations[29]; } }
        public override int ConvertOffsetY { get { return -1; } }

        public override Rectangle2D[] Area { get { return AreaArray; } }
        public override Point3D BaseBanLocation { get { return new Point3D(1, 8, 0); } }

        public LargeSmithy(Mobile owner) : base(0x8C, owner, 1100, 8)
        {
            uint keyValue = CreateKeys(owner);

            AddSouthDoors(-4, 6, 7, keyValue);

            SetSign(1, 8, 16);

            AddEastDoorShop(1, 4, 7, keyValue);
            AddEastDoorShop(1, -4, 7);
            AddSouthDoorShop(4, -1, 7);

            AddAnvil(5, 4, 7);
            AddLargeForge(3, 2, 7);

        }

        public LargeSmithy(Serial serial) : base(serial)
        {
        }

        public override HouseDeed GetDeed() { return new LargeSmithyDeed(); }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);//version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class LargeMillersShop : BaseHouse
    {
        public static Rectangle2D[] AreaArray = new Rectangle2D[] { new Rectangle2D(-7, -7, 15, 14), new Rectangle2D(-5, 7, 4, 1) };

        public override int DefaultPrice { get { return 152800; } }

        public override HousePlacementEntry ConvertEntry { get { return HousePlacementEntry.ThreeStoryFoundations[29]; } }
        public override int ConvertOffsetY { get { return -1; } }

        public override Rectangle2D[] Area { get { return AreaArray; } }
        public override Point3D BaseBanLocation { get { return new Point3D(1, 8, 0); } }

        public LargeMillersShop(Mobile owner) : base(0x8C, owner, 1100, 8)
        {
            uint keyValue = CreateKeys(owner);

            AddSouthDoors(-4, 6, 7, keyValue);

            SetSign(1, 8, 16);

            AddEastDoorShop(1, 4, 7, keyValue);
            AddEastDoorShop(1, -4, 7);
            AddSouthDoorShop(4, -1, 7);

            AddMill(3, 1, 7);
            AddFireplace(5, -6, 7);

        }

        public LargeMillersShop(Serial serial) : base(serial)
        {
        }

        public override HouseDeed GetDeed() { return new LargeMillersShopDeed(); }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);//version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class LargeClothiersShop : BaseHouse
    {
        public static Rectangle2D[] AreaArray = new Rectangle2D[] { new Rectangle2D(-7, -7, 15, 14), new Rectangle2D(-5, 7, 4, 1) };

        public override int DefaultPrice { get { return 152800; } }

        public override HousePlacementEntry ConvertEntry { get { return HousePlacementEntry.ThreeStoryFoundations[29]; } }
        public override int ConvertOffsetY { get { return -1; } }

        public override Rectangle2D[] Area { get { return AreaArray; } }
        public override Point3D BaseBanLocation { get { return new Point3D(1, 8, 0); } }

        public LargeClothiersShop(Mobile owner) : base(0x8C, owner, 1100, 8)
        {
            uint keyValue = CreateKeys(owner);

            AddSouthDoors(-4, 6, 7, keyValue);

            SetSign(1, 8, 16);

            AddEastDoorShop(1, 4, 7, keyValue);
            AddEastDoorShop(1, -4, 7);
            AddSouthDoorShop(4, -1, 7);

            AddLoom(5, 2, 7);
            AddSpinningWheelS(4, 5, 7);
        }

        public LargeClothiersShop(Serial serial) : base(serial)
        {
        }

        public override HouseDeed GetDeed() { return new LargeClothiersShopDeed(); }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);//version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class WeaponTrainingHut : BaseHouse
    {
        public static Rectangle2D[] AreaArray = new Rectangle2D[] { new Rectangle2D(-3, -3, 7, 7), new Rectangle2D(-1, 4, 3, 1) };

        public override Rectangle2D[] Area { get { return AreaArray; } }
        public override Point3D BaseBanLocation { get { return new Point3D(2, 4, 0); } }

        public override int DefaultPrice { get { return 43800; } }

        public override HousePlacementEntry ConvertEntry { get { return HousePlacementEntry.TwoStoryFoundations[0]; } }

        public WeaponTrainingHut(Mobile owner, int id) : base(id, owner, 425, 3)
        {
            uint keyValue = CreateKeys(owner);

            AddSouthDoor(0, 3, 7, keyValue);
            AddTrainingS(1, -2, 7);
            AddTrainingE(-2, 1, 7);

            SetSign(2, 4, 5);
        }

        public WeaponTrainingHut(Serial serial) : base(serial)
        {
        }

        public override HouseDeed GetDeed() { return new WeaponTrainingHutDeed(); }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);//version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }

    public class PickpocketsDen : BaseHouse
    {
        public static Rectangle2D[] AreaArray = new Rectangle2D[] { new Rectangle2D(-3, -3, 7, 7), new Rectangle2D(-1, 4, 3, 1) };

        public override Rectangle2D[] Area { get { return AreaArray; } }
        public override Point3D BaseBanLocation { get { return new Point3D(2, 4, 0); } }

        public override int DefaultPrice { get { return 43800; } }

        public override HousePlacementEntry ConvertEntry { get { return HousePlacementEntry.TwoStoryFoundations[0]; } }

        public PickpocketsDen(Mobile owner, int id) : base(id, owner, 425, 3)
        {
            uint keyValue = CreateKeys(owner);

            AddSouthDoor(0, 3, 7, keyValue);
            AddPickS(1, -2, 7);
            AddPickE(-2, 1, 7);

            SetSign(2, 4, 5);
        }

        public PickpocketsDen(Serial serial) : base(serial)
        {
        }

        public override HouseDeed GetDeed() { return new PickpocketsDenDeed(); }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);//version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}