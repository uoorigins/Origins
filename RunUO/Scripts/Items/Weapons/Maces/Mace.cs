using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0xF5C, 0xF5D )]
	public class Mace : BaseBashing
	{
        public override string AsciiName { get { return "mace"; } }

        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ConcussionBlow; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.Disarm; } }

        public override int AosStrengthReq { get { return 45; } }
        public override int AosMinDamage { get { return 12; } }
        public override int AosMaxDamage { get { return 14; } }
        public override int AosSpeed { get { return 40; } }

        public override int OldStrengthReq { get { return 20; } }
        public override int OldMinDamage { get { return 5; } }
        public override int OldMaxDamage { get { return 13; } }
        public override int OldSpeed { get { return 50; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 70; } }

		[Constructable]
		public Mace() : base( 0xF5C )
		{
			Weight = 14.0;
		}

		public Mace( Serial serial ) : base( serial )
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

    [FlipableAttribute(0xF5C, 0xF5D)]
    public class PracticeMace : BaseBashing
    {
        public override string AsciiName { get { return "mace (practice weapon)"; } }

        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ConcussionBlow; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.Disarm; } }

        public override int AosStrengthReq { get { return 45; } }
        public override int AosMinDamage { get { return 12; } }
        public override int AosMaxDamage { get { return 14; } }
        public override int AosSpeed { get { return 40; } }

        public override int OldStrengthReq { get { return 10; } }
        public override int OldMinDamage { get { return 2; } }
        public override int OldMaxDamage { get { return 8; } }
        public override int OldSpeed { get { return 50; } }

        public override int InitMinHits { get { return 21; } }
        public override int InitMaxHits { get { return 40; } }

        [Constructable]
        public PracticeMace() : base(0xF5C)
        {
            Weight = 14.0;
        }

        public PracticeMace(Serial serial) : base(serial)
        {
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
            else
                from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "a mace (practice weapon)"));
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