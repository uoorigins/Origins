// Treasure Chest Pack - Version 0.99H
// By Nerun

using Server;
using Server.Items;
using Server.Network;
using System;

namespace Server.Items
{
	public abstract class BaseTreasureChestMod : LockableContainer
	{
		private ChestTimer m_DeleteTimer;
		//public override bool Decays { get{ return true; } }
		//public override TimeSpan DecayTime{ get{ return TimeSpan.FromMinutes( Utility.Random( 30, 5 ) ); } }
		//public override int DefaultGumpID{ get{ return 0x42; } }
		//public override int DefaultDropSound{ get{ return 0x42; } }
		public override Rectangle2D Bounds{ get{ return new Rectangle2D( 20, 105, 150, 180 ); } }
		public override bool IsDecoContainer{get{ return false; }}
		
		public BaseTreasureChestMod( int itemID ) : base ( itemID )
		{
			Locked = true;
			Movable = false;

			Key key = (Key)FindItemByType( typeof(Key) );

			if( key != null )
				key.Delete();
		}

		public BaseTreasureChestMod( Serial serial ) : base( serial )
		{
		}

        public override bool TryDropItem(Mobile from, Item dropped, bool sendFullMessage)
        {
            if (from == null)
            {
                DropItem(dropped);
                return true;
            }

            return base.TryDropItem(from, dropped, sendFullMessage);
        }

        public override void OnSingleClick(Mobile from)
        {
            if (ItemID == 0xE3C || ItemID == 0xE3E || ItemID == 0x9a9)
            {
                if (this.Name != null)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
                    if (this.Locked == false)
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "(" + this.TotalItems + " items, " + this.TotalWeight + " stones)"));
                    }
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a crate"));
                    if (this.Locked == false)
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "(" + this.TotalItems + " items, " + this.TotalWeight + " stones)"));
                    }
                }
            }
            else if (ItemID == 0xe42 || ItemID == 0xe43)
            {
                if (this.Name != null)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
                    if (this.Locked == false)
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "(" + this.TotalItems + " items, " + this.TotalWeight + " stones)"));
                    }
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a wooden chest"));
                    if (this.Locked == false)
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "(" + this.TotalItems + " items, " + this.TotalWeight + " stones)"));
                    }
                }
            }
            else if (ItemID == 0xe40 || ItemID == 0x9ab)
            {
                if (this.Name != null)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
                    if (this.Locked == false)
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "(" + this.TotalItems + " items, " + this.TotalWeight + " stones)"));
                    }
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a metal chest"));
                    if (this.Locked == false)
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "(" + this.TotalItems + " items, " + this.TotalWeight + " stones)"));
                    }
                }
            }
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void OnTelekinesis( Mobile from )
		{
			if ( CheckLocked( from ) )
			{
				Effects.SendLocationParticles( EffectItem.Create( Location, Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5022 );
				Effects.PlaySound( Location, Map, 0x1F5 );
				return;
			}

			base.OnTelekinesis( from );
			StartDeleteTimer();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( CheckLocked( from ) )
				return;

			base.OnDoubleClick( from );
			StartDeleteTimer();
		}

		private void StartDeleteTimer()
		{
			if( m_DeleteTimer == null )
				m_DeleteTimer = new ChestTimer( this );
			else
				m_DeleteTimer.Delay = TimeSpan.FromSeconds( Utility.Random( 1, 2 ));
				
			m_DeleteTimer.Start();
		}

		private class ChestTimer : Timer
		{
			private BaseTreasureChestMod m_Chest;
			
			public ChestTimer( BaseTreasureChestMod chest ) : base ( TimeSpan.FromMinutes( Utility.Random( 15, 30 ) ) )
			{
				m_Chest = chest;
				Priority = TimerPriority.OneMinute;
			}

			protected override void OnTick()
			{
				m_Chest.Delete();
			}
		}
	}
}