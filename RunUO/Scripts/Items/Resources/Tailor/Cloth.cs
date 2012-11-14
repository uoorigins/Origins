using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0x1766, 0x1768 )]
	public class Cloth : Item, IScissorable, IDyable, ICommodity
	{
		string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} piece of cloth" : "{0} pieces of cloth", Amount );
			}
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public Cloth() : this( 1 )
		{
		}

		[Constructable]
		public Cloth( int amount ) : base( 0x1766 )
		{
			Stackable = true;
			Amount = amount;
		}

		public Cloth( Serial serial ) : base( serial )
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
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("{0} pieces of cloth ({0} yards of cloth)", Amount)));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a piece of cloth (1 yard of cloth)"));
                }
            }
        }

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

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

		/*public override void OnSingleClick( Mobile from )
		{
			int number = (Amount == 1) ? 1049124 : 1049123;

			from.Send( new MessageLocalized( Serial, ItemID, MessageType.Regular, 0x3B2, 3, number, "", Amount.ToString() ) );
		}*/
		
		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) ) return false;

            Consume();
            Item bandage = new Bandage();
            bandage.Hue = Hue;
            if (!from.PlaceInBackpack(bandage))
                bandage.MoveToWorld(from.Location, from.Map);

			//base.ScissorHelper( from, new Bandage(), 1 );

			return true;
		}
	}
}