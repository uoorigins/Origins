using System;
using System.Collections.Generic;
using Server;
using Server.ContextMenus;
using Server.Engines.PartySystem;
using Server.Gumps;
using Server.Multis;
using Server.Network;

namespace Server.Items
{
	public class TreasureMapChest : LockableContainer
	{
		public override int LabelNumber{ get{ return 3000541; } }

		public static Type[] Artifacts { get { return m_Artifacts; } }

		private static Type[] m_Artifacts = new Type[]
		{
			typeof( CandelabraOfSouls ), typeof( GoldBricks ), typeof( PhillipsWoodenSteed ),
			typeof( ArcticDeathDealer ), typeof( BlazeOfDeath ), typeof( BurglarsBandana ),
			typeof( CavortingClub ), typeof( DreadPirateHat ),
			typeof( EnchantedTitanLegBone ), typeof( GwennosHarp ), typeof( IolosLute ),
			typeof( LunaLance ), typeof( NightsKiss ), typeof( NoxRangersHeavyCrossbow ),
			typeof( PolarBearMask ), typeof( VioletCourage ), typeof( HeartOfTheLion ),
			typeof( ColdBlood ), typeof( AlchemistsBauble )
		};

		private int m_Level;
		private DateTime m_DeleteTime;
		private Timer m_Timer;
		private Mobile m_Owner;
		private bool m_Temporary;

		private List<Mobile> m_Guardians;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Level{ get{ return m_Level; } set{ m_Level = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner{ get{ return m_Owner; } set{ m_Owner = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime DeleteTime{ get{ return m_DeleteTime; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Temporary{ get{ return m_Temporary; } set{ m_Temporary = value; } }

		public List<Mobile> Guardians { get { return m_Guardians; } }

		[Constructable]
		public TreasureMapChest( int level ) : this( null, level, false )
		{
		}

		public TreasureMapChest( Mobile owner, int level, bool temporary ) : base( 0xE40 )
		{
			m_Owner = owner;
			m_Level = level;
			m_DeleteTime = DateTime.Now + TimeSpan.FromHours( 3.0 );

			m_Temporary = temporary;
			m_Guardians = new List<Mobile>();

			m_Timer = new DeleteTimer( this, m_DeleteTime );
			m_Timer.Start();

			Fill( this, level );
		}

		private static void GetRandomAOSStats( out int attributeCount, out int min, out int max )
		{
			int rnd = Utility.Random( 15 );

			if ( rnd < 1 )
			{
				attributeCount = Utility.RandomMinMax( 2, 6 );
				min = 20; max = 70;
			}
			else if ( rnd < 3 )
			{
				attributeCount = Utility.RandomMinMax( 2, 4 );
				min = 20; max = 50;
			}
			else if ( rnd < 6 )
			{
				attributeCount = Utility.RandomMinMax( 2, 3 );
				min = 20; max = 40;
			}
			else if ( rnd < 10 )
			{
				attributeCount = Utility.RandomMinMax( 1, 2 );
				min = 10; max = 30;
			}
			else
			{
				attributeCount = 1;
				min = 10; max = 20;
			}
		}

		public static void Fill( LockableContainer cont, int level )
		{
		}

		public override bool CheckLocked( Mobile from )
		{
			if ( !this.Locked )
				return false;

			if ( this.Level == 0 && from.AccessLevel < AccessLevel.GameMaster )
			{
				foreach ( Mobile m in this.Guardians )
				{
					if ( m.Alive )
					{
						from.SendAsciiMessage( "You must first kill the guardians before you may open this chest." ); // You must first kill the guardians before you may open this chest.
						return true;
					}
				}

				LockPick( from );
				return false;
			}
			else
			{
				return base.CheckLocked( from );
			}
		}

		private List<Item> m_Lifted = new List<Item>();

		private bool CheckLoot( Mobile m, bool criminalAction )
		{
			if ( m_Temporary )
				return false;

			if ( m.AccessLevel >= AccessLevel.GameMaster || m_Owner == null || m == m_Owner )
				return true;

			Party p = Party.Get( m_Owner );

			if ( p != null && p.Contains( m ) )
				return true;

			Map map = this.Map;

			if ( map != null && (map.Rules & MapRules.HarmfulRestrictions) == 0 )
			{
				if ( criminalAction )
					m.CriminalAction( true );
				else
					m.SendLocalizedMessage( 1010630 ); // Taking someone else's treasure is a criminal offense!

				return true;
			}

			m.SendLocalizedMessage( 1010631 ); // You did not discover this chest!
			return false;
		}

		public override bool IsDecoContainer
		{
			get{ return false; }
		}

		public override bool CheckItemUse( Mobile from, Item item )
		{
			return CheckLoot( from, item != this ) && base.CheckItemUse( from, item );
		}

		public override bool CheckLift( Mobile from, Item item, ref LRReason reject )
		{
			return CheckLoot( from, true ) && base.CheckLift( from, item, ref reject );
		}

		public override void OnItemLifted( Mobile from, Item item )
		{
			bool notYetLifted = !m_Lifted.Contains( item );

			from.RevealingAction();

			if ( notYetLifted )
			{
				m_Lifted.Add( item );

				if ( 0.1 >= Utility.RandomDouble() ) // 10% chance to spawn a new monster
					TreasureMap.Spawn( m_Level, GetWorldLocation(), Map, from, false );
			}

			base.OnItemLifted( from, item );
		}

		public override bool CheckHold( Mobile m, Item item, bool message, bool checkItems, int plusItems, int plusWeight )
		{
			if ( m.AccessLevel < AccessLevel.GameMaster )
			{
				m.SendLocalizedMessage( 1048122, "", 0x8A5 ); // The chest refuses to be filled with treasure again.
				return false;
			}

			return base.CheckHold( m, item, message, checkItems, plusItems, plusWeight );
		}

		public TreasureMapChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version

			writer.Write( m_Guardians, true );
			writer.Write( (bool) m_Temporary );

			writer.Write( m_Owner );

			writer.Write( (int) m_Level );
			writer.WriteDeltaTime( m_DeleteTime );
			writer.Write( m_Lifted, true );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 2:
				{
					m_Guardians = reader.ReadStrongMobileList();
					m_Temporary = reader.ReadBool();

					goto case 1;
				}
				case 1:
				{
					m_Owner = reader.ReadMobile();

					goto case 0;
				}
				case 0:
				{
					m_Level = reader.ReadInt();
					m_DeleteTime = reader.ReadDeltaTime();
					m_Lifted = reader.ReadStrongItemList();

					if ( version < 2 )
						m_Guardians = new List<Mobile>();

					break;
				}
			}

			if ( !m_Temporary )
			{
				m_Timer = new DeleteTimer( this, m_DeleteTime );
				m_Timer.Start();
			}
			else
			{
				Delete();
			}
		}

		public override void OnAfterDelete()
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = null;

			base.OnAfterDelete();
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			if ( from.Alive )
				list.Add( new RemoveEntry( from, this ) );
		}

		public void BeginRemove( Mobile from )
		{
			if ( !from.Alive )
				return;

			from.CloseGump( typeof( RemoveGump ) );
			from.SendGump( new RemoveGump( from, this ) );
		}

		public void EndRemove( Mobile from )
		{
			if ( Deleted || from != m_Owner || !from.InRange( GetWorldLocation(), 3 ) )
				return;

			from.SendLocalizedMessage( 1048124, "", 0x8A5 ); // The old, rusted chest crumbles when you hit it.
			this.Delete();
		}

		private class RemoveGump : Gump
		{
			private Mobile m_From;
			private TreasureMapChest m_Chest;

			public RemoveGump( Mobile from, TreasureMapChest chest ) : base( 15, 15 )
			{
				m_From = from;
				m_Chest = chest;

				Closable = false;
				Disposable = false;

				AddPage( 0 );

				AddBackground( 30, 0, 240, 240, 2620 );

				AddHtmlLocalized( 45, 15, 200, 80, 1048125, 0xFFFFFF, false, false ); // When this treasure chest is removed, any items still inside of it will be lost.
				AddHtmlLocalized( 45, 95, 200, 60, 1048126, 0xFFFFFF, false, false ); // Are you certain you're ready to remove this chest?

				AddButton( 40, 153, 4005, 4007, 1, GumpButtonType.Reply, 0 );
				AddHtmlLocalized( 75, 155, 180, 40, 1048127, 0xFFFFFF, false, false ); // Remove the Treasure Chest

				AddButton( 40, 195, 4005, 4007, 2, GumpButtonType.Reply, 0 );
				AddHtmlLocalized( 75, 197, 180, 35, 1006045, 0xFFFFFF, false, false ); // Cancel
			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				if ( info.ButtonID == 1 )
					m_Chest.EndRemove( m_From );
			}
		}

		private class RemoveEntry : ContextMenuEntry
		{
			private Mobile m_From;
			private TreasureMapChest m_Chest;

			public RemoveEntry( Mobile from, TreasureMapChest chest ) : base( 6149, 3 )
			{
				m_From = from;
				m_Chest = chest;

				Enabled = ( from == chest.Owner );
			}

			public override void OnClick()
			{
				if ( m_Chest.Deleted || m_From != m_Chest.Owner || !m_From.CheckAlive() )
					return;

				m_Chest.BeginRemove( m_From );
			}
		}

		private class DeleteTimer : Timer
		{
			private Item m_Item;

			public DeleteTimer( Item item, DateTime time ) : base( time - DateTime.Now )
			{
				m_Item = item;
				Priority = TimerPriority.OneMinute;
			}

			protected override void OnTick()
			{
				m_Item.Delete();
			}
		}
	}
}
