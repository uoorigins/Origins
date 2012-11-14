using System;
using Server.Items;
using Server.Targeting;
using Server.Network;

namespace Server.Items
{
	public class Wool : Item, IDyable
	{
		[Constructable]
		public Wool() : this( 1 )
		{
		}

		[Constructable]
		public Wool( int amount ) : base( 0xDF8 )
		{
			Stackable = true;
			Weight = 4.0;
			Amount = amount;
		}

		public Wool( Serial serial ) : base( serial )
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
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " piles of wool"));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a pile of wool"));
                }
            }
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
		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			Hue = sender.DyedHue;

			return true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.SendAsciiMessage( "What spinning wheel do you wish to spin this on?" ); // What spinning wheel do you wish to spin this on?
				from.Target = new PickWheelTarget( this );
			}
			else
			{
				from.SendAsciiMessage( "That must be in your pack for you to use it." ); // That must be in your pack for you to use it.
			}
		}

		public static void OnSpun( ISpinningWheel wheel, Mobile from, int hue )
		{
			Item item = new DarkYarn( 3 );
			item.Hue = hue;

			from.AddToBackpack( item );
			from.SendAsciiMessage( "You put the balls of yarn in your backpack." ); // You put the balls of yarn in your backpack.
		}

		private class PickWheelTarget : Target
		{
			private Wool m_Wool;

			public PickWheelTarget( Wool wool ) : base( 3, false, TargetFlags.None )
			{
				m_Wool = wool;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Wool.Deleted )
					return;

				ISpinningWheel wheel = targeted as ISpinningWheel;

				if ( wheel == null && targeted is AddonComponent )
					wheel = ((AddonComponent)targeted).Addon as ISpinningWheel;

				if ( wheel is Item )
				{
					Item item = (Item)wheel;

					if ( !m_Wool.IsChildOf( from.Backpack ) )
					{
						from.SendAsciiMessage( "That must be in your pack for you to use it." ); // That must be in your pack for you to use it.
					}
					else if ( wheel.Spinning )
					{
						from.SendAsciiMessage( "That spinning wheel is being used." ); // That spinning wheel is being used.
					}
					else
					{
						m_Wool.Consume();
						wheel.BeginSpin( new SpinCallback( Wool.OnSpun ), from, m_Wool.Hue );
					}
				}
				else
				{
					from.SendAsciiMessage( "Use that on a spinning wheel." ); // Use that on a spinning wheel.
				}
			}
		}
	}
}