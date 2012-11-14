using System;
using System.Collections.Generic;
using System.Text;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
    public class HumanJerky : Food
    {
        [Constructable]
        public HumanJerky() : this(null)
        {
        }

        public HumanJerky(String from) : base(1, 0x979)
        {
            if (from != null)
                this.Name = String.Format("jerky of {0}", from);
            else
                this.Name = "human jerky";

            this.Stackable = false;
            this.Weight = 0.5;
            this.FillFactor = 1;
        }

        public HumanJerky(Serial serial) : base(serial)
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
