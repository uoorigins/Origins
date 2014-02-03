using System;
using System.Text;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Spells.First;
using Server.Spells;

namespace Server.Items
{
	public enum WandEffect
	{
        None,
		Clumsiness,
		Identification,
		Healing,
        GreaterHealing,
		Feeblemindedness,
		Weakness,
		MagicArrow,
		Harming,
		Fireball,
		Lightning,
		ManaDraining
	}

	public abstract class BaseWand : BaseBashing
	{
		private WandEffect m_WandEffect;

        public override string AsciiName { get { return "wand"; } }

        public override int AosStrengthReq { get { return 5; } }
        public override int AosMinDamage { get { return 9; } }
        public override int AosMaxDamage { get { return 11; } }
        public override int AosSpeed { get { return 40; } }

        public override int OldStrengthReq { get { return 0; } }
        public override int OldMinDamage { get { return 2; } }
        public override int OldMaxDamage { get { return 6; } }
        public override int OldSpeed { get { return 35; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 110; } }

		public virtual TimeSpan GetUseDelay{ get{ return TimeSpan.FromSeconds( 4.0 ); } }

        public override string GetEffectString()
        {
            switch ( m_WandEffect )
            {
                default:
                case WandEffect.None: return "";
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

		[CommandProperty( AccessLevel.GameMaster )]
		public WandEffect Effect
		{
			get{ return m_WandEffect; }
			set{ m_WandEffect = value; InvalidateProperties(); }
		}

		public BaseWand( WandEffect effect, int minCharges, int maxCharges ) : base( Utility.RandomList( 0xDF2, 0xDF3, 0xDF4, 0xDF5 ) )
		{
			Weight = 1.0;
			Effect = effect;
			Charges = Utility.RandomMinMax( minCharges, maxCharges );
		}

		public void ConsumeCharge( Mobile from )
		{
			--Charges;

			if ( Charges == 0 )
				from.SendAsciiMessage( "This item is out of charges." ); // This item is out of charges.

			ApplyDelayTo( from );
		}

		public BaseWand( Serial serial ) : base( serial )
		{
		}

		public virtual void ApplyDelayTo( Mobile from )
		{
			from.BeginAction( typeof( BaseWand ) );
			Timer.DelayCall( GetUseDelay, new TimerStateCallback( ReleaseWandLock_Callback ), from );
		}

		public virtual void ReleaseWandLock_Callback( object state )
		{
			((Mobile)state).EndAction( typeof( BaseWand ) );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.CanBeginAction( typeof( BaseWand ) ) )
				return;

			if ( Parent == from )
			{
				if ( Charges > 0 )
					OnWandUse( from );
				else
					from.SendAsciiMessage( "This item is out of charges." ); // This item is out of charges.
			}
			else
			{
				from.SendAsciiMessage( "You must equip this item to use it." ); // You must equip this item to use it.
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (int) m_WandEffect );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_WandEffect = (WandEffect)reader.ReadInt();
					base.Charges = (int)reader.ReadInt();

					break;
				}
                case 1:
                {
                    m_WandEffect = (WandEffect)reader.ReadInt();

                    break;
                }
			}
		}

		public void Cast( Spell spell )
		{
			bool m = Movable;

			Movable = false;
			spell.Cast();
			Movable = m;
            
		}

		public virtual void OnWandUse( Mobile from )
		{
			from.Target = new WandTarget( this );
		}

		public virtual void DoWandTarget( Mobile from, object o )
		{
			if ( Deleted || Charges <= 0 || Parent != from || o is StaticTarget || o is LandTarget )
				return;

			if ( OnWandTarget( from, o ) )
				ConsumeCharge( from );
		}

		public virtual bool OnWandTarget( Mobile from, object o )
		{
			return true;
		}
	}
}