using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0xE89, 0xE8a )]
	public class QuarterStaff : BaseStaff
	{
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.DoubleStrike; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.ConcussionBlow; } }

        public override int AosStrengthReq { get { return 30; } }
        public override int AosMinDamage { get { return 11; } }
        public override int AosMaxDamage { get { return 14; } }
        public override int AosSpeed { get { return 48; } }

        public override int OldStrengthReq { get { return 30; } }
        public override int OldMinDamage { get { return 4; } }
        public override int OldMaxDamage { get { return 16; } }
        public override int OldSpeed { get { return 50; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 60; } }

		[Constructable]
		public QuarterStaff() : base( 0xE89 )
		{
			Weight = 4.0;
		}

		public QuarterStaff( Serial serial ) : base( serial )
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
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("an exceptional quarter staff (crafted by {0})", this.Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "an exceptional quarter staff"));
                }
                else if ((this.IsInIDList(from) == false) && ((this.DamageLevel != WeaponDamageLevel.Regular) || (Slayer == SlayerName.Silver) || (Effect != WeaponEffect.None) || (this.DurabilityLevel != WeaponDurabilityLevel.Regular) || (this.AccuracyLevel != WeaponAccuracyLevel.Regular)))
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic quarter staff"));
                }
                else if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                {
                    if ((this.DamageLevel > WeaponDamageLevel.Regular || Effect != WeaponEffect.None) && ((this.DurabilityLevel == WeaponDurabilityLevel.Regular) && (this.AccuracyLevel == WeaponAccuracyLevel.Regular)))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", (Slayer == SlayerName.Silver ? "a silver " : "a ") + "quarter staff " + damagelevel));
                    }
                    else if ((this.DurabilityLevel > WeaponDurabilityLevel.Regular) && ((this.DamageLevel == WeaponDamageLevel.Regular && Effect == WeaponEffect.None) && (this.AccuracyLevel == WeaponAccuracyLevel.Regular)))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + " quarter staff"));
                    }
                    else if ((this.AccuracyLevel > WeaponAccuracyLevel.Regular) && ((this.DamageLevel == WeaponDamageLevel.Regular && Effect == WeaponEffect.None) && (this.DurabilityLevel == WeaponDurabilityLevel.Regular)))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + accuracylevel + " quarter staff"));
                    }



                    else if (((this.DamageLevel > WeaponDamageLevel.Regular || Effect != WeaponEffect.None) && (this.DurabilityLevel > WeaponDurabilityLevel.Regular)) && (this.AccuracyLevel == WeaponAccuracyLevel.Regular))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + " quarter staff " + damagelevel));
                    }
                    else if ((this.DamageLevel > WeaponDamageLevel.Regular || Effect != WeaponEffect.None) && (this.AccuracyLevel > WeaponAccuracyLevel.Regular) && (this.DurabilityLevel == WeaponDurabilityLevel.Regular))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + accuracylevel + " quarter staff " + damagelevel));
                    }
                    else if ((this.DurabilityLevel > WeaponDurabilityLevel.Regular) && (this.AccuracyLevel > WeaponAccuracyLevel.Regular) && (this.DamageLevel == WeaponDamageLevel.Regular && Effect == WeaponEffect.None))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + ", " + accuracylevel + " quarter staff"));
                    }
                    else if ((this.DurabilityLevel > WeaponDurabilityLevel.Regular) && (this.AccuracyLevel > WeaponAccuracyLevel.Regular) && (this.DamageLevel > WeaponDamageLevel.Regular || Effect != WeaponEffect.None))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + ", " + accuracylevel + " quarter staff " + damagelevel));
                    }
                    else
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", (Slayer == SlayerName.Silver ? "a silver " : "a ") + "quarter staff"));
                    }
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a quarter staff"));
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