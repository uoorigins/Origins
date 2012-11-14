using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class MetalShield : BaseShield
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 1; } }
		public override int BaseColdResistance{ get{ return 0; } }
		public override int BasePoisonResistance{ get{ return 0; } }
		public override int BaseEnergyResistance{ get{ return 0; } }

		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 65; } }

		public override int AosStrReq{ get{ return 45; } }
        public override int OldStrReq { get { return 15; } }

		public override int ArmorBase{ get{ return 11; } }

		[Constructable]
		public MetalShield() : base( 0x1B7B )
		{
			Weight = 6.0;
		}

		public MetalShield( Serial serial ) : base(serial)
		{
		}

        public override void OnSingleClick(Mobile from)
        {

            string durabilitylevel = GetDurabilityLevel();
            string protectionlevel = GetProtectionLevel();
            string beginning;

            if (durabilitylevel == "indestructible")
            {
                beginning = "an ";
            }
            else
            {
                beginning = "a ";
            }

            if (this.Name != null)
            {
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            }
            else
            {
                if (this.Quality == ArmorQuality.Exceptional)
                {
                    if (this.Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("an exceptional metal shield (crafted by {0})", this.Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "an exceptional metal shield"));
                }
                else if (IsInIDList(from) == false && (this.ProtectionLevel != ArmorProtectionLevel.Regular || this.Durability != ArmorDurabilityLevel.Regular))
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic metal shield"));
                }
                else if (IsInIDList(from) == true)
                {
                    if (this.Durability > ArmorDurabilityLevel.Regular && this.ProtectionLevel == ArmorProtectionLevel.Regular)
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + " metal shield"));
                    }
                    else if (this.ProtectionLevel > ArmorProtectionLevel.Regular && this.Durability == ArmorDurabilityLevel.Regular)
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a metal shield " + protectionlevel));
                    }
                    else if (this.ProtectionLevel > ArmorProtectionLevel.Regular && this.Durability > ArmorDurabilityLevel.Regular)
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + " metal shield " + protectionlevel));
                    }
                    else
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a metal shield"));
                    }
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a metal shield"));
                }
            }
        }


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
		}
	}
}
