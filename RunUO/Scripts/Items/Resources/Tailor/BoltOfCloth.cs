using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class BoltOfCloth1 : BoltOfCloth
    {
        [Constructable]
        public BoltOfCloth1() : this(1)
        {
        }

        [Constructable]
        public BoltOfCloth1(int amount) : base(0xF96)
        {
            Stackable = true;
            Weight = 5.0;
            Amount = amount;
        }

        public BoltOfCloth1(Serial serial) : base(serial)
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

    public class BoltOfCloth2 : BoltOfCloth
    {
        [Constructable]
        public BoltOfCloth2() : this(1)
        {
        }

        [Constructable]
        public BoltOfCloth2(int amount) : base(0xF97)
        {
            Stackable = true;
            Weight = 5.0;
            Amount = amount;
        }

        public BoltOfCloth2(Serial serial) : base(serial)
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

    public class BoltOfCloth3 : BoltOfCloth
    {
        [Constructable]
        public BoltOfCloth3()
            : this(1)
        {
        }

        [Constructable]
        public BoltOfCloth3(int amount)
            : base(0xF9B)
        {
            Stackable = true;
            Weight = 5.0;
            Amount = amount;
        }

        public BoltOfCloth3(Serial serial)
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

    public class BoltOfCloth4 : BoltOfCloth
    {
        [Constructable]
        public BoltOfCloth4()
            : this(1)
        {
        }

        [Constructable]
        public BoltOfCloth4(int amount)
            : base(0xF9C)
        {
            Stackable = true;
            Weight = 5.0;
            Amount = amount;
        }

        public BoltOfCloth4(Serial serial)
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

	//[FlipableAttribute( 0xF95, 0xF96, 0xF97, 0xF98, 0xF99, 0xF9A, 0xF9B, 0xF9C )]
	public class BoltOfCloth : Item, IScissorable, IDyable, ICommodity
	{
		string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} bolt of cloth" : "{0} bolts of cloth", Amount );
			}
		}

        [Constructable]
        public BoltOfCloth() : this(Utility.RandomList(0xF96, 0xF97, 0xF9B, 0xF9C), 1)
		{
		}

        [Constructable]
        public BoltOfCloth(int amount) : this(Utility.RandomList(0xF96, 0xF97, 0xF9B, 0xF9C), amount)
        {
        }

        [Constructable]
		public BoltOfCloth( int itemID, int amount ) : base( itemID )
		{
			Stackable = true;
			Weight = 5.0;
            Amount = amount;
		}

		public BoltOfCloth( Serial serial ) : base( serial )
		{
		}

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                if (Amount >= 2)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " " + this.Name));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
                }
            }
            else
            {
                if (Amount >= 2)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("{0} bolts of cloth ({1} yards of cloth)", Amount, Amount*50)));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a bolt of cloth ({0} yards of cloth)", Amount*50)));
                }
            }
        }
		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted ) return false;

			Hue = sender.DyedHue;

			return true;
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) ) return false;

            base.ScissorHelper(from, new Cloth(), 50);

			return true;
		}
	}
}