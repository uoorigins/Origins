using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;
using Server.ContextMenus;
using EDI = Server.Mobiles.EscortDestinationInfo;
using Server.Multis;

namespace Server.Mobiles
{
	public class BaseEscortable : BaseCreature
	{
		private EDI m_Destination;
		private string m_DestinationString;

		private DateTime m_DeleteTime;
		private Timer m_DeleteTimer;

        private BulletinMessage m_Message;
        public BulletinMessage Message { get; set; }

        private BaseCamp m_Camp;
        public BaseCamp Camp { get; set; }

		public override bool Commandable{ get{ return false; } } // Our master cannot boss us around!

		[CommandProperty( AccessLevel.GameMaster )]
		public string Destination
		{
			get{ return m_Destination == null ? null : m_Destination.Name; }
			set{ m_DestinationString = value; m_Destination = EDI.Find( value ); }
		}

		private static string[] m_TownNames = new string[]
			{
				"Cove", "Britain", "Jhelom",
				"Minoc", "Ocllo", "Trinsic",
				"Vesper", "Yew", "Skara Brae",
				"Nujel'm", "Moonglow", "Magincia"
			};

		[Constructable]
		public BaseEscortable() : this(null)
		{
		}

        public BaseEscortable(BaseCamp camp) : base(AIType.AI_Melee, FightMode.Aggressor, 22, 1, 0.4, 1.0)
        {
            m_Camp = camp;
            InitBody();
            InitOutfit();
        }

		public virtual void InitBody()
		{
			SetStr( 90, 100 );
			SetDex( 90, 100 );
			SetInt( 15, 25 );

			Hue = Utility.RandomSkinHue();
            SpeechHue = Utility.RandomDyedHue();

			if ( Female = Utility.RandomBool() )
			{
				Body = 401;
				Name = NameList.RandomName( "female" );
			}
			else
			{
				Body = 400;
				Name = NameList.RandomName( "male" );
			}
		}

		public virtual void InitOutfit()
		{
			AddItem( new FancyShirt( Utility.RandomAllColors() ) );
			AddItem( new ShortPants( Utility.RandomAllColors() ) );
			AddItem( new Boots( Utility.RandomNeutralHue() ) );

			Utility.AssignRandomHair( this );
		}

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Poor);
            AddLootPouch(LootPack.PoorPouch);
            AddLoot(LootPack.PoorPile);
        }

		public virtual bool SayDestinationTo( Mobile m )
		{
			EDI dest = GetDestination();

			if ( dest == null || !m.Alive )
				return false;

			Mobile escorter = GetEscorter();

			if ( escorter == null )
			{
                Say(true, String.Format("I am waiting for my escort to {0}. If thou art interested, check the local bulletin board for details, or just say 'I will take thee.'", (dest.Name == "Ocllo" && m.Map == Map.Trammel) ? "Haven" : dest.Name));
				return true;
			}
			else if ( escorter == m )
			{
				Say( true, String.Format("Lead on! Payment will be made when we arrive in {0}.", (dest.Name == "Ocllo" && m.Map == Map.Trammel) ? "Haven" : dest.Name ));
				return true;
			}

			return false;
		}

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            base.OnMovement(m, oldLocation);

            if (!m.Player || (m.Hidden && m.AccessLevel > AccessLevel.Player))
                return;

            if (!this.InRange(m, 10) || this.InRange(oldLocation, 10))
                return;

            SayDestinationTo(m);
        }

		private static Hashtable m_EscortTable = new Hashtable();

		public static Hashtable EscortTable
		{
			get{ return m_EscortTable; }
		}

        private static TimeSpan m_EscortDelay = TimeSpan.Zero;

		public virtual bool AcceptEscorter( Mobile m )
		{
			EDI dest = GetDestination();

			if ( dest == null )
				return false;

			Mobile escorter = GetEscorter();

			if ( escorter != null || !m.Alive )
				return false;

			BaseEscortable escortable = (BaseEscortable)m_EscortTable[m];

			/*if ( escortable != null && !escortable.Deleted && escortable.GetEscorter() == m )
			{
				Say( true, "I see you already have an escort." );
				return false;
			}
			else*/ if ( m is PlayerMobile && (((PlayerMobile)m).LastEscortTime + m_EscortDelay) >= DateTime.Now )
			{
				int minutes = (int)Math.Ceiling( ((((PlayerMobile)m).LastEscortTime + m_EscortDelay) - DateTime.Now).TotalMinutes );

				Say( true, String.Format("You must rest {0} minute{1} before we set out on this journey.", minutes, minutes == 1 ? "" : "s" ));
				return false;
			}
			else if ( SetControlMaster( m ) )
			{
				m_LastSeenEscorter = DateTime.Now;

				if ( m is PlayerMobile )
					((PlayerMobile)m).LastEscortTime = DateTime.Now;

				Say( true, String.Format("Lead on! Payment will be made when we arrive in {0}.", (dest.Name == "Ocllo" && m.Map == Map.Trammel) ? "Haven" : dest.Name ) );
				m_EscortTable[m] = this;
				StartFollow();

                // We have an escort, Remove bulletin board post
                if (m_Message != null)
                    m_Message.Delete();

				return true;
			}

			return false;
		}

		public override bool HandlesOnSpeech( Mobile from )
		{
			if ( from.InRange( this.Location, 3 ) )
				return true;

			return base.HandlesOnSpeech( from );
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			base.OnSpeech( e );

			EDI dest = GetDestination();

			if ( dest != null && !e.Handled && e.Mobile.InRange( this.Location, 3 ) )
			{
				if ( e.HasKeyword( 0x1D ) ) // *destination*
					e.Handled = SayDestinationTo( e.Mobile );
				else if ( e.HasKeyword( 0x1E ) ) // *i will take thee*
					e.Handled = AcceptEscorter( e.Mobile );
			}
		}

		public override void OnAfterDelete()
		{
			if ( m_DeleteTimer != null )
				m_DeleteTimer.Stop();

			m_DeleteTimer = null;

			base.OnAfterDelete();
		}

		public override void OnThink()
		{
			base.OnThink();
			CheckAtDestination();
		}

		protected override bool OnMove( Direction d )
		{
			if ( !base.OnMove( d ) )
				return false;

			CheckAtDestination();

			return true;
		}

		public virtual void StartFollow()
		{
			StartFollow( GetEscorter() );
		}

		public virtual void StartFollow( Mobile escorter )
		{
			if ( escorter == null )
				return;

			ActiveSpeed = 0.4;
			PassiveSpeed = 1;

			ControlOrder = OrderType.Follow;
			ControlTarget = escorter;

			CurrentSpeed = 0.4;
		}

		public virtual void StopFollow()
		{
			ActiveSpeed = 0.2;
			PassiveSpeed = 1.0;

			ControlOrder = OrderType.None;
			ControlTarget = null;

			CurrentSpeed = 1.0;
		}

		private DateTime m_LastSeenEscorter;

		public virtual Mobile GetEscorter()
		{
			if ( !Controlled )
				return null;

			Mobile master = ControlMaster;

			if ( master == null )
				return null;

			if ( master.Deleted || master.Map != this.Map || !master.InRange( Location, 30 ) || !master.Alive )
			{
				StopFollow();

				TimeSpan lastSeenDelay = DateTime.Now - m_LastSeenEscorter;

				if ( lastSeenDelay >= TimeSpan.FromMinutes( 2.0 ) )
				{
					master.SendAsciiMessage( "You have lost the person you were escorting." ); // You have lost the person you were escorting.
					Say( true, "Hmmm.  I seem to have lost my master." ); // Hmmm.  I seem to have lost my master.

					SetControlMaster( null );
					m_EscortTable.Remove( master );

					Timer.DelayCall( TimeSpan.FromSeconds( 5.0 ), new TimerCallback( Delete ) );
					return null;
				}
				else
				{
					ControlOrder = OrderType.Stay;
					return master;
				}
			}

			if ( ControlOrder != OrderType.Follow )
				StartFollow( master );

			m_LastSeenEscorter = DateTime.Now;
			return master;
		}

        public override void OnDelete()
        {
            // We have been deleted, remove bulletin board post
            if (m_Message != null)
                m_Message.Delete();

            base.OnDelete();
        }

		public virtual void BeginDelete()
		{
			if ( m_DeleteTimer != null )
				m_DeleteTimer.Stop();

			m_DeleteTime = DateTime.Now + TimeSpan.FromSeconds( 30.0 );

			m_DeleteTimer = new DeleteTimer( this, m_DeleteTime - DateTime.Now );
			m_DeleteTimer.Start();
		}

        public virtual bool CheckAtDestination()
		{
			EDI dest = GetDestination();

			if ( dest == null )
				return false;

			Mobile escorter = GetEscorter();

			if ( escorter == null )
				return false;

			if ( dest.Contains( Location ) )
			{
				Say( true, String.Format("We have arrived! I thank thee, {0} I have no further need of thy services. Here is thy pay.", escorter.Name ) ); // We have arrived! I thank thee, ~1_PLAYER_NAME~! I have no further need of thy services. Here is thy pay.

				// not going anywhere
				m_Destination = null;
				m_DestinationString = null;

				Container cont = escorter.Backpack;

				if ( cont == null )
					cont = escorter.BankBox;

				Gold gold = new Gold( 500, 750 );

				if ( !cont.TryDropItem( escorter, gold, false ) )
					gold.MoveToWorld( escorter.Location, escorter.Map );

				StopFollow();
				SetControlMaster( null );
				m_EscortTable.Remove( escorter );
				BeginDelete();

				Misc.Titles.AwardKarma( escorter, 10, true );

				return true;
			}

			return false;
		}

		public BaseEscortable( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			EDI dest = GetDestination();

			writer.Write( dest != null );

			if ( dest != null )
				writer.Write( dest.Name );

			writer.Write( m_DeleteTimer != null );

			if ( m_DeleteTimer != null )
				writer.WriteDeltaTime( m_DeleteTime );

            writer.Write(m_Message);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( reader.ReadBool() )
				m_DestinationString = reader.ReadString(); // NOTE: We cannot EDI.Find here, regions have not yet been loaded :-(

			if ( reader.ReadBool() )
			{
				m_DeleteTime = reader.ReadDeltaTime();
				m_DeleteTimer = new DeleteTimer( this, m_DeleteTime - DateTime.Now );
				m_DeleteTimer.Start();
			}

            m_Message = reader.ReadItem() as BulletinMessage;
		}

		public override bool CanBeRenamedBy( Mobile from )
		{
			return ( from.AccessLevel >= AccessLevel.GameMaster );
		}

		public override void AddCustomContextEntries( Mobile from, List<ContextMenuEntry> list )
		{
			EDI dest = GetDestination();

			if ( dest != null && from.Alive )
			{
				Mobile escorter = GetEscorter();

				if ( escorter == null || escorter == from )
					list.Add( new AskDestinationEntry( this, from ) );

				if ( escorter == null )
					list.Add( new AcceptEscortEntry( this, from ) );
				else if ( escorter == from )
					list.Add( new AbandonEscortEntry( this, from ) );
			}

			base.AddCustomContextEntries( from, list );
		}

		public virtual string[] GetPossibleDestinations()
		{
			return m_TownNames;
		}
        protected override void OnLocationChange(Point3D oldLocation)
        {
            base.OnLocationChange(oldLocation);

            if (oldLocation == Point3D.Zero)
            {
                EDI dest = GetDestination();

                if (dest != null && m_Message == null)
                {
                    if (m_Camp != null)
                        m_Message = new PrisonerMessage(m_Camp, this);
                    else
                        m_Message = new EscortMessage(this);
                }
            }
        }

		public virtual string PickRandomDestination()
		{
			if ( Map.Felucca.Regions.Count == 0 /*|| Map == null || Map == Map.Internal || Location == Point3D.Zero*/ )
				return null; // Not yet fully initialized

			string[] possible = GetPossibleDestinations();
			string picked = null;

			while ( picked == null )
			{
				picked = possible[Utility.Random( possible.Length )];
				EDI test = EDI.Find( picked );

				if ( test != null && test.Contains( Location ) )
					picked = null;
			}

			return picked;
		}

		public EDI GetDestination()
		{
			if ( m_DestinationString == null && m_DeleteTimer == null )
				m_DestinationString = PickRandomDestination();

			if ( m_Destination != null && m_Destination.Name == m_DestinationString )
				return m_Destination;

			if ( Map.Felucca.Regions.Count > 0 )
				return ( m_Destination = EDI.Find( m_DestinationString ) );

			return ( m_Destination = null );
		}

		private class DeleteTimer : Timer
		{
			private Mobile m_Mobile;

			public DeleteTimer( Mobile m, TimeSpan delay ) : base( delay )
			{
				m_Mobile = m;

				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				m_Mobile.Delete();
			}
		}
	}

	public class EscortDestinationInfo
	{
		private string m_Name;
		private Region m_Region;
		//private Rectangle2D[] m_Bounds;

		public string Name
		{
			get{ return m_Name; }
		}

		public Region Region
		{
			get{ return m_Region; }
		}

		/*public Rectangle2D[] Bounds
		{
			get{ return m_Bounds; }
		}*/

		public bool Contains( Point3D p )
		{
			return m_Region.Contains( p );
		}

		public EscortDestinationInfo( string name, Region region )
		{
			m_Name = name;
			m_Region = region;
		}

		private static Hashtable m_Table;

		public static void LoadTable()
		{
			ICollection list = Map.Felucca.Regions.Values;

			if ( list.Count == 0 )
				return;

			m_Table = new Hashtable();

			foreach ( Region r in list )
			{
				if ( r.Name == null )
					continue;

				if ( r is Regions.DungeonRegion || r is Regions.TownRegion )
					m_Table[r.Name] = new EscortDestinationInfo( r.Name, r );
			}
		}

		public static EDI Find( string name )
		{
			if ( m_Table == null )
				LoadTable();

			if ( name == null || m_Table == null )
				return null;

			return (EscortDestinationInfo)m_Table[name];
		}
	}

	public class AskDestinationEntry : ContextMenuEntry
	{
		private BaseEscortable m_Mobile;
		private Mobile m_From;

		public AskDestinationEntry( BaseEscortable m, Mobile from ) : base( 6100, 3 )
		{
			m_Mobile = m;
			m_From = from;
		}

		public override void OnClick()
		{
			m_Mobile.SayDestinationTo( m_From );
		}
	}

	public class AcceptEscortEntry : ContextMenuEntry
	{
		private BaseEscortable m_Mobile;
		private Mobile m_From;

		public AcceptEscortEntry( BaseEscortable m, Mobile from ) : base( 6101, 3 )
		{
			m_Mobile = m;
			m_From = from;
		}

		public override void OnClick()
		{
			m_Mobile.AcceptEscorter( m_From );
		}
	}

	public class AbandonEscortEntry : ContextMenuEntry
	{
		private BaseEscortable m_Mobile;
		private Mobile m_From;

		public AbandonEscortEntry( BaseEscortable m, Mobile from ) : base( 6102, 3 )
		{
			m_Mobile = m;
			m_From = from;
		}

		public override void OnClick()
		{
			m_Mobile.Delete(); // OSI just seems to delete instantly
		}
	}
}