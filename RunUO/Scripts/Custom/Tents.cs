using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Multis.Deeds;

namespace Server.Multis
{
    public class BlueTent : BaseHouse
    {
        public static Rectangle2D[] AreaArray = new Rectangle2D[] { new Rectangle2D(-3, -3, 8, 8) };

        public override Rectangle2D[] Area { get { return AreaArray; } }
        public override Point3D BaseBanLocation { get { return new Point3D(2, 8, 0); } }

        public override int DefaultPrice { get { return 43800; } }

        public override HousePlacementEntry ConvertEntry { get { return HousePlacementEntry.TwoStoryFoundations[0]; } }

        public BlueTent(Mobile owner, int id) : base(id, owner, 425, 3)
        {
            uint keyValue = CreateTentKeys(owner);

            SetChest(keyValue, 0, -2, 0);
        }

        public BlueTent(Serial serial) : base(serial)
        {
        }

        public override bool IsInside(Point3D p, int height)
        {
            if (Deleted)
                return false;

            foreach (Rectangle2D rect in Area)
            {
                if (rect.Contains(new Point2D(p.X - X, p.Y - Y)))
                    return true;
            }

            return false;
        }

        public override HouseDeed GetDeed()
        {
            switch (ItemID ^ 0x4000)
            {
                case 0x70: return new BlueTentDeed();
                default: return new BlueTentDeed();
            }
        }

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

    public class GreenTent : BaseHouse
    {
        public static Rectangle2D[] AreaArray = new Rectangle2D[] { new Rectangle2D(-3, -3, 8, 8) };

        public override Rectangle2D[] Area { get { return AreaArray; } }
        public override Point3D BaseBanLocation { get { return new Point3D(2, 8, 0); } }

        public override int DefaultPrice { get { return 43800; } }

        //public override HousePlacementEntry ConvertEntry { get { return HousePlacementEntry.TwoStoryFoundations[0]; } }

        public GreenTent(Mobile owner, int id) : base(id, owner, 425, 3)
        {
            uint keyValue = CreateTentKeys(owner);

            SetChest(keyValue, 0, -2, 0);
        }

        public GreenTent(Serial serial) : base(serial)
        {
        }

        public override bool IsInside(Point3D p, int height)
        {
            if (Deleted)
                return false;

            foreach (Rectangle2D rect in Area)
            {
                if (rect.Contains(new Point2D(p.X - X, p.Y - Y)))
                    return true;
            }

            return false;
        }

        public override HouseDeed GetDeed()
        {
            switch (ItemID ^ 0x4000)
            {
                case 0x72: return new GreenTentDeed();
                default: return new GreenTentDeed();
            }
        }

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