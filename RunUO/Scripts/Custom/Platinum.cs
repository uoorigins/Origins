using System;
using Server;
using Server.Network;

namespace Server.Items
{
    public class Platinum : Item
    {
        public override double DefaultWeight
        {
            get { return 0.02; }
        }

        [Constructable]
        public Platinum()
            : this( 1 )
        {
        }

        [Constructable]
        public Platinum( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
        {
        }

        [Constructable]
        public Platinum( int amount )
            : base( 0xEF0 )
        {
            Stackable = true;
            Amount = amount;
            Hue = 1154;
        }

        public Platinum( Serial serial )
            : base( serial )
        {
        }

        public override void OnSingleClick( Mobile from )
        {
            if ( this.Name != null )
            {
                if ( Amount >= 2 )
                {
                    from.Send( new AsciiMessage( Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " " + this.Name ) );
                }
                else
                {
                    from.Send( new AsciiMessage( Serial, ItemID, MessageType.Label, 0, 3, "", this.Name ) );
                }
            }
            else
            {
                if ( Amount >= 2 )
                {
                    from.Send( new AsciiMessage( Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " platinum coins" ) );
                }
                else
                {
                    from.Send( new AsciiMessage( Serial, ItemID, MessageType.Label, 0, 3, "", "platinum coin" ) );
                }
            }
        }

        public override int GetDropSound()
        {
            if ( Amount <= 1 )
                return 0x2E4;
            else if ( Amount <= 5 )
                return 0x2E5;
            else
                return 0x2E6;
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int)0 ); // version
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();
        }
    }
}