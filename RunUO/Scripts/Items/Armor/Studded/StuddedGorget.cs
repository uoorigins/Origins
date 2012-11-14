using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class StuddedGorget : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 2; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 35; } }
		public override int InitMaxHits{ get{ return 45; } }

		public override int AosStrReq{ get{ return 25; } }
		public override int OldStrReq{ get{ return 25; } }

		public override int ArmorBase{ get{ return 16; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Studded; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.Half; } }

		[Constructable]
		public StuddedGorget() : base( 0x13D6 )
		{
			Weight = 1.0;
		}

		public StuddedGorget( Serial serial ) : base( serial )
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
                if (this.Crafter != null)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a studded gorget sewn by {0}", this.Crafter.Name)));
                }
                else if (IsInIDList(from) == false && (this.ProtectionLevel != ArmorProtectionLevel.Regular || this.Durability != ArmorDurabilityLevel.Regular))
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic studded gorget"));
                }
                else if (IsInIDList(from) == true)
                {
                    if (this.Durability > ArmorDurabilityLevel.Regular && this.ProtectionLevel == ArmorProtectionLevel.Regular)
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + " studded gorget"));
                    }
                    else if (this.ProtectionLevel > ArmorProtectionLevel.Regular && this.Durability == ArmorDurabilityLevel.Regular)
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a studded gorget " + protectionlevel));
                    }
                    else if (this.ProtectionLevel > ArmorProtectionLevel.Regular && this.Durability > ArmorDurabilityLevel.Regular)
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + " studded gorget " + protectionlevel));
                    }
                    else
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a studded gorget"));
                    }
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a studded gorget"));
                }
            }
        }
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Weight == 2.0 )
				Weight = 1.0;
		}
	}
}