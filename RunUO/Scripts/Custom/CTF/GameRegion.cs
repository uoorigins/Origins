using Server;
using System;
using Server.Items;
using System.Collections;
using Server.Spells.Fifth;
using Server.Spells.Eighth;
using Server.Spells.Fourth;
using Server.Spells.Third;
using Server.Spells.Sixth;
using Server.Spells.Seventh;
using System.Text;

namespace Server.Regions
{
	public class GameRegion : Region
	{
		public GameRegion() : base("Game Arena", Map.Felucca, Region.DefaultPriority, new Rectangle2D( 5896, 393, 111, 111 ))
		{
		}

		public override bool CanUseStuckMenu( Mobile m )
		{
			return false;
		}

		public override bool OnBeginSpellCast( Mobile m, ISpell s )
		{
			if ( m.AccessLevel == AccessLevel.Player && 
				( s is MarkSpell || s is RecallSpell || s is GateTravelSpell || s is PolymorphSpell ||
				s is SummonDaemonSpell || s is AirElementalSpell || s is EarthElementalSpell || s is EnergyVortexSpell || 
				s is FireElementalSpell || s is WaterElementalSpell || s is BladeSpiritsSpell || s is SummonCreatureSpell || 
				s is PoisonFieldSpell || s is EnergyFieldSpell || s is WallOfStoneSpell || s is ParalyzeFieldSpell || s is FireFieldSpell ) )
			{
				m.SendAsciiMessage( "That spell is not allowed." );
				return false;
			}
			else
			{
				return base.OnBeginSpellCast( m, s );
			}
		}

		public override bool AllowHousing( Mobile from, Point3D p )
		{
			return from.AccessLevel != AccessLevel.Player;
		}

		public override bool OnSingleClick( Mobile from, object o )
		{
			if ( !(o is Mobile) )
				return base.OnSingleClick( from, o );
			
			Mobile m = (Mobile)o;
			CTFTeam team = CTFGame.FindTeamFor( m );
			if ( team != null )
			{ 
				string msg;
				Item[] items = null;

				if ( m.Backpack != null )
					items = m.Backpack.FindItemsByType(typeof(CTFFlag));
						
				if ( items == null || items.Length == 0 )
				{
					msg = String.Format( "(Team: {0})", team.Name );
				}
				else
				{
					StringBuilder sb = new StringBuilder("(Team: " );
					sb.Append( team.Name );
					sb.Append( " -- Flag" );
					if ( items.Length > 1 )
						sb.Append( "s" );
					sb.Append( ": " );

					for(int j=0;j<items.Length;j++)
					{
						CTFFlag flag = (CTFFlag)items[j];

						if ( flag != null && flag.Team != null )
						{
							if ( j > 0 )
								sb.Append( ", " );

							sb.Append( flag.Team.Name );
						}
					}

					sb.Append( ")" );
					msg = sb.ToString();
				}
				m.PrivateOverheadMessage( Network.MessageType.Label, team.Hue, true, msg, from.NetState );
			}

			return true;
		}

		public override bool AllowBeneficial(Mobile from, Mobile target)
        {
			CTFTeam ft = CTFGame.FindTeamFor( from );
			if ( ft == null )
				return false;
			CTFTeam tt = CTFGame.FindTeamFor( target );
			if ( tt == null )
				return false;
			return ft == tt && ft.Game.Running;
		}

		public override bool AllowHarmful(Mobile from, Mobile target)
		{
			CTFTeam ft = CTFGame.FindTeamFor( from );
			if ( ft == null )
				return false;
			CTFTeam tt = CTFGame.FindTeamFor( target );
			if ( tt == null )
				return false;
			return ft != tt && ft.Game == tt.Game && ft.Game.Running;
		}
	}
}
