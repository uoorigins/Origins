using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13FF, 0x13FE )]
	public class Katana : BaseSword
	{
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.DoubleStrike; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.ArmorIgnore; } }

        public override int AosStrengthReq { get { return 25; } }
        public override int AosMinDamage { get { return 11; } }
        public override int AosMaxDamage { get { return 13; } }
        public override int AosSpeed { get { return 46; } }

        public override int OldStrengthReq { get { return 10; } }
        public override int OldMinDamage { get { return 1; } }
        public override int OldMaxDamage { get { return 16; } }
        public override int OldSpeed { get { return 45; } }

        public override int DefHitSound { get { return 0x23B; } }
        public override int DefMissSound { get { return 0x23A; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 90; } }

		[Constructable]
		public Katana() : base( 0x13FF )
		{
			Weight = 6.0;
		}

		public Katana( Serial serial ) : base( serial )
		{
		}

        public override void OnSingleClick(Mobile from)
        {

            string durabilitylevel = GetDurabilityString();
            string accuracylevel = GetAccuracyString();
            string damagelevel = GetDamageString();
            string beginning;

            if ((durabilitylevel == "indestructible") || (accuracylevel == "accurate") || (accuracylevel == "eminently accurate") || (accuracylevel == "exceedingly accurate"))
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
                if (this.Quality == WeaponQuality.Exceptional)
                {
                    if (this.Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("an exceptional katana (crafted by {0})", this.Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "an exceptional katana"));
                }
                else if ((this.IsInIDList(from) == false) && ((this.DamageLevel != WeaponDamageLevel.Regular) || (Slayer == SlayerName.Silver) || (Effect != WeaponEffect.None) || (this.DurabilityLevel != WeaponDurabilityLevel.Regular) || (this.AccuracyLevel != WeaponAccuracyLevel.Regular)))
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic katana"));
                }
                else if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                {
                    if ((this.DamageLevel > WeaponDamageLevel.Regular || Effect != WeaponEffect.None) && ((this.DurabilityLevel == WeaponDurabilityLevel.Regular) && (this.AccuracyLevel == WeaponAccuracyLevel.Regular)))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a " + (Slayer == SlayerName.Silver ? "silver " : "") + "katana " + damagelevel));
                    }
                    else if ((this.DurabilityLevel > WeaponDurabilityLevel.Regular) && ((this.DamageLevel == WeaponDamageLevel.Regular && Effect == WeaponEffect.None) && (this.AccuracyLevel == WeaponAccuracyLevel.Regular)))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + " katana"));
                    }
                    else if ((this.AccuracyLevel > WeaponAccuracyLevel.Regular) && ((this.DamageLevel == WeaponDamageLevel.Regular && Effect == WeaponEffect.None) && (this.DurabilityLevel == WeaponDurabilityLevel.Regular)))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + accuracylevel + " katana"));
                    }



                    else if (((this.DamageLevel > WeaponDamageLevel.Regular || Effect != WeaponEffect.None) && (this.DurabilityLevel > WeaponDurabilityLevel.Regular)) && (this.AccuracyLevel == WeaponAccuracyLevel.Regular))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + " katana " + damagelevel));
                    }
                    else if ((this.DamageLevel > WeaponDamageLevel.Regular || Effect != WeaponEffect.None) && (this.AccuracyLevel > WeaponAccuracyLevel.Regular) && (this.DurabilityLevel == WeaponDurabilityLevel.Regular))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + accuracylevel + " katana " + damagelevel));
                    }
                    else if ((this.DurabilityLevel > WeaponDurabilityLevel.Regular) && (this.AccuracyLevel > WeaponAccuracyLevel.Regular) && (this.DamageLevel == WeaponDamageLevel.Regular && Effect == WeaponEffect.None))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + ", " + accuracylevel + " katana"));
                    }
                    else if ((this.DurabilityLevel > WeaponDurabilityLevel.Regular) && (this.AccuracyLevel > WeaponAccuracyLevel.Regular) && (this.DamageLevel > WeaponDamageLevel.Regular || Effect != WeaponEffect.None))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + ", " + accuracylevel + " katana " + damagelevel));
                    }
                    else
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a " + (Slayer == SlayerName.Silver ? "silver " : "") + "katana"));
                    }
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a katana"));
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