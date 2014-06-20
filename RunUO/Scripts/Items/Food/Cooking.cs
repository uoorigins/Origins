using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class UtilityItem
	{
		static public int RandomChoice( int itemID1, int itemID2 )
		{
			int iRet = 0;
			switch ( Utility.Random( 2 ) )
			{
				default:
				case 0: iRet = itemID1; break;
				case 1: iRet = itemID2; break;
			}
			return iRet;
		}
	}

	// ********** Dough **********
    public class Dough : CookableFood
	{
		[Constructable]
		public Dough() : base( 0x103d, 0 )
		{
			Weight = 1.0;
		}

		public Dough( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "dough"));
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
        }

        public override Food Cook()
        {
            return new BreadLoaf();
        }

#if true
        public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

            from.SendAsciiMessage("What should I use this on?");
			from.Target = new InternalTarget( this );
		}
#endif

		private class InternalTarget : Target
		{
			private Dough m_Item;

			public InternalTarget( Dough item ) : base( 1, false, TargetFlags.None )
			{
				m_Item = item;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Item.Deleted ) return;

                if (CookableFood.IsHeatSource(targeted))
                {
                    from.PlaySound(0x225);

                    m_Item.Consume();
                    CookableFood.InternalTarget.InternalTimer t = new CookableFood.InternalTarget.InternalTimer(from, targeted as IPoint3D, from.Map, m_Item);
                    t.Start();
                }
				else if ( targeted is Eggs )
				{
					m_Item.Delete();

					((Eggs)targeted).Consume();

					from.AddToBackpack( new UnbakedQuiche() );
				}
                else if (targeted is JarHoney)
                {
                    m_Item.Delete();
                    ((JarHoney)targeted).Consume();

                    from.AddToBackpack(new SweetDough());
                }
				else if ( targeted is CookedBird )
				{
					m_Item.Delete();

					((CookedBird)targeted).Consume();

					from.AddToBackpack( new UnbakedMeatPie() );
				}
                else if ( targeted is Ham )
                {
                    m_Item.Delete();

                    ( (Ham)targeted ).Consume();

                    from.AddToBackpack( new UnbakedMeatPie() );
                }
                else if ( targeted is Pear )
                {
                    m_Item.Delete();

                    ( (Pear)targeted ).Consume();

                    from.AddToBackpack( new UnbakedFruitPie() );
                }
                else if ( targeted is CheeseWheel)
                {
                    m_Item.Delete();

                    ( (CheeseWheel)targeted ).Consume();

                    from.AddToBackpack( new UncookedCheesePizza() );
                }
                else if ( targeted is HumanJerky )
                {
                    m_Item.Delete();

                    ( (HumanJerky)targeted ).Consume();

                    from.AddToBackpack( new UnbakedMeatPie() );
                }
                else if ( targeted is FishSteak )
                {
                    m_Item.Delete();

                    ( (FishSteak)targeted ).Consume();

                    from.AddToBackpack( new UnbakedMeatPie() );
                }
                else if ( targeted is Pumpkin )
                {
                    m_Item.Delete();

                    ( (Pumpkin)targeted ).Consume();

                    from.AddToBackpack( new UnbakedPumpkinPie() );
                }
				else if ( targeted is Sausage )
				{
					m_Item.Delete();

					((Sausage)targeted).Consume();

					from.AddToBackpack( new UncookedSausagePizza() );
				}
				else if ( targeted is Apple )
				{
					m_Item.Delete();

					((Apple)targeted).Consume();

					from.AddToBackpack( new UnbakedApplePie() );
				}

				else if ( targeted is Peach )
				{
					m_Item.Delete();

					((Peach)targeted).Consume();

					from.AddToBackpack( new UnbakedPeachCobbler() );
				}
			}
		}
	}

	// ********** SweetDough **********
    public class SweetDough : CookableFood
	{
		public override int LabelNumber{ get{ return 1041340; } } // sweet dough

		[Constructable]
		public SweetDough() : base( 0x103d, 10 )
		{
			Weight = 1.0;
			Hue = 150;
		}

		public SweetDough( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "sweet dough"));
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

			if ( Hue == 51 )
				Hue = 150;
		}

        public override Food Cook()
        {
            return new Muffins();
        }

#if true
		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

            from.SendAsciiMessage("What should I use this on?");
			from.Target = new InternalTarget( this );
		}
#endif

		private class InternalTarget : Target
		{
			private SweetDough m_Item;

			public InternalTarget( SweetDough item ) : base( 1, false, TargetFlags.None )
			{
				m_Item = item;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Item.Deleted ) return;

				if ( targeted is SackFlourOpen )
				{
					m_Item.Delete();
                    ((SackFlourOpen)targeted).Quantity--;

					from.AddToBackpack( new CakeMix() );
				}
				else if ( CookableFood.IsHeatSource( targeted ) )
				{
					from.PlaySound( 0x225 );
					m_Item.Consume();
                    CookableFood.InternalTarget.InternalTimer t = new CookableFood.InternalTarget.InternalTimer(from, targeted as IPoint3D, from.Map, m_Item);
                    t.Start();
				}
                else if (targeted is JarHoney)
                {
                    m_Item.Consume();
                    ((JarHoney)targeted).Delete();

                    from.AddToBackpack(new CookieMix());
                }
			}
		}
	}

	// ********** JarHoney **********
	public class JarHoney : Item
	{
		[Constructable]
		public JarHoney() : base( 0x9ec )
		{
			Weight = 1.0;
			Stackable = true;
		}

		public JarHoney( Serial serial ) : base( serial )
		{
		}

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                if (Amount >= 2)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " " + this.Name));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
                }
            }
            else
            {
                if (Amount >= 2)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " jars of honey"));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a jar of honey"));
                }
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
			Stackable = true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

            from.SendAsciiMessage("What should I use this on?");
			from.Target = new InternalTarget( this );
		}

		private class InternalTarget : Target
		{
			private JarHoney m_Item;

			public InternalTarget( JarHoney item ) : base( 1, false, TargetFlags.None )
			{
				m_Item = item;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Item.Deleted ) return;

				if ( targeted is Dough )
				{
					m_Item.Delete();
					((Dough)targeted).Consume();

					from.AddToBackpack( new SweetDough() );
				}

                if (targeted is SweetDough)
				{
					m_Item.Consume();
                    ((SweetDough)targeted).Delete();

					from.AddToBackpack( new CookieMix() );
				}
			}
		}
	}

	// ********** BowlFlour **********
	public class BowlFlour : Item
	{
		[Constructable]
		public BowlFlour() : base( 0xa1e )
		{
			Weight = 1.0;
		}

		public BowlFlour( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a bowl of flour"));
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
		}
	}

	// ********** WoodenBowl **********
	public class WoodenBowl : Item
	{
		[Constructable]
		public WoodenBowl() : base( 0x15f8 )
		{
			Weight = 1.0;
		}

		public WoodenBowl( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a wooden bowl"));
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
		}
	}

	// ********** PitcherWater **********
	/*public class PitcherWater : Item
	{
		[Constructable]
		public PitcherWater() : base(Utility.Random( 0x1f9d, 2 ))
		{
			Weight = 1.0;
		}

		public PitcherWater( Serial serial ) : base( serial )
		{
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
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			from.Target = new InternalTarget( this );
		}

		private class InternalTarget : Target
		{
			private PitcherWater m_Item;

			public InternalTarget( PitcherWater item ) : base( 1, false, TargetFlags.None )
			{
				m_Item = item;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Item.Deleted ) return;

				if ( targeted is BowlFlour )
				{
					m_Item.Delete();
					((BowlFlour)targeted).Delete();

					from.AddToBackpack( new Dough() );
					from.AddToBackpack( new WoodenBowl() );
				}
			}
		}
	}*/

	// ********** SackFlour **********
	[TypeAlias( "Server.Items.SackFlourOpen" )]
	public class SackFlour : Item, IHasQuantity
	{
        public static void Initialize()
        {
            TileData.ItemTable[0x1039].Flags = TileFlag.Impassable;
        }

		private int m_Quantity;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Quantity
		{
			get{ return m_Quantity; }
			set
			{
				if ( value < 0 )
					value = 0;
				else if ( value > 20 )
					value = 20;

				m_Quantity = value;

				if ( m_Quantity == 0 )
					Delete();
				else if ( m_Quantity < 20 && (ItemID == 0x1039 || ItemID == 0x1045) )
					++ItemID;
			}
		}

		[Constructable]
		public SackFlour() : base( 0x1039 )
		{
			Weight = 5.0;
			m_Quantity = 20;
		}

		public SackFlour( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a sack of flour"));
            }
        }


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version

			writer.Write( (int) m_Quantity );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 2:
				case 1:
				{
					m_Quantity = reader.ReadInt();
					break;
				}
				case 0:
				{
					m_Quantity = 20;
					break;
				}
			}

			if ( version < 2 && Weight == 1.0 )
				Weight = 5.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

            if ((ItemID == 0x1039 || ItemID == 0x1045))
            {
                ++ItemID;
            }

#if true
			this.Delete();

            from.AddToBackpack(new SackFlourOpen(Quantity));
#endif
		}

	}

#if true
	// ********** SackFlourOpen **********
	public class SackFlourOpen : Item
	{
        public static void Initialize()
        {
            TileData.ItemTable[0x103a].Flags = TileFlag.Impassable;
        }

		public override int LabelNumber{ get{ return 1024166; } } // open sack of flour

        private int m_Quantity;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Quantity
        {
            get { return m_Quantity; }
            set
            {
                if (value < 0)
                    value = 0;
                else if (value > 20)
                    value = 20;

                m_Quantity = value;

                if (m_Quantity == 0)
                    Delete();
                else if (m_Quantity < 20 && (ItemID == 0x1039 || ItemID == 0x1045))
                    ++ItemID;
            }
        }

		[Constructable]
		public SackFlourOpen(int q) : base( 0x103a )
		{
            m_Quantity = q;
			Weight = 1.0;
		}

		public SackFlourOpen( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

            writer.Write((int)m_Quantity);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

            if (version > 0)
                m_Quantity = reader.ReadInt();
		}

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "an open sack of flour"));
            }
        }

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

            from.Send(new AsciiMessage(Serial, ItemID, MessageType.Regular, 0, 3, "", "Mix the flour with water to make dough"));
			from.Target = new InternalTarget( this );
		}

		private class InternalTarget : Target
		{
			private SackFlourOpen m_Item;

			public InternalTarget( SackFlourOpen item ) : base( 1, false, TargetFlags.None )
			{
				m_Item = item;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Item.Deleted ) return;

                if (targeted is Pitcher)
                {
                    if (((Pitcher)targeted).Content == BeverageType.Water && ((Pitcher)targeted).Quantity > 0)
                    {
                        ((Pitcher)targeted).Quantity--;
                        m_Item.Quantity--;

                        from.SendAsciiMessage("You make some dough and put it in your backpack.");
                        from.AddToBackpack(new Dough());
                    }
                }

				/*if ( targeted is WoodenBowl )
				{
					m_Item.Delete();
					((WoodenBowl)targeted).Delete();

					from.AddToBackpack( new BowlFlour() );
				}
				else if ( targeted is TribalBerry )
				{
					if ( from.Skills[SkillName.Cooking].Base >= 80.0 )
					{
						m_Item.Delete();
						((TribalBerry)targeted).Delete();

						from.AddToBackpack( new TribalPaint() );

						from.SendLocalizedMessage( 1042002 ); // You combine the berry and the flour into the tribal paint worn by the savages.
					}
					else
					{
						from.SendLocalizedMessage( 1042003 ); // You don't have the cooking skill to create the body paint.
					}
				}*/
			}
		}
	}
#endif

	// ********** Eggshells **********
	public class Eggshells : Item
	{
		[Constructable]
		public Eggshells() : base( 0x9b4 )
		{
			Weight = 0.5;
		}

		public Eggshells( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "egg shells"));
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
		}
	}

	public class WheatSheaf : Item
	{
        public static void Initialize()
        {
            TileData.ItemTable[7869].Flags = TileFlag.Impassable;
        }

		[Constructable]
		public WheatSheaf() : this( 1 )
		{
		}

		[Constructable]
		public WheatSheaf( int amount ) : base( 7869 )
		{
			Weight = 1.0;
			Stackable = true;
			Amount = amount;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			from.BeginTarget( 4, false, TargetFlags.None, new TargetCallback( OnTarget ) );
		}

		public virtual void OnTarget( Mobile from, object obj )
		{
			if ( obj is AddonComponent )
				obj = (obj as AddonComponent).Addon;

			IFlourMill mill = obj as IFlourMill;

			if ( mill != null )
			{
				int needs = mill.MaxFlour - mill.CurFlour;

				if ( needs > this.Amount )
					needs = this.Amount;

				mill.CurFlour += needs;
				Consume( needs );
			}
		}

		public WheatSheaf( Serial serial ) : base( serial )
		{
		}

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                if (Amount >= 2)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " " + this.Name));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
                }
            }
            else
            {
                if (Amount >= 2)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " wheat sheafs"));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a wheat sheaf"));
                }
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
		}
	}
}