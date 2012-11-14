using System;
using Server;
using Server.Network;
using System.Collections;
using System.Collections.Generic;

namespace Server.Items
{
    public class RabbitHole : Static
    {
        [Constructable]
        public RabbitHole() : base(6013 )
        {
            Name = "a rabbit hole";
            Hue = -1;
            Tag = "easter";

			Timer.DelayCall( TimeSpan.Zero, new TimerCallback( CheckAddComponents ) );
		}

        public RabbitHole(Serial serial): base(serial)
        {
        }

        public void CheckAddComponents()
        {
            if (Deleted)
                return;

            AddDirt();
        }

        public void AddDirt()
        {
            List<Static> dirt = new List<Static>();
            dirt.Add(new Static(0x1B27));
            dirt.Add(new Static(0x1B28));
            dirt.Add(new Static(0x1B2A));
            dirt.Add(new Static(0x1B29));
            dirt.Add(new Static(0x1B30));
            dirt.Add(new Static(0x1B2F));
            dirt.Add(new Static(0x1B32));
            dirt.Add(new Static(0x1B31));
            dirt.Add(new Static(0x1B30));
            dirt.Add(new Static(0x1B2F));
            dirt.Add(new Static(0x1B32));
            dirt.Add(new Static(0x1B31));

            foreach (Static mystatic in dirt)
            {
                mystatic.Name = "dirt";
                mystatic.Tag = "easter";
                mystatic.Hue = 1810;
                mystatic.MoveToWorld(this.Location, this.Map);
            }

            dirt[0].Y += 1;
            dirt[1].X += 1;
            dirt[2].Y -= 1;
            dirt[3].X -= 1;
            dirt[4].X -= 1;
            dirt[4].Y -= 1;
            dirt[5].X += 1;
            dirt[5].Y += 1;
            dirt[6].X += 1;
            dirt[6].Y -= 1;
            dirt[7].X -= 1;
            dirt[7].Y += 1;
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