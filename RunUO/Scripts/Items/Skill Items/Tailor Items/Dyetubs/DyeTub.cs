using System;
using Server;
using Server.Multis;
using Server.Targeting;
using Server.Network;

namespace Server.Items
{
	public interface IDyable
	{
		bool Dye( Mobile from, DyeTub sender );
	}

	public class DyeTub : Item
	{
		private bool m_Redyable;
		private int m_DyedHue;

		public virtual CustomHuePicker CustomHuePicker{ get{ return null; } }

        public virtual bool AllowBolts
        {
            get { return true; }
        }

		public virtual bool AllowRunebooks
		{
			get{ return false; }
		}

		public virtual bool AllowFurniture
		{
			get{ return false; }
		}

		public virtual bool AllowStatuettes
		{
			get{ return false; }
		}

		public virtual bool AllowLeather
		{
			get{ return false; }
		}

		public virtual bool AllowDyables
		{
			get{ return true; }
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (bool) m_Redyable );
			writer.Write( (int) m_DyedHue );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Redyable = reader.ReadBool();
					m_DyedHue = reader.ReadInt();

					break;
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Redyable
		{
			get
			{
				return m_Redyable;
			}
			set
			{
				m_Redyable = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int DyedHue
		{
			get
			{
				return m_DyedHue;
			}
			set
			{
				if ( m_Redyable )
				{
					m_DyedHue = value;
					Hue = value;
				}
			}
		}

		[Constructable] 
		public DyeTub() : base( 0xFAB )
		{
			Weight = 10.0;
			m_Redyable = true;
		}

		public DyeTub( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a dye tub"));
            }
        }

		// Select the clothing to dye.
		public virtual string TargetMessage{ get{ return "Select the clothing to dye."; } }

        // You can not dye that.
        public virtual string FailMessage { get { return "You can not dye that."; } }

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 1 ) )
			{
				from.SendAsciiMessage( "Select the clothing to dye." );
				from.Target = new InternalTarget( this );
			}
			else
			{
				from.SendAsciiMessage( "That is too far away." ); // That is too far away.
			}
		}

        protected virtual void AfterDye(Mobile from)
        {
        }

		private class InternalTarget : Target
		{
			private DyeTub m_Tub;

			public InternalTarget( DyeTub tub ) : base( 1, false, TargetFlags.None )
			{
				m_Tub = tub;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Item )
				{
					Item item = (Item)targeted;

					if ( item is IDyable && m_Tub.AllowDyables )
					{
                        if ( !from.InRange( m_Tub.GetWorldLocation(), 1 ) || !from.InRange( item.GetWorldLocation(), 1 ) )
                            from.SendAsciiMessage( "That is too far away." ); // That is too far away.
                        else if ( item.Parent is Mobile )
                            from.SendAsciiMessage( "Can't Dye clothing that is being worn." ); // Can't Dye clothing that is being worn.
                        else if ( !m_Tub.AllowBolts && ( item is BoltOfCloth || item is Cloth || item is UncutCloth || item is BaseClothMaterial || item is Wool || item is Cotton || item is OilCloth) )
                            from.SendAsciiMessage( "You can't dye this with a charged dye tub." );
                        else if ( ( (IDyable)item ).Dye( from, m_Tub ) )
                        {
                            from.PlaySound( 0x23E );
                            m_Tub.AfterDye( from );
                        }
					}
					else if ( (FurnitureAttribute.Check( item ) || (item is PotionKeg)) && m_Tub.AllowFurniture )
					{
						if ( !from.InRange( m_Tub.GetWorldLocation(), 1 ) || !from.InRange( item.GetWorldLocation(), 1 ) )
						{
                            from.SendAsciiMessage("That is too far away."); // That is too far away.
						}
						else
						{
							bool okay = ( item.IsChildOf( from.Backpack ) );

							if ( !okay )
							{
								if ( item.Parent == null )
								{
									BaseHouse house = BaseHouse.FindHouseAt( item );

									if ( house == null || !house.IsLockedDown( item ) )
                                        from.SendLocalizedMessage( "Furniture must be locked down to paint it." ); // Furniture must be locked down to paint it.
									else if ( !house.IsCoOwner( from ) )
                                        from.SendLocalizedMessage( "You must be the owner to use this item." ); // You must be the owner to use this item.
									else
										okay = true;
								}
								else
								{
                                    from.SendLocalizedMessage( "The furniture must be in your backpack to be painted." ); // The furniture must be in your backpack to be painted.
								}
							}

							if ( okay )
							{
								item.Hue = m_Tub.DyedHue;
								from.PlaySound( 0x23E );
                                m_Tub.AfterDye(from);
							}
						}
					}
					else if ( (item is Runebook || item is RecallRune ) && m_Tub.AllowRunebooks )
					{
						if ( !from.InRange( m_Tub.GetWorldLocation(), 1 ) || !from.InRange( item.GetWorldLocation(), 1 ) )
						{
							from.SendAsciiMessage( "That is too far away." ); // That is too far away.
						}
						else if ( !item.Movable )
						{
                            from.SendLocalizedMessage( "You cannot dye runes or runebooks that are locked down." ); // You cannot dye runes or runebooks that are locked down.
						}
						else
						{
							item.Hue = m_Tub.DyedHue;
							from.PlaySound( 0x23E );
                            m_Tub.AfterDye(from);
						}
					}
					else if ( item is MonsterStatuette && m_Tub.AllowStatuettes )
					{
						if ( !from.InRange( m_Tub.GetWorldLocation(), 1 ) || !from.InRange( item.GetWorldLocation(), 1 ) )
						{
							from.SendLocalizedMessage( "That is too far away." ); // That is too far away.
						}
						else if ( !item.Movable )
						{
                            from.SendLocalizedMessage( "You cannot dye statuettes that are locked down." ); // You cannot dye statuettes that are locked down.
						}
						else
						{
							item.Hue = m_Tub.DyedHue;
							from.PlaySound( 0x23E );
                            m_Tub.AfterDye(from);
						}
					}
					else if ( (item is BaseArmor && (((BaseArmor)item).MaterialType == ArmorMaterialType.Leather || ((BaseArmor)item).MaterialType == ArmorMaterialType.Studded)) && m_Tub.AllowLeather )
					{
						if ( !from.InRange( m_Tub.GetWorldLocation(), 1 ) || !from.InRange( item.GetWorldLocation(), 1 ) )
						{
							from.SendLocalizedMessage( "That is too far away." ); // That is too far away.
						}
						else if ( !item.Movable )
						{
                            from.SendLocalizedMessage( "You may not dye leather items which are locked down." ); // You may not dye leather items which are locked down.
						}
						else if ( item.Parent is Mobile )
						{
							from.SendLocalizedMessage( "Can't Dye clothing that is being worn." ); // Can't Dye clothing that is being worn.
						}
						else
						{
							item.Hue = m_Tub.DyedHue;
							from.PlaySound( 0x23E );
                            m_Tub.AfterDye(from);
						}
					}
					else
					{
						from.SendAsciiMessage( "You can not dye that." );
					}
				}
				else
				{
                    from.SendAsciiMessage("You can not dye that.");
				}
			}
		}
	}
}