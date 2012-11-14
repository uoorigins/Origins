using System;
using Server.Network;

namespace Server.Items
{
    public class MagicCrystalBall : BaseStationary
    {
        [Constructable]
        public MagicCrystalBall() : base(0xE2E)
        {
            Weight = 10;
            Stackable = false;
            Light = LightType.Circle150;
        }

        public MagicCrystalBall(Serial serial) : base(serial)
        {
        }

        public override string StringName { get { return "crystal ball"; } }

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

    public class Statue1 : BaseStationary
    {
        [Constructable]
        public Statue1()
            : base(4644)
        {
            Weight = 10;
        }

        public Statue1(Serial serial) : base(serial)
        {
        }

        public override string StringName { get { return "statue"; } }

        public override bool ForceShowProperties { get { return ObjectPropertyList.Enabled; } }

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

    public class Statue2 : BaseStationary
    {
        [Constructable]
        public Statue2()
            : base(4645)
        {
            Weight = 10;
        }

        public Statue2(Serial serial)
            : base(serial)
        {
        }

        public override string StringName { get { return "statue"; } }

        public override bool ForceShowProperties { get { return ObjectPropertyList.Enabled; } }

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

    public class Statue3 : BaseStationary
    {
        [Constructable]
        public Statue3()
            : base(4646)
        {
            Weight = 10;
        }

        public Statue3(Serial serial)
            : base(serial)
        {
        }

        public override string StringName { get { return "statue"; } }

        public override bool ForceShowProperties { get { return ObjectPropertyList.Enabled; } }

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

    public class Statue4 : BaseStationary
    {
        [Constructable]
        public Statue4()
            : base(4647)
        {
            Weight = 10;
        }

        public Statue4(Serial serial)
            : base(serial)
        {
        }

        public override string StringName { get { return "statue"; } }

        public override bool ForceShowProperties { get { return ObjectPropertyList.Enabled; } }

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

    public class Statue5 : BaseStationary
    {
        [Constructable]
        public Statue5()
            : base(4648)
        {
            Weight = 10;
        }

        public Statue5(Serial serial)
            : base(serial)
        {
        }

        public override string StringName { get { return "statue"; } }

        public override bool ForceShowProperties { get { return ObjectPropertyList.Enabled; } }

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

    public class MagicBrazier : BaseStationary
    {
        [Constructable]
        public MagicBrazier() : base(0xE31)
        {
            Weight = 10;
            Light = LightType.Circle225;
        }

        public MagicBrazier(Serial serial) : base(serial)
        {
        }

        public override string StringName { get { return "brazier"; } }

        public override bool ForceShowProperties { get { return ObjectPropertyList.Enabled; } }

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