using System;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
    public class JaanasHangoverRemedy : Item
    {
        private int m_Uses;

        [CommandProperty( AccessLevel.GameMaster )]
		public int Uses
		{
			get{ return m_Uses; }
			set{ m_Uses = value; }
		}

		[Constructable]
		public JaanasHangoverRemedy() : base( 0xE2B )
		{
			Weight = 1.0;
			Hue = 0x2D;

			m_Uses = 20;
		}

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "Jaana's Hangover Remedy"));
            }
        }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
                from.SendAsciiMessage("You must have the object in your backpack to use it.");
				return;
			}

			if ( m_Uses > 0 )
			{
				from.PlaySound( 0x2D6 );
				from.SendAsciiMessage( "An awful taste fills your mouth." ); // An awful taste fills your mouth.

				if ( from.BAC > 0 )
				{
					from.BAC = 0;
					from.SendAsciiMessage( "You are now sober!" ); // You are now sober!
				}

				m_Uses--;
			}
			else
			{
				Delete();
				from.SendAsciiMessage( "There wasn't enough left to have any effect." ); // There wasn't enough left to have any effect.
			}
		}

        public JaanasHangoverRemedy(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.WriteEncodedInt( m_Uses );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_Uses = reader.ReadEncodedInt();
					break;
				}
				case 0:
				{
					m_Uses = 20;
					break;
				}
			}
		}
    }
}