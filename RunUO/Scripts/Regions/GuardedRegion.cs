using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Server;
using Server.Commands;
using Server.Mobiles;
using Server.Spells;

namespace Server.Regions
{
	public class GuardedRegion : BaseRegion
	{
		private static object[] m_GuardParams = new object[1];
		private Type m_GuardType;
		private bool m_Disabled;

		public bool Disabled{ get{ return m_Disabled; } set{ m_Disabled = value; } }

		public virtual bool IsDisabled()
		{
			return m_Disabled;
		}

		public static void Initialize()
		{
			CommandSystem.Register( "CheckGuarded", AccessLevel.GameMaster, new CommandEventHandler( CheckGuarded_OnCommand ) );
			CommandSystem.Register( "SetGuarded", AccessLevel.Administrator, new CommandEventHandler( SetGuarded_OnCommand ) );
			CommandSystem.Register( "ToggleGuarded", AccessLevel.Administrator, new CommandEventHandler( ToggleGuarded_OnCommand ) );
		}

		[Usage( "CheckGuarded" )]
		[Description( "Returns a value indicating if the current region is guarded or not." )]
		private static void CheckGuarded_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			GuardedRegion reg = (GuardedRegion) from.Region.GetRegion( typeof( GuardedRegion ) );

			if ( reg == null )
				from.SendMessage( "You are not in a guardable region." );
			else if ( reg.Disabled )
				from.SendMessage( "The guards in this region have been disabled." );
			else
				from.SendMessage( "This region is actively guarded." );
		}

		[Usage( "SetGuarded <true|false>" )]
		[Description( "Enables or disables guards for the current region." )]
		private static void SetGuarded_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( e.Length == 1 )
			{
				GuardedRegion reg = (GuardedRegion) from.Region.GetRegion( typeof( GuardedRegion ) );

				if ( reg == null )
				{
					from.SendMessage( "You are not in a guardable region." );
				}
				else
				{
					reg.Disabled = !e.GetBoolean( 0 );

					if ( reg.Disabled )
						from.SendMessage( "The guards in this region have been disabled." );
					else
						from.SendMessage( "The guards in this region have been enabled." );
				}
			}
			else
			{
				from.SendMessage( "Format: SetGuarded <true|false>" );
			}
		}

		[Usage( "ToggleGuarded" )]
		[Description( "Toggles the state of guards for the current region." )]
		private static void ToggleGuarded_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			GuardedRegion reg = (GuardedRegion) from.Region.GetRegion( typeof( GuardedRegion ) );

			if ( reg == null )
			{
				from.SendMessage( "You are not in a guardable region." );
			}
			else
			{
				reg.Disabled = !reg.Disabled;

				if ( reg.Disabled )
					from.SendMessage( "The guards in this region have been disabled." );
				else
					from.SendMessage( "The guards in this region have been enabled." );
			}
		}

		public static GuardedRegion Disable( GuardedRegion reg )
		{
			reg.Disabled = true;
			return reg;
		}

		public virtual bool AllowReds{ get{ return Core.AOS; } }

		public virtual bool CheckVendorAccess( BaseVendor vendor, Mobile from )
		{
			if ( from.AccessLevel >= AccessLevel.GameMaster || IsDisabled() )
				return true;

			return ( from.Kills < 5 );
		}

		public virtual Type DefaultGuardType
		{
			get
			{
				if ( this.Map == Map.Ilshenar || this.Map == Map.Malas )
					return typeof( ArcherGuard );
				else
					return typeof( WarriorGuard );
			}
		}

		public GuardedRegion( string name, Map map, int priority, params Rectangle3D[] area ) : base( name, map, priority, area )
		{
			m_GuardType = DefaultGuardType;
		}

		public GuardedRegion( string name, Map map, int priority, params Rectangle2D[] area )
			: base( name, map, priority, area )
		{
			m_GuardType = DefaultGuardType;
		}
		
		public GuardedRegion( XmlElement xml, Map map, Region parent ) : base( xml, map, parent )
		{
			XmlElement el = xml["guards"];

			if ( ReadType( el, "type", ref m_GuardType, false ) )
			{
				if ( !typeof( Mobile ).IsAssignableFrom( m_GuardType ) )
				{
					Console.WriteLine( "Invalid guard type for region '{0}'", this );
					m_GuardType = DefaultGuardType;
				}
			}
			else
			{
				m_GuardType = DefaultGuardType;
			}

			bool disabled = false;
			if ( ReadBoolean( el, "disabled", ref disabled, false ) )
				this.Disabled = disabled;
		}

		public override bool OnBeginSpellCast( Mobile m, ISpell s )
		{
			if ( !IsDisabled() && !s.OnCastInTown( this ) )
			{
				//m.SendLocalizedMessage( 500946 ); // You cannot cast this in town!
                m.SendAsciiMessage("You cannot cast this in town!");
				return false;
			}

			return base.OnBeginSpellCast( m, s );
		}

		public override bool AllowHousing( Mobile from, Point3D p )
		{
			return false;
		}

		public override void MakeGuard( Mobile focus )
		{
			BaseGuard useGuard = null;
			foreach ( Mobile m in focus.GetMobilesInRange( 12 ) )
			{
				if ( m is BaseGuard )
				{
					BaseGuard g = (BaseGuard)m;

					if ( g.Focus == null ) // idling
					{
						useGuard = g;
						break;
					}
				}
			}

			if ( useGuard == null )
			{
				m_GuardParams[0] = focus;

				try { Activator.CreateInstance( m_GuardType, m_GuardParams ); } catch {}
			}
			else
				useGuard.Focus = focus;
		}

		public override void OnEnter( Mobile m )
		{
			if ( IsDisabled() )
				return;

            if ((m.Karma <= -110 && m.Player) || (m is BaseCreature && (m.Body.IsMonster || ((BaseCreature)m).AlwaysMurderer)))
				CheckGuardCandidate( m );
		}

		public override void OnExit( Mobile m )
		{
			if ( IsDisabled() )
				return;
		}

		public override void OnSpeech( SpeechEventArgs args )
		{
			base.OnSpeech( args );

			if ( IsDisabled() )
				return;

			if ( args.Mobile.Alive && args.HasKeyword( 0x0007 ) ) // *guards*
				CallGuards( args.Mobile.Location );
		}

		public override void OnAggressed( Mobile aggressor, Mobile aggressed, bool criminal )
		{
			base.OnAggressed( aggressor, aggressed, criminal );

            if (!IsDisabled() && ((aggressor != aggressed && criminal) || (aggressed.Karma > -110 && aggressed.Karma < 0) && !aggressed.Criminal))
                CheckGuardCandidate(aggressor);
		}

		public override void OnGotBeneficialAction( Mobile helper, Mobile helped )
		{
			base.OnGotBeneficialAction( helper, helped );

			if ( IsDisabled() )
				return;

			int noto = Notoriety.Compute( helper, helped );

			if ( helper != helped && (noto == Notoriety.Criminal || noto == Notoriety.Murderer) )
				CheckGuardCandidate( helper );
		}

		public override void OnCriminalAction( Mobile m, bool message )
		{
			base.OnCriminalAction( m, message );

			if ( !IsDisabled() )
				CheckGuardCandidate( m );
		}

		private Dictionary<Mobile, GuardTimer> m_GuardCandidates = new Dictionary<Mobile, GuardTimer>();

		public void CheckGuardCandidate( Mobile m )
		{
			if ( IsDisabled() )
				return;

			if ( IsGuardCandidate( m ) )
			{
				GuardTimer timer = null;
				m_GuardCandidates.TryGetValue( m, out timer );

				if ( timer == null )
				{
					timer = new GuardTimer( m, m_GuardCandidates );
					timer.Start();

					m_GuardCandidates[m] = timer;
                    m.SendAsciiMessage("Guards can now be called on you!");

					Map map = m.Map;

					if ( map != null )
					{
						Mobile fakeCall = null;
						double prio = 0.0;

						foreach ( Mobile v in m.GetMobilesInRange( 8 ) )
						{
                            if (!v.Player && v != m && !IsGuardCandidate(v) && ((v is BaseCreature) ? ((BaseCreature)v).IsHumanInTown() : (v.Body.IsHuman && v.Region.IsPartOf(this))))
							{
								double dist = m.GetDistanceToSqrt( v );

                                if (v is BaseGuard && ((BaseGuard)v).Focus != null && ((BaseGuard)v).Focus == m)
                                    return;

								if ( fakeCall == null || dist < prio )
								{
									fakeCall = v;
									prio = dist;
								}
							}
						}

                        if (fakeCall is BaseGuard && ((BaseGuard)fakeCall).Focus == null)
                        {
                            ((BaseGuard)fakeCall).Focus = m;
                            return;
                        }

						if ( fakeCall != null )
						{
                            switch (Utility.Random(9))
                            {
                                case 0: fakeCall.Say(true, "I hope the guards catch thee, scum!"); break;
                                case 1: fakeCall.Say(true, "Guards! Help!"); break;
                                case 2: fakeCall.Say(true, "Thou'rt scum! Guards!"); break;
                                case 3: fakeCall.Say(true, "Guards! A villain!"); break;
                                case 4: fakeCall.Say(true, "Tis a villain! Guards!"); break;
                                case 5: fakeCall.Say(true, "Help! Guards! Flood, fire, famine!"); break;
                                case 6: fakeCall.Say(true, "Aaaah! They will kill me! Guards!"); break;
                                case 7: fakeCall.Say(true, "Arrest this scum!"); break;
                                case 8: fakeCall.Say(true, "Guards! Guards!"); break;
                            }

							MakeGuard( m );
							//timer.Stop();
							//m_GuardCandidates.Remove( m );
                           //m.SendAsciiMessage("Guards can no longer be called on you.");
						}
					}
				}
				else
				{
					timer.Stop();
					timer.Start();
				}
			}
		}

		public void CallGuards( Point3D p )
		{
			if ( IsDisabled() )
				return;

			IPooledEnumerable eable = Map.GetMobilesInRange( p, 14 );

			foreach ( Mobile m in eable )
			{
                if (IsGuardCandidate(m) && (((m is PlayerMobile) && (m.Karma <= -110) && m.Region.IsPartOf(this)) || m_GuardCandidates.ContainsKey(m)))
				{
					GuardTimer timer = null;
					m_GuardCandidates.TryGetValue( m, out timer );

					if ( timer != null )
					{
						timer.Stop();
						m_GuardCandidates.Remove( m );
					}

					MakeGuard( m );
                    m.SendAsciiMessage("Guards can no longer be called on you.");
					break;
				}
			}

			eable.Free();
		}

		public bool IsGuardCandidate( Mobile m )
		{
			if ( m is BaseGuard || !m.Alive || m.AccessLevel > AccessLevel.Player || m.Blessed || IsDisabled() )
				return false;

            if (m.Player)
            {
                foreach (AggressorInfo info in m.Aggressed)
                {
                    if (info.Defender.Karma < -39 && info.Defender.Karma > -110)
                        return true;
                }
            }

            return ((m.Karma <= -110 && m.Player) || m.Criminal || (m is BaseCreature && (m.Body.IsMonster || ((BaseCreature)m).AlwaysMurderer) && ((BaseCreature)m).Controlled == false && ((BaseCreature)m).ControlMaster == null));
		}

        public static bool IsPet(BaseCreature c)
        {
            return (c != null && c.Controlled);
        }

		private class GuardTimer : Timer
		{
			private Mobile m_Mobile;
			private Dictionary<Mobile, GuardTimer> m_Table;

			public GuardTimer( Mobile m, Dictionary<Mobile, GuardTimer> table ) : base( TimeSpan.FromSeconds( 15.0 ) )
			{
				Priority = TimerPriority.TwoFiftyMS;

				m_Mobile = m;
				m_Table = table;
			}

			protected override void OnTick()
			{
				if ( m_Table.ContainsKey( m_Mobile ) )
				{
					m_Table.Remove( m_Mobile );
                    m_Mobile.SendAsciiMessage("Guards can no longer be called on you.");
				}
			}
		}
	}
}
