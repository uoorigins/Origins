using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Multis;
using Server.Spells;
using Server.Spells.Sixth;
using Server.Guilds;
using Server.Gumps;
using Server.Network;

namespace Server.Regions
{
	public class HouseRegion : BaseRegion
	{
		public static readonly int HousePriority = Region.DefaultPriority + 1;

		private BaseHouse m_House;

		public static void Initialize()
		{
			EventSink.Login += new LoginEventHandler( OnLogin );
		}

		public static void OnLogin( LoginEventArgs e )
		{
            return;
		}

		public HouseRegion( BaseHouse house ) : base( null, house.Map, (house.Region != null ? house.Region.Priority+1 : HousePriority), GetArea( house ) )
		{
			m_House = house;

			Point3D ban = house.RelativeBanLocation;

			this.GoLocation = new Point3D( house.X + ban.X, house.Y + ban.Y, house.Z + ban.Z );
		}

		private static Rectangle3D[] GetArea( BaseHouse house )
		{
			int x = house.X;
			int y = house.Y;
			int z = house.Z;

			Rectangle2D[] houseArea = house.Area;
			Rectangle3D[] area = new Rectangle3D[houseArea.Length];

			for ( int i = 0; i < area.Length; i++ )
			{
				Rectangle2D rect = houseArea[i];
				area[i] = Region.ConvertTo3D( new Rectangle2D( x + rect.Start.X, y + rect.Start.Y, rect.Width, rect.Height ) );
			}

			return area;
		}

		public override bool SendInaccessibleMessage( Item item, Mobile from )
		{
			if ( item is Container )
                 from.Send(new AsciiMessage(item.Serial, item.ItemID, MessageType.Label, 0, 3, "", "That is secure."));
			else
                from.Send( new AsciiMessage( item.Serial, item.ItemID, MessageType.Label, 0, 3, "", "You are not allowed to access this." ) );

			return true;
		}

		public override bool CheckAccessibility( Item item, Mobile from )
		{
			return m_House.CheckAccessibility( item, from );
		}

		private bool m_Recursion;

		// Use OnLocationChanged instead of OnEnter because it can be that we enter a house region even though we're not actually inside the house
		public override void OnLocationChanged( Mobile m, Point3D oldLocation )
		{
			if ( m_Recursion )
				return;

			base.OnLocationChanged( m, oldLocation );

			m_Recursion = true;

			if ( m is BaseCreature && ((BaseCreature)m).NoHouseRestrictions )
			{
			}
			else if ( m is BaseCreature && ((BaseCreature)m).IsHouseSummonable && (BaseCreature.Summoning || m_House.IsInside( oldLocation, 16 )) )
			{
			}
			else if ( (m_House.Public || !m_House.IsAosRules) && m_House.IsBanned( m ) && m_House.IsInside( m ) )
			{
				m.Location = m_House.BanLocation;

				if( !Core.SE )
					m.SendLocalizedMessage( 501284 ); // You may not enter.
			}
			else if ( m_House.IsAosRules && !m_House.Public && !m_House.HasAccess( m ) && m_House.IsInside( m ) )
			{
				m.Location = m_House.BanLocation;

				if( !Core.SE )
					m.SendLocalizedMessage( 501284 ); // You may not enter.
			}
			else if ( m_House.IsCombatRestricted( m ) && m_House.IsInside( m ) && !m_House.IsInside( oldLocation, 16 ) )
			{
				m.Location = m_House.BanLocation;
				m.SendLocalizedMessage( 1061637 ); // You are not allowed to access this.
			}
			else if ( m_House is HouseFoundation )
			{
				HouseFoundation foundation = (HouseFoundation)m_House;

				if ( foundation.Customizer != null && foundation.Customizer != m && m_House.IsInside( m ) )
					m.Location = m_House.BanLocation;
			}

			if ( m_House.InternalizedVendors.Count > 0 && m_House.IsInside( m ) && !m_House.IsInside( oldLocation, 16 ) && m_House.IsOwner( m ) && m.Alive && !m.HasGump( typeof( NoticeGump ) ) )
			{
				/* This house has been customized recently, and vendors that work out of this
				 * house have been temporarily relocated.  You must now put your vendors back to work.
				 * To do this, walk to a location inside the house where you wish to station
				 * your vendor, then activate the context-sensitive menu on your avatar and
				 * select "Get Vendor".
				 */
				m.SendGump( new NoticeGump( 1060635, 30720, 1061826, 32512, 320, 180, null, null ) );
			}

			m_Recursion = false;
		}

		public override bool OnMoveInto( Mobile from, Direction d, Point3D newLocation, Point3D oldLocation )
		{
			if ( !base.OnMoveInto( from, d, newLocation, oldLocation ) )
				return false;

			if ( from is BaseCreature && ((BaseCreature)from).NoHouseRestrictions )
			{
			}
			else if ( from is BaseCreature && ((BaseCreature)from).IsHouseSummonable && (BaseCreature.Summoning || m_House.IsInside( oldLocation, 16 )) )
			{
			}
			else if ( (m_House.Public || !m_House.IsAosRules) && m_House.IsBanned( from ) && m_House.IsInside( newLocation, 16 ) )
			{
				from.Location = m_House.BanLocation;

				if( !Core.SE )
					from.SendLocalizedMessage( 501284 ); // You may not enter.

				return false;
			}
			else if ( m_House.IsAosRules && !m_House.Public && !m_House.HasAccess( from ) && m_House.IsInside( newLocation, 16 ) )
			{
				if( !Core.SE )
					from.SendLocalizedMessage( 501284 ); // You may not enter.

				return false;
			}
			else if ( m_House.IsCombatRestricted( from ) && !m_House.IsInside( oldLocation, 16 ) && m_House.IsInside( newLocation, 16 ) )
			{
				from.SendLocalizedMessage( 1061637 ); // You are not allowed to access this.
				return false;
			}
			else if ( m_House is HouseFoundation )
			{
				HouseFoundation foundation = (HouseFoundation)m_House;

				if ( foundation.Customizer != null && foundation.Customizer != from && m_House.IsInside( newLocation, 16 ) )
					return false;
			}

			if ( m_House.InternalizedVendors.Count > 0 && m_House.IsInside( from ) && !m_House.IsInside( oldLocation, 16 ) && m_House.IsOwner( from ) && from.Alive && !from.HasGump( typeof( NoticeGump ) ) )
			{
				/* This house has been customized recently, and vendors that work out of this
				 * house have been temporarily relocated.  You must now put your vendors back to work.
				 * To do this, walk to a location inside the house where you wish to station
				 * your vendor, then activate the context-sensitive menu on your avatar and
				 * select "Get Vendor".
				 */
				from.SendGump( new NoticeGump( 1060635, 30720, 1061826, 32512, 320, 180, null, null ) );
			}

			return true;
		}

		public override bool OnDecay( Item item )
		{
			/*if ( (m_House.IsLockedDown( item ) || m_House.IsSecure( item )) && m_House.IsInside( item ) )*/
            if (item is DeathRobe)
                return base.OnDecay(item);

            if (m_House.IsInside(item))
				return false;
			else
				return base.OnDecay(item );
		}

		public static TimeSpan CombatHeatDelay = TimeSpan.FromSeconds( 30.0 );

		public override TimeSpan GetLogoutDelay( Mobile m )
		{
			if ( m.Backpack != null && Key.ContainsKey(m.Backpack, m_House.keyValue)/*m_House.IsFriend( m )*/ && m_House.IsInside( m ) )
			{
				for ( int i = 0; i < m.Aggressed.Count; ++i )
				{
					AggressorInfo info = m.Aggressed[i];

					if ( info.Defender.Player && (DateTime.Now - info.LastCombatTime) < CombatHeatDelay )
						return base.GetLogoutDelay( m );
				}

				return TimeSpan.Zero;
			}

			return base.GetLogoutDelay( m );
		}

        public override bool CheckLift(Mobile m, object o)
        {
            if (o is Item)
            {
                if (Region.Find(m.Location, m.Map) != this && ((Item)o).Z >= m_House.Z + 7)
                    return false;
            }

            return base.CheckLift(m, o);
        }

		public override bool OnDoubleClick( Mobile from, object o )
		{
			if ( o is Container )
			{
				Container c = (Container)o;

				SecureAccessResult res = m_House.CheckSecureAccess( from, c );

				switch ( res )
				{
					case SecureAccessResult.Insecure: break;
					case SecureAccessResult.Accessible: return true;
					case SecureAccessResult.Inaccessible: c.SendLocalizedMessageTo( from, 1010563 ); return false;
				}
			}

			return base.OnDoubleClick( from, o );
		}

        public override void OnSpeech( SpeechEventArgs e )
        {
            base.OnSpeech( e );

            Mobile from = e.Mobile;
            Item sign = m_House.Sign;

            bool isKeyOwner = m_House.IsKeyOwner( from );
            bool houseIsProtected = m_House.LockDownCount > 0 || m_House.SecureCount > 0;

            if ( !from.Alive )
                return;

            if ( !isKeyOwner )
                return;

            if ( !m_House.IsInside( from ) || !m_House.IsActive )
                return;

            if ( !houseIsProtected )
            {
                if ( from.Guild != null )
                {
                    Guild guild = (Guild)from.Guild;

                    if ( !guild.IsProtected( from ) )
                    {
                        from.SendAsciiMessage( "You have not been offered protection by your guild" );
                    }
                    else
                    {
                        if ( e.HasKeyword( 0x23 ) ) // I wish to lock this down
                        {
                            from.SendAsciiMessage( "Lock what down?" ); // Lock what down?
                            from.Target = new LockdownTarget( false, m_House );
                        }
                        else if ( e.HasKeyword( 0x25 ) ) // I wish to secure this
                        {
                            from.SendAsciiMessage( "Choose the item you wish to secure" ); // Choose the item you wish to secure
                            from.Target = new SecureTarget( false, m_House );
                        }
                        else if ( e.HasKeyword( 0x26 ) ) // I wish to unsecure this
                        {
                            from.SendAsciiMessage( "Choose the item you wish to unsecure" ); // Choose the item you wish to unsecure
                            from.Target = new SecureTarget( true, m_House );
                        }
                        else if ( e.HasKeyword( 0x24 ) ) // I wish to release this
                        {
                            from.SendAsciiMessage( "Choose the item you wish to release" ); // Choose the item you wish to release
                            from.Target = new LockdownTarget( true, m_House );
                        }
                    }
                }
            }
            else
            {
                if ( e.HasKeyword( 0x23 ) ) // I wish to lock this down
                {
                    from.SendAsciiMessage( "Lock what down?" ); // Lock what down?
                    from.Target = new LockdownTarget( false, m_House );
                }
                else if ( e.HasKeyword( 0x25 ) ) // I wish to secure this
                {
                    from.SendAsciiMessage( "Choose the item you wish to secure" ); // Choose the item you wish to secure
                    from.Target = new SecureTarget( false, m_House );
                }
                else if ( e.HasKeyword( 0x26 ) ) // I wish to unsecure this
                {
                    from.SendAsciiMessage( "Choose the item you wish to unsecure" ); // Choose the item you wish to unsecure
                    from.Target = new SecureTarget( true, m_House );
                }
                else if ( e.HasKeyword( 0x24 ) ) // I wish to release this
                {
                    from.SendAsciiMessage( "Choose the item you wish to release" ); // Choose the item you wish to release
                    from.Target = new LockdownTarget( true, m_House );
                }
            }
        }

		public override bool OnSingleClick( Mobile from, object o )
		{
			if ( o is Item )
			{
				Item item = (Item)o;

				if ( m_House.IsLockedDown( item ) )
                    from.Send(new AsciiMessage(item.Serial, item.ItemID, MessageType.Label, 0, 3, "", "locked down"));
				else if ( m_House.IsSecure( item ) )
                    from.Send( new AsciiMessage( item.Serial, item.ItemID, MessageType.Label, 0, 3, "", "locked down & secure" ) );
			}

			return base.OnSingleClick( from, o );
		}

		public BaseHouse House
		{
			get
			{
				return m_House;
			}
		}
	}
}