using System;
using Server.Gumps;
using Server.Network;
using Server.Menus.Questions;
using Server.Mobiles;

namespace Server.Items
{
	public class ResGate : Item
	{
		public override string DefaultName
		{
			get { return "a resurrection gate"; }
		}

		[Constructable]
		public ResGate() : base( 0xF6C )
		{
			Movable = false;
			Hue = 0x2D1;
			Light = LightType.Circle300;
		}

		public ResGate( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a resurrection gate"));
            }
        }

		public override bool OnMoveOver( Mobile m )
		{
			if ( !m.Alive && m.Map != null && m.Map.CanFit( m.Location, 16, false, false ) )
			{
				m.PlaySound( 0x214 );
				m.FixedEffect( 0x376A, 10, 16 );

                m.CantWalk = true;
				m.CloseGump( typeof( ResurrectGump ) );
                if (m is PlayerMobile && !((PlayerMobile)m).HasMenu)
                {
                    ((PlayerMobile)m).HasMenu = true;
                    m.SendMenu(new ResurrectGump(m));
                }
				//m.SendGump( new ResurrectGump( m ) );
			}
			else
			{
                m.SendAsciiMessage("Thou can not be resurrected there!");
				//m.SendLocalizedMessage( 502391 ); // Thou can not be resurrected there!
			}

			return false;
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
	}
}
