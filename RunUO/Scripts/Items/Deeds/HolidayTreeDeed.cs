using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Multis;
using Server.Network;
using Server.Targeting;
using Server.Menus.Questions;
using Server.Guilds;

namespace Server.Items
{
	public class HolidayTreeDeed : Item
	{
		public override int LabelNumber{ get{ return 1041116; } } // a deed for a holiday tree

		[Constructable]
		public HolidayTreeDeed() : base( 0x14F0 )
		{
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public HolidayTreeDeed( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a deed for a holiday tree"));
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

			LootType = LootType.Blessed;
		}

		public bool ValidatePlacement( Mobile from, Point3D loc )
		{
			if ( from.AccessLevel >= AccessLevel.GameMaster )
				return true;

			if ( !from.InRange( this.GetWorldLocation(), 1 ) )
			{
				from.SendAsciiMessage( "That is too far away." ); // That is too far away.
				return false;
			}

			if ( DateTime.Now.Month != 12 )
			{
				from.SendAsciiMessage( "You will have to wait till next December to put your tree back up for display." ); // You will have to wait till next December to put your tree back up for display.
				return false;
			}

			Map map = from.Map;

			if ( map == null )
				return false;

			BaseHouse house = BaseHouse.FindHouseAt( loc, map, 20 );

            if (house == null || !Key.ContainsKey(from.Backpack, house.keyValue))
			{
				from.SendAsciiMessage( "The holiday tree can only be placed in your house." ); // The holiday tree can only be placed in your house.
				return false;
			}

			if ( !map.CanFit( loc, 20 ) )
			{
				from.SendAsciiMessage( "You cannot build that there." ); // You cannot build that there.
				return false;
			}

			return true;
		}

		public void BeginPlace( Mobile from, HolidayTreeType type )
		{
			from.BeginTarget( -1, true, TargetFlags.None, new TargetStateCallback( Placement_OnTarget ), type );
		}

		public void Placement_OnTarget( Mobile from, object targeted, object state )
		{
			IPoint3D p = targeted as IPoint3D;

			if ( p == null )
				return;

			Point3D loc = new Point3D( p );

			if ( p is StaticTarget )
				loc.Z -= TileData.ItemTable[((StaticTarget)p).ItemID & 0x3FFF].CalcHeight; /* NOTE: OSI does not properly normalize Z positioning here.
																							* A side affect is that you can only place on floors (due to the CanFit call).
																							* That functionality may be desired. And so, it's included in this script.
																							*/

			if ( ValidatePlacement( from, loc ) )
				EndPlace( from, (HolidayTreeType) state, loc );
		}

		public void EndPlace( Mobile from, HolidayTreeType type, Point3D loc )
		{
			this.Delete();
			new HolidayTree( from, type, loc );
		}

		public override void OnDoubleClick( Mobile from )
		{
            from.SendMenu(new HolidayTreeChoiceMenu(from, this));
		}
	}

    public class HolidayTreeChoiceMenu : QuestionMenu
    {
        private Mobile m_From;
        private HolidayTreeDeed m_Deed;

        public HolidayTreeChoiceMenu(Mobile from, HolidayTreeDeed deed)
            : base("What kind of holiday tree do you want to place?",
                new string[] { "Classic",
                "Modern"})
        {
            m_From = from;
            m_Deed = deed;
        }

        public override void OnCancel(NetState state)
        {
        }

        public override void OnResponse(NetState state, int index)
        {
            if (index == 0) // classic
            {
                m_Deed.BeginPlace(m_From, HolidayTreeType.Classic);
            }
            else if (index == 1) // modern
            {
                m_Deed.BeginPlace(m_From, HolidayTreeType.Modern);
            }
        }
    }
}