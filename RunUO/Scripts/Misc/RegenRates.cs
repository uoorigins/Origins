using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.Necromancy;
using Server.Spells.Ninjitsu;
using Server.Mobiles;

namespace Server.Misc
{
	public class RegenRates
	{
		[CallPriority( 10 )]
		public static void Configure()
		{
			Mobile.DefaultHitsRate = TimeSpan.FromSeconds( 11.0 );
			Mobile.DefaultStamRate = TimeSpan.FromSeconds(  7.0 );
			Mobile.DefaultManaRate = TimeSpan.FromSeconds(  1.5 );

			Mobile.ManaRegenRateHandler = new RegenRateHandler( Mobile_ManaRegenRate );
            Mobile.HitsRegenRateHandler = new RegenRateHandler( Mobile_HitsRegenRate );

			if ( Core.AOS )
			{
				Mobile.StamRegenRateHandler = new RegenRateHandler( Mobile_StamRegenRate );
			}
		}

		private static void CheckBonusSkill( Mobile m, int cur, int max, SkillName skill )
		{
			if ( !m.Alive )
				return;

			double n = (double)cur / max;
			double v = Math.Sqrt( m.Skills[skill].Value * 0.005 );

			n *= (1.0 - v);
			n += v;

			m.CheckSkill( skill, n );
		}

		private static bool CheckTransform( Mobile m, Type type )
		{
			return TransformationSpellHelper.UnderTransformation( m, type );
		}

		private static bool CheckAnimal( Mobile m, Type type )
		{
			return AnimalForm.UnderTransformation( m, type );
		}

		private static TimeSpan Mobile_HitsRegenRate( Mobile from )
		{
            if (!from.Player)
                return Mobile.DefaultHitsRate;

            double rate = ((double)20 - ((double)((double)12 / (double)20) * (double)from.Hunger));

            return TimeSpan.FromSeconds(rate);
		}

		private static TimeSpan Mobile_StamRegenRate( Mobile from )
		{
			if ( from.Skills == null )
				return Mobile.DefaultStamRate;

			CheckBonusSkill( from, from.Stam, from.StamMax, SkillName.Focus );

			int points =(int)(from.Skills[SkillName.Focus].Value * 0.1);

			if( (from is BaseCreature && ((BaseCreature)from).IsParagon) || from is Leviathan )
				points += 40;

			int cappedPoints = AosAttributes.GetValue( from, AosAttribute.RegenStam );

			points += cappedPoints;

			if ( points < -1 )
				points = -1;

			return TimeSpan.FromSeconds( 1.0 / (0.1 * (2 + points)) );
		}

		private static TimeSpan Mobile_ManaRegenRate( Mobile from )
		{
            if (!from.Player && !from.Body.IsHuman)
                return TimeSpan.FromSeconds(0.5);
            else
                return Mobile.DefaultManaRate;
		}

		public static double GetArmorOffset( Mobile from )
		{
			double rating = 0.0;

			if ( !Core.AOS )
				rating += GetArmorMeditationValue( from.ShieldArmor as BaseArmor );

			rating += GetArmorMeditationValue( from.NeckArmor as BaseArmor );
			rating += GetArmorMeditationValue( from.HandArmor as BaseArmor );
			rating += GetArmorMeditationValue( from.HeadArmor as BaseArmor );
			rating += GetArmorMeditationValue( from.ArmsArmor as BaseArmor );
			rating += GetArmorMeditationValue( from.LegsArmor as BaseArmor );
			rating += GetArmorMeditationValue( from.ChestArmor as BaseArmor );

			return rating / 4;
		}

		private static double GetArmorMeditationValue( BaseArmor ar )
		{
			if ( ar == null || ar.ArmorAttributes.MageArmor != 0 || ar.Attributes.SpellChanneling != 0 )
				return 0.0;

			switch ( ar.MeditationAllowance )
			{
				default:
				case ArmorMeditationAllowance.None: return ar.BaseArmorRatingScaled;
				case ArmorMeditationAllowance.Half: return ar.BaseArmorRatingScaled / 2.0;
				case ArmorMeditationAllowance.All:  return 0.0;
			}
		}
	}
}