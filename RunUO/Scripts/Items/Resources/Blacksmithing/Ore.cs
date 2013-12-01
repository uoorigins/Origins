using System;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Engines.Craft;

namespace Server.Items
{
	public abstract class BaseOre : Item, ICommodity
	{
		private CraftResource m_Resource;

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; InvalidateProperties(); }
		}

		string ICommodity.Description
		{
			get
			{
				return String.Format( "{0} {1} ore", Amount, CraftResources.GetName( m_Resource ).ToLower() );
			}
		}

		public abstract BaseIngot GetIngot();

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (int) m_Resource );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_Resource = (CraftResource)reader.ReadInt();
					break;
				}
				case 0:
				{
					OreInfo info;

					switch ( reader.ReadInt() )
					{
						case 0: info = OreInfo.Iron; break;
						case 1: info = OreInfo.DullCopper; break;
						case 2: info = OreInfo.ShadowIron; break;
						case 3: info = OreInfo.Copper; break;
						case 4: info = OreInfo.Bronze; break;
						case 5: info = OreInfo.Gold; break;
						case 6: info = OreInfo.Agapite; break;
						case 7: info = OreInfo.Verite; break;
						case 8: info = OreInfo.Valorite; break;
						default: info = null; break;
					}

					m_Resource = CraftResources.GetFromOreInfo( info );
					break;
				}
			}
		}

        public BaseOre( CraftResource resource ) : this( resource, 1 )
		{
		}

		public BaseOre( CraftResource resource, int amount ) : base( 0x19B9 )
		{
			Stackable = true;
			Weight = 12.0;
			Amount = amount;
			Hue = CraftResources.GetHue( resource );

			m_Resource = resource;
		}

		public BaseOre( Serial serial ) : base( serial )
		{
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Amount > 1 )
				list.Add( 1050039, "{0}\t#{1}", Amount, 1026583 ); // ~1_NUMBER~ ~2_ITEMNAME~
			else
				list.Add( 1026583 ); // ore
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( !CraftResources.IsStandard( m_Resource ) )
			{
				int num = CraftResources.GetLocalizationNumber( m_Resource );

				if ( num > 0 )
					list.Add( num );
				else
					list.Add( CraftResources.GetName( m_Resource ) );
			}
		}

		public override int LabelNumber
		{
			get
			{
				if ( m_Resource >= CraftResource.DullCopper && m_Resource <= CraftResource.Valorite )
					return 1042845 + (int)(m_Resource - CraftResource.DullCopper);

				return 1042853; // iron ore;
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			if ( from.InRange( this.GetWorldLocation(), 2 ) )
			{
				from.SendAsciiMessage( "Select the forge on which to smelt the ore, or another pile of ore with which to combine it." ); // Select the forge on which to smelt the ore, or another pile of ore with which to combine it.
				from.Target = new InternalTarget( this );
			}
			else
			{
				from.SendAsciiMessage( "The ore is too far away." ); // The ore is too far away.
			}
		}

		private class InternalTarget : Target
		{
			private BaseOre m_Ore;

			public InternalTarget( BaseOre ore ) :  base ( 2, false, TargetFlags.None )
			{
				m_Ore = ore;
			}

			private bool IsForge( object obj )
			{
				if ( obj.GetType().IsDefined( typeof( ForgeAttribute ), false ) )
					return true;

				int itemID = 0;

				if ( obj is Item )
					itemID = ((Item)obj).ItemID;
				else if ( obj is StaticTarget )
					itemID = ((StaticTarget)obj).ItemID & 0x3FFF;

				return ( itemID == 4017 || (itemID >= 6522 && itemID <= 6569) );
			}

            private bool IsOrePile(object obj)
            {
                int itemID = 0;

                if (obj is Item)
                    itemID = ((Item)obj).ItemID;

                return itemID == 0x19B9 || itemID == 0x19B8 || itemID == 0x19BA || itemID == 0x19B7;
            }

            private bool IsLargeOrePile(object obj)
            {
                int itemID = 0;

                if (obj is Item)
                    itemID = ((Item)obj).ItemID;

                return itemID == 0x19B9; 
            }

            private bool IsMediumOrePile(object obj)
            {
                int itemID = 0;

                if (obj is Item)
                    itemID = ((Item)obj).ItemID;

                return itemID == 0x19B8 || itemID == 0x19BA;
            }

            private bool IsSmallOrePile(object obj)
            {
                int itemID = 0;

                if (obj is Item)
                    itemID = ((Item)obj).ItemID;

                return itemID == 0x19B7;
            }

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Ore.Deleted )
					return;

				if ( !from.InRange( m_Ore.GetWorldLocation(), 2 ) )
				{
					from.SendAsciiMessage( "The ore is too far away." ); // The ore is too far away.
					return;
				}

				if ( IsForge( targeted ) )
				{
					double difficulty;

					switch ( m_Ore.Resource )
					{
						default: difficulty = 50.0; break;
						case CraftResource.DullCopper: difficulty = 65.0; break;
						case CraftResource.ShadowIron: difficulty = 70.0; break;
						case CraftResource.Copper: difficulty = 75.0; break;
						case CraftResource.Bronze: difficulty = 80.0; break;
						case CraftResource.Gold: difficulty = 85.0; break;
						case CraftResource.Agapite: difficulty = 90.0; break;
						case CraftResource.Verite: difficulty = 95.0; break;
						case CraftResource.Valorite: difficulty = 99.0; break;
					}

					double minSkill = difficulty - 25.0;
					double maxSkill = difficulty + 25.0;
					
					if ( difficulty > 50.0 && difficulty > from.Skills[SkillName.Mining].Value )
					{
						from.SendAsciiMessage( "You have no idea how to smelt this strange ore!" ); // You have no idea how to smelt this strange ore!
						return;
					}

                    int toConsume = m_Ore.Amount;
                    if (m_Ore.ItemID == 0x19B7)
                    {
                        if (toConsume < 2)
                        {
                            from.SendAsciiMessage("There is not enough metal-bearing ore in this pile to make an ingot."); // There is not enough metal-bearing ore in this pile to make an ingot.
                            return;
                        }
                    }
                    else if (m_Ore.ItemID == 0x19B8 || m_Ore.ItemID == 0x19BA)
                    {
                        if (toConsume < 1)
                        {
                            from.SendAsciiMessage("There is not enough metal-bearing ore in this pile to make an ingot.");
                            return;
                        }
                    }

					if ( from.CheckTargetSkill( SkillName.Mining, targeted, minSkill, maxSkill ) )
					{
                        if (m_Ore.ItemID == 0x19B7)
                        {
                            if (toConsume < 2)
                            {
                                from.SendAsciiMessage("There is not enough metal-bearing ore in this pile to make an ingot."); // There is not enough metal-bearing ore in this pile to make an ingot.
                            }
                            else
                            {
                                int count = 0;
                                while ((count+2) <= m_Ore.Amount)
                                    count += 2;

                                BaseIngot ingot = m_Ore.GetIngot();
                                ingot.Amount = count/2;

                                m_Ore.Consume(count);
                                from.AddToBackpack(ingot);
                                //from.PlaySound( 0x57 );


                                from.SendAsciiMessage("You smelt the ore removing the impurities and put the metal in your backpack."); // You smelt the ore removing the impurities and put the metal in your backpack.
                            }
                        }
                        else if (m_Ore.ItemID == 0x19B8 || m_Ore.ItemID == 0x19BA)
                        {
                            if (toConsume < 1)
                            {
                                from.SendAsciiMessage("There is not enough metal-bearing ore in this pile to make an ingot.");
                            }
                            else
                            {
                                if (toConsume > 60000)
                                    toConsume = 60000;

                                BaseIngot ingot = m_Ore.GetIngot();
                                ingot.Amount = toConsume;

                                m_Ore.Consume(toConsume);
                                from.AddToBackpack(ingot);
                                //from.PlaySound( 0x57 );


                                from.SendAsciiMessage("You smelt the ore removing the impurities and put the metal in your backpack."); // You smelt the ore removing the impurities and put the metal in your backpack.
                            }
                        }
                        else
                        {
                            if (toConsume <= 0)
                            {
                                from.SendAsciiMessage("There is not enough metal-bearing ore in this pile to make an ingot."); // There is not enough metal-bearing ore in this pile to make an ingot.
                            }
                            else
                            {
                                if (toConsume > 30000)
                                    toConsume = 30000;

                                BaseIngot ingot = m_Ore.GetIngot();
                                ingot.Amount = toConsume * 2;

                                m_Ore.Consume(toConsume);
                                from.AddToBackpack(ingot);
                                //from.PlaySound( 0x57 );


                                from.SendAsciiMessage("You smelt the ore removing the impurities and put the metal in your backpack."); // You smelt the ore removing the impurities and put the metal in your backpack.
                            }
                        }
					}
					else if ( m_Ore.Amount < 2 )
					{
						from.SendAsciiMessage( "You burn away the impurities but are left with no useable metal." ); // You burn away the impurities but are left with no useable metal.
						m_Ore.Delete();
					}
					else
					{
						from.SendAsciiMessage( "You burn away the impurities but are left with less useable metal." ); // You burn away the impurities but are left with less useable metal.
						m_Ore.Amount /= 2;
					}
				}
                else if ( IsOrePile(targeted) ) //combining stuff
                {
                    if (m_Ore == targeted)
                    {
                        return;
                    }
                    
                    BaseOre targetedOre = (BaseOre)targeted;
                    int transferAmount = 0;
                    int transferItemID = 0;

                    if (IsLargeOrePile(m_Ore))
                    {
                        if (IsLargeOrePile(targetedOre))
                        {
                            transferAmount = m_Ore.Amount;
                            transferItemID = targetedOre.ItemID;
                        }
                        else if (IsMediumOrePile(targetedOre))
                        {
                            transferAmount = m_Ore.Amount * 2;
                            transferItemID = targetedOre.ItemID;
                        }
                        else if (IsSmallOrePile(targetedOre))
                        {
                            transferAmount = m_Ore.Amount * 4;
                            transferItemID = targetedOre.ItemID;
                        }
                    }
                    else if (IsMediumOrePile(m_Ore))
                    {
                        if (IsLargeOrePile(targetedOre))
                        {
                            transferAmount = targetedOre.Amount * 2;
                            transferItemID = m_Ore.ItemID;
                        }
                        else if (IsMediumOrePile(targetedOre))
                        {
                            transferAmount = m_Ore.Amount;
                            transferItemID = m_Ore.ItemID;
                        }
                        else if (IsSmallOrePile(targetedOre))
                        {
                            transferAmount = m_Ore.Amount * 2;
                            transferItemID = targetedOre.ItemID;
                        }
                    }
                    else if (IsSmallOrePile(m_Ore))
                    {
                        if (IsLargeOrePile(targetedOre))
                        {
                            transferAmount = targetedOre.Amount * 4;
                            transferItemID = m_Ore.ItemID;
                        }
                        else if (IsMediumOrePile(targetedOre))
                        {
                            transferAmount = targetedOre.Amount * 2;
                            transferItemID = m_Ore.ItemID;
                        }
                        else if (IsSmallOrePile(targetedOre))
                        {
                            transferAmount = m_Ore.Amount;
                            transferItemID = m_Ore.ItemID;
                        }
                    }

                    targetedOre.Amount += transferAmount;
                    targetedOre.ItemID = transferItemID;

                    m_Ore.Delete();
                }
			}
		}
	}

	public class IronOre : BaseOre
	{
		[Constructable]
		public IronOre() : this( 1 )
		{
		}

		[Constructable]
		public IronOre( int amount ) : base( CraftResource.Iron, amount )
		{
            Weight = 20.0;
		}

		public IronOre( Serial serial ) : base( serial )
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
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " iron ore"));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "iron ore"));
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

		

		public override BaseIngot GetIngot()
		{
			return new IronIngot();
		}
	}

    public class SmallIronOre : BaseOre
    {
        [Constructable]
        public SmallIronOre() : this(1)
        {
        }

        [Constructable]
        public SmallIronOre(int amount) : base(CraftResource.Iron, amount)
        {
            Weight = 7.0;

            switch (Utility.Random(2))
            {
                case 0: ItemID = 0x19B8; break;
                case 1: ItemID = 0x19BA; break;
            }
        }

        public SmallIronOre(Serial serial) : base(serial)
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
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " iron ore"));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "iron ore"));
                }
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }



        public override BaseIngot GetIngot()
        {
            return new IronIngot();
        }
    }

    public class SmallestIronOre : BaseOre
    {
        [Constructable] public SmallestIronOre() : this(1)
        {
        }

        [Constructable]
        public SmallestIronOre(int amount) : base(CraftResource.Iron, amount)
        {
            Weight = 2.0;
            ItemID = 0x19B7;
        }

        public SmallestIronOre(Serial serial) : base(serial)
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
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " iron ore"));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "iron ore"));
                }
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }



        public override BaseIngot GetIngot()
        {
            return new IronIngot();
        }
    }

	public class DullCopperOre : BaseOre
	{

		public DullCopperOre() : this( 1 )
		{
		}


		public DullCopperOre( int amount ) : base( CraftResource.DullCopper, amount )
		{
		}

		public DullCopperOre( Serial serial ) : base( serial )
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

		

		public override BaseIngot GetIngot()
		{
			return new DullCopperIngot();
		}
	}

	public class ShadowIronOre : BaseOre
	{

		public ShadowIronOre() : this( 1 )
		{
		}


		public ShadowIronOre( int amount ) : base( CraftResource.ShadowIron, amount )
		{
		}

		public ShadowIronOre( Serial serial ) : base( serial )
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

		

		public override BaseIngot GetIngot()
		{
			return new ShadowIronIngot();
		}
	}

	public class CopperOre : BaseOre
	{

		public CopperOre() : this( 1 )
		{
		}


		public CopperOre( int amount ) : base( CraftResource.Copper, amount )
		{
		}

		public CopperOre( Serial serial ) : base( serial )
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

		

		public override BaseIngot GetIngot()
		{
			return new CopperIngot();
		}
	}

	public class BronzeOre : BaseOre
	{

		public BronzeOre() : this( 1 )
		{
		}


		public BronzeOre( int amount ) : base( CraftResource.Bronze, amount )
		{
		}

		public BronzeOre( Serial serial ) : base( serial )
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

		

		public override BaseIngot GetIngot()
		{
			return new BronzeIngot();
		}
	}

	public class GoldOre : BaseOre
	{

		public GoldOre() : this( 1 )
		{
		}


		public GoldOre( int amount ) : base( CraftResource.Gold, amount )
		{
		}

		public GoldOre( Serial serial ) : base( serial )
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

		

		public override BaseIngot GetIngot()
		{
			return new GoldIngot();
		}
	}

	public class AgapiteOre : BaseOre
	{

		public AgapiteOre() : this( 1 )
		{
		}


		public AgapiteOre( int amount ) : base( CraftResource.Agapite, amount )
		{
		}

		public AgapiteOre( Serial serial ) : base( serial )
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

		

		public override BaseIngot GetIngot()
		{
			return new AgapiteIngot();
		}
	}

	public class VeriteOre : BaseOre
	{

		public VeriteOre() : this( 1 )
		{
		}


		public VeriteOre( int amount ) : base( CraftResource.Verite, amount )
		{
		}

		public VeriteOre( Serial serial ) : base( serial )
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

		

		public override BaseIngot GetIngot()
		{
			return new VeriteIngot();
		}
	}

	public class ValoriteOre : BaseOre
	{

		public ValoriteOre() : this( 1 )
		{
		}


		public ValoriteOre( int amount ) : base( CraftResource.Valorite, amount )
		{
		}

		public ValoriteOre( Serial serial ) : base( serial )
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

		

		public override BaseIngot GetIngot()
		{
			return new ValoriteIngot();
		}
	}
}