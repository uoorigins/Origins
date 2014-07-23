using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;

namespace Server.SkillHandlers
{
	public class Provocation
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.Provocation].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile m )
		{
			m.RevealingAction();

			BaseInstrument.PickInstrument( m, new InstrumentPickedCallback( OnPickedInstrument ) );

			return TimeSpan.FromSeconds( 1.0 ); // Cannot use another skill for 1 second
		}

		public static void OnPickedInstrument( Mobile from, BaseInstrument instrument )
		{
			from.RevealingAction();
			//from.SendLocalizedMessage( 501587 ); // Whom do you wish to incite?
            from.SendAsciiMessage("Whom do you wish to incite?");
			from.Target = new InternalFirstTarget( from, instrument );
		}

		private class InternalFirstTarget : Target
		{
			private BaseInstrument m_Instrument;

			public InternalFirstTarget( Mobile from, BaseInstrument instrument ) : base( BaseInstrument.GetBardRange( from, SkillName.Provocation ), false, TargetFlags.None )
			{
				m_Instrument = instrument;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				from.RevealingAction();

				if ( targeted is BaseCreature && from.CanBeHarmful( (Mobile)targeted, true ) )
				{
					BaseCreature creature = (BaseCreature)targeted;

					if ( !m_Instrument.IsChildOf( from.Backpack ) )
					{
                        from.SendAsciiMessage("The instrument you are trying to play is no longer in your backpack!");
						//from.SendLocalizedMessage( 1062488 ); // The instrument you are trying to play is no longer in your backpack!
					}
					else if ( creature.Controlled )
					{
                        from.SendAsciiMessage("They are too loyal to their master to be provoked.");
						//from.SendLocalizedMessage( 501590 ); // They are too loyal to their master to be provoked.
					}
					else if ( creature.IsParagon && BaseInstrument.GetBaseDifficulty( creature ) >= 160.0 )
					{
                        from.SendAsciiMessage("You have no chance of provoking those creatures.");
						//from.SendLocalizedMessage( 1049446 ); // You have no chance of provoking those creatures.
					}
					else
					{
						from.RevealingAction();
						m_Instrument.PlayInstrumentWell( from );
                        //creature.SayTo(from, true, "You play your music, inciting anger, and it begins to look furious.  Whom do you wish them to attack?");
                        from.SendAsciiMessage("You play your music, inciting anger, and it begins to look furious.  Whom do you wish them to attack?");
						//from.SendLocalizedMessage( 1008085 ); // You play your music and your target becomes angered.  Whom do you wish them to attack?
						from.Target = new InternalSecondTarget( from, m_Instrument, creature );
					}
				}
                else if (targeted is PlayerMobile)
                {
                    from.SendAsciiMessage("Verbal taunts might be more effective!");
                }
				else
				{
                    from.SendAsciiMessage("You can't incite that!");
					//from.SendLocalizedMessage( 501589 ); // You can't incite that!
				}
			}
		}

		private class InternalSecondTarget : Target
		{
			private BaseCreature m_Creature;
			private BaseInstrument m_Instrument;

			public InternalSecondTarget( Mobile from, BaseInstrument instrument, BaseCreature creature ) : base( BaseInstrument.GetBardRange( from, SkillName.Provocation ), false, TargetFlags.None )
			{
				m_Instrument = instrument;
				m_Creature = creature;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				from.RevealingAction();

				if ( targeted is BaseCreature )
				{
					BaseCreature creature = (BaseCreature)targeted;

					if ( !m_Instrument.IsChildOf( from.Backpack ) )
					{
                        from.SendAsciiMessage("The instrument you are trying to play is no longer in your backpack!"); // The instrument you are trying to play is no longer in your backpack!
					}
					else if ( m_Creature.Unprovokable || creature.Unprovokable )
					{
                        from.SendAsciiMessage("You have no chance of provoking those creatures."); // You have no chance of provoking those creatures.
					}
					else if ( m_Creature.Map != creature.Map || !m_Creature.InRange( creature, BaseInstrument.GetBardRange( from, SkillName.Provocation ) ) )
					{
                        from.SendAsciiMessage("The creatures you are trying to provoke are too far away from each other for your music to have an effect."); // The creatures you are trying to provoke are too far away from each other for your music to have an effect.
					}
					else if ( m_Creature != creature )
					{
						from.NextSkillTime = DateTime.Now + TimeSpan.FromSeconds( 6.0 );

						double diff = ((m_Instrument.GetDifficultyFor( m_Creature ) + m_Instrument.GetDifficultyFor( creature )) * 0.5) - 5.0;
						double music = from.Skills[SkillName.Musicianship].Value;

						if ( music > 100.0 )
							diff -= (music - 100.0) * 0.5;

						if ( from.CanBeHarmful( m_Creature, true ) && from.CanBeHarmful( creature, true ) )
						{
							if ( !BaseInstrument.CheckMusicianship( from ) )
							{
								from.NextSkillTime = DateTime.Now + TimeSpan.FromSeconds( 6.0 );
                                //from.SayTo(from, true, "You play poorly, and there is no effect.");
                                from.SendAsciiMessage("You play poorly, and there is no effect."); // You play poorly, and there is no effect.
								m_Instrument.PlayInstrumentBadly( from );
								m_Instrument.ConsumeUse( from );
							}
							else
							{
                                if ( !from.CheckTargetSkill(SkillName.Provocation, creature, 0, 100))
								{
									from.NextSkillTime = DateTime.Now + TimeSpan.FromSeconds( 6.0 );
                                    //creature.SayTo(from, true, "Your music fails to incite enough anger.");
                                    from.SendAsciiMessage("Your music fails to incite enough anger."); // Your music fails to incite enough anger.
									m_Instrument.PlayInstrumentBadly( from );
									m_Instrument.ConsumeUse( from );
								}
								else
								{
                                    //creature.SayTo(from, true, "Your music succeeds, as you start a fight.");
                                    from.SendAsciiMessage("Your music succeeds, as you start a fight."); // Your music succeeds, as you start a fight.
									m_Instrument.PlayInstrumentWell( from );
									m_Instrument.ConsumeUse( from );
									m_Creature.Provoke( from, creature, true );
								}
							}
						}
					}
					else
					{
                        from.SendAsciiMessage("You can't tell someone to attack themselves!"); // You can't tell someone to attack themselves!
					}
				}
                else if ( targeted is Mobile )
                {
                    Mobile m = (Mobile)targeted;

                    if ( !m_Instrument.IsChildOf( from.Backpack ) )
                    {
                        from.SendAsciiMessage( "The instrument you are trying to play is no longer in your backpack!" ); // The instrument you are trying to play is no longer in your backpack!
                    }
                    else if ( m_Creature.Unprovokable )
                    {
                        from.SendAsciiMessage( "You have no chance of provoking those creatures." ); // You have no chance of provoking those creatures.
                    }
                    else if ( m_Creature.Map != m.Map || !m_Creature.InRange( m, BaseInstrument.GetBardRange( from, SkillName.Provocation ) ) )
                    {
                        from.SendAsciiMessage( "The creatures you are trying to provoke are too far away from each other for your music to have an effect." ); // The creatures you are trying to provoke are too far away from each other for your music to have an effect.
                    }
                    else
                    {
                        from.NextSkillTime = DateTime.Now + TimeSpan.FromSeconds( 6.0 );

                        double diff = ( ( m_Instrument.GetDifficultyFor( m_Creature ) + m_Instrument.GetDifficultyFor( m ) ) * 0.5 ) - 5.0;
                        double music = from.Skills[SkillName.Musicianship].Value;

                        if ( music > 100.0 )
                            diff -= ( music - 100.0 ) * 0.5;

                        if ( from.CanBeHarmful( m_Creature, true ) && from.CanBeHarmful( m, true ) )
                        {
                            if ( !BaseInstrument.CheckMusicianship( from ) )
                            {
                                from.NextSkillTime = DateTime.Now + TimeSpan.FromSeconds( 6.0 );
                                //from.SayTo(from, true, "You play poorly, and there is no effect.");
                                from.SendAsciiMessage( "You play poorly, and there is no effect." ); // You play poorly, and there is no effect.
                                m_Instrument.PlayInstrumentBadly( from );
                                m_Instrument.ConsumeUse( from );
                            }
                            else
                            {
                                if ( !from.CheckTargetSkill( SkillName.Provocation, m, 0, 100 ) )
                                {
                                    from.NextSkillTime = DateTime.Now + TimeSpan.FromSeconds( 6.0 );
                                    //creature.SayTo(from, true, "Your music fails to incite enough anger.");
                                    from.SendAsciiMessage( "Your music fails to incite enough anger." ); // Your music fails to incite enough anger.
                                    m_Instrument.PlayInstrumentBadly( from );
                                    m_Instrument.ConsumeUse( from );
                                }
                                else
                                {
                                    //creature.SayTo(from, true, "Your music succeeds, as you start a fight.");
                                    from.SendAsciiMessage( "Your music succeeds, as you start a fight." ); // Your music succeeds, as you start a fight.
                                    m_Instrument.PlayInstrumentWell( from );
                                    m_Instrument.ConsumeUse( from );
                                    m_Creature.Provoke( from, m, true );
                                }
                            }
                        }
                    }
                }
                else
                {
                    from.SendAsciiMessage( "You can't incite that!" ); // You can't incite that!
                }
			}
		}
	}
}