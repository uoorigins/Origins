using System;
using System.Text;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
    public class HairDye : Item
    {
        public override int LabelNumber { get { return 1041060; } } // Hair Dye

        public static int GetHue()
        {
            int Hue = 0;

            /*switch (Utility.Random(12))
            {
                default:
                case 0: Hue = 1602; Offset = Utility.RandomMinMax(0, 26); break;
                case 1: Hue = 1628; Offset = Utility.RandomMinMax(0, 27); break;
                case 2: Hue = 1502; Offset = Utility.RandomMinMax(0, 32); break;
                case 3: Hue = 1302; Offset = Utility.RandomMinMax(0, 32); break;
                case 4: Hue = 1402; Offset = Utility.RandomMinMax(0, 32); break;
                case 5: Hue = 1202; Offset = Utility.RandomMinMax(0, 24); break;
                case 6: Hue = 2402; Offset = Utility.RandomMinMax(0, 29); break;
                case 7: Hue = 2213; Offset = Utility.RandomMinMax(0, 6); break;
                case 8: Hue = 1102; Offset = Utility.RandomMinMax(0, 8); break;
                case 9: Hue = 1110; Offset = Utility.RandomMinMax(0, 8); break;
                case 10: Hue = 1118; Offset = Utility.RandomMinMax(0, 16); break;
                case 11: Hue = 1134; Offset = Utility.RandomMinMax(0, 16); break;
            }*/

            switch (Utility.RandomMinMax(1, 8))
            {
                case 1:
                    Hue = Utility.RandomMinMax(0x0641, 0x0676);
                    break;
                case 2:
                    Hue = Utility.RandomMinMax(0x0515, 0x054A);
                    break;
                case 3:
                    Hue = Utility.RandomMinMax(0x0579, 0x05A7);
                    break;
                case 4:
                    Hue = Utility.RandomMinMax(0x05DD, 0x060B);
                    break;
                case 5:
                    Hue = Utility.RandomMinMax(0x04B1, 0x04DF);
                    break;
                case 6:
                    Hue = Utility.RandomMinMax(0x0961, 0x097E);
                    break;
                case 7:
                    Hue = Utility.RandomMinMax(0x0899, 0x08B0);
                    break;
                default:
                case 8:
                    Hue = Utility.RandomMinMax(0x044E, 0x047C);
                    break;
            }

            return Hue;
        }

        [Constructable]
        public HairDye()
            : base(0xEFF)
        {
            Weight = 1.0;
            Hue = GetHue();
        }

        public HairDye(Serial serial)
            : base(serial)
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "hair dye"));
            }
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

        public override void OnDoubleClick(Mobile from)
        {
            if (from.InRange(this.GetWorldLocation(), 1))
            {
                if (!IsChildOf(from.Backpack))
                {
                    from.SendAsciiMessage("You must have the object in your backpack to use it."); //You must have the objectin your backpack to use it.
                    return;
                }

                if (from.HairItemID == 0 && from.FacialHairItemID == 0)
                    from.SendAsciiMessage("You have no hair to dye and cannot use this.");	// You have no hair to dye and cannot use this
                else
                {
                    from.HairHue = Hue;
                    from.FacialHairHue = Hue;

                    Delete();
                    from.PlaySound(0x4E);
                }
                /*from.CloseGump( typeof( HairDyeGump ) );
                from.SendGump( new HairDyeGump( this ) );*/
            }
            else
            {
                from.LocalOverheadMessage(MessageType.Regular, 906, true, "I can't reach that."); // I can't reach that.
            }
        }
    }

	public class HairDyeGump : Gump
	{
		private HairDye m_HairDye;

		private class HairDyeEntry
		{
			private string m_Name;
			private int m_HueStart;
			private int m_HueCount;

			public string Name
			{
				get
				{
					return m_Name;
				}
			}

			public int HueStart
			{
				get
				{
					return m_HueStart;
				}
			}

			public int HueCount
			{
				get
				{
					return m_HueCount;
				}
			}

			public HairDyeEntry( string name, int hueStart, int hueCount )
			{
				m_Name = name;
				m_HueStart = hueStart;
				m_HueCount = hueCount;
			}
		}

		private static HairDyeEntry[] m_Entries = new HairDyeEntry[]
			{
				new HairDyeEntry( "*****", 1602, 26 ),
				new HairDyeEntry( "*****", 1628, 27 ),
				new HairDyeEntry( "*****", 1502, 32 ),
				new HairDyeEntry( "*****", 1302, 32 ),
				new HairDyeEntry( "*****", 1402, 32 ),
				new HairDyeEntry( "*****", 1202, 24 ),
				new HairDyeEntry( "*****", 2402, 29 ),
				new HairDyeEntry( "*****", 2213, 6 ),
				new HairDyeEntry( "*****", 1102, 8 ),
				new HairDyeEntry( "*****", 1110, 8 ),
				new HairDyeEntry( "*****", 1118, 16 ),
				new HairDyeEntry( "*****", 1134, 16 )
			};

		public HairDyeGump( HairDye dye ) : base( 50, 50 )
		{
			m_HairDye = dye;

			AddPage( 0 );

			AddBackground( 100, 10, 350, 355, 2600 );
			AddBackground( 120, 54, 110, 270, 5100 );

			AddHtmlLocalized( 70, 25, 400, 35, 1011013, false, false ); // <center>Hair Color Selection Menu</center>

			AddButton( 149, 328, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 185, 329, 250, 35, 1011014, false, false ); // Dye my hair this color!

			for ( int i = 0; i < m_Entries.Length; ++i )
			{
				AddLabel( 130, 59 + (i * 22), m_Entries[i].HueStart - 1, m_Entries[i].Name );
				AddButton( 207, 60 + (i * 22), 5224, 5224, 0, GumpButtonType.Page, i + 1 );
			}

			for ( int i = 0; i < m_Entries.Length; ++i )
			{
				HairDyeEntry e = m_Entries[i];

				AddPage( i + 1 );

				for ( int j = 0; j < e.HueCount; ++j )
				{
					AddLabel( 278 + ((j / 16) * 80), 52 + ((j % 16) * 17), e.HueStart + j - 1, "*****" );
					AddRadio( 260 + ((j / 16) * 80), 52 + ((j % 16) * 17), 210, 211, false, (i * 100) + j );
				}
			}
		}

		public override void OnResponse( NetState from, RelayInfo info )
		{
			if ( m_HairDye.Deleted )
				return;

			Mobile m = from.Mobile;
			int[] switches = info.Switches;

			if ( !m_HairDye.IsChildOf( m.Backpack ) ) 
			{
				m.SendLocalizedMessage( 1042010 ); //You must have the objectin your backpack to use it.
				return;
			}

			if ( info.ButtonID != 0 && switches.Length > 0 )
			{
				if( m.HairItemID == 0 && m.FacialHairItemID == 0 )
				{
					m.SendLocalizedMessage( 502623 );	// You have no hair to dye and cannot use this
				}
				else
				{
					// To prevent this from being exploited, the hue is abstracted into an internal list

					int entryIndex = switches[0] / 100;
					int hueOffset = switches[0] % 100;

					if ( entryIndex >= 0 && entryIndex < m_Entries.Length )
					{
						HairDyeEntry e = m_Entries[entryIndex];

						if ( hueOffset >= 0 && hueOffset < e.HueCount )
						{
							int hue = e.HueStart + hueOffset;

							m.HairHue = hue;
							m.FacialHairHue = hue;

							m.SendLocalizedMessage( 501199 );  // You dye your hair
							m_HairDye.Delete();
							m.PlaySound( 0x4E );
						}
					}
				}
			}
			else
			{
				m.SendLocalizedMessage( 501200 ); // You decide not to dye your hair
			}
		}
	}
}