using System;
using Server;
using Server.Items;
using Server.Spells.Fourth;
using Server.Targeting;

namespace Server.Items
{
	public abstract class BaseStaff : BaseMeleeWeapon
	{
		public override int DefHitSound{ get{ return 0x233; } }
		public override int DefMissSound{ get{ return 0x239; } }

		public override SkillName DefSkill{ get{ return SkillName.Macing; } }
		public override WeaponType DefType{ get{ return WeaponType.Staff; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Bash2H; } }

        public virtual TimeSpan GetUseDelay { get { return TimeSpan.FromSeconds(4.0); } }
        private WandEffect m_WandEffect;

        [CommandProperty(AccessLevel.GameMaster)]
        public WandEffect StaffEffect
        {
            get { return m_WandEffect; }
            set { m_WandEffect = value; InvalidateProperties(); }
        }

        public override string GetEffectString()
        {
            switch ( m_WandEffect )
            {
                default:
                case WandEffect.None: return base.GetEffectString();
                case WandEffect.Clumsiness: return "clumsiness";
                case WandEffect.Feeblemindedness: return "feeblemindedness";
                case WandEffect.Fireball: return "fireballs";
                case WandEffect.Harming: return "harming";
                case WandEffect.Identification: return "identification";
                case WandEffect.Lightning: return "lightning";
                case WandEffect.MagicArrow: return "magic arrow";
                case WandEffect.ManaDraining: return "mana draining";
                case WandEffect.Weakness: return "weakness";
                case WandEffect.Healing: return "healing";
                case WandEffect.GreaterHealing: return "great healing";
            }
        }

		public BaseStaff( int itemID ) : this( itemID, WandEffect.None, 0, 0 )
		{
		}

        public BaseStaff(int itemID, WandEffect effect, int minCharges, int maxCharges ) : base(itemID)
        {
            StaffEffect = effect;
            Charges = Utility.RandomMinMax(minCharges, maxCharges);
        }

		public BaseStaff( Serial serial ) : base( serial )
		{
		}

        public void ConsumeCharge(Mobile from)
        {
            --Charges;

            if (Charges == 0)
                from.SendAsciiMessage("This item is out of charges."); // This item is out of charges.

            ApplyDelayTo(from);
        }

        public virtual void ApplyDelayTo(Mobile from)
        {
            from.BeginAction(typeof(BaseWand));
            Timer.DelayCall(GetUseDelay, new TimerStateCallback(ReleaseWandLock_Callback), from);
        }

        public virtual void ReleaseWandLock_Callback(object state)
        {
            ((Mobile)state).EndAction(typeof(BaseWand));
        }

        public virtual void OnWandUse(Mobile from)
        {
            from.Target = new StaffTarget(this);
        }

        public virtual void DoWandTarget(Mobile from, object o)
        {
            if (Deleted || Charges <= 0 || Parent != from || o is StaticTarget || o is LandTarget)
                return;

            if (OnWandTarget(from, o))
                ConsumeCharge(from);
        }

        public virtual bool OnWandTarget(Mobile from, object o)
        {
            return true;
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
            writer.Write((int)m_WandEffect);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
            if (version == 1)
                m_WandEffect = (WandEffect)reader.ReadInt();
		}

		public override void OnHit( Mobile attacker, Mobile defender, double damageBonus )
		{
			base.OnHit( attacker, defender, damageBonus );

			defender.Stam -= Utility.Random( 3, 3 ); // 3-5 points of stamina loss
		}


	}
}