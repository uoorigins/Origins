using System;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseRing : BaseJewel
	{
		public override int BaseGemTypeNumber{ get{ return 1044176; } } // star sapphire ring

		public BaseRing( int itemID ) : base( itemID, Layer.Ring )
		{
		}

		public BaseRing( Serial serial ) : base( serial )
		{
		}

        /*public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a ring"));
            }
        }*/

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                if (Effect != JewelEffect.None)
                {
                    if (Identified || from.AccessLevel >= AccessLevel.GameMaster)
                    {
                        if (Effect == JewelEffect.Invisibility)
                            from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a ring of invisibility ({0} charges)", Charges)));
                        else if (Effect == JewelEffect.Teleportation)
                            from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a ring of teleportation ({0} charges)", Charges)));
                    }
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic ring"));

                }
                else
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a ring"));
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

	public class GoldRing : BaseRing
	{
		[Constructable]
		public GoldRing() : base( 0x108a )
		{
			Weight = 0.1;
		}

		public GoldRing( Serial serial ) : base( serial )
		{
		}

        /*public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                if (Effect != JewelEffect.None)
                {
                    if (this.Identified)
                    {
                        if (Effect == JewelEffect.Invisibility)
                            from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a ring of invisibility"));
                        else if (Effect == JewelEffect.Teleportation)
                            from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a ring of teleportation"));
                    }
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic ring"));

                }
                else
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a ring"));
            }
        }
        */
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

	public class SilverRing : BaseRing
	{
		[Constructable]
		public SilverRing() : base( 0x1F09 )
		{
			Weight = 0.1;
		}

		public SilverRing( Serial serial ) : base( serial )
		{
		}

       /* public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                if (Effect != JewelEffect.None)
                {
                    if (this.Identified)
                    {
                        if (Effect == JewelEffect.Invisibility)
                            from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a ring of invisibility"));
                        else if (Effect == JewelEffect.Teleportation)
                            from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a ring of teleportation"));
                    }
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic ring"));

                }
                else
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a ring"));
            }
        }*/

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
