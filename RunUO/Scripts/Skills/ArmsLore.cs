using System;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.SkillHandlers
{
	public class ArmsLore
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.ArmsLore].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse(Mobile m)
		{
			m.Target = new InternalTarget();

            m.SendAsciiMessage("What item do you wish to get information about?");
			//m.SendLocalizedMessage( 500349 ); // What item do you wish to get information about?

			return TimeSpan.FromSeconds( 10.0 );
		}

		[PlayerVendorTarget]
		private class InternalTarget : Target
		{
			public InternalTarget() : base( 2, false, TargetFlags.None )
			{
				AllowNonlocal = true;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is BaseWeapon )
				{
					if ( from.CheckTargetSkill( SkillName.ArmsLore, targeted, 0, 100 ) )
					{
						BaseWeapon weap = (BaseWeapon)targeted;

						if ( weap.MaxHitPoints != 0 )
						{
							int hp = (int)((weap.HitPoints / (double)weap.MaxHitPoints) * 10);

							if ( hp < 0 )
								hp = 0;
							else if ( hp > 9 )
								hp = 9;

							//from.SendLocalizedMessage( 1038285 + hp );
                            switch (hp)
                            {
                                case 0: from.SendAsciiMessage("It looks like it is about to fall apart."); break;
                                case 1: from.SendAsciiMessage("It looks rather flimsy and not at all trustworthy."); break;
                                case 2: from.SendAsciiMessage("It looks somewhat badly damaged."); break;
                                case 3: from.SendAsciiMessage("It looks rather battered."); break;
                                case 4: from.SendAsciiMessage("It looks like it has been well-used."); break;
                                case 5: from.SendAsciiMessage("It looks to have suffered some wear and tear."); break;
                                case 6: from.SendAsciiMessage("It looks to be in fairly good condition."); break;
                                case 7: from.SendAsciiMessage("It looks fairly new, with just a few nicks and scratches."); break;
                                case 8: from.SendAsciiMessage("It looks almost new."); break;
                                case 9: from.SendAsciiMessage("It looks unused."); break;
                            }
						}

						int damage = (weap.MaxDamage + weap.MinDamage) / 2;
						int hand = (weap.Layer == Layer.OneHanded ? 0 : 1);

						if ( damage < 3 )
							damage = 0;
						else
							damage = (int)Math.Ceiling( Math.Min( damage, 30 ) / 5.0 );
							/*
						else if ( damage < 6 )
							damage = 1;
						else if ( damage < 11 )
							damage = 2;
						else if ( damage < 16 )
							damage = 3;
						else if ( damage < 21 )
							damage = 4;
						else if ( damage < 26 )
							damage = 5;
						else
							damage = 6;
							 * */

						WeaponType type = weap.Type;

                        if (type == WeaponType.Ranged)
                        {
                            switch (damage)
                            {
                                case 0: from.SendAsciiMessage("This weapon might scratch your opponent slightly when you shot someone with it at long range.") ; break;
                                case 1: from.SendAsciiMessage("This weapon would do minimal damage when you shot someone with it at long range."); break;
                                case 2: from.SendAsciiMessage("This weapon would do some damage when you shot someone with it at long range."); break;
                                case 3: from.SendAsciiMessage("This weapon would probably hurt your opponent a fair amount when you shot someone with it at long range."); break;
                                case 4: from.SendAsciiMessage("This weapon would inflict quite a lot of damage and pain when you shot someone with it at long range."); break;
                                case 5: from.SendAsciiMessage("This weapon would be a superior weapon when you shot someone with it at long range."); break;
                                case 6: from.SendAsciiMessage("This weapon would be extraordinarily deadly when you shot someone with it at long range."); break;
                            }
                            //from.SendLocalizedMessage(1038224 + (damage * 9));
                        }
                        else if (type == WeaponType.Piercing)
                        {
                            switch (hand)
                            {
                                case 0:
                                    switch (damage)
                                    {
                                        case 0: from.SendAsciiMessage("This weapon might scratch your opponent slightly when you stabbed with it at short range."); break;
                                        case 1: from.SendAsciiMessage("This weapon would do minimal damage when you stabbed with it at short range."); break;
                                        case 2: from.SendAsciiMessage("This weapon would do some damage when you stabbed with it at short range."); break;
                                        case 3: from.SendAsciiMessage("This weapon would probably hurt your opponent a fair amount when you stabbed with it at short range."); break;
                                        case 4: from.SendAsciiMessage("This weapon would inflict quite a lot of damage and pain when you stabbed with it at short range."); break;
                                        case 5: from.SendAsciiMessage("This weapon would be a superior weapon when you stabbed with it at short range."); break;
                                        case 6: from.SendAsciiMessage("This weapon would be extraordinarily deadly when you stabbed with it at short range."); break;
                                    } break;
                                case 1:
                                    switch (damage)
                                    {
                                        case 0: from.SendAsciiMessage("This weapon might scratch your opponent slightly when you stabbed with it two-handed at short range."); break;
                                        case 1: from.SendAsciiMessage("This weapon would do minimal damage when you stabbed with it two-handed at short range."); break;
                                        case 2: from.SendAsciiMessage("This weapon would do some damage when you stabbed with it two-handed at short range."); break;
                                        case 3: from.SendAsciiMessage("This weapon would probably hurt your opponent a fair amount when you stabbed with it two-handed at short range."); break;
                                        case 4: from.SendAsciiMessage("This weapon would inflict quite a lot of damage and pain when you stabbed with it two-handed at short range."); break;
                                        case 5: from.SendAsciiMessage("This weapon would be a superior weapon when you stabbed with it two-handed at short range."); break;
                                        case 6: from.SendAsciiMessage("This weapon would be extraordinarily deadly when you stabbed with it two-handed at short range."); break;
                                    } break;                                
                            }
                            //from.SendLocalizedMessage(1038218 + hand + (damage * 9));
                        }
                        else if (type == WeaponType.Slashing)
                        {
                            switch (hand)
                            {
                                case 0:
                                    switch (damage)
                                    {
                                        case 0: from.SendAsciiMessage("This weapon might scratch your opponent slightly when you slashed with it at short range."); break;
                                        case 1: from.SendAsciiMessage("This weapon would do minimal damage when you slashed with it at short range."); break;
                                        case 2: from.SendAsciiMessage("This weapon would do some damage when you slashed with it at short range."); break;
                                        case 3: from.SendAsciiMessage("This weapon would probably hurt your opponent a fair amount when you slashed with it at short range."); break;
                                        case 4: from.SendAsciiMessage("This weapon would inflict quite a lot of damage and pain when you slashed with it at short range."); break;
                                        case 5: from.SendAsciiMessage("This weapon would be a superior weapon when you slashed with it at short range."); break;
                                        case 6: from.SendAsciiMessage("This weapon would be extraordinarily deadly when you slashed with it at short range."); break;
                                    } break;
                                case 1:
                                    switch (damage)
                                    {
                                        case 0: from.SendAsciiMessage("This weapon might scratch your opponent slightly when you slashed with it two-handed at short range."); break;
                                        case 1: from.SendAsciiMessage("This weapon would do minimal damage when you slashed with it two-handed at short range."); break;
                                        case 2: from.SendAsciiMessage("This weapon would do some damage when you slashed with it two-handed at short range."); break;
                                        case 3: from.SendAsciiMessage("This weapon would probably hurt your opponent a fair amount when you slashed with it two-handed at short range."); break;
                                        case 4: from.SendAsciiMessage("This weapon would inflict quite a lot of damage and pain when you slashed with it two-handed at short range."); break;
                                        case 5: from.SendAsciiMessage("This weapon would be a superior weapon when you slashed with it two-handed at short range."); break;
                                        case 6: from.SendAsciiMessage("This weapon would be extraordinarily deadly when you slashed with it two-handed at short range."); break;
                                    } break;
                            }
                            //from.SendLocalizedMessage(1038220 + hand + (damage * 9));
                        }
                        else if (type == WeaponType.Bashing)
                        {
                            switch (hand)
                            {
                                case 0:
                                    switch (damage)
                                    {
                                        case 0: from.SendAsciiMessage("This weapon might scratch your opponent slightly when you bashed with it at short range."); break;
                                        case 1: from.SendAsciiMessage("This weapon would do minimal damage when you bashed with it at short range."); break;
                                        case 2: from.SendAsciiMessage("This weapon would do some damage when you bashed with it at short range."); break;
                                        case 3: from.SendAsciiMessage("This weapon would probably hurt your opponent a fair amount when you bashed with it at short range."); break;
                                        case 4: from.SendAsciiMessage("This weapon would inflict quite a lot of damage and pain when you bashed with it at short range."); break;
                                        case 5: from.SendAsciiMessage("This weapon would be a superior weapon when you bashed with it at short range."); break;
                                        case 6: from.SendAsciiMessage("This weapon would be extraordinarily deadly when you bashed with it at short range."); break;
                                    } break;
                                case 1:
                                    switch (damage)
                                    {
                                        case 0: from.SendAsciiMessage("This weapon might scratch your opponent slightly when you bashed with it two-handed at short range."); break;
                                        case 1: from.SendAsciiMessage("This weapon would do minimal damage when you bashed with it two-handed at short range."); break;
                                        case 2: from.SendAsciiMessage("This weapon would do some damage when you bashed with it two-handed at short range."); break;
                                        case 3: from.SendAsciiMessage("This weapon would probably hurt your opponent a fair amount when you bashed with it two-handed at short range."); break;
                                        case 4: from.SendAsciiMessage("This weapon would inflict quite a lot of damage and pain when you bashed with it two-handed at short range."); break;
                                        case 5: from.SendAsciiMessage("This weapon would be a superior weapon when you bashed with it two-handed at short range."); break;
                                        case 6: from.SendAsciiMessage("This weapon would be extraordinarily deadly when you bashed with it two-handed at short range."); break;
                                    } break;
                            }
                            //from.SendLocalizedMessage(1038222 + hand + (damage * 9));
                        }
                        else
                        {
                            switch (hand)
                            {
                                case 0:
                                    switch (damage)
                                    {
                                        case 0: from.SendAsciiMessage("This weapon might scratch your opponent slightly when you hit with it at short range."); break;
                                        case 1: from.SendAsciiMessage("This weapon would do minimal damage when you hit with it at short range."); break;
                                        case 2: from.SendAsciiMessage("This weapon would do some damage when you hit with it at short range."); break;
                                        case 3: from.SendAsciiMessage("This weapon would probably hurt your opponent a fair amount when you hit with it at short range."); break;
                                        case 4: from.SendAsciiMessage("This weapon would inflict quite a lot of damage and pain when you hit with it at short range."); break;
                                        case 5: from.SendAsciiMessage("This weapon would be a superior weapon when you hit with it at short range."); break;
                                        case 6: from.SendAsciiMessage("This weapon would be extraordinarily deadly when you hit with it at short range."); break;
                                    } break;
                                case 1:
                                    switch (damage)
                                    {
                                        case 0: from.SendAsciiMessage("This weapon might scratch your opponent slightly when you hit with it two-handed at short range."); break;
                                        case 1: from.SendAsciiMessage("This weapon would do minimal damage when you hit with it two-handed at short range."); break;
                                        case 2: from.SendAsciiMessage("This weapon would do some damage when you hit with it two-handed at short range."); break;
                                        case 3: from.SendAsciiMessage("This weapon would probably hurt your opponent a fair amount when you hit with it two-handed at short range."); break;
                                        case 4: from.SendAsciiMessage("This weapon would inflict quite a lot of damage and pain when you hit with it two-handed at short range."); break;
                                        case 5: from.SendAsciiMessage("This weapon would be a superior weapon when you hit with it two-handed at short range."); break;
                                        case 6: from.SendAsciiMessage("This weapon would be extraordinarily deadly when you hit with it two-handed at short range."); break;
                                    } break;
                            }
                            //from.SendLocalizedMessage(1038216 + hand + (damage * 9));
                        }

						if ( weap.Poison != null && weap.PoisonCharges > 0 )
							from.SendAsciiMessage( "It appears to have poison smeared on it." ); // It appears to have poison smeared on it.
					}
					else
					{
						from.SendAsciiMessage( "You are not certain..." ); // You are not certain...
					}
				}
				else if(targeted is BaseArmor)
				{
					if( from.CheckTargetSkill(SkillName.ArmsLore, targeted, 0, 100) )
					{
						BaseArmor arm = (BaseArmor)targeted;

						if ( arm.MaxHitPoints != 0 )
						{
							int hp = (int)((arm.HitPoints / (double)arm.MaxHitPoints) * 10);

							if ( hp < 0 )
								hp = 0;
							else if ( hp > 9 )
								hp = 9;

							//from.SendLocalizedMessage( 1038285 + hp );
                            switch (hp)
                            {
                                case 0: from.SendAsciiMessage("It looks like it is about to fall apart."); break;
                                case 1: from.SendAsciiMessage("It looks rather flimsy and not at all trustworthy."); break;
                                case 2: from.SendAsciiMessage("It looks somewhat badly damaged."); break;
                                case 3: from.SendAsciiMessage("It looks rather battered."); break;
                                case 4: from.SendAsciiMessage("It looks like it has been well-used."); break;
                                case 5: from.SendAsciiMessage("It looks to have suffered some wear and tear."); break;
                                case 6: from.SendAsciiMessage("It looks to be in fairly good condition."); break;
                                case 7: from.SendAsciiMessage("It looks fairly new, with just a few nicks and scratches."); break;
                                case 8: from.SendAsciiMessage("It looks almost new."); break;
                                case 9: from.SendAsciiMessage("It looks unused."); break;
                            }
						}


						//from.SendLocalizedMessage( 1038295 + (int)Math.Ceiling( Math.Min( arm.ArmorRating, 35 ) / 5.0 ) );
                        switch ((int)Math.Ceiling(Math.Min(arm.ArmorRating, 35) / 5.0))
                        {
                            case 0: from.SendAsciiMessage("This armor offers no defense against attackers."); break;
                            case 1: from.SendAsciiMessage("This armor provides almost no protection."); break;
                            case 2: from.SendAsciiMessage("This armor provides very little protection."); break;
                            case 3: from.SendAsciiMessage("This armor offers some protection against blows."); break;
                            case 4: from.SendAsciiMessage("This armor serves as sturdy protection."); break;
                            case 5: from.SendAsciiMessage("This armor is a superior defense against attack."); break;
                            case 6: from.SendAsciiMessage("This armor offers excellent protection."); break;
                            case 7: from.SendAsciiMessage("This armor is superbly crafted to provide maximum protection."); break;
                        }
						/*
						if ( arm.ArmorRating < 1 )
							from.SendLocalizedMessage( 1038295 ); // This armor offers no defense against attackers.
						else if ( arm.ArmorRating < 6 )
							from.SendLocalizedMessage( 1038296 ); // This armor provides almost no protection.
						else if ( arm.ArmorRating < 11 )
							from.SendLocalizedMessage( 1038297 ); // This armor provides very little protection.
						else if ( arm.ArmorRating < 16 )
							from.SendLocalizedMessage( 1038298 ); // This armor offers some protection against blows.
						else if ( arm.ArmorRating < 21 )
							from.SendLocalizedMessage( 1038299 ); // This armor serves as sturdy protection.
						else if ( arm.ArmorRating < 26 )
							from.SendLocalizedMessage( 1038300 ); // This armor is a superior defense against attack.
						else if ( arm.ArmorRating < 31 )
							from.SendLocalizedMessage( 1038301 ); // This armor offers excellent protection.
						else
							from.SendLocalizedMessage( 1038302 ); // This armor is superbly crafted to provide maximum protection.
						 * */
					}
					else
					{
                        from.SendAsciiMessage("You are not certain...");
						//from.SendLocalizedMessage( 500353 ); // You are not certain...
					}
				}
				else if ( targeted is SwampDragon && ((SwampDragon)targeted).HasBarding )
				{
					SwampDragon pet = (SwampDragon)targeted;

					if ( from.CheckTargetSkill( SkillName.ArmsLore, targeted, 0, 100 ) )
					{
						int perc = (4 * pet.BardingHP) / pet.BardingMaxHP;

						if ( perc < 0 )
							perc = 0;
						else if ( perc > 4 )
							perc = 4;

						pet.PrivateOverheadMessage( MessageType.Regular, 0x3B2, 1053021 - perc, from.NetState );
					}
					else
					{
                        from.SendAsciiMessage("You are not certain...");
						//from.SendLocalizedMessage( 500353 ); // You are not certain...
					}
				}
				else
				{
                    from.SendAsciiMessage("This is neither weapon nor armor.");
					//from.SendLocalizedMessage( 500352 ); // This is neither weapon nor armor.
				}
			}
		}
	}
}