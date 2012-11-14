using System;
using System.Collections;
using System.Text;
using Server;
using Server.Mobiles;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.SkillHandlers
{
	public class ForensicEvaluation
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.Forensics].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile m )
		{
			m.Target = new ForensicTarget();
			m.RevealingAction();

            m.SendAsciiMessage("What would you like to evaluate?");
			//m.SendLocalizedMessage( 500906 ); // What would you like to evaluate?

			return TimeSpan.FromSeconds( 10.0 );
		}

		public class ForensicTarget : Target
		{
			public ForensicTarget() : base( 10, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object target )
			{
				if ( target is Mobile )
				{
					if ( from.CheckTargetSkill( SkillName.Forensics, target, 40.0, 100.0 ) )
					{
                        if (target is PlayerMobile && ((PlayerMobile)target).NpcGuild == NpcGuild.ThievesGuild)
                            from.SendAsciiMessage("That individual is a thief!");
                        //from.SendLocalizedMessage( 501004 );//That individual is a thief!
                        else
                            from.SendAsciiMessage("You notice nothing unusual.");
                            //from.SendLocalizedMessage(501003);//You notice nothing unusual.
					}
					else
					{
                        from.SendAsciiMessage("You cannot determain anything useful.");
						//from.SendLocalizedMessage( 501001 );//You cannot determain anything useful.
					}
				}
				else if ( target is Corpse )
				{
					if ( from.CheckTargetSkill( SkillName.Forensics, target, 0.0, 100.0 ) )
					{
						Corpse c = (Corpse)target;

						if ( ((Body)c.Amount).IsHuman )
                            from.Send(new AsciiMessage(c.Serial, c.ItemID, MessageType.Regular, 0, 3, "", String.Format("This person was killed by {0}", (c.Killer == null ? "no one." : c.Killer.Name + "."))));
							//c.LabelTo( from, 1042751, ( c.Killer == null ? "no one" : c.Killer.Name ) );//This person was killed by ~1_KILLER_NAME~

						if ( c.Looters.Count > 0 )
						{
							StringBuilder sb = new StringBuilder();
							for (int i=0;i<c.Looters.Count;i++)
							{
								if ( i>0 )
									sb.Append( ", " );
								sb.Append( ((Mobile)c.Looters[i]).Name );
							}

                            from.Send(new AsciiMessage(c.Serial, c.ItemID, MessageType.Regular, 0, 3, "", String.Format("This body has been distrubed by {0}", sb.ToString()+".")));
							//c.LabelTo( from, 1042752, sb.ToString() );//This body has been distrubed by ~1_PLAYER_NAMES~
						}
						else
						{
                            from.Send(new AsciiMessage(c.Serial, c.ItemID, MessageType.Regular, 0, 3, "", "The corpse has not be desecrated."));
							//c.LabelTo( from, 501002 );//The corpse has not be desecrated.
						}
					}
					else
					{
                        from.SendAsciiMessage("You cannot determain anything useful.");
						//from.SendLocalizedMessage( 501001 );//You cannot determain anything useful.
					}
				}
				else if ( target is ILockpickable )
				{
					ILockpickable p = (ILockpickable)target;
                    if (p.Picker != null)
                    {
                        from.SendAsciiMessage(String.Format("This lock was opened by {0}", p.Picker.Name));
                        //from.SendLocalizedMessage(1042749, p.Picker.Name);//This lock was opened by ~1_PICKER_NAME~
                    }
                    else
                        from.SendAsciiMessage("You notice nothing unusual.");
                        //from.SendLocalizedMessage(501003);//You notice nothing unusual.
				}
			}
		}
	}
}
