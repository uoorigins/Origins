using System;
using Server.Engines.Craft;
using Server.Network;
using System.Collections.Generic;

namespace Server.Items
{
	public abstract class BaseHat : BaseClothing, IShipwreckedItem
	{
		private bool m_IsShipwreckedItem;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsShipwreckedItem
		{
			get { return m_IsShipwreckedItem; }
			set { m_IsShipwreckedItem = value; }
		}

		public BaseHat( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseHat( int itemID, int hue ) : base( itemID, Layer.Helm, hue )
		{
		}

		public BaseHat( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( m_IsShipwreckedItem );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_IsShipwreckedItem = reader.ReadBool();
					break;
				}
			}
		}

		public override void AddEquipInfoAttributes( Mobile from, List<EquipInfoAttribute> attrs )
		{
			base.AddEquipInfoAttributes( from, attrs );

			if( m_IsShipwreckedItem )
				attrs.Add( new EquipInfoAttribute( 1041645 ) );	// recovered from a shipwreck
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );

			if ( m_IsShipwreckedItem )
				list.Add( 1041645 ); // recovered from a shipwreck
		}

		public override int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			Quality = (ClothingQuality)quality;

			if( Quality == ClothingQuality.Exceptional )
				DistributeBonuses( (tool is BaseRunicTool ? 6 : (Core.SE ? 15 : 14)) );	//BLAME OSI. (We can't confirm it's an OSI bug yet.)

			return base.OnCraft( quality, makersMark, from, craftSystem, typeRes, tool, craftItem, resHue );
		}

	}
	[Flipable( 0x2798, 0x27E3 )]
	public class Kasa : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public Kasa() : this( 0 )
		{
		}


		public Kasa( int hue ) : base( 0x2798, hue )
		{
			Weight = 3.0;
		}

		public Kasa( Serial serial ) : base( serial )
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
	}

	[Flipable( 0x278F, 0x27DA )]
	public class ClothNinjaHood : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 9; } }
		public override int BaseEnergyResistance{ get{ return 9; } }


		public ClothNinjaHood() : this( 0 )
		{
		}


		public ClothNinjaHood( int hue ) : base( 0x278F, hue )
		{
			Weight = 2.0;
		}

		public ClothNinjaHood( Serial serial ) : base( serial )
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
	}

	[Flipable( 0x2306, 0x2305 )]
	public class FlowerGarland : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 9; } }
		public override int BaseEnergyResistance{ get{ return 9; } }

		public FlowerGarland() : this( 0 )
		{
		}


		public FlowerGarland( int hue ) : base( 0x2306, hue )
		{
			Weight = 1.0;
		}

		public FlowerGarland( Serial serial ) : base( serial )
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
	}

	public class FloppyHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public FloppyHat() : this( 0 )
		{
		}

		[Constructable]
		public FloppyHat( int hue ) : base( 0x1713, hue )
		{
			Weight = 1.0;
		}

		public FloppyHat( Serial serial ) : base( serial )
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
                if (Effect != ClothEffect.None)
                {
                    if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a floppy hat {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic floppy hat"));
                }
                else
                {
                    if (Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a floppy hat sewn by {0}", Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a floppy hat"));
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

	public class WideBrimHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public WideBrimHat() : this( 0 )
		{
		}

		[Constructable]
		public WideBrimHat( int hue ) : base( 0x1714, hue )
		{
			Weight = 1.0;
		}

		public WideBrimHat( Serial serial ) : base( serial )
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
                if (Effect != ClothEffect.None)
                {
                    if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a wide-brim hat {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic wide-brim hat"));
                }
                else
                {
                    if (Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a wide-brim hat sewn by {0}", Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a wide-brim hat"));
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

	public class Cap : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public Cap() : this( 0 )
		{
		}

		[Constructable]
		public Cap( int hue ) : base( 0x1715, hue )
		{
			Weight = 1.0;
		}

		public Cap( Serial serial ) : base( serial )
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
                if (Effect != ClothEffect.None)
                {
                    if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a cap {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic cap"));
                }
                else
                {
                    if (Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a cap sewn by {0}", Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a cap"));
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

	public class SkullCap : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BasePoisonResistance{ get{ return 8; } }
		public override int BaseEnergyResistance{ get{ return 8; } }

		[Constructable]
		public SkullCap() : this( 0 )
		{
		}

		[Constructable]
		public SkullCap( int hue ) : base( 0x1544, hue )
		{
			Weight = 1.0;
		}

		public SkullCap( Serial serial ) : base( serial )
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
                if (Effect != ClothEffect.None)
                {
                    if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a skull cap {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic skull cap"));
                }
                else
                {
                    if (Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a skull cap sewn by {0}", Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a skull cap"));
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

	public class Bandana : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BasePoisonResistance{ get{ return 8; } }
		public override int BaseEnergyResistance{ get{ return 8; } }

		[Constructable]
		public Bandana() : this( 0 )
		{
		}

		[Constructable]
		public Bandana( int hue ) : base( 0x1540, hue )
		{
			Weight = 1.0;
		}

		public Bandana( Serial serial ) : base( serial )
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
                if (Effect != ClothEffect.None)
                {
                    if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a bandana {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic bandana"));
                }
                else
                {
                    if (Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a bandana sewn by {0}", Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a bandana"));
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

    [Flipable( 0x1545, 0x1546 )]
	public class BearMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 5; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 8; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		[Constructable]
		public BearMask() : this( 0 )
		{
		}

		[Constructable]
		public BearMask( int hue ) : base( Utility.RandomList(0x1545, 0x1546), hue )
		{
			Weight = 5.0;
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
            from.SendAsciiMessage( sender.FailMessage );
			return false;
		}

		public BearMask( Serial serial ) : base( serial )
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
                if (Effect != ClothEffect.None)
                {
                    if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a bear mask {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic bear mask"));
                }
                else
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a bear mask"));
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

    [Flipable( 0x1547, 0x1548 )]
	public class DeerMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 2; } }
		public override int BaseFireResistance{ get{ return 6; } }
		public override int BaseColdResistance{ get{ return 8; } }
		public override int BasePoisonResistance{ get{ return 1; } }
		public override int BaseEnergyResistance{ get{ return 7; } }

		[Constructable]
		public DeerMask() : this( 0 )
		{
		}

		[Constructable]
		public DeerMask( int hue ) : base( Utility.RandomList(0x1547, 0x1548), hue )
		{
			Weight = 4.0;
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
            from.SendAsciiMessage( sender.FailMessage );
			return false;
		}

		public DeerMask( Serial serial ) : base( serial )
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
                if (Effect != ClothEffect.None)
                {
                    if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a deer mask {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic deer mask"));
                }
                else
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a deer mask"));
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

    [Flipable( 0x1549, 5450 )]
	public class HornedTribalMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 6; } }
		public override int BaseFireResistance{ get{ return 9; } }
		public override int BaseColdResistance{ get{ return 0; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public HornedTribalMask() : this( 0 )
		{
		}

		[Constructable]
		public HornedTribalMask( int hue ) : base( Utility.RandomList(0x1549, 5450), hue )
		{
			Weight = 2.0;
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
            from.SendAsciiMessage( sender.FailMessage );
			return false;
		}

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                if (Effect != ClothEffect.None)
                {
                    if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a tribal mask {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic tribal mask"));
                }
                else
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a tribal mask"));
            }
        }

		public HornedTribalMask( Serial serial ) : base( serial )
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
	}

    [Flipable( 0x154B, 5452 )]
	public class TribalMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public TribalMask() : this( 0 )
		{
		}

		[Constructable]
		public TribalMask( int hue ) : base( Utility.RandomList(0x154B,5452), hue )
		{
			Weight = 2.0;
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
            from.SendAsciiMessage( sender.FailMessage );
			return false;
		}

		public TribalMask( Serial serial ) : base( serial )
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
                if (Effect != ClothEffect.None)
                {
                    if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a tribal mask {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic tribal mask"));
                }
                else
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a tribal mask"));
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

	public class TallStrawHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public TallStrawHat() : this( 0 )
		{
		}

		[Constructable]
		public TallStrawHat( int hue ) : base( 0x1716, hue )
		{
			Weight = 1.0;
		}

		public TallStrawHat( Serial serial ) : base( serial )
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
                if (Effect != ClothEffect.None)
                {
                    if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a tall straw hat {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic tall straw hat"));
                }
                else
                {
                    if (Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a tall straw hat sewn by {0}", Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a tall straw hat"));
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

	public class StrawHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public StrawHat() : this( 0 )
		{
		}

		[Constructable]
		public StrawHat( int hue ) : base( 0x1717, hue )
		{
			Weight = 1.0;
		}

		public StrawHat( Serial serial ) : base( serial )
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
                if (Effect != ClothEffect.None)
                {
                    if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a straw hat {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic straw hat"));
                }
                else
                {
                    if (Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a straw hat sewn by {0}", Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a straw hat"));
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

	public class OrcishKinMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 1; } }
		public override int BaseFireResistance{ get{ return 1; } }
		public override int BaseColdResistance{ get{ return 7; } }
		public override int BasePoisonResistance{ get{ return 7; } }
		public override int BaseEnergyResistance{ get{ return 8; } }

		public override bool Dye( Mobile from, DyeTub sender )
		{
            from.SendAsciiMessage( sender.FailMessage );
			return false;
		}

		public override string DefaultName
		{
			get { return "a mask of orcish kin"; }
		}


		public OrcishKinMask() : this( 0x8A4 )
		{
		}


		public OrcishKinMask( int hue ) : base( 0x141B, hue )
		{
			Weight = 2.0;
		}

		public override bool CanEquip( Mobile m )
		{
			if ( !base.CanEquip( m ) )
				return false;

			if ( m.BodyMod == 183 || m.BodyMod == 184 )
			{
				m.SendLocalizedMessage( 1061629 ); // You can't do that while wearing savage kin paint.
				return false;
			}

			return true;
		}

		public override void OnAdded( object parent )
		{
			base.OnAdded( parent );

			if ( parent is Mobile )
				Misc.Titles.AwardKarma( (Mobile)parent, -20, true );
		}

		public OrcishKinMask( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a mask of orcish kin"));
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

			if ( Hue != 0x8A4 )
				Hue = 0x8A4;
		}
	}

    public class OrcMask : BaseHat
    {
        public override int BasePhysicalResistance { get { return 1; } }
        public override int BaseFireResistance { get { return 1; } }
        public override int BaseColdResistance { get { return 7; } }
        public override int BasePoisonResistance { get { return 7; } }
        public override int BaseEnergyResistance { get { return 8; } }

        public override bool Dye(Mobile from, DyeTub sender)
        {
            from.SendAsciiMessage( sender.FailMessage );
            return false;
        }

        public override string DefaultName
        {
            get { return "an orc mask"; }
        }

        [Constructable]
        public OrcMask() : this(0)
        {
        }

        [Constructable]
        public OrcMask(int hue) : base(0x141B, hue)
        {
            Weight = 2.0;
        }

        /*public override bool CanEquip(Mobile m)
        {
            if (!base.CanEquip(m))
                return false;

            if (m.BodyMod == 183 || m.BodyMod == 184)
            {
                m.SendLocalizedMessage(1061629); // You can't do that while wearing savage kin paint.
                return false;
            }

            return true;
        }*/

        /*public override void OnAdded(object parent)
        {
            base.OnAdded(parent);

            if (parent is Mobile)
                Misc.Titles.AwardKarma((Mobile)parent, -20, true);
        }*/

        public OrcMask(Serial serial) : base(serial)
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "an orc mask"));
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
    }

	public class SavageMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public static int GetRandomHue()
		{
			int v = Utility.RandomBirdHue();

			if ( v == 2101 )
				v = 0;

			return v;
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
            from.SendAsciiMessage( sender.FailMessage );
			return false;
		}


		public SavageMask() : this( GetRandomHue() )
		{
		}


		public SavageMask( int hue ) : base( 0x154B, hue )
		{
			Weight = 2.0;
		}

		public SavageMask( Serial serial ) : base( serial )
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

			if ( Hue != 0 && (Hue < 2101 || Hue > 2130) )
				Hue = GetRandomHue();
		}
	}

	public class WizardsHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public WizardsHat() : this( 0 )
		{
		}

		[Constructable]
		public WizardsHat( int hue ) : base( 0x1718, hue )
		{
			Weight = 1.0;
		}

		public WizardsHat( Serial serial ) : base( serial )
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
                if (Effect != ClothEffect.None)
                {
                    if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a wizards hat {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic wizards hat"));
                }
                else
                {
                    if (Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a wizards hat sewn by {0}", Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a wizards hat"));
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

	public class MagicWizardsHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int LabelNumber{ get{ return 1041072; } } // a magical wizard's hat

		public override int BaseStrBonus{ get{ return -5; } }
		public override int BaseDexBonus{ get{ return -5; } }
		public override int BaseIntBonus{ get{ return +5; } }

		[Constructable]
		public MagicWizardsHat() : this( 0 )
		{
		}

		[Constructable]
		public MagicWizardsHat( int hue ) : base( 0x1718, hue )
		{
			Weight = 1.0;
		}

		public MagicWizardsHat( Serial serial ) : base( serial )
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
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magical wizards hat"));
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

	public class Bonnet : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public Bonnet() : this( 0 )
		{
		}

		[Constructable]
		public Bonnet( int hue ) : base( 0x1719, hue )
		{
			Weight = 1.0;
		}

		public Bonnet( Serial serial ) : base( serial )
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
                if (Effect != ClothEffect.None)
                {
                    if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a bonnet {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic bonnet"));
                }
                else
                {
                    if (Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a bonnet sewn by {0}", Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a bonnet"));
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

	public class FeatheredHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public FeatheredHat() : this( 0 )
		{
		}

		[Constructable]
		public FeatheredHat( int hue ) : base( 0x171A, hue )
		{
			Weight = 1.0;
		}

		public FeatheredHat( Serial serial ) : base( serial )
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
                if (Effect != ClothEffect.None)
                {
                    if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a feathered hat {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic feathered hat"));
                }
                else
                {
                    if (Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a feathered hat sewn by {0}", Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a feathered hat"));
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

	public class TricorneHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public TricorneHat() : this( 0 )
		{
		}

		[Constructable]
		public TricorneHat( int hue ) : base( 0x171B, hue )
		{
			Weight = 1.0;
		}

		public TricorneHat( Serial serial ) : base( serial )
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
                if (Effect != ClothEffect.None)
                {
                    if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a tricorne hat {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic tricorne hat"));
                }
                else
                {
                    if (Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a tricorne hat sewn by {0}", Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a tricorne hat"));
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

	public class JesterHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public JesterHat() : this( 0 )
		{
		}

		[Constructable]
		public JesterHat( int hue ) : base( 0x171C, hue )
		{
			Weight = 1.0;
		}

		public JesterHat( Serial serial ) : base( serial )
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
                if (Effect != ClothEffect.None)
                {
                    if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a jester hat {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic jester hat"));
                }
                else
                {
                    if (Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a jester hat sewn by {0}", Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a jester hat"));
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