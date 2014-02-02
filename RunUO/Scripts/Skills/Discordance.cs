using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.SkillHandlers
{
	public class Discordance
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.Discordance].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile m )
		{
			m.RevealingAction();

			BaseInstrument.PickInstrument( m, new InstrumentPickedCallback( OnPickedInstrument ) );

			return TimeSpan.FromSeconds( 10.0 ); // Cannot use another skill for 1 second
		}

		public static void OnPickedInstrument( Mobile from, BaseInstrument instrument )
		{
			from.RevealingAction();
			from.SendAsciiMessage( "Whom do you wish to entice?"); // Choose the target for your song of discordance.
			from.Target = new DiscordanceTarget( from, instrument );
		}

        private class DiscordanceTarget : Target
        {
            private BaseInstrument m_Instrument;
            private bool m_SetSkillTime = true;

            public DiscordanceTarget(Mobile from, BaseInstrument instrument) : base(BaseInstrument.GetBardRange(from, SkillName.Discordance), false, TargetFlags.None)
            {
                m_Instrument = instrument;
                CheckLOS = false;
            }

            protected override void OnTargetFinish(Mobile from)
            {
                if (m_SetSkillTime)
                    from.NextSkillTime = DateTime.Now;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                from.RevealingAction();

                if (targeted == from)
                    from.SayTo(from, true, "You cannot entice yourself!");
                //from.SendAsciiMessage("You cannot entice yourself!");
                else if (!m_Instrument.IsChildOf(from.Backpack))
                    from.SendAsciiMessage("The instrument you are trying to play is no longer in your backpack!"); // The instrument you are trying to play is no longer in your backpack!
                else if (targeted is Mobile)
                {
                    Mobile targ = targeted as Mobile;
                    m_SetSkillTime = false;
                    from.NextSkillTime = DateTime.Now + TimeSpan.FromSeconds(10.0);

                    if (!BaseInstrument.CheckMusicianship(from))
                    {
                        targ.SayTo(targ, true, "You hear lovely music, and for a moment are drawn towards it.");
                        targ.SayTo(from, true, "Your music fails to attract them.");
                        //targ.LocalOverheadMessage(MessageType.Regular, targ.SpeechHue, true, String.Format("You hear lovely music, and for a moment are drawn towards it."));
                        //targ.NonlocalOverheadMessage(MessageType.Regular, targ.SpeechHue, true, String.Format("You hear lovely music, and for a moment are drawn towards it."));
                        m_Instrument.PlayInstrumentBadly(from);
                        m_Instrument.ConsumeUse(from);
                    }
                    else if (!from.CheckSkill(SkillName.Discordance, 0.0, 100.0))
                    {
                        targ.SayTo(targ, true, "You hear lovely music, and for a moment are drawn towards it.");
                        targ.SayTo(from, true, "Your music fails to attract them.");
                        m_Instrument.PlayInstrumentBadly(from);
                        m_Instrument.ConsumeUse(from);
                    }
                    else
                    {
                        m_Instrument.PlayInstrumentWell(from);
                        m_Instrument.ConsumeUse(from);
                        targ.SayTo(targ, true, "You hear lovely music, and are drawn towards it...");
                        from.SayTo(from, true, "You play your hypnotic music, luring them near.");

                        if (targ is PlayerMobile)
                        {
                            targ.SayTo(from, true, "What am I hearing?");
                            targ.SayTo(from, true, "You might have better luck with sweet words.");
                        }
                        else if (targ is BaseVendor)
                        {
                            targ.SayTo(from, true, "What am I hearing?");
                            targ.SayTo(from, true, "Oh, but I cannot wander too far from my shop!");
                        }
                        else if (targ is BaseCreature)
                        {
                            BaseCreature bc = (BaseCreature)targ;

                            if ( bc.EnticeTimer != null )
                            {
                                bc.EnticeTimer.Stop();
                                bc.EnticeTimer = null;
                            }

                            bc.EnticeTimer = new EnticementTimer( from, bc );
                            bc.EnticeTimer.Start();
                        }
                    }
                }
                else
                    from.SendAsciiMessage("You cannot entice that!");
            }
        }
    }

    public class EnticementTimer : Timer
    {
        private Mobile m_Player;
        private BaseCreature m_Target;
        private int Count;

        public EnticementTimer( Mobile from, BaseCreature target )
            : base( TimeSpan.Zero, TimeSpan.FromSeconds( 1.0 ) )
        {
            m_Player = from;
            m_Target = target;
            Priority = TimerPriority.OneSecond;
            Count = 0;
        }

        protected override void OnTick()
        {
            Count++;
            if ( Count > 10 )
            {
                Stop();
                return;
            }

            if ( m_Target == null || !m_Target.Alive )
            {
                Stop();
                return;
            }

            if ( m_Target != null && m_Target.InRange( m_Player.Location, 1 ) )
            {
                Stop();
                return;
            }
            else if ( m_Target != null && m_Target.GetDistanceToSqrt( m_Player.Location ) > 20 )
            {
                Stop();
                return;
            }
            else
            {
                if ( m_Target != null )
                {
                    m_Target.TargetLocation = new Point2D( (IPoint2D)m_Player.Location );
                    m_Target.BardEndTime = DateTime.Now;
                    m_Target.BardTarget = null;
                    m_Target.BardMaster = null;
                    m_Target.BardProvoked = false;
                    m_Target.Combatant = null;
                    m_Target.Warmode = false;
                    m_Target.NextCombatTime = DateTime.Now + TimeSpan.FromSeconds( 2.0 );
                    m_Target.AIObject.Action = ActionType.Wander;
                }
            }
        }
    }
}

/*using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.SkillHandlers
{
	public class Discordance
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.Discordance].Callback = new SkillUseCallback( OnUse );
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
			from.SendLocalizedMessage( 1049541 ); // Choose the target for your song of discordance.
			from.Target = new DiscordanceTarget( from, instrument );
		}

		private class DiscordanceInfo
		{
			public Mobile m_From;
			public Mobile m_Creature;
			public DateTime m_EndTime;
			public Timer m_Timer;
			public int m_Effect;
			public ArrayList m_Mods;

			public DiscordanceInfo( Mobile from, Mobile creature, TimeSpan duration, int effect, ArrayList mods )
			{
				m_From = from;
				m_Creature = creature;
				m_EndTime = DateTime.Now + duration;
				m_Effect = effect;
				m_Mods = mods;

				Apply();
			}

			public void Apply()
			{
				for ( int i = 0; i < m_Mods.Count; ++i )
				{
					object mod = m_Mods[i];

					if ( mod is ResistanceMod )
						m_Creature.AddResistanceMod( (ResistanceMod) mod );
					else if ( mod is StatMod )
						m_Creature.AddStatMod( (StatMod) mod );
					else if ( mod is SkillMod )
						m_Creature.AddSkillMod( (SkillMod) mod );
				}
			}

			public void Clear()
			{
				for ( int i = 0; i < m_Mods.Count; ++i )
				{
					object mod = m_Mods[i];

					if ( mod is ResistanceMod )
						m_Creature.RemoveResistanceMod( (ResistanceMod) mod );
					else if ( mod is StatMod )
						m_Creature.RemoveStatMod( ((StatMod) mod).Name );
					else if ( mod is SkillMod )
						m_Creature.RemoveSkillMod( (SkillMod) mod );
				}
			}
		}

		private static Hashtable m_Table = new Hashtable();

		public static bool GetEffect( Mobile targ, ref int effect )
		{
			DiscordanceInfo info = m_Table[targ] as DiscordanceInfo;

			if ( info == null )
				return false;

			effect = info.m_Effect;
			return true;
		}

		private static void ProcessDiscordance( object state )
		{
			DiscordanceInfo info = (DiscordanceInfo)state;
			Mobile from = info.m_From;
			Mobile targ = info.m_Creature;

			if ( DateTime.Now >= info.m_EndTime || targ.Deleted || from.Map != targ.Map || targ.GetDistanceToSqrt( from ) > 16 )
			{
				if ( info.m_Timer != null )
					info.m_Timer.Stop();

				info.Clear();
				m_Table.Remove( targ );
			}
			else
			{
				targ.FixedEffect( 0x376A, 1, 32 );
			}
		}

		public class DiscordanceTarget : Target
		{
			private BaseInstrument m_Instrument;

			public DiscordanceTarget( Mobile from, BaseInstrument inst ) : base( BaseInstrument.GetBardRange( from, SkillName.Discordance ), false, TargetFlags.Harmful )
			{
				m_Instrument = inst;
			}

			protected override void OnTarget( Mobile from, object target )
			{
				from.RevealingAction();

				if ( !m_Instrument.IsChildOf( from.Backpack ) )
				{
					from.SendLocalizedMessage( 1062488 ); // The instrument you are trying to play is no longer in your backpack!
				}
				else if ( target is Mobile )
				{
					Mobile targ = (Mobile)target;

					if ( targ == from || (targ is BaseCreature && ( ((BaseCreature)targ).BardImmune || !from.CanBeHarmful( targ, false ) )) )
					{
						from.SendLocalizedMessage( 1049535 ); // A song of discord would have no effect on that.
					}
					else if ( !targ.Player )
					{
						TimeSpan len = TimeSpan.FromSeconds( from.Skills[SkillName.Discordance].Value * 2 );
						double diff = m_Instrument.GetDifficultyFor( targ ) - 10.0;
						double music = from.Skills[SkillName.Musicianship].Value;

						if ( music > 100.0 )
							diff -= (music - 100.0) * 0.5;

						if ( !BaseInstrument.CheckMusicianship( from ) )
						{
							from.SendLocalizedMessage( 500612 ); // You play poorly, and there is no effect.
							m_Instrument.PlayInstrumentBadly( from );
							m_Instrument.ConsumeUse( from );
						}
						else if ( from.CheckTargetSkill( SkillName.Discordance, target, diff-25.0, diff+25.0 ) )
						{
							if ( !m_Table.Contains( targ ) )
							{
								from.SendLocalizedMessage( 1049539 ); // You play the song surpressing your targets strength
								m_Instrument.PlayInstrumentWell( from );
								m_Instrument.ConsumeUse( from );

								ArrayList mods = new ArrayList();
								int effect;
								double scalar;

								if ( Core.AOS )
								{
									double discord = from.Skills[SkillName.Discordance].Value;

									if ( discord > 100.0 )
										effect = -20 + (int)((discord - 100.0) / -2.5);
									else
										effect = (int)(discord / -5.0);

									if ( Core.SE && BaseInstrument.GetBaseDifficulty( targ ) >= 160.0 )
										effect /= 2;

									scalar = effect * 0.01;

									mods.Add( new ResistanceMod( ResistanceType.Physical, effect ) );
									mods.Add( new ResistanceMod( ResistanceType.Fire, effect ) );
									mods.Add( new ResistanceMod( ResistanceType.Cold, effect ) );
									mods.Add( new ResistanceMod( ResistanceType.Poison, effect ) );
									mods.Add( new ResistanceMod( ResistanceType.Energy, effect ) );

									for ( int i = 0; i < targ.Skills.Length; ++i )
									{
										if ( targ.Skills[i].Value > 0 )
											mods.Add( new DefaultSkillMod( (SkillName)i, true, targ.Skills[i].Value * scalar ) );
									}
								}
								else
								{
									effect = (int)( from.Skills[SkillName.Discordance].Value / -5.0 );
									scalar = effect * 0.01;

									mods.Add( new StatMod( StatType.Str, "DiscordanceStr", (int)(targ.RawStr * scalar), TimeSpan.Zero ) );
									mods.Add( new StatMod( StatType.Int, "DiscordanceInt", (int)(targ.RawInt * scalar), TimeSpan.Zero ) );
									mods.Add( new StatMod( StatType.Dex, "DiscordanceDex", (int)(targ.RawDex * scalar), TimeSpan.Zero ) );

									for ( int i = 0; i < targ.Skills.Length; ++i )
									{
										if ( targ.Skills[i].Value > 0 )
											mods.Add( new DefaultSkillMod( (SkillName)i, true, targ.Skills[i].Value * scalar ) );
									}
								}

								DiscordanceInfo info = new DiscordanceInfo( from, targ, len, Math.Abs( effect ), mods );
								info.m_Timer = Timer.DelayCall( TimeSpan.Zero, TimeSpan.FromSeconds( 1.25 ), new TimerStateCallback( ProcessDiscordance ), info );

								m_Table[targ] = info;
							}
							else
							{
								from.SendLocalizedMessage( 1049537 );// Your target is already in discord.
							}
						}
						else
						{
							from.SendLocalizedMessage( 1049540 );// You fail to disrupt your target
							m_Instrument.PlayInstrumentBadly( from );
							m_Instrument.ConsumeUse( from );
						}
					}
					else
					{
						m_Instrument.PlayInstrumentBadly( from );
					}
				}
				else
				{
					from.SendLocalizedMessage( 1049535 ); // A song of discord would have no effect on that.
				}
			}
		}
	}
}*/