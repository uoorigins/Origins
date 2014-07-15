using System;

namespace Server.Items
{
    public class ChargedDyeTub : DyeTub
    {
        public override bool AllowBolts { get { return false; } }

        private int m_Charges;

        [CommandProperty( AccessLevel.GameMaster )]
        public int Charges
        {
            get { return m_Charges; }
            set { m_Charges = value; }
        }

        [Constructable]
        public ChargedDyeTub() :this(3)
        {
        }

        [Constructable]
        public ChargedDyeTub(int charge)
        {
            Charges = charge;
        }

        public ChargedDyeTub( Serial serial )
            : base( serial )
        {
        }

        protected override void AfterDye(Mobile from)
        {
            Charges--;
            if ( Charges < 1 )
            {
                from.SendAsciiMessage("The dye tub has run out of charges.");
                Delete();
            }
            else
            {
                base.AfterDye(from);
            }
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );

            writer.Write( (int)0 ); // version

            writer.Write( (int)m_Charges );
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );

            int version = reader.ReadInt();

            m_Charges = reader.ReadInt();
        }
    }
}