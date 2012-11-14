using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13B2, 0x13B1 )]
	public class Bow : BaseRanged
	{
        public override int EffectID { get { return 0xF42; } }
        public override Type AmmoType { get { return typeof(Arrow); } }
        public override Item Ammo { get { return new Arrow(); } }

        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ParalyzingBlow; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.MortalStrike; } }

        public override int AosStrengthReq { get { return 30; } }
        public override int AosMinDamage { get { return 16; } }
        public override int AosMaxDamage { get { return 18; } }
        public override int AosSpeed { get { return 25; } }

        public override int OldStrengthReq { get { return 20; } }
        public override int OldMinDamage { get { return 9; } }
        public override int OldMaxDamage { get { return 41; } }
        public override int OldSpeed { get { return 20; } }

        public override int DefMaxRange { get { return 10; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 60; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		[Constructable]
		public Bow() : base( 0x13B2 )
		{
			Weight = 6.0;
			Layer = Layer.TwoHanded;
		}

		public Bow( Serial serial ) : base( serial )
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
                /*if (this.Quality == WeaponQuality.Exceptional)
                {
                    if (this.Crafter != null)
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("an exceptional bow (crafted by {0})", this.Crafter.Name)));
                    else
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "an exceptional bow"));
                }
                else*/ if ((this.IsInIDList(from) == false) && ((this.DamageLevel != WeaponDamageLevel.Regular) || (Slayer == SlayerName.Silver) || (Effect != WeaponEffect.None) || (this.DurabilityLevel != WeaponDurabilityLevel.Regular) || (this.AccuracyLevel != WeaponAccuracyLevel.Regular)))
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a magic bow"));
                }
                else if (IsInIDList(from) || from.AccessLevel >= AccessLevel.GameMaster)
                {
                    if ((this.DamageLevel > WeaponDamageLevel.Regular || Effect != WeaponEffect.None) && ((this.DurabilityLevel == WeaponDurabilityLevel.Regular) && (this.AccuracyLevel == WeaponAccuracyLevel.Regular)))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a " + (Slayer == SlayerName.Silver ? "silver " : "") + "bow " + damagelevel));
                    }
                    else if ((this.DurabilityLevel > WeaponDurabilityLevel.Regular) && ((this.DamageLevel == WeaponDamageLevel.Regular && Effect == WeaponEffect.None) && (this.AccuracyLevel == WeaponAccuracyLevel.Regular)))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + " bow"));
                    }
                    else if ((this.AccuracyLevel > WeaponAccuracyLevel.Regular) && ((this.DamageLevel == WeaponDamageLevel.Regular && Effect == WeaponEffect.None) && (this.DurabilityLevel == WeaponDurabilityLevel.Regular)))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + accuracylevel + " bow"));
                    }



                    else if (((this.DamageLevel > WeaponDamageLevel.Regular || Effect != WeaponEffect.None) && (this.DurabilityLevel > WeaponDurabilityLevel.Regular)) && (this.AccuracyLevel == WeaponAccuracyLevel.Regular))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + " bow " + damagelevel));
                    }
                    else if ((this.DamageLevel > WeaponDamageLevel.Regular || Effect != WeaponEffect.None) && (this.AccuracyLevel > WeaponAccuracyLevel.Regular) && (this.DurabilityLevel == WeaponDurabilityLevel.Regular))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + accuracylevel + " bow " + damagelevel));
                    }
                    else if ((this.DurabilityLevel > WeaponDurabilityLevel.Regular) && (this.AccuracyLevel > WeaponAccuracyLevel.Regular) && (this.DamageLevel == WeaponDamageLevel.Regular && Effect == WeaponEffect.None))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + ", " + accuracylevel + " bow"));
                    }
                    else if ((this.DurabilityLevel > WeaponDurabilityLevel.Regular) && (this.AccuracyLevel > WeaponAccuracyLevel.Regular) && (this.DamageLevel > WeaponDamageLevel.Regular || Effect != WeaponEffect.None))
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", beginning + durabilitylevel + ", " + accuracylevel + " bow " + damagelevel));
                    }
                    else
                    {
                        from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a " + (Slayer == SlayerName.Silver ? "silver " : "") + "bow"));
                    }
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a bow"));
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

			if ( Weight == 7.0 )
				Weight = 6.0;
		}
	}

    [FlipableAttribute(0x13B2, 0x13B1)]
    public class PracticeBow : BaseRanged
    {
        public override int EffectID { get { return 0xF42; } }
        public override Type AmmoType { get { return typeof(Arrow); } }
        public override Item Ammo { get { return new Arrow(); } }

        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ParalyzingBlow; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.MortalStrike; } }

        public override int AosStrengthReq { get { return 30; } }
        public override int AosMinDamage { get { return 16; } }
        public override int AosMaxDamage { get { return 18; } }
        public override int AosSpeed { get { return 25; } }

        public override int OldStrengthReq { get { return 10; } }
        public override int OldMinDamage { get { return 2; } }
        public override int OldMaxDamage { get { return 8; } }
        public override int OldSpeed { get { return 20; } }

        public override int DefMaxRange { get { return 10; } }

        public override int InitMinHits { get { return 21; } }
        public override int InitMaxHits { get { return 40; } }

        public override WeaponAnimation DefAnimation { get { return WeaponAnimation.ShootBow; } }

        [Constructable]
        public PracticeBow(): base(0x13B2)
        {
            Weight = 6.0;
            Layer = Layer.TwoHanded;
        }

        public PracticeBow(Serial serial) : base(serial)
        {
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            else
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a bow (practice weapon)"));
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

            if (Weight == 7.0)
                Weight = 6.0;
        }
    }
}