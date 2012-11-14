using System;
using Server;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
	public enum HeadType
	{
		Regular,
		Duel,
		Tournament
	}

	public class Head : Item, ICarvable
	{
		private string m_PlayerName;
        private Serial m_PlayerSerial;
		private HeadType m_HeadType;
        private PlayerMobile m_Killer;
        private DateTime m_WhenKilled;

		[CommandProperty( AccessLevel.GameMaster )]
		public string PlayerName
		{
			get { return m_PlayerName; }
			set { m_PlayerName = value; }
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public Serial PlayerSerial
        {
            get { return m_PlayerSerial; }
            set { m_PlayerSerial = value; }
        }

		[CommandProperty( AccessLevel.GameMaster )]
		public HeadType HeadType
		{
			get { return m_HeadType; }
			set { m_HeadType = value; }
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public PlayerMobile Killer
        {
            get { return m_Killer; }
            set { m_Killer = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime WhenKilled
        {
            get { return m_WhenKilled; }
        }

		public override string DefaultName
		{
			get
			{
				if ( m_PlayerName == null )
					return base.DefaultName;

				switch ( m_HeadType )
				{
					default:
						return String.Format( "head of {0}", m_PlayerName );

					case HeadType.Duel:
						return String.Format( "head of {0}, taken in a duel", m_PlayerName );

					case HeadType.Tournament:
						return String.Format( "head of {0}, taken in a tournament", m_PlayerName );
				}
			}
		}

		[Constructable]
		public Head()
			: this( null )
		{
		}

		[Constructable]
		public Head( string playerName ) : this( HeadType.Regular, playerName, 0 )
		{
		}

        [Constructable]
        public Head(string playerName, Serial playerSerial) : this(HeadType.Regular, playerName, playerSerial)
        {
        }

        [Constructable]
        public Head(HeadType headType, string playerName, Serial playerSerial) : this(headType, playerName, playerSerial, null)
        {
        }

        [Constructable]
        public Head(HeadType headType, string playerName, Serial playerSerial, PlayerMobile mobile) : this(headType, playerName, playerSerial, mobile, DateTime.Now)
        {
        }

		[Constructable]
		public Head( HeadType headType, string playerName, Serial playerSerial, PlayerMobile mobile, DateTime whenkilled ) : base( 0x1DA0 )
		{
			m_HeadType = headType;
			m_PlayerName = playerName;
            m_PlayerSerial = playerSerial;
            m_Killer = mobile;
            m_WhenKilled = whenkilled;
		}

		public Head( Serial serial )
			: base( serial )
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
                if (m_PlayerName == null)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "head"));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("head of {0}", m_PlayerName)));
                }
            }
        }

        public void Carve(Mobile from, Item item)
        {
            from.AddToBackpack(new Skull(m_PlayerName));
            from.AddToBackpack(new Brain(m_PlayerName));
            Delete();
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (string) m_PlayerName );
			writer.WriteEncodedInt( (int) m_HeadType );
            writer.Write((int)m_PlayerSerial);
            writer.Write((DateTime)m_WhenKilled);
            writer.Write(m_Killer);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
					m_PlayerName = reader.ReadString();
					m_HeadType = (HeadType) reader.ReadEncodedInt();
                    m_PlayerSerial = (Serial)reader.ReadInt();
                    m_WhenKilled = reader.ReadDateTime();
                    m_Killer = (PlayerMobile)reader.ReadMobile();
					break;

				case 0:
					string format = this.Name;

					if ( format != null )
					{
						if ( format.StartsWith( "the head of " ) )
							format = format.Substring( "the head of ".Length );

						if ( format.EndsWith( ", taken in a duel" ) )
						{
							format = format.Substring( 0, format.Length - ", taken in a duel".Length );
							m_HeadType = HeadType.Duel;
						}
						else if ( format.EndsWith( ", taken in a tournament" ) )
						{
							format = format.Substring( 0, format.Length - ", taken in a tournament".Length );
							m_HeadType = HeadType.Tournament;
						}
					}

					m_PlayerName = format;
					this.Name = null;

					break;
			}
		}
	}

    public class Brain : Item
    {
        private String m_Owner;

        [Constructable]
        public Brain() : this(null)
        {
        }

		[Constructable]
        public Brain(String owner) : base(0x1CF0)
		{
			Weight = 1.0;
            m_Owner = owner;
		}

        public Brain(Serial serial) : base(serial)
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", (m_Owner == null ? "brain" : "brain of " + m_Owner)));
            }
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

            writer.Write(m_Owner);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

            m_Owner = reader.ReadString();
		}
    }

    public class Skull : Item
    {
        private String m_Owner;

        [Constructable]
        public Skull() : this(null)
        {
        }

        [Constructable]
        public Skull(String owner) : base(0x1AE2)
        {
            Weight = 1.0;
            m_Owner = owner;
        }

        public Skull(Serial serial)
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", (m_Owner == null ? "skull" : "skull of " + m_Owner)));
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write(m_Owner);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_Owner = reader.ReadString();
        }
    }
}