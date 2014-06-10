using System;
using System.Collections;
using Server.Multis;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
    public class TentChest : LockableContainer
    {
        public override int DefaultGumpID { get { return 0x49; } }
        public override int DefaultDropSound { get { return 0x42; } }

        public override Rectangle2D Bounds
        {
            get { return new Rectangle2D(18, 105, 144, 73); }
        }

        private BaseHouse m_House;

        public TentChest( BaseHouse house ) : base(0xe43)
        {
            Movable = false;
            m_House = house;
        }

        public TentChest(Serial serial) : base(serial)
        {
        }

        public override bool IsDecoContainer
        {
            get { return false; }
        }

        public override void Open(Mobile from)
        {
            base.Open(from);

            if (m_House != null && Key.ContainsKey(from.Backpack, this.KeyValue) && from.AccessLevel == AccessLevel.Player && !Locked)
                m_House.RefreshDecay();
        }

        public override void OnAfterDelete()
        {
            base.OnAfterDelete();

            if (m_House != null && !m_House.Deleted)
                m_House.Delete();
        }

        public override void OnSingleClick(Mobile from)
        {
            from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a wooden chest"));
		
		if (this.Locked == false)
			from.Send( new AsciiMessage( Serial, ItemID, MessageType.Label, 0, 3, "", "(" + this.TotalItems + " items, " + this.TotalWeight + " stones)" ) );

            if (m_House != null && m_House.DecayPeriod != TimeSpan.Zero)
            {
                string message;
                string full;

                switch (m_House.DecayLevel)
                {
                    case DecayLevel.Ageless: message = "ageless"; break;
                    case DecayLevel.Fairly: message = "fairly worn"; break;
                    case DecayLevel.Greatly: message = "greatly worn"; break;
                    case DecayLevel.LikeNew: message = "like new"; break;
                    case DecayLevel.Slightly: message = "slightly worn"; break;
                    case DecayLevel.Somewhat: message = "somewhat worn"; break;
                    default: message = "in danger of collapsing"; break;
                }
                full = "This tent is " + message + ".";

                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", full));
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write(m_House);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_House = reader.ReadItem() as BaseHouse;
        }
    }
}