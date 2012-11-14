using System;
using System.Text;
using Server;
using Server.Mobiles;
using Server.Engines.CannedEvil;

namespace Server.Misc
{
	public class Titles
	{
		public const int MinFame = 0;
		public const int MaxFame = 0;

		public static void AwardFame( Mobile m, int offset, bool message )
		{
            return;

			if ( offset > 0 )
			{
				if ( m.Fame >= MaxFame )
					return;

				offset -= m.Fame / 100;

				if ( offset < 0 )
					offset = 0;
			}
			else if ( offset < 0 )
			{
				if ( m.Fame <= MinFame )
					return;

				offset -= m.Fame / 100;

				if ( offset > 0 )
					offset = 0;
			}

			if ( (m.Fame + offset) > MaxFame )
				offset = MaxFame - m.Fame;
			else if ( (m.Fame + offset) < MinFame )
				offset = MinFame - m.Fame;

			m.Fame += offset;

			/*if ( message )
			{
				if ( offset > 40 )
					m.SendLocalizedMessage( 1019054 ); // You have gained a lot of fame.
				else if ( offset > 20 )
					m.SendLocalizedMessage( 1019053 ); // You have gained a good amount of fame.
				else if ( offset > 10 )
					m.SendLocalizedMessage( 1019052 ); // You have gained some fame.
				else if ( offset > 0 )
					m.SendLocalizedMessage( 1019051 ); // You have gained a little fame.
				else if ( offset < -40 )
					m.SendLocalizedMessage( 1019058 ); // You have lost a lot of fame.
				else if ( offset < -20 )
					m.SendLocalizedMessage( 1019057 ); // You have lost a good amount of fame.
				else if ( offset < -10 )
					m.SendLocalizedMessage( 1019056 ); // You have lost some fame.
				else if ( offset < 0 )
					m.SendLocalizedMessage( 1019055 ); // You have lost a little fame.
			}*/
		}

        public const int MinKarma = -127;
        public const int MaxKarma = 127;

        private static TimeSpan m_KarmaGainDelay = TimeSpan.FromMinutes(15.0);

        public static void AwardKarma(Mobile m, int offset, bool message)
        {
            //Debug
            //m.SendAsciiMessage(String.Format("{0}",((PlayerMobile)m).LastKarmaGain));
            //m.SendAsciiMessage(String.Format("Before: {0}", ((PlayerMobile)m).Karma));

            //Check to see if player gets Lord/Lady title
            if ((m.Karma >= 80) || (m.Karma <= -80))
                m.Fame = 10000;
            else
                m.Fame = 0;

            //Award Karma
            if (((((PlayerMobile)m).LastKarmaGain + m_KarmaGainDelay) >= DateTime.Now) && (offset > 0))
            {
                return;
            }
            else
            {

                ((PlayerMobile)m).LastKarmaGain = DateTime.Now;

                if (offset > 0)
                {
                    offset = 1;
                    if (m.Karma >= MaxKarma)
                    {
                        m.Karma = 127;
                        return;
                    }
                }
                else if (offset < 0)
                {
                    //offset = -1;
                    if (m.Karma <= MinKarma)
                    {
                        m.Karma = -127;
                        return;
                    }
                }

                m.Karma = m.Karma + offset;

                if (m.Karma >= MaxKarma)
                    m.Karma = 127;
                else if (m.Karma <= MinKarma)
                    m.Karma = -127;

                //Check to see if player gets Lord/Lady title
                if ((m.Karma >= 80) || (m.Karma <= -80))
                    m.Fame = 10000;
                else
                    m.Fame = 0;
            }

            //m.SendAsciiMessage(String.Format("After: {0}", ((PlayerMobile)m).Karma));

        }

		public static string[] HarrowerTitles = new string[] { "Spite", "Opponent", "Hunter", "Venom", "Executioner", "Annihilator", "Champion" };

		public static string ComputeTitle( Mobile beholder, Mobile beheld )
		{
			StringBuilder title = new StringBuilder();

			int fame = beheld.Fame;
			int karma = beheld.Karma;

			bool showSkillTitle = beheld.ShowFameTitle && ( (beholder == beheld) || (fame >= 5000) );

			/*if ( beheld.Kills >= 5 )
			{
				title.AppendFormat( beheld.Fame >= 10000 ? "The Murderer {1} {0}" : "The Murderer {0}", beheld.Name, beheld.Female ? "Lady" : "Lord" );
			}
			else*/if ( beheld.ShowFameTitle || (beholder == beheld) )
			{
				for ( int i = 0; i < m_FameEntries.Length; ++i )
				{
					FameEntry fe = m_FameEntries[i];

					if ( fame <= fe.m_Fame || i == (m_FameEntries.Length - 1) )
					{
						KarmaEntry[] karmaEntries = fe.m_Karma;

						for ( int j = 0; j < karmaEntries.Length; ++j )
						{
							KarmaEntry ke = karmaEntries[j];

							if ( karma <= ke.m_Karma || j == (karmaEntries.Length - 1) )
							{
								title.AppendFormat( ke.m_Title, beheld.Name, beheld.Female ? "Lady" : "Lord" );
								break;
							}
						}

						break;
					}
				}
			}
			else
			{
				title.Append( beheld.Name );
			}

			if( beheld is PlayerMobile && ((PlayerMobile)beheld).DisplayChampionTitle )
			{
				PlayerMobile.ChampionTitleInfo info = ((PlayerMobile)beheld).ChampionTitles;

				if( info.Harrower > 0 )
					title.AppendFormat( ": {0} of Evil", HarrowerTitles[Math.Min( HarrowerTitles.Length, info.Harrower )-1] );
				else
				{
					int highestValue = 0, highestType = 0;
					for( int i = 0; i < ChampionSpawnInfo.Table.Length; i++ )
					{
						int v = info.GetValue( i );

						if( v > highestValue )
						{
							highestValue = v;
							highestType = i;
						}
					}

					int offset = 0;
					if( highestValue > 800 )
						offset = 3;
					else if( highestValue > 300 )
						offset = (int)(highestValue/300);

					if( offset > 0 )
					{
						ChampionSpawnInfo champInfo = ChampionSpawnInfo.GetInfo( (ChampionSpawnType)highestType );
						title.AppendFormat( ": {0} of the {1}", champInfo.LevelNames[Math.Min( offset, champInfo.LevelNames.Length ) -1], champInfo.Name );
					}
				}
			}

			string customTitle = beheld.Title;

			if ( customTitle != null && (customTitle = customTitle.Trim()).Length > 0 )
			{
				title.AppendFormat( " {0}", customTitle );
			}
			else if ( showSkillTitle && beheld.Player )
			{
				string skillTitle = GetSkillTitle( beheld );

				if ( skillTitle != null ) {
					title.Append( ", " ).Append( skillTitle );
				}
			}

			return title.ToString();
		}

		public static string GetSkillTitle( Mobile mob ) {
			Skill highest = GetHighestSkill( mob );// beheld.Skills.Highest;

			if ( highest != null && highest.BaseFixedPoint >= 300 )
			{
				string skillLevel = GetSkillLevel( highest );
				string skillTitle = highest.Info.Title;

				if ( mob.Female && skillTitle.EndsWith( "man" ) )
					skillTitle = skillTitle.Substring( 0, skillTitle.Length - 3 ) + "woman";

				return String.Concat( skillLevel, " ", skillTitle );
			}

			return null;
		}

		private static Skill GetHighestSkill( Mobile m )
		{
			Skills skills = m.Skills;

			if ( !Core.AOS )
				return skills.Highest;

			Skill highest = null;

			for ( int i = 0; i < m.Skills.Length; ++i )
			{
				Skill check = m.Skills[i];

				if ( highest == null || check.BaseFixedPoint > highest.BaseFixedPoint )
					highest = check;
				else if ( highest != null && highest.Lock != SkillLock.Up && check.Lock == SkillLock.Up && check.BaseFixedPoint == highest.BaseFixedPoint )
					highest = check;
			}

			return highest;
		}

		private static string[,] m_Levels = new string[,]
			{
				{ "Neophyte",		"Neophyte",		"Neophyte"		},
				{ "Novice",			"Novice",		"Novice"		},
				{ "Apprentice",		"Apprentice",	"Apprentice"	},
				{ "Journeyman",		"Journeyman",	"Journeyman"	},
				{ "Expert",			"Expert",		"Expert"		},
				{ "Adept",			"Adept",		"Adept"			},
				{ "Master",			"Master",		"Master"		},
				{ "Grandmaster",	"Grandmaster",	"Grandmaster"	},
				{ "Elder",			"Tatsujin",		"Shinobi"		},
				{ "Legendary",		"Kengo",		"Ka-ge"			}
			};

		private static string GetSkillLevel( Skill skill )
		{
			return m_Levels[GetTableIndex( skill ), GetTableType( skill )];
		}

		private static int GetTableType( Skill skill )
		{
			switch ( skill.SkillName )
			{
				default: return 0;
				case SkillName.Bushido: return 1;
				case SkillName.Ninjitsu: return 2;
			}
		}

		private static int GetTableIndex( Skill skill )
		{
			int fp = Math.Min( skill.BaseFixedPoint, 1200 );

			return (fp - 300) / 100;
		}

        private static FameEntry[] m_FameEntries = new FameEntry[]
			{
				new FameEntry( 10000, new KarmaEntry[]
				{
					new KarmaEntry( -127, "The Dread {1} {0}" ),
					new KarmaEntry( -110, "The Evil {1} {0}" ),
					new KarmaEntry( -80, "The Dark {1} {0}" ),
					new KarmaEntry( -60, "The Dastardly {0}" ),
					new KarmaEntry( -40, "The Dishonorable {0}" ),
					new KarmaEntry( -39, "{0}" ),
					new KarmaEntry( 39, "{0}" ),
					new KarmaEntry( 59, "The Honorable {0}" ),
					new KarmaEntry( 79, "The Noble {0}" ),
					new KarmaEntry( 109, "The {1} {0}" ),
					new KarmaEntry( 126, "The Noble {1} {0}" ),
					new KarmaEntry( 127, "The Great {1} {0}" )

                    /*new KarmaEntry( -127, "The Great {1} {0}" ),
                    new KarmaEntry( -110, "The Noble {1} {0}" ),
                    new KarmaEntry( -80, "The {1} {0}" ),
                    new KarmaEntry( -60, "The Noble {0}" ),
                    new KarmaEntry( -40, "The Honorable {0}" ),
                    new KarmaEntry( -39, "{0}" ),
                    new KarmaEntry( 39, "{0}" ),
                    new KarmaEntry( 40, "The Dishonorable {0}" ),
                    new KarmaEntry( 60, "The Infamous {0}" ),
                    new KarmaEntry( 80, "The Dark {1} {0}" ),
                    new KarmaEntry( 110, "The Evil {1} {0}" ),
                    new KarmaEntry( 127, "The Dread {1} {0}" )*/
				} )
			};
	}

	public class FameEntry
	{
		public int m_Fame;
		public KarmaEntry[] m_Karma;

		public FameEntry( int fame, KarmaEntry[] karma )
		{
			m_Fame = fame;
			m_Karma = karma;
		}
	}

	public class KarmaEntry
	{
		public int m_Karma;
		public string m_Title;

		public KarmaEntry( int karma, string title )
		{
			m_Karma = karma;
			m_Title = title;
		}
	}
}