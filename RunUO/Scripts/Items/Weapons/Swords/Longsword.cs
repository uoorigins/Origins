using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0xF61, 0xF60 )]
	public class Longsword : BaseSword
	{
        public override string AsciiName { get { return "longsword"; } }

        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ArmorIgnore; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.ConcussionBlow; } }

        public override int AosStrengthReq { get { return 35; } }
        public override int AosMinDamage { get { return 15; } }
        public override int AosMaxDamage { get { return 16; } }
        public override int AosSpeed { get { return 30; } }

        public override int OldStrengthReq { get { return 25; } }
        public override int OldMinDamage { get { return 4; } }
        public override int OldMaxDamage { get { return 19; } }
        public override int OldSpeed { get { return 43; } }

        public override int DefHitSound { get { return 0x237; } }
        public override int DefMissSound { get { return 0x23A; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 110; } }

		[Constructable]
		public Longsword() : base( 0xF61 )
		{
			Weight = 7.0;
		}

		public Longsword( Serial serial ) : base( serial )
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

    [FlipableAttribute(0xF61, 0xF60)]
    public class PracticeLongsword : BaseSword
    {
        public override string AsciiName { get { return "longsword (practice weapon)"; } }

        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ArmorIgnore; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.ConcussionBlow; } }

        public override int AosStrengthReq { get { return 35; } }
        public override int AosMinDamage { get { return 15; } }
        public override int AosMaxDamage { get { return 16; } }
        public override int AosSpeed { get { return 30; } }

        public override int OldStrengthReq { get { return 10; } }
        public override int OldMinDamage { get { return 2; } }
        public override int OldMaxDamage { get { return 8; } }
        public override int OldSpeed { get { return 43; } }

        public override int DefHitSound { get { return 0x237; } }
        public override int DefMissSound { get { return 0x23A; } }

        public override int InitMinHits { get { return 21; } }
        public override int InitMaxHits { get { return 40; } }

        [Constructable]
        public PracticeLongsword() : base(0xF61)
        {
            Weight = 7.0;
        }

        public PracticeLongsword(Serial serial) : base(serial)
        {
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            else
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a longsword (practice weapon)"));
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
}