using System;
using Server.Network;
using Server.Items;
using Server.Spells;
using Server.Targeting;
using Server.Spells.Fourth;
using Server.Spells.Third;
using Server.Spells.Second;
using Server.Spells.First;
using Server.Spells.Fifth;

namespace Server.Items
{
	[FlipableAttribute( 0x13F8, 0x13F9 )]
	public class GnarledStaff : BaseStaff
	{
        public override string AsciiName { get { return "gnarled staff"; } }

        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ConcussionBlow; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.ParalyzingBlow; } }

        public override int AosStrengthReq { get { return 20; } }
        public override int AosMinDamage { get { return 15; } }
        public override int AosMaxDamage { get { return 17; } }
        public override int AosSpeed { get { return 33; } }

        public override int OldStrengthReq { get { return 20; } }
        public override int OldMinDamage { get { return 9; } }
        public override int OldMaxDamage { get { return 21; } }
        public override int OldSpeed { get { return 30; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 50; } }

		[Constructable]
		public GnarledStaff() : base( 0x13F8 )
		{
			Weight = 3.0;
		}

		public GnarledStaff( Serial serial ) : base( serial )
		{
		}

        public override void OnDoubleClick(Mobile from)
        {
            if (StaffEffect != WandEffect.None)
            {
                if (!from.CanBeginAction(typeof(BaseWand)))
                    return;

                if (Parent == from)
                {
                    if (Charges > 0)
                        OnWandUse(from);
                    else
                        from.SendAsciiMessage("This item is out of charges."); // This item is out of charges.
                }
                else
                {
                    from.SendAsciiMessage("You must equip this item to use it."); // You must equip this item to use it.
                }
            }
            else
                base.OnDoubleClick(from);
        }

        public virtual void OnWandUse(Mobile from)
        {
            if (StaffEffect == WandEffect.Clumsiness)
                Cast(new ClumsySpell(from, this as GnarledStaff));
            else if (StaffEffect == WandEffect.Identification)
                base.OnWandUse(from);
            else if (StaffEffect == WandEffect.GreaterHealing)
                Cast(new GreaterHealSpell(from, this));
            else if (StaffEffect == WandEffect.Feeblemindedness)
                Cast(new FeeblemindSpell(from, this));
            else if (StaffEffect == WandEffect.Weakness)
                Cast(new WeakenSpell(from, this));
            else if (StaffEffect == WandEffect.MagicArrow)
                Cast(new MagicArrowSpell(from, this));
            else if (StaffEffect == WandEffect.Harming)
                Cast(new HarmSpell(from, this));
            else if (StaffEffect == WandEffect.Fireball)
                Cast(new FireballSpell(from, this));
            else if (StaffEffect == WandEffect.Healing)
                Cast(new HealSpell(from, this));
            else if (StaffEffect == WandEffect.Lightning)
                Cast(new LightningSpell(from, this));
            else if (StaffEffect == WandEffect.ManaDraining)
                Cast(new ManaDrainSpell(from, this));
        }

        public override bool OnWandTarget(Mobile from, object o)
        {
            bool inlist = false;
            if (o is Item)
            {
                if (o is BaseWeapon)
                    inlist = ((BaseWeapon)o).IsInIDList(from);
                else if (o is BaseArmor)
                    inlist = ((BaseArmor)o).IsInIDList(from);
                else if (o is BaseClothing)
                    inlist = ((BaseClothing)o).IsInIDList(from);
                else if (o is BaseJewel)
                    inlist = ((BaseJewel)o).IsInIDList(from);

                if (o is BaseWeapon)
                    ((BaseWeapon)o).AddToIDList(from);
                else if (o is BaseArmor)
                    ((BaseArmor)o).AddToIDList(from);
                else if (o is BaseClothing)
                    ((BaseClothing)o).AddToIDList(from);
                else if (o is BaseJewel)
                    ((BaseJewel)o).AddToIDList(from);

                if (!Core.AOS)
                    ((Item)o).OnSingleClick(from);

                if (o is BaseWeapon && (((BaseWeapon)o).IDList.Count > 50 && !inlist || from.AccessLevel > AccessLevel.Player))
                    ((BaseWeapon)o).RemoveFromIDList(from);
                else if (o is BaseArmor && (((BaseArmor)o).IDList.Count > 50 && !inlist || from.AccessLevel > AccessLevel.Player))
                    ((BaseArmor)o).RemoveFromIDList(from);
                else if (o is BaseClothing && (((BaseClothing)o).IDList.Count > 50 && !inlist || from.AccessLevel > AccessLevel.Player))
                    ((BaseClothing)o).RemoveFromIDList(from);
                else if (o is BaseJewel && (((BaseJewel)o).IDList.Count > 50 && !inlist || from.AccessLevel > AccessLevel.Player))
                    ((BaseJewel)o).RemoveFromIDList(from);

                return (o is Item);
            }
            else if (o is Mobile)
            {
                ((Mobile)o).OnSingleClick(from);
                return (o is Mobile);
            }
            return true;
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