using Server;
using System;
using System.Collections;
using Server.Multis;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Multis.Deeds
{
    public class BlueTentDeed : HouseDeed
    {
        [Constructable]
        public BlueTentDeed() : base(0x70, new Point3D(0, 4, 0))
        {
            //Name = "deed to a blue tent";
        }

        public BlueTentDeed(Serial serial) : base(serial)
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a deed to a blue tent"));
            }
        }

        public override BaseHouse GetHouse(Mobile owner)
        {
            return new BlueTent(owner, 0x70);
        }

        public override int LabelNumber { get { return 1041217; } }
        public override Rectangle2D[] Area { get { return BlueTent.AreaArray; } }

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

    public class GreenTentDeed : HouseDeed
    {
        [Constructable]
        public GreenTentDeed() : base(0x72, new Point3D(0, 4, 0))
        {
        }

        public GreenTentDeed(Serial serial) : base(serial)
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a deed to a green tent"));
            }
        }

        public override BaseHouse GetHouse(Mobile owner)
        {
            return new GreenTent(owner, 0x72);
        }

        public override int LabelNumber { get { return 1041218; } }
        public override Rectangle2D[] Area { get { return GreenTent.AreaArray; } }

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