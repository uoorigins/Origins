using System;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseMiddleTorso : BaseClothing
	{
		public BaseMiddleTorso( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseMiddleTorso( int itemID, int hue ) : base( itemID, Layer.MiddleTorso, hue )
		{
		}

		public BaseMiddleTorso( Serial serial ) : base( serial )
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

	[Flipable( 0x1541, 0x1542 )]
	public class BodySash : BaseMiddleTorso
	{
		[Constructable]
		public BodySash() : this( 0 )
		{
		}

		[Constructable]
		public BodySash( int hue ) : base( 0x1541, hue )
		{
			Weight = 1.0;
		}

		public BodySash( Serial serial ) : base( serial )
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
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a body sash {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic body sash"));
                }
                else
                {
                    if (Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a body sash sewn by {0}", Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a body sash"));
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

	[Flipable( 0x153d, 0x153e )]
	public class FullApron : BaseMiddleTorso
	{
		[Constructable]
		public FullApron() : this( 0 )
		{
		}

		[Constructable]
		public FullApron( int hue ) : base( 0x153d, hue )
		{
			Weight = 4.0;
		}

		public FullApron( Serial serial ) : base( serial )
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
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a full apron {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic full apron"));
                }
                else
                {
                    if (Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a full apron sewn by {0}", Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a full apron"));
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

	[Flipable( 0x1f7b, 0x1f7c )]
	public class Doublet : BaseMiddleTorso
	{
		[Constructable]
		public Doublet() : this( 0 )
		{
		}

		[Constructable]
		public Doublet( int hue ) : base( 0x1F7B, hue )
		{
			Weight = 2.0;
		}

		public Doublet( Serial serial ) : base( serial )
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
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a doublet {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic doublet"));
                }
                else
                {
                    if (Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a doublet sewn by {0}", Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a doublet"));
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

	[Flipable( 0x1ffd, 0x1ffe )]
	public class Surcoat : BaseMiddleTorso
	{
		[Constructable]
		public Surcoat() : this( 0 )
		{
		}

		[Constructable]
		public Surcoat( int hue ) : base( 0x1FFD, hue )
		{
			Weight = 6.0;
		}

		public Surcoat( Serial serial ) : base( serial )
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
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a surcoat {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic surcoat"));
                }
                else
                {
                    if (Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a surcoat sewn by {0}", Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a surcoat"));
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

			if ( Weight == 3.0 )
				Weight = 6.0;
		}
	}

	[Flipable( 0x1fa1, 0x1fa2 )]
	public class Tunic : BaseMiddleTorso
	{
		[Constructable]
		public Tunic() : this( 0 )
		{
		}

		[Constructable]
		public Tunic( int hue ) : base( 0x1FA1, hue )
		{
			Weight = 5.0;
		}

		public Tunic( Serial serial ) : base( serial )
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
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a tunic {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic tunic"));
                }
                else
                {
                    if (Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a tunic sewn by {0}", Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a tunic"));
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

	[Flipable( 0x2310, 0x230F )]
	public class FormalShirt : BaseMiddleTorso
	{
		[Constructable]
		public FormalShirt() : this( 0 )
		{
		}

		[Constructable]
		public FormalShirt( int hue ) : base( 0x2310, hue )
		{
			Weight = 1.0;
		}

		public FormalShirt( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			if ( Weight == 2.0 )
				Weight = 1.0;
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	[Flipable( 0x1f9f, 0x1fa0 )]
	public class JesterSuit : BaseMiddleTorso
	{
		[Constructable]
		public JesterSuit() : this( 0 )
		{
		}

		[Constructable]
		public JesterSuit( int hue ) : base( 0x1F9F, hue )
		{
			Weight = 4.0;
		}

		public JesterSuit( Serial serial ) : base( serial )
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
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a jester suit {0} ({1} charges)", GetEffectString(), Charges)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic jester suit"));
                }
                else
                {
                    if (Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a jester suit sewn by {0}", Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a jester suit"));
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

	[Flipable( 0x27A1, 0x27EC )]
	public class JinBaori : BaseMiddleTorso
	{

		public JinBaori() : this( 0 )
		{
		}


		public JinBaori( int hue ) : base( 0x27A1, hue )
		{
			Weight = 3.0;
		}

		public JinBaori( Serial serial ) : base( serial )
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
}